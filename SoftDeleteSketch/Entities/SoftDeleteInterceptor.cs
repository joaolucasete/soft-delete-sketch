using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SoftDeleteSketch.Entities;

namespace SoftDeleteSketch {
    public class SoftDeleteInterceptor : SaveChangesInterceptor {

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result) {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries()) {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;

                entry.State = EntityState.Modified;
                delete.Delete();
            }
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken()) {
            if (eventData.Context is null) return new(result);

            foreach (var entry in eventData.Context.ChangeTracker.Entries()) {
                if (entry is { State: EntityState.Deleted, Entity: ISoftDelete delete }) {
                    entry.State = EntityState.Modified;
                    delete.Delete();

                    entry.Metadata.GetNavigations();

                    foreach (var dependentEntity in entry.Metadata.GetDeclaredReferencingForeignKeys()
                        .Where(x => x.DeleteBehavior is DeleteBehavior.Cascade or DeleteBehavior.ClientCascade)
                        .ToList()) {
                        entry.Collection(dependentEntity.GetDefaultName() + "s").Load();
                        
                    }

                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}