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
    }
}