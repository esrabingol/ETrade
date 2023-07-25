using ETrade.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Context
{
	//ShopContext bu kısmı içeriyor.!!!!!
	public  class HomeContext : DbContext
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
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder) //çoka-çok ilişki için gereken yapılandırmalar
		{
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
		}
	}
}
