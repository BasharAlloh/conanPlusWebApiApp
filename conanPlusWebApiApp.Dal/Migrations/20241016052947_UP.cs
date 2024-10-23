using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace conanPlusWebApiApp.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    AboutUsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.AboutUsId);
                });

            migrationBuilder.CreateTable(
                name: "ContactForms",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactForms", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "CustomServiceBtnLinks",
                columns: table => new
                {
                    CustomServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomServiceBtnLinks", x => x.CustomServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    FAQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.FAQId);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WhatsAppLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PartnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TokenVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Visions",
                columns: table => new
                {
                    VisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visions", x => x.VisionId);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterId);
                    table.ForeignKey(
                        name: "FK_Filters_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    InternalImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProjectLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    FilterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Filters_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filters",
                        principalColumn: "FilterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "AboutUsId", "Description", "Title" },
                values: new object[] { 1, "Conan Plus is a leading company offering innovative solutions...", "Why Choose Conan Plus?" });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "ContactId", "Address", "Email", "Instagram", "Phone", "WhatsApp", "WorkingHours" },
                values: new object[] { 1, "123 Main St, City", "info@conanplus.com", "@conanplus", "123-456-7890", "987-654-3210", "Mon-Fri 9:00AM-5:00PM" });

            migrationBuilder.InsertData(
                table: "CustomServiceBtnLinks",
                columns: new[] { "CustomServiceId", "Link" },
                values: new object[] { 1, "www.google.com/" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateAdded", "ImagePath", "Name", "Role", "Specialization" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 16, 8, 29, 46, 97, DateTimeKind.Local).AddTicks(1838), "/images/johndoe.jpg", "John Doe", 1, "Software Engineer" },
                    { 2, new DateTime(2024, 10, 16, 8, 29, 46, 97, DateTimeKind.Local).AddTicks(1845), "/images/janesmith.jpg", "Jane Smith", 0, "HR Manager" }
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "FAQId", "Answer", "Question" },
                values: new object[] { 1, "We offer a variety of services including web development, mobile apps, and more.", "What services do you offer?" });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "FeatureId", "Text" },
                values: new object[,]
                {
                    { 1, "Innovative Solutions" },
                    { 2, "Expert Team" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalId", "GoalText" },
                values: new object[] { 1, "To become a leader in the technology industry." });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "PartnerId", "ImageFileName", "PartnerName" },
                values: new object[] { 1, "/images/partner1.jpg", "Tech Corp" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[,]
                {
                    { 1, "We provide professional website development services tailored to your needs.", "Website Development" },
                    { 2, "Our graphic design services specialize in creating compelling visuals for your brand.", "Graphic Design" },
                    { 3, "We develop high-quality mobile applications for iOS and Android platforms.", "Mobile Application" },
                    { 4, "Enhance user experience and interface design with our specialized UX & UI services.", "UX & UI" },
                    { 5, "Manage your social media presence effectively with our tailored strategies.", "Social Media Management" },
                    { 6, "Create dynamic motion graphics for your marketing and promotional needs.", "Motion Graphic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "Role", "TokenVersion", "Username" },
                values: new object[] { 1, "$2a$11$AdU/j8mbLs95sasj4gDCROdgbCI21GZQsw12R9DRAR2rKnKhc2o5a", "Admin", 1, "a" });

            migrationBuilder.InsertData(
                table: "Filters",
                columns: new[] { "FilterId", "FilterName", "ServiceId" },
                values: new object[,]
                {
                    { 1, "E-Commerce", 1 },
                    { 2, "Corporate", 1 },
                    { 3, "Logos", 2 },
                    { 4, "Flyers", 2 },
                    { 5, "Android", 3 },
                    { 6, "iOS", 3 },
                    { 7, "Prototyping", 4 },
                    { 8, "Wireframes", 4 },
                    { 9, "Content Creation", 5 },
                    { 10, "Campaigns", 5 },
                    { 11, "Explainer Videos", 6 },
                    { 12, "Ads", 6 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "ExternalImageUrl", "FilterId", "InternalImageUrl", "ProjectLink", "ProjectTitle", "ServiceId" },
                values: new object[,]
                {
                    { 1, "/images/ecommerce_external.jpg", 1, "/images/ecommerce_internal.jpg", "https://www.behance.net/ecommerce", "E-Commerce Website", 1 },
                    { 2, "/images/corporate_external.jpg", 2, "/images/corporate_internal.jpg", "https://www.behance.net/corporate", "Corporate Website", 1 },
                    { 3, "/images/logo_design.jpg", 3, "/images/logo_design_internal.jpg", "https://www.behance.net/logo", "Logo Design", 2 },
                    { 4, "/images/flyer_design.jpg", 4, "/images/flyer_design_internal.jpg", "https://www.behance.net/flyer", "Flyer Design", 2 },
                    { 5, "/images/android_app.jpg", 5, "/images/android_app_internal.jpg", "https://www.behance.net/androidapp", "Android App", 3 },
                    { 6, "/images/ios_app.jpg", 6, "/images/ios_app_internal.jpg", "https://www.behance.net/iosapp", "iOS App", 3 },
                    { 7, "/images/prototyping.jpg", 7, "/images/prototyping_internal.jpg", "https://www.behance.net/prototyping", "Prototyping Project", 4 },
                    { 8, "/images/wireframe.jpg", 8, "/images/wireframe_internal.jpg", "https://www.behance.net/wireframe", "Wireframe Project", 4 },
                    { 9, "/images/content_creation.jpg", 9, "/images/content_creation_internal.jpg", "https://www.behance.net/contentcreation", "Content Creation Campaign", 5 },
                    { 10, "/images/campaign.jpg", 10, "/images/campaign_internal.jpg", "https://www.behance.net/campaign", "Marketing Campaign", 5 },
                    { 11, "/images/explainer_video.jpg", 11, "/images/explainer_video_internal.jpg", "https://www.behance.net/explainervideo", "Explainer Video", 6 },
                    { 12, "/images/ad_video.jpg", 12, "/images/ad_video_internal.jpg", "https://www.behance.net/advideo", "Ad Video", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ServiceId",
                table: "Filters",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FilterId",
                table: "Projects",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ServiceId",
                table: "Projects",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "ContactForms");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "CustomServiceBtnLinks");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Visions");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
