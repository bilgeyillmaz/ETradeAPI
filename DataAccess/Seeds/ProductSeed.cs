//using Entities.Concrete;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess.Seeds
//{
//    internal class ProductSeed : IEntityTypeConfiguration<Product>
//    {
//        public void Configure(EntityTypeBuilder<Product> builder)
//        {
//            builder.HasData(
//                new Product()
//                {
//                    Id = 1,
//                    CategoryId = 1,
//                    Name = "Kalem",
//                    Price = 100,
//                    Quantity = 20,
//                    UpdatedDate = DateTime.Now,
//                    IsStock= true
//                },
//                new Product()
//                {
//                    Id = 2,
//                    CategoryId = 1,
//                    Name = "Silgi",
//                    Price = 200,
//                    Quantity = 30,
//                    UpdatedDate = DateTime.Now,
//                    IsStock= true
//                },
//                new Product()
//                {
//                    Id = 3,
//                    CategoryId = 1,
//                    Name = "Defter",
//                    Price = 600,
//                    Quantity = 60,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                },
//                new Product()
//                {
//                    Id = 4,
//                    CategoryId = 1,
//                    Name = "Kalemtraş",
//                    Price = 600,
//                    Quantity = 60,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                },
//                new Product()
//                {
//                    Id = 5,
//                    CategoryId = 2,
//                    Name = "Televizyon",
//                    Price = 6600,
//                    Quantity = 320,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                },
//                new Product()
//                {
//                    Id = 7,
//                    CategoryId = 2,
//                    Name = "Laptop",
//                    Price = 6600,
//                    Quantity = 320,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                },
//                new Product()
//                {
//                    Id = 8,
//                    CategoryId = 2,
//                    Name = "Klavye",
//                    Price = 6600,
//                    Quantity = 320,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                },
//                new Product()
//                {
//                    Id = 9,
//                    CategoryId = 2,
//                    Name = "Monitör",
//                    Price = 6600,
//                    Quantity = 320,
//                    UpdatedDate = DateTime.Now,
//                    IsStock = true
//                }

//            );
//        }
//    }
//}
