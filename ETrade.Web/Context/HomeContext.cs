using ETrade.Abstract;
using ETrade.Entities;
using ETrade.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETrade.Context
{
    public class HomeContext : IdentityDbContext<ApplicationUser, Role, int>
    {
        private IConfiguration Configuration { get; }
        public HomeContext(DbContextOptions<HomeContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Veritabanı bağlantı dizesini burada ayarlayın
            optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:DefaultConnecting"]);
        }

        //veritabanında olmasını istediğimiz classları bu kısma işliyoruz.
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<AnnouncementTopic> AnnouncementTopics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FavoriteItems> FavoriteItems { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<RoleClaim> roleClaims { get; set; }
        public DbSet<UserClaim> userClaims { get; set; }
        public DbSet<UserLogin> userLogins { get; set; }
        public DbSet<UserToken> userTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //çoka-çok ilişki için gereken yapılandırmalar
        {

            base.OnModelCreating(modelBuilder);

         
            //Fluent-Api kullanımı 
            modelBuilder.Entity<ProductCategory>()
               .HasKey(t => new { t.ProductId, t.CategoryId }); // tablonun birincil anahtarı da iki anahtar olmuş oluyor

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pt => pt.product)
                .WithMany(p => p.productCategories)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductCategory>()
            .HasOne(pt => pt.category)
            .WithMany(p => p.productCategories)
            .HasForeignKey(pt => pt.CategoryId);

            modelBuilder.Entity<AnnouncementTopic>()
                .HasKey(t => new { t.AnnouncementId, t.TopicId }); //üzerinde işlem yapacağımız

            modelBuilder.Entity<AnnouncementTopic>()
                .HasOne(pt => pt.announcement)
                .WithMany(p => p.announcementTopics)
                .HasForeignKey(pt => pt.AnnouncementId);

            modelBuilder.Entity<AnnouncementTopic>()
                .HasOne(pt => pt.topic)
                .WithMany(p => p.announcementTopics)
                .HasForeignKey(pt => pt.TopicId);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                base.OnModelCreating(modelBuilder);
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                // Maps to the AspNetUsers table
                b.ToTable("AspNetUsers");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                // Each User can have many UserClaims
                b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            });

            modelBuilder.Entity<IdentityUserLogin>().HasNoKey();






        }
    }
}

