using DbModelsCore.Models.Admin;
using DbModelsCore.Models.Ecommerce;
using DbModelsCore.Models.Ecommerce.Catalog;
using DbModelsCore.Models.Ecommerce.Product;
using DbModelsCore.Models.TestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*
         * Common
         */

        public virtual DbSet<DBUserRole> UserRole { get; set; }

        /*
         * Admin
         */
        public virtual DbSet<DBUser> Users { get; set; }
        public virtual DbSet<DBTestTableModel> TestTable { get; set; }
        public virtual DbSet<DBApplicationAccessLog> ApplicationAccessLogs { get; set; }

        /*
         * ECommerce
         */

        #region "Ecommerce ============================"
        
        #region"Catalog"
        public virtual DbSet<DBProductCategory> Categories { get; set; }
        public virtual DbSet<DBProductTag> ProductTag { get; set; }
        public virtual DbSet<DBProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<DBCategoryAttributeMaper> CategoryAttributeMapers { get; set; }
        public virtual DbSet<DBAttributeItemCategoryMap> AttributeItemCategoryMaps { get; set; }
        public virtual DbSet<DBProductAttributeItems> AttributeItems { get; set; }
        public virtual DbSet<DBSliderImage> SliderImages { get; set; }
        #endregion


        #region "Product "
        public virtual DbSet<DBProduct> Products { get; set; }
        public virtual DbSet<DBProductAttributeItemMap> ProductAttributeItemMaps { get; set; }
        //public virtual DbSet<DBProductAttributeMap> ProductAttributeMaps { get; set; }
        //public virtual DbSet<DBProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual DbSet<DBProductTagMap> ProductTagMaps { get; set; }
        public virtual DbSet<DBProdutImageMap> ProdutImageMaps { get; set; }
        public virtual DbSet<DBProductFeedback> ProductFeedbacks  { get; set; }

        #endregion

        #endregion



        #region Converting into Oracle format
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Oracle Table Name format
                entity.SetTableName(CaseChanger(entity.GetTableName()));

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(CaseChanger(property.Name));
                }

            }


            modelBuilder.Entity<DBUserRole>().HasData(
             new DBUserRole
             {
                 ID = 1,
                 Name = "Super Admin"
             },
             new DBUserRole
             {
                 ID = 2,
                 Name = "Admin"
             },
             new DBUserRole
             {
                 ID = 3,
                 Name = "Vendor"
             },
             new DBUserRole
             {
                 ID = 4,
                 Name = "User"
             }
         );
        }*/

        // Return ABC_EXAMPLE_ABC
        private static string CaseChanger(string item)
        {
            string name = null;
            foreach (char character in item)
            {
                if (character >= 'A' && character <= 'Z')
                {
                    name += '_';
                }
                name += character;
            }
            return (name.Substring(1)).ToUpper();
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");



            modelBuilder.Entity<DBUserRole>().HasData(
                new DBUserRole
                {
                    ID = 1,
                    Name = "Super Admin"
                },
                new DBUserRole
                {
                    ID = 2,
                    Name = "Admin"
                },
                new DBUserRole
                {
                    ID = 3,
                    Name = "Vendor"
                },
                new DBUserRole
                {
                    ID = 4,
                    Name = "User"
                }
            );

        }
    }
}
