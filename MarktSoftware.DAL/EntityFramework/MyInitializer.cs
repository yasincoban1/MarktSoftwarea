using System;
using System.Collections.Generic;
using System.Data.Entity;
using MarktSoftware.Entity;

namespace MarktSoftware.DAL.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {

            var kategoriler = new List<Category>()
            {
                new Category(){ Title = "Kamera", Description = "Kamera ürünleri",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"},
                new Category(){ Title = "Bilgisayar", Description = "Bilgisayar ürünleri",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"},
                new Category(){ Title = "Televizyon", Description = "Televizyon ürünleri",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"},
                new Category(){ Title = "Telefon", Description = "Telefon ürünleri",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"},
                new Category(){ Title="Beyaz Eşya",Description="Beyaz Eşya Ürünleri",CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"}
            };

            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();
            var marktuser = new List<MarktUser>()
            {
                new MarktUser(){Name="Yasin",LastName="Çoban",Username="yasincoban",Email="yasincoban601@gmail.com",Password="123",ConfirmPassword="123",ProfileImageFilename="admin.png",Phone="05425314977",IsActive=true,IsAdmin=true,ActivateGuid=Guid.NewGuid(),CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"},
                new MarktUser(){Name="Sinem",LastName="Yılmaz",Username="sinemyilmaz",Email="yasincoban601@gmail.com",Password="123",ConfirmPassword="123",ProfileImageFilename="user.png",Phone="05425314977",IsActive=true,IsAdmin=false,ActivateGuid=Guid.NewGuid(),CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban"}
            };

            foreach (var users in marktuser)
            {
                context.MarktUsers.Add(users);
            }
            context.SaveChanges();

            var products = new List<Product>()
            {
                new Product(){Name="Samsung Galaxy S9",Brand="Samsung",Model="Galaxy",Price=5000,Image="alternative1.png",IsHome=true,IsApproved=true,CreatedOn=DateTime.Now,ModifiedOn=DateTime.Now,ModifiedUsername="yasincoban",CategoryId=1},
               
            };

            foreach (var pro in products)
            {
                context.Products.Add(pro);
            }
            context.SaveChanges();
            base.Seed(context);

            
        }
    }
}
