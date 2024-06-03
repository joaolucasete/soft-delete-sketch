using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SoftDeleteSketch.Entities;
using System.Collections;
using System.Reflection;

// based on: https://github.com/JonPSmith/EfCore.SoftDeleteServices/blob/master/SoftDeleteServices/Concrete/Internal/CascadeWalker.cs
namespace SoftDeleteSketch.Services {
    public class CascadeWalker {

        private readonly HashSet<object> stopCircularLook = [];

        private readonly DbContext context;

        public int NumFound { get; private set; }


        public CascadeWalker(
            DbContext context
        ) {
            this.context = context;
        }

        public async Task WalkEntitiesSoftDelete(object principalInstance) {
            if (!(principalInstance is ISoftDelete castToCascadeSoftDelete && principalInstance.GetType().IsClass)
                || stopCircularLook.Contains(principalInstance))
                return; //isn't something we need to consider, or we saw it before, so it returns 

            stopCircularLook.Add(principalInstance); //we keep a reference to this to stop the method going in a circular loop

            if (applyChangeIfAppropriate(castToCascadeSoftDelete)) {

                //If the entity shouldn't be changed then we leave this entity and any of it children
                // logger.LogInformation("Entity {Entity} is already marked as deleted", principalInstance.GetType().Name);
                return;
            }

            var principalNavs = context.Entry(principalInstance)
                .Metadata.GetNavigations()
                .Where(x => !x.IsOnDependent
                    && //navigational link goes to dependent entity(s)
                    //The developer has whatDoing a Cascade delete behaviour (two options) on this link
                    x.ForeignKey.DeleteBehavior is DeleteBehavior.ClientCascade or DeleteBehavior.Cascade)
                .ToList();

            foreach (var navigation in principalNavs) {
                if (navigation.PropertyInfo == null)
                    //This could be changed by enhancing the navigation.PropertyInfo.GetValue(principalInstance);
                    throw new NotImplementedException("Currently only works with navigation links that are properties");

                if (navigation.TargetEntityType.IsOwned())
                    //We ignore owned types
                    continue;

                //It loads the current navigational value so that we can limit the number of database selects if the data is already loaded
                var navValue = navigation.PropertyInfo.GetValue(principalInstance);
                if (navigation.IsCollection) {

                    navValue ??= await LoadNavigationCollection(principalInstance, navigation);

                    if (navValue != null) {
                        foreach (var entity in navValue as IEnumerable) {
                            await WalkEntitiesSoftDelete(entity);
                        }
                    }

                } else {
                    navValue ??= await LoadNavigationSingleton(principalInstance, navigation);
                    if (navValue != null) {
                        await WalkEntitiesSoftDelete(navValue);
                    }
                }
            }
        }

        private bool applyChangeIfAppropriate(ISoftDelete castToCascadeSoftDelete) {
            if (castToCascadeSoftDelete.IsDeleted) {
                return true;
            }

            castToCascadeSoftDelete.Delete();
            NumFound++;
            return false;
        }

        private async Task<IEnumerable> LoadNavigationCollection(object principalInstance, INavigation navigation) {

            var navValueType = navigation.PropertyInfo.PropertyType;
            var innerType = navValueType.GetGenericArguments().Single();
            var genericHelperType = typeof(GenericCollectionLoader<>).MakeGenericType(innerType);
            dynamic loader = Activator.CreateInstance(genericHelperType, context, principalInstance, navigation.PropertyInfo);
            return await loader.GetFilteredEntities();
        }

        private class GenericCollectionLoader<TEntity> where TEntity : class, ISoftDelete {
            private readonly IQueryable<TEntity> _queryOfFilteredEntities;

            public async ValueTask<IEnumerable> GetFilteredEntities() {
                return await _queryOfFilteredEntities.ToListAsync();
            }

            public GenericCollectionLoader(DbContext context, object principalInstance, PropertyInfo propertyInfo) {
                var query = context.Entry(principalInstance).Collection(propertyInfo.Name).Query();
                _queryOfFilteredEntities = query.Provider.CreateQuery<TEntity>(query.Expression);
            }
        }

        private async Task<object> LoadNavigationSingleton(object principalInstance, INavigation navigation) {

            //for everything else we need to load the singleton with a IgnoreQueryFilters method
            var navValueType = navigation.PropertyInfo.PropertyType;
            var genericHelperType = typeof(GenericSingletonLoader<>).MakeGenericType(navValueType);
            dynamic loader = Activator.CreateInstance(genericHelperType, context, principalInstance, navigation.PropertyInfo);
            return await loader.GetFilteredSingleton();
        }

        private class GenericSingletonLoader<TEntity> where TEntity : class, ISoftDelete {
            private readonly IQueryable<TEntity> _queryOfFilteredSingle;

            public async ValueTask<object> GetFilteredSingleton() {
                return await _queryOfFilteredSingle.SingleOrDefaultAsync();
            }

            public GenericSingletonLoader(DbContext context, object principalInstance, PropertyInfo propertyInfo) {
                var query = context.Entry(principalInstance).Reference(propertyInfo.Name).Query();
                _queryOfFilteredSingle = query.Provider.CreateQuery<TEntity>(query.Expression);
            }
        }
    }
}