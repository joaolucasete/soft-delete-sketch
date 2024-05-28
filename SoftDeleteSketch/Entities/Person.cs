using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftDeleteSketch.Entities {

    public class Person : ISoftDelete {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        #region DeletionDate

        [NotMapped]
        public DateTimeOffset? DeletionDate { get; set; }

        public DateTime? DeletionDateUtc {
            get => DeletionDate?.UtcDateTime;
            set => DeletionDate = value.HasValue ? new DateTimeOffset(value.Value) : null;
        }

        #endregion

        public List<Blog> Blogs { get; set; }

        public List<Post> Posts { get; set; }

        public Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default) {
            // DELETE RELATED ENTITIES
            Blogs.ForEach(b => context.Remove(b));
            Posts.ForEach(p => context.Remove(p));
            return Task.CompletedTask;
        }

        public void OnSoftDelete(DbContext context) {
            throw new NotImplementedException();
        }

        public async Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default) {
            await context.Entry(this).Collection(p => p.Blogs).LoadAsync(cancellationToken);
            await context.Entry(this).Collection(p => p.Posts).LoadAsync(cancellationToken);
        }

        public void LoadRelations(DbContext context) {
            context.Entry(this).Collection(p => p.Blogs).Load();
            context.Entry(this).Collection(p => p.Posts).Load();
        }

    }
}