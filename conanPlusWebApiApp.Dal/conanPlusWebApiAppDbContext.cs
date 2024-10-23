using Microsoft.EntityFrameworkCore;
using conanPlusWebApiApp.Models;
using Microsoft.Extensions.Configuration;

namespace conanPlusWebApiApp.Dal
{
    public class conanPlusWebApiAppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public conanPlusWebApiAppDbContext(DbContextOptions<conanPlusWebApiAppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<CustomServiceBtnLink> CustomServiceBtnLinks { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vision> Visions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PromoVideo> promoVideos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تعطيل الحذف المتسلسل بين Projects و Services
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Service)
                .WithMany(s => s.Projects)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // تعطيل الحذف المتسلسل بين Projects و Filters
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Filter)
                .WithMany(f => f.Projects)
                .HasForeignKey(p => p.FilterId)
                .OnDelete(DeleteBehavior.Restrict);

            // إعداد المستخدم الافتراضي باستخدام _configuration
            var adminUsername = _configuration["AdminCredentials:Username"];
            var adminPassword = _configuration["AdminCredentials:Password"];
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminPassword);

            // التأكد من إضافة المستخدم مرة واحدة فقط
            if (!string.IsNullOrWhiteSpace(adminUsername) && !string.IsNullOrWhiteSpace(adminPassword))
            {
                modelBuilder.Entity<User>().HasData(new User
                {
                    UserId = 1,
                    Username = adminUsername,
                    PasswordHash = hashedPassword,
                    Role = "Admin"
                });
            }

            SeedData(modelBuilder);

        
        }
        
        private void SeedData(ModelBuilder modelBuilder)    
        {

            // Seeding Services
            modelBuilder.Entity<Service>().HasData(
        new Service { ServiceId = 1, ServiceName = "Website Development", Description = "We provide professional website development services tailored to your needs." },
        new Service { ServiceId = 2, ServiceName = "Graphic Design", Description = "Our graphic design services specialize in creating compelling visuals for your brand." },
        new Service { ServiceId = 3, ServiceName = "Mobile Application", Description = "We develop high-quality mobile applications for iOS and Android platforms." },
        new Service { ServiceId = 4, ServiceName = "UX & UI", Description = "Enhance user experience and interface design with our specialized UX & UI services." },
        new Service { ServiceId = 5, ServiceName = "Social Media Management", Description = "Manage your social media presence effectively with our tailored strategies." },
        new Service { ServiceId = 6, ServiceName = "Motion Graphic", Description = "Create dynamic motion graphics for your marketing and promotional needs." }
    );

            // Seeding Filters مرتبط بالخدمات
            modelBuilder.Entity<Filter>().HasData(
                new Filter { FilterId = 1, FilterName = "E-Commerce", ServiceId = 1 },
                new Filter { FilterId = 2, FilterName = "Corporate", ServiceId = 1 },


                new Filter { FilterId = 3, FilterName = "Logos", ServiceId = 2 },
                new Filter { FilterId = 4, FilterName = "Flyers", ServiceId = 2 },


                new Filter { FilterId = 5, FilterName = "Android", ServiceId = 3 },
                new Filter { FilterId = 6, FilterName = "iOS", ServiceId = 3 },


                new Filter { FilterId = 7, FilterName = "Prototyping", ServiceId = 4 },
                new Filter { FilterId = 8, FilterName = "Wireframes", ServiceId = 4 },


                new Filter { FilterId = 9, FilterName = "Content Creation", ServiceId = 5 },
                new Filter { FilterId = 10, FilterName = "Campaigns", ServiceId = 5 },


                new Filter { FilterId = 11, FilterName = "Explainer Videos", ServiceId = 6 },
                new Filter { FilterId = 12, FilterName = "Ads", ServiceId = 6 }
            );

            

            // Seeding Projects 
            modelBuilder.Entity<Project>().HasData(
      new Project
      {
          ProjectId = 1,
          ProjectTitle = "E-Commerce Website",
          ExternalImageUrl = "/images/ecommerce_external.jpg",
          InternalImageUrl = "/images/ecommerce_internal.jpg",
          ProjectLink = "https://www.behance.net/ecommerce",
          ServiceId = 1,
          FilterId = 1  
      },
      new Project
      {
          ProjectId = 2,
          ProjectTitle = "Corporate Website",
          ExternalImageUrl = "/images/corporate_external.jpg",
          InternalImageUrl = "/images/corporate_internal.jpg",
          ProjectLink = "https://www.behance.net/corporate",
          ServiceId = 1,
          FilterId = 2  
      },

      new Project
      {
          ProjectId = 3,
          ProjectTitle = "Logo Design",
          ExternalImageUrl = "/images/logo_design.jpg",
          InternalImageUrl = "/images/logo_design_internal.jpg",
          ProjectLink = "https://www.behance.net/logo",
          ServiceId = 2,
          FilterId = 3  
      },
      new Project
      {
          ProjectId = 4,
          ProjectTitle = "Flyer Design",
          ExternalImageUrl = "/images/flyer_design.jpg",
          InternalImageUrl = "/images/flyer_design_internal.jpg",
          ProjectLink = "https://www.behance.net/flyer",
          ServiceId = 2,
          FilterId = 4  
      },

      new Project
      {
          ProjectId = 5,
          ProjectTitle = "Android App",
          ExternalImageUrl = "/images/android_app.jpg",
          InternalImageUrl = "/images/android_app_internal.jpg",
          ProjectLink = "https://www.behance.net/androidapp",
          ServiceId = 3,
          FilterId = 5  
      },
      new Project
      {
          ProjectId = 6,
          ProjectTitle = "iOS App",
          ExternalImageUrl = "/images/ios_app.jpg",
          InternalImageUrl = "/images/ios_app_internal.jpg",
          ProjectLink = "https://www.behance.net/iosapp",
          ServiceId = 3,
          FilterId = 6  
      },

      new Project
      {
          ProjectId = 7,
          ProjectTitle = "Prototyping Project",
          ExternalImageUrl = "/images/prototyping.jpg",
          InternalImageUrl = "/images/prototyping_internal.jpg",
          ProjectLink = "https://www.behance.net/prototyping",
          ServiceId = 4,
          FilterId = 7  
      },
      new Project
      {
          ProjectId = 8,
          ProjectTitle = "Wireframe Project",
          ExternalImageUrl = "/images/wireframe.jpg",
          InternalImageUrl = "/images/wireframe_internal.jpg",
          ProjectLink = "https://www.behance.net/wireframe",
          ServiceId = 4,
          FilterId = 8  
      },

      new Project
      {
          ProjectId = 9,
          ProjectTitle = "Content Creation Campaign",
          ExternalImageUrl = "/images/content_creation.jpg",
          InternalImageUrl = "/images/content_creation_internal.jpg",
          ProjectLink = "https://www.behance.net/contentcreation",
          ServiceId = 5,
          FilterId = 9  
      },
      new Project
      {
          ProjectId = 10,
          ProjectTitle = "Marketing Campaign",
          ExternalImageUrl = "/images/campaign.jpg",
          InternalImageUrl = "/images/campaign_internal.jpg",
          ProjectLink = "https://www.behance.net/campaign",
          ServiceId = 5,
          FilterId = 10  
      },

      new Project
      {
          ProjectId = 11,
          ProjectTitle = "Explainer Video",
          ExternalImageUrl = "/images/explainer_video.jpg",
          InternalImageUrl = "/images/explainer_video_internal.jpg",
          ProjectLink = "https://www.behance.net/explainervideo",
          ServiceId = 6,
          FilterId = 11 
      },
      new Project
      {
          ProjectId = 12,
          ProjectTitle = "Ad Video",
          ExternalImageUrl = "/images/ad_video.jpg",
          InternalImageUrl = "/images/ad_video_internal.jpg",
          ProjectLink = "https://www.behance.net/advideo",
          ServiceId = 6,
          FilterId = 12  
      }
  );


            // Seeding Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Name = "John Doe",
                    Specialization = "Software Engineer",
                    ImagePath = "/images/johndoe.jpg",
                    Role = EmployeeRole.Employee,
                    DateAdded = DateTime.Now
                },
                new Employee
                {
                    EmployeeId = 2,
                    Name = "Jane Smith",
                    Specialization = "HR Manager",
                    ImagePath = "/images/janesmith.jpg",
                    Role = EmployeeRole.Hr,
                    DateAdded = DateTime.Now
                }
            );

            // Seeding AboutUs
            modelBuilder.Entity<AboutUs>().HasData(
                new AboutUs
                {
                    AboutUsId = 1,
                    Title = "Why Choose Conan Plus?",
                    Description = "Conan Plus is a leading company offering innovative solutions..."
                }
            );

            // Seeding ContactInfo
            modelBuilder.Entity<ContactInfo>().HasData(
                new ContactInfo
                {
                    ContactId = 1,
                    Address = "123 Main St, City",
                    Phone = "123-456-7890",
                    WorkingHours = "Mon-Fri 9:00AM-5:00PM",
                    Email = "info@conanplus.com",
                    WhatsApp = "987-654-3210",
                    Instagram = "@conanplus"
                }
            );

            // Seeding FAQ
            modelBuilder.Entity<FAQ>().HasData(
                new FAQ
                {
                    FAQId = 1,
                    Question = "What services do you offer?",
                    Answer = "We offer a variety of services including web development, mobile apps, and more."
                }
            );

            // Seeding Features
            modelBuilder.Entity<Feature>().HasData(
                new Feature
                {
                    FeatureId = 1,
                    Text = "Innovative Solutions"
                },
                new Feature
                {
                    FeatureId = 2,
                    Text = "Expert Team"
                }
            );

            // Seeding Goal
            modelBuilder.Entity<Goal>().HasData(
                new Goal
                {
                    GoalId = 1,
                    GoalText = "To become a leader in the technology industry."
                }
            );

            // Seeding Partner
            modelBuilder.Entity<Partner>().HasData(
                new Partner
                {
                    PartnerId = 1,
                    ImageFileName = "/images/partner1.jpg",
                    PartnerName = "Tech Corp"
                }
            );

            // Seeding CustomServiceBtnLink
            modelBuilder.Entity<CustomServiceBtnLink>().HasData(
                new CustomServiceBtnLink
                {
                    CustomServiceId = 1,
                    Link = "www.google.com/"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MsSqlConStr"));
            }
        }
    }
}
