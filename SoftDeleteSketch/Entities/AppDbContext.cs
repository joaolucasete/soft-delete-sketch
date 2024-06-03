using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RT.Comb;
using SoftDeleteSketch.Extensions;
using SoftDeleteSketch.Services;
using System.Collections;
using System.Reflection;

namespace SoftDeleteSketch.Entities {

    public class AppDbContext : DbContext {

        private readonly HashSet<object> _stopCircularLook = new HashSet<object>();
        private readonly ICombProvider _combProvider;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ICombProvider combProvider,
            ILogger<AppDbContext> logger
        ) : base(options) {
            _combProvider = combProvider;
            _logger = logger;
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Person> People { get; set; }

        // public DbSet<Review> Reviews { get; set; }

        public virtual async Task SoftDeleteAsync<TEntity>(TEntity entity)
            where TEntity : class, ISoftDelete {
            ArgumentNullException.ThrowIfNull(entity);
            var walker = new CascadeWalker(this);
            await walker.WalkEntitiesSoftDelete(entity);
            _logger.LogInformation("soft deleted {NumFound} entities", walker.NumFound);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.AddSoftDeleteQueryFilter();
            // modelBuilder.Entity<Post>()
            //     .Navigation(p => p.Author)
            //     .IsRequired(false);
            // modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
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
                    OwnerId = persons[0].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 4",
                    OwnerId = persons[0].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 2",
                    OwnerId = persons[1].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 5",
                    OwnerId = persons[1].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 3",
                    OwnerId = persons[2].Id,
                },
                new Blog {
                    Id = _combProvider.Create(),
                    Name = "Blog 6",
                    OwnerId = persons[2].Id,
                }
            };

            var posts = new List<Post> {
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 1",
                    Content = "Content 1",
                    BlogId = blogs[0].Id,
                    AuthorId = persons[0].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 4",
                    Content = "Content 1",
                    BlogId = blogs[1].Id,
                    AuthorId = persons[0].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 2",
                    Content = "Content 2",
                    BlogId = blogs[2].Id,
                    AuthorId = persons[1].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 5",
                    Content = "Content 2",
                    BlogId = blogs[3].Id,
                    AuthorId = persons[1].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 3",
                    Content = "Content 3",
                    BlogId = blogs[4].Id,
                    AuthorId = persons[2].Id
                },
                new Post {
                    Id = _combProvider.Create(),
                    Title = "Post 6",
                    Content = "Content 2",
                    BlogId = blogs[5].Id,
                    AuthorId = persons[2].Id
                },
            };

            modelBuilder.Entity<Person>().HasData(persons);
            modelBuilder.Entity<Blog>().HasData(blogs);
            modelBuilder.Entity<Post>().HasData(posts);

        }

    }

}