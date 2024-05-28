using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RT.Comb;
using SoftDeleteSketch.Extensions;

namespace SoftDeleteSketch.Entities {

    public class AppDbContext : DbContext {

        private readonly ICombProvider _combProvider;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ICombProvider combProvider
        ) : base(options) {
            _combProvider = combProvider;
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Person> People { get; set; }

        // public DbSet<Review> Reviews { get; set; }

        public override EntityEntry Remove(object entity) {
            return base.Remove(entity);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity) {
            var result = base.Remove(entity);

            if (entity is not ISoftDelete softDeleteEntity) {
                return result;
            }

            softDeleteEntity.LoadRelations(this);
            softDeleteEntity.OnSoftDelete(this);

            return result;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.AddSoftDeleteQueryFilter();
            // modelBuilder.Entity<Post>()
            //     .Navigation(p => p.Author)
            //     .IsRequired(false);
            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
            // modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
            // modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);

            var persons = new List<Person> {
                new Person {
                    Id = _combProvider.Create(),
                    Name = "Person 1",
                },
                new Person {
                    Id = _combProvider.Create(),
                    Name = "Person 2",
                },
                new Person {
                    Id = _combProvider.Create(),
                    Name = "Person 3",
                },
            };

            var blogs = new List<Blog> {
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 1",
                    OwnerId = persons[2].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 2",
                    OwnerId = persons[1].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 3",
                    OwnerId = persons[0].Id,
                }
            };

            var posts = new List<Post> {
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 1",
                    Content = "Content 1",
                    BlogId = blogs[2].Id,
                    AuthorId = persons[2].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 2",
                    Content = "Content 2",
                    BlogId = blogs[1].Id,
                    AuthorId = persons[1].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 3",
                    Content = "Content 3",
                    BlogId = blogs[0].Id,
                    AuthorId = persons[0].Id
                },
            };

            modelBuilder.Entity<Person>().HasData(persons);
            modelBuilder.Entity<Blog>().HasData(blogs);
            modelBuilder.Entity<Post>().HasData(posts);

        }

    }

}