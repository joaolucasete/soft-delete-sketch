﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftDeleteSketch.Entities {

    public class Blog : ISoftDelete {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }
        public Person Owner { get; set; }

        public bool IsDeleted { get; set; }

        #region DeletionDate
        [NotMapped]
        public DateTimeOffset? DeletionDate { get; set; }

        public DateTime? DeletionDateUtc {
            get => DeletionDate?.UtcDateTime;
            set => DeletionDate = value.HasValue ? new DateTimeOffset(value.Value) : null;
        }
        #endregion

        public List<Post> Posts { get; set; }
    }

}