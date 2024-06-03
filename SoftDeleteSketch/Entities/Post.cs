using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftDeleteSketch.Entities {

    public class Post : ISoftDelete {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        public Guid AuthorId { get; set; }
        public Person Author { get; set; }

        public bool IsDeleted { get; set; }

        #region DeletionDate
        [NotMapped]
        public DateTimeOffset? DeletionDate { get; set; }

        public DateTime? DeletionDateUtc {
            get => DeletionDate?.UtcDateTime;
            set => DeletionDate = value.HasValue ? new DateTimeOffset(value.Value) : null;
        }
        #endregion
    }

}