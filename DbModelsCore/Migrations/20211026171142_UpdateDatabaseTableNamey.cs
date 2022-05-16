using Microsoft.EntityFrameworkCore.Migrations;

namespace DbModelsCore.Migrations
{
    public partial class UpdateDatabaseTableNamey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "USERS",
                newName: "USERS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "USER_ROLE",
                newName: "USER_ROLE",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TEST_TABLE",
                newName: "TEST_TABLE",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SLIDER_IMAGES",
                newName: "SLIDER_IMAGES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUT_IMAGE_MAPS",
                newName: "PRODUT_IMAGE_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCTS",
                newName: "PRODUCTS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG_MAPS",
                newName: "PRODUCT_TAG_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG",
                newName: "PRODUCT_TAG",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_FEEDBACKS",
                newName: "PRODUCT_FEEDBACKS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTES",
                newName: "PRODUCT_ATTRIBUTES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "D_B_USER_INFO",
                newName: "D_B_USER_INFO",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "CATEGORY_ATTRIBUTE_MAPERS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CATEGORIES",
                newName: "CATEGORIES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEMS",
                newName: "ATTRIBUTE_ITEMS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "APPLICATION_ACCESS_LOGS",
                newName: "APPLICATION_ACCESS_LOGS",
                newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "USERS",
                schema: "dbo",
                newName: "USERS");

            migrationBuilder.RenameTable(
                name: "USER_ROLE",
                schema: "dbo",
                newName: "USER_ROLE");

            migrationBuilder.RenameTable(
                name: "TEST_TABLE",
                schema: "dbo",
                newName: "TEST_TABLE");

            migrationBuilder.RenameTable(
                name: "SLIDER_IMAGES",
                schema: "dbo",
                newName: "SLIDER_IMAGES");

            migrationBuilder.RenameTable(
                name: "PRODUT_IMAGE_MAPS",
                schema: "dbo",
                newName: "PRODUT_IMAGE_MAPS");

            migrationBuilder.RenameTable(
                name: "PRODUCTS",
                schema: "dbo",
                newName: "PRODUCTS");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG_MAPS",
                schema: "dbo",
                newName: "PRODUCT_TAG_MAPS");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG",
                schema: "dbo",
                newName: "PRODUCT_TAG");

            migrationBuilder.RenameTable(
                name: "PRODUCT_FEEDBACKS",
                schema: "dbo",
                newName: "PRODUCT_FEEDBACKS");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTES",
                schema: "dbo",
                newName: "PRODUCT_ATTRIBUTES");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                schema: "dbo",
                newName: "PRODUCT_ATTRIBUTE_ITEM_MAPS");

            migrationBuilder.RenameTable(
                name: "D_B_USER_INFO",
                schema: "dbo",
                newName: "D_B_USER_INFO");

            migrationBuilder.RenameTable(
                name: "CATEGORY_ATTRIBUTE_MAPERS",
                schema: "dbo",
                newName: "CATEGORY_ATTRIBUTE_MAPERS");

            migrationBuilder.RenameTable(
                name: "CATEGORIES",
                schema: "dbo",
                newName: "CATEGORIES");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEMS",
                schema: "dbo",
                newName: "ATTRIBUTE_ITEMS");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                schema: "dbo",
                newName: "ATTRIBUTE_ITEM_CATEGORY_MAPS");

            migrationBuilder.RenameTable(
                name: "APPLICATION_ACCESS_LOGS",
                schema: "dbo",
                newName: "APPLICATION_ACCESS_LOGS");
        }
    }
}
