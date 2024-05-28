using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftDeleteSketch.Entities {

    public interface ISoftDelete {

        bool IsDeleted { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }

        void Delete() {
            IsDeleted = true;
            DeletionDate = DateTimeOffset.Now;
        }

        Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default);

        void OnSoftDelete(DbContext context);

        Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default);

        void LoadRelations(DbContext context);

    }
}