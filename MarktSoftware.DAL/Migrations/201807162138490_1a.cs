namespace MarktSoftware.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 20),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        Image = c.String(),
                        AlternativeImage1 = c.String(),
                        AlternativeImage2 = c.String(),
                        AlternativeImage3 = c.String(),
                        IsHome = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        IsCarousel = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false, maxLength: 30),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.MarktUsers", t => t.Owner_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false, maxLength: 30),
                        Owner_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarktUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.MarktUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        LastName = c.String(maxLength: 25),
                        Username = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 70),
                        Password = c.String(nullable: false, maxLength: 25),
                        ConfirmPassword = c.String(nullable: false),
                        ProfileImageFilename = c.String(maxLength: 30),
                        Phone = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        ActivateGuid = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedUser_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarktUsers", t => t.LikedUser_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.LikedUser_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        Total = c.Double(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        OrderState = c.Int(nullable: false),
                        Username = c.String(),
                        AdresBasligi = c.String(),
                        Adres = c.String(),
                        Sehir = c.String(),
                        Semt = c.String(),
                        Mahalle = c.String(),
                        PostaKodu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Comments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Owner_Id", "dbo.MarktUsers");
            DropForeignKey("dbo.Likeds", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Likeds", "LikedUser_Id", "dbo.MarktUsers");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.MarktUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.OrderLines", new[] { "ProductId" });
            DropIndex("dbo.OrderLines", new[] { "OrderId" });
            DropIndex("dbo.Likeds", new[] { "Product_Id" });
            DropIndex("dbo.Likeds", new[] { "LikedUser_Id" });
            DropIndex("dbo.Comments", new[] { "Product_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Products", new[] { "Owner_Id" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Likeds");
            DropTable("dbo.MarktUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
