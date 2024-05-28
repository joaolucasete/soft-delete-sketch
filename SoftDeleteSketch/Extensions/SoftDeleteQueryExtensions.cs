using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SoftDeleteSketch.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace SoftDeleteSketch.Extensions {
    public static class SoftDeleteQueryExtensions {

        // This method is used to add a query filter to the entity type
        // that implements the ISoftDeletable interface.
        // But it added only a query filter to the entity type.
        // If you want more than one query filter, you must add them manually.
        public static void AddSoftDeleteQueryFilter(this ModelBuilder modelBuilder) {

            //other manual configurations left out       

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                //other automated configurations left out
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }

        public static void AddSoftDeleteQueryFilter(
            this IMutableEntityType entityData) {
            var methodToCall = typeof(SoftDeleteQueryExtensions)
                .GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)filter);
            entityData.AddIndex(entityData.FindProperty(nameof(ISoftDelete.IsDeleted)));
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDelete {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }

    }
}