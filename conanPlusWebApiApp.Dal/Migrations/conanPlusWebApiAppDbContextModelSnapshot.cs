﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using conanPlusWebApiApp.Dal;

#nullable disable

namespace conanPlusWebApiApp.Dal.Migrations
{
    [DbContext(typeof(conanPlusWebApiAppDbContext))]
    partial class conanPlusWebApiAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactForm", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MessageId");

                    b.ToTable("ContactForms");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.AboutUs", b =>
                {
                    b.Property<int>("AboutUsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AboutUsId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AboutUsId");

                    b.ToTable("AboutUs");

                    b.HasData(
                        new
                        {
                            AboutUsId = 1,
                            Description = "Conan Plus is a leading company offering innovative solutions...",
                            Title = "Why Choose Conan Plus?"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.ContactInfo", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Instagram")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("WhatsApp")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ContactId");

                    b.ToTable("ContactInfos");

                    b.HasData(
                        new
                        {
                            ContactId = 1,
                            Address = "123 Main St, City",
                            Email = "info@conanplus.com",
                            Instagram = "@conanplus",
                            Phone = "123-456-7890",
                            WhatsApp = "987-654-3210",
                            WorkingHours = "Mon-Fri 9:00AM-5:00PM"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.CustomServiceBtnLink", b =>
                {
                    b.Property<int>("CustomServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomServiceId"));

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("CustomServiceId");

                    b.ToTable("CustomServiceBtnLinks");

                    b.HasData(
                        new
                        {
                            CustomServiceId = 1,
                            Link = "www.google.com/"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DateAdded = new DateTime(2024, 10, 19, 5, 21, 42, 637, DateTimeKind.Local).AddTicks(5185),
                            ImagePath = "/images/johndoe.jpg",
                            Name = "John Doe",
                            Role = 1,
                            Specialization = "Software Engineer"
                        },
                        new
                        {
                            EmployeeId = 2,
                            DateAdded = new DateTime(2024, 10, 19, 5, 21, 42, 637, DateTimeKind.Local).AddTicks(5191),
                            ImagePath = "/images/janesmith.jpg",
                            Name = "Jane Smith",
                            Role = 0,
                            Specialization = "HR Manager"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.FAQ", b =>
                {
                    b.Property<int>("FAQId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FAQId"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("FAQId");

                    b.ToTable("FAQs");

                    b.HasData(
                        new
                        {
                            FAQId = 1,
                            Answer = "We offer a variety of services including web development, mobile apps, and more.",
                            Question = "What services do you offer?"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Feature", b =>
                {
                    b.Property<int>("FeatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeatureId"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("FeatureId");

                    b.ToTable("Features");

                    b.HasData(
                        new
                        {
                            FeatureId = 1,
                            Text = "Innovative Solutions"
                        },
                        new
                        {
                            FeatureId = 2,
                            Text = "Expert Team"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Filter", b =>
                {
                    b.Property<int>("FilterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FilterId"));

                    b.Property<string>("FilterName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("FilterId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Filters");

                    b.HasData(
                        new
                        {
                            FilterId = 1,
                            FilterName = "E-Commerce",
                            ServiceId = 1
                        },
                        new
                        {
                            FilterId = 2,
                            FilterName = "Corporate",
                            ServiceId = 1
                        },
                        new
                        {
                            FilterId = 3,
                            FilterName = "Logos",
                            ServiceId = 2
                        },
                        new
                        {
                            FilterId = 4,
                            FilterName = "Flyers",
                            ServiceId = 2
                        },
                        new
                        {
                            FilterId = 5,
                            FilterName = "Android",
                            ServiceId = 3
                        },
                        new
                        {
                            FilterId = 6,
                            FilterName = "iOS",
                            ServiceId = 3
                        },
                        new
                        {
                            FilterId = 7,
                            FilterName = "Prototyping",
                            ServiceId = 4
                        },
                        new
                        {
                            FilterId = 8,
                            FilterName = "Wireframes",
                            ServiceId = 4
                        },
                        new
                        {
                            FilterId = 9,
                            FilterName = "Content Creation",
                            ServiceId = 5
                        },
                        new
                        {
                            FilterId = 10,
                            FilterName = "Campaigns",
                            ServiceId = 5
                        },
                        new
                        {
                            FilterId = 11,
                            FilterName = "Explainer Videos",
                            ServiceId = 6
                        },
                        new
                        {
                            FilterId = 12,
                            FilterName = "Ads",
                            ServiceId = 6
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalId"));

                    b.Property<string>("GoalText")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("GoalId");

                    b.ToTable("Goals");

                    b.HasData(
                        new
                        {
                            GoalId = 1,
                            GoalText = "To become a leader in the technology industry."
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Package", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("WhatsAppLink")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Partner", b =>
                {
                    b.Property<int>("PartnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartnerId"));

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PartnerId");

                    b.ToTable("Partners");

                    b.HasData(
                        new
                        {
                            PartnerId = 1,
                            ImageFileName = "/images/partner1.jpg",
                            PartnerName = "Tech Corp"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("ExternalImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("FilterId")
                        .HasColumnType("int");

                    b.Property<string>("InternalImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ProjectLink")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ProjectTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("FilterId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = 1,
                            ExternalImageUrl = "/images/ecommerce_external.jpg",
                            FilterId = 1,
                            InternalImageUrl = "/images/ecommerce_internal.jpg",
                            ProjectLink = "https://www.behance.net/ecommerce",
                            ProjectTitle = "E-Commerce Website",
                            ServiceId = 1
                        },
                        new
                        {
                            ProjectId = 2,
                            ExternalImageUrl = "/images/corporate_external.jpg",
                            FilterId = 2,
                            InternalImageUrl = "/images/corporate_internal.jpg",
                            ProjectLink = "https://www.behance.net/corporate",
                            ProjectTitle = "Corporate Website",
                            ServiceId = 1
                        },
                        new
                        {
                            ProjectId = 3,
                            ExternalImageUrl = "/images/logo_design.jpg",
                            FilterId = 3,
                            InternalImageUrl = "/images/logo_design_internal.jpg",
                            ProjectLink = "https://www.behance.net/logo",
                            ProjectTitle = "Logo Design",
                            ServiceId = 2
                        },
                        new
                        {
                            ProjectId = 4,
                            ExternalImageUrl = "/images/flyer_design.jpg",
                            FilterId = 4,
                            InternalImageUrl = "/images/flyer_design_internal.jpg",
                            ProjectLink = "https://www.behance.net/flyer",
                            ProjectTitle = "Flyer Design",
                            ServiceId = 2
                        },
                        new
                        {
                            ProjectId = 5,
                            ExternalImageUrl = "/images/android_app.jpg",
                            FilterId = 5,
                            InternalImageUrl = "/images/android_app_internal.jpg",
                            ProjectLink = "https://www.behance.net/androidapp",
                            ProjectTitle = "Android App",
                            ServiceId = 3
                        },
                        new
                        {
                            ProjectId = 6,
                            ExternalImageUrl = "/images/ios_app.jpg",
                            FilterId = 6,
                            InternalImageUrl = "/images/ios_app_internal.jpg",
                            ProjectLink = "https://www.behance.net/iosapp",
                            ProjectTitle = "iOS App",
                            ServiceId = 3
                        },
                        new
                        {
                            ProjectId = 7,
                            ExternalImageUrl = "/images/prototyping.jpg",
                            FilterId = 7,
                            InternalImageUrl = "/images/prototyping_internal.jpg",
                            ProjectLink = "https://www.behance.net/prototyping",
                            ProjectTitle = "Prototyping Project",
                            ServiceId = 4
                        },
                        new
                        {
                            ProjectId = 8,
                            ExternalImageUrl = "/images/wireframe.jpg",
                            FilterId = 8,
                            InternalImageUrl = "/images/wireframe_internal.jpg",
                            ProjectLink = "https://www.behance.net/wireframe",
                            ProjectTitle = "Wireframe Project",
                            ServiceId = 4
                        },
                        new
                        {
                            ProjectId = 9,
                            ExternalImageUrl = "/images/content_creation.jpg",
                            FilterId = 9,
                            InternalImageUrl = "/images/content_creation_internal.jpg",
                            ProjectLink = "https://www.behance.net/contentcreation",
                            ProjectTitle = "Content Creation Campaign",
                            ServiceId = 5
                        },
                        new
                        {
                            ProjectId = 10,
                            ExternalImageUrl = "/images/campaign.jpg",
                            FilterId = 10,
                            InternalImageUrl = "/images/campaign_internal.jpg",
                            ProjectLink = "https://www.behance.net/campaign",
                            ProjectTitle = "Marketing Campaign",
                            ServiceId = 5
                        },
                        new
                        {
                            ProjectId = 11,
                            ExternalImageUrl = "/images/explainer_video.jpg",
                            FilterId = 11,
                            InternalImageUrl = "/images/explainer_video_internal.jpg",
                            ProjectLink = "https://www.behance.net/explainervideo",
                            ProjectTitle = "Explainer Video",
                            ServiceId = 6
                        },
                        new
                        {
                            ProjectId = 12,
                            ExternalImageUrl = "/images/ad_video.jpg",
                            FilterId = 12,
                            InternalImageUrl = "/images/ad_video_internal.jpg",
                            ProjectLink = "https://www.behance.net/advideo",
                            ProjectTitle = "Ad Video",
                            ServiceId = 6
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.PromoVideo", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("VideoFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.ToTable("promoVideos");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ServiceId = 1,
                            Description = "We provide professional website development services tailored to your needs.",
                            ServiceName = "Website Development"
                        },
                        new
                        {
                            ServiceId = 2,
                            Description = "Our graphic design services specialize in creating compelling visuals for your brand.",
                            ServiceName = "Graphic Design"
                        },
                        new
                        {
                            ServiceId = 3,
                            Description = "We develop high-quality mobile applications for iOS and Android platforms.",
                            ServiceName = "Mobile Application"
                        },
                        new
                        {
                            ServiceId = 4,
                            Description = "Enhance user experience and interface design with our specialized UX & UI services.",
                            ServiceName = "UX & UI"
                        },
                        new
                        {
                            ServiceId = 5,
                            Description = "Manage your social media presence effectively with our tailored strategies.",
                            ServiceName = "Social Media Management"
                        },
                        new
                        {
                            ServiceId = 6,
                            Description = "Create dynamic motion graphics for your marketing and promotional needs.",
                            ServiceName = "Motion Graphic"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TokenVersion")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PasswordHash = "$2a$11$PNQWbFQPVhR768Mwn/OIOucmmJnxf0eb98.UVbCOlo.NT4gLNrxmm",
                            Role = "Admin",
                            TokenVersion = 1,
                            Username = "a"
                        });
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Vision", b =>
                {
                    b.Property<int>("VisionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("VisionId");

                    b.ToTable("Visions");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Filter", b =>
                {
                    b.HasOne("conanPlusWebApiApp.Models.Service", "Service")
                        .WithMany("Filters")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Project", b =>
                {
                    b.HasOne("conanPlusWebApiApp.Models.Filter", "Filter")
                        .WithMany("Projects")
                        .HasForeignKey("FilterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("conanPlusWebApiApp.Models.Service", "Service")
                        .WithMany("Projects")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Filter");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Filter", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("conanPlusWebApiApp.Models.Service", b =>
                {
                    b.Navigation("Filters");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
