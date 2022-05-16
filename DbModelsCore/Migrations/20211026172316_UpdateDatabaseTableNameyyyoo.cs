using Microsoft.EntityFrameworkCore.Migrations;

namespace DbModelsCore.Migrations
{
    public partial class UpdateDatabaseTableNameyyyoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTES_PRODUCT_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS");

            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORIES_CATEGORIES_PARENT_ID",
                schema: "dbo",
                table: "CATEGORIES");

            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORY_ATTRIBUTE_MAPERS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS");

            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORY_ATTRIBUTE_MAPERS_PRODUCT_ATTRIBUTES_ATTRIBUTE_ID",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS");

            migrationBuilder.DropForeignKey(
                name: "FK_D_B_USER_INFO_USERS_USER_ID",
                schema: "dbo",
                table: "D_B_USER_INFO");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_FEEDBACKS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_TAG_MAPS_PRODUCT_TAG_TAG_ID",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_TAG_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "PRODUCTS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUT_IMAGE_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS");

            migrationBuilder.DropForeignKey(
                name: "FK_USERS_USER_ROLE_ROLE_ID",
                schema: "dbo",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS",
                schema: "dbo",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCTS",
                schema: "dbo",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CATEGORIES",
                schema: "dbo",
                table: "CATEGORIES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_ROLE",
                schema: "dbo",
                table: "USER_ROLE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEST_TABLE",
                schema: "dbo",
                table: "TEST_TABLE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SLIDER_IMAGES",
                schema: "dbo",
                table: "SLIDER_IMAGES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUT_IMAGE_MAPS",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_TAG_MAPS",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_TAG",
                schema: "dbo",
                table: "PRODUCT_TAG");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_FEEDBACKS",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_ATTRIBUTES",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_ATTRIBUTE_ITEM_MAPS",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_D_B_USER_INFO",
                schema: "dbo",
                table: "D_B_USER_INFO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CATEGORY_ATTRIBUTE_MAPERS",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ATTRIBUTE_ITEMS",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ATTRIBUTE_ITEM_CATEGORY_MAPS",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_APPLICATION_ACCESS_LOGS",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS");

            migrationBuilder.RenameTable(
                name: "USERS",
                schema: "dbo",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCTS",
                schema: "dbo",
                newName: "Products",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CATEGORIES",
                schema: "dbo",
                newName: "Categories",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "USER_ROLE",
                schema: "dbo",
                newName: "UserRole",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TEST_TABLE",
                schema: "dbo",
                newName: "TestTable",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SLIDER_IMAGES",
                schema: "dbo",
                newName: "SliderImages",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUT_IMAGE_MAPS",
                schema: "dbo",
                newName: "ProdutImageMaps",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG_MAPS",
                schema: "dbo",
                newName: "ProductTagMaps",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_TAG",
                schema: "dbo",
                newName: "ProductTag",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_FEEDBACKS",
                schema: "dbo",
                newName: "ProductFeedbacks",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTES",
                schema: "dbo",
                newName: "ProductAttributes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                schema: "dbo",
                newName: "ProductAttributeItemMaps",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "D_B_USER_INFO",
                schema: "dbo",
                newName: "DBUserInfo",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CATEGORY_ATTRIBUTE_MAPERS",
                schema: "dbo",
                newName: "CategoryAttributeMapers",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEMS",
                schema: "dbo",
                newName: "AttributeItems",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                schema: "dbo",
                newName: "AttributeItemCategoryMaps",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "APPLICATION_ACCESS_LOGS",
                schema: "dbo",
                newName: "ApplicationAccessLogs",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "PASSWORD",
                schema: "dbo",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "MOBILE",
                schema: "dbo",
                table: "Users",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                schema: "dbo",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "Users",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "ROLE_ID",
                schema: "dbo",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "LAST_NAME",
                schema: "dbo",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "Users",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "FULL_NAME",
                schema: "dbo",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "FIRST_NAME",
                schema: "dbo",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "DATE_OF_BIRTH",
                schema: "dbo",
                table: "Users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_USERS_ROLE_ID",
                schema: "dbo",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "SLUG",
                schema: "dbo",
                table: "Products",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "Products",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "Products",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "S_K_U",
                schema: "dbo",
                table: "Products",
                newName: "SKU");

            migrationBuilder.RenameColumn(
                name: "STOKE_AMOUNT",
                schema: "dbo",
                table: "Products",
                newName: "StokeAmount");

            migrationBuilder.RenameColumn(
                name: "SHORT_DESCRIPTION",
                schema: "dbo",
                table: "Products",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "OLD_PRICE",
                schema: "dbo",
                table: "Products",
                newName: "OldPrice");

            migrationBuilder.RenameColumn(
                name: "NEW_PRICE",
                schema: "dbo",
                table: "Products",
                newName: "NewPrice");

            migrationBuilder.RenameColumn(
                name: "MINIMUM_STOKE_LIMIT",
                schema: "dbo",
                table: "Products",
                newName: "MinimumStokeLimit");

            migrationBuilder.RenameColumn(
                name: "IS_TRACK_STOKE",
                schema: "dbo",
                table: "Products",
                newName: "IsTrackStoke");

            migrationBuilder.RenameColumn(
                name: "IS_SHOW_ON_HOME",
                schema: "dbo",
                table: "Products",
                newName: "IsShowOnHome");

            migrationBuilder.RenameColumn(
                name: "IS_RETURN_ABLE",
                schema: "dbo",
                table: "Products",
                newName: "IsReturnAble");

            migrationBuilder.RenameColumn(
                name: "IS_OPEN_REVIEW",
                schema: "dbo",
                table: "Products",
                newName: "IsOpenReview");

            migrationBuilder.RenameColumn(
                name: "IS_NEW",
                schema: "dbo",
                table: "Products",
                newName: "IsNew");

            migrationBuilder.RenameColumn(
                name: "IS_MULTIPLE_WARE_HOUSE",
                schema: "dbo",
                table: "Products",
                newName: "IsMultipleWareHouse");

            migrationBuilder.RenameColumn(
                name: "IS_ENABLE_SHOP",
                schema: "dbo",
                table: "Products",
                newName: "IsEnableShop");

            migrationBuilder.RenameColumn(
                name: "IS_DRAFT",
                schema: "dbo",
                table: "Products",
                newName: "IsDraft");

            migrationBuilder.RenameColumn(
                name: "IS_DISPLAY_AVAIABLE",
                schema: "dbo",
                table: "Products",
                newName: "IsDisplayAvaiable");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "Products",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "G_T_I_N",
                schema: "dbo",
                table: "Products",
                newName: "GTIN");

            migrationBuilder.RenameColumn(
                name: "FULL_DESCRIPTION",
                schema: "dbo",
                table: "Products",
                newName: "FullDescription");

            migrationBuilder.RenameColumn(
                name: "FEATURE_IMAGE",
                schema: "dbo",
                table: "Products",
                newName: "FeatureImage");

            migrationBuilder.RenameColumn(
                name: "DISCOUNT_IN_PERCENT",
                schema: "dbo",
                table: "Products",
                newName: "DiscountInPercent");

            migrationBuilder.RenameColumn(
                name: "DISCOUNT_AMOUNT",
                schema: "dbo",
                table: "Products",
                newName: "DiscountAmount");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "Products",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CATEGORY_ID",
                schema: "dbo",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ADMIN_COMMENT",
                schema: "dbo",
                table: "Products",
                newName: "AdminComment");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                schema: "dbo",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                schema: "dbo",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "Categories",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "Categories",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "PARENT_ID",
                schema: "dbo",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "IS_SHOW_ON_HOME",
                schema: "dbo",
                table: "Categories",
                newName: "IsShowOnHome");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "Categories",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IMAGE_URL",
                schema: "dbo",
                table: "Categories",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "Categories",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "Categories",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORIES_PARENT_ID",
                schema: "dbo",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "UserRole",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "UserRole",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "TestTable",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "TestTable",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "SliderImages",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "SliderImages",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "SliderImages",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IMAGE_VIEWER",
                schema: "dbo",
                table: "SliderImages",
                newName: "ImageViewer");

            migrationBuilder.RenameColumn(
                name: "IMAGE_URL",
                schema: "dbo",
                table: "SliderImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "IMAGE_TITLE",
                schema: "dbo",
                table: "SliderImages",
                newName: "ImageTitle");

            migrationBuilder.RenameColumn(
                name: "IMAGE_NAME",
                schema: "dbo",
                table: "SliderImages",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "SliderImages",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "SliderImages",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "SliderImages",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                schema: "dbo",
                table: "ProdutImageMaps",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "IMAGE_U_R_L",
                schema: "dbo",
                table: "ProdutImageMaps",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProdutImageMaps",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUT_IMAGE_MAPS_PRODUCT_ID",
                schema: "dbo",
                table: "ProdutImageMaps",
                newName: "IX_ProdutImageMaps_ProductId");

            migrationBuilder.RenameColumn(
                name: "TAG_ID",
                schema: "dbo",
                table: "ProductTagMaps",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                schema: "dbo",
                table: "ProductTagMaps",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProductTagMaps",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_TAG_MAPS_TAG_ID",
                schema: "dbo",
                table: "ProductTagMaps",
                newName: "IX_ProductTagMaps_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_TAG_MAPS_PRODUCT_ID",
                schema: "dbo",
                table: "ProductTagMaps",
                newName: "IX_ProductTagMaps_ProductId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "ProductTag",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "ProductTag",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "ProductTag",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "ProductTag",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "ProductTag",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "ProductTag",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProductTag",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RATING",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "MESSAGE",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "USER_I_D",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "REPLY_I_D",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "ReplyID");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_FEEDBACKS_PRODUCT_ID",
                schema: "dbo",
                table: "ProductFeedbacks",
                newName: "IX_ProductFeedbacks_ProductId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProductAttributes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "ItemAttributeId");

            migrationBuilder.RenameColumn(
                name: "ATTRIBUTE_ID",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "AttributeId");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCT_ID",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "IX_ProductAttributeItemMaps_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                newName: "IX_ProductAttributeItemMaps_ItemAttributeId");

            migrationBuilder.RenameColumn(
                name: "USER_ID",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_STATE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryState");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_POSTAL_CODE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryPostalCode");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_COUNTRY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryCountry");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_CITY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryCity");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_APARTMENT",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryApartment");

            migrationBuilder.RenameColumn(
                name: "TEMPORARY_ADDRESS",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "TemporaryAddress");

            migrationBuilder.RenameColumn(
                name: "PROFILE_IMAGE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "ProfileImage");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_STATE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentState");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_POSTAL_CODE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentPostalCode");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_COUNTRY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentCountry");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_CITY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentCity");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_APARTMENT",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentApartment");

            migrationBuilder.RenameColumn(
                name: "PERMANENT_ADDRESS",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "PermanentAddress");

            migrationBuilder.RenameColumn(
                name: "N_I_D_IMAGE_FRONT",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "NIDImageFront");

            migrationBuilder.RenameColumn(
                name: "N_I_D_IMAGE_BACK",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "NIDImageBack");

            migrationBuilder.RenameColumn(
                name: "N_I_D",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "NID");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_D_B_USER_INFO_USER_ID",
                schema: "dbo",
                table: "DBUserInfo",
                newName: "IX_DBUserInfo_UserId");

            migrationBuilder.RenameColumn(
                name: "CATEGORY_ID",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ATTRIBUTE_ID",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                newName: "AttributeId");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORY_ATTRIBUTE_MAPERS_CATEGORY_ID",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                newName: "IX_CategoryAttributeMapers_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORY_ATTRIBUTE_MAPERS_ATTRIBUTE_ID",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                newName: "IX_CategoryAttributeMapers_AttributeId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                schema: "dbo",
                table: "AttributeItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UPDATED_DATE",
                schema: "dbo",
                table: "AttributeItems",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATED_BY",
                schema: "dbo",
                table: "AttributeItems",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ATTRIBUTE_ID",
                schema: "dbo",
                table: "AttributeItems",
                newName: "ProductAttributeId");

            migrationBuilder.RenameColumn(
                name: "IS_DELETE",
                schema: "dbo",
                table: "AttributeItems",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "CREATED_DATE",
                schema: "dbo",
                table: "AttributeItems",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATED_BY",
                schema: "dbo",
                table: "AttributeItems",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "AttributeItems",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTE_ID",
                schema: "dbo",
                table: "AttributeItems",
                newName: "IX_AttributeItems_ProductAttributeId");

            migrationBuilder.RenameColumn(
                name: "ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                newName: "ItemAttributeId");

            migrationBuilder.RenameColumn(
                name: "CATEGORY_ID",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                newName: "IX_AttributeItemCategoryMaps_ItemAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORY_ID",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                newName: "IX_AttributeItemCategoryMaps_CategoryId");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "USER_NAME",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "USER_IP_HOST_NAME",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "UserIpHostName");

            migrationBuilder.RenameColumn(
                name: "USER_ID",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "MODULE_NAME",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "ModuleName");

            migrationBuilder.RenameColumn(
                name: "ACTIVITY_NAME",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "ActivityName");

            migrationBuilder.RenameColumn(
                name: "ACTION_TYPE",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "ActionType");

            migrationBuilder.RenameColumn(
                name: "ACCESS_DATE_TIME",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "AccessDateTime");

            migrationBuilder.RenameColumn(
                name: "I_D",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "dbo",
                table: "Products",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "dbo",
                table: "Categories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                schema: "dbo",
                table: "UserRole",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestTable",
                schema: "dbo",
                table: "TestTable",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SliderImages",
                schema: "dbo",
                table: "SliderImages",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutImageMaps",
                schema: "dbo",
                table: "ProdutImageMaps",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTagMaps",
                schema: "dbo",
                table: "ProductTagMaps",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                schema: "dbo",
                table: "ProductTag",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFeedbacks",
                schema: "dbo",
                table: "ProductFeedbacks",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributes",
                schema: "dbo",
                table: "ProductAttributes",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeItemMaps",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DBUserInfo",
                schema: "dbo",
                table: "DBUserInfo",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryAttributeMapers",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeItems",
                schema: "dbo",
                table: "AttributeItems",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeItemCategoryMaps",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationAccessLogs",
                schema: "dbo",
                table: "ApplicationAccessLogs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeItemCategoryMaps_AttributeItems_ItemAttributeId",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                column: "ItemAttributeId",
                principalSchema: "dbo",
                principalTable: "AttributeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeItemCategoryMaps_Categories_CategoryId",
                schema: "dbo",
                table: "AttributeItemCategoryMaps",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeItems_ProductAttributes_ProductAttributeId",
                schema: "dbo",
                table: "AttributeItems",
                column: "ProductAttributeId",
                principalSchema: "dbo",
                principalTable: "ProductAttributes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                schema: "dbo",
                table: "Categories",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttributeMapers_Categories_CategoryId",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttributeMapers_ProductAttributes_AttributeId",
                schema: "dbo",
                table: "CategoryAttributeMapers",
                column: "AttributeId",
                principalSchema: "dbo",
                principalTable: "ProductAttributes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DBUserInfo_Users_UserId",
                schema: "dbo",
                table: "DBUserInfo",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeItemMaps_AttributeItems_ItemAttributeId",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                column: "ItemAttributeId",
                principalSchema: "dbo",
                principalTable: "AttributeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeItemMaps_Products_ProductId",
                schema: "dbo",
                table: "ProductAttributeItemMaps",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeedbacks_Products_ProductId",
                schema: "dbo",
                table: "ProductFeedbacks",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "dbo",
                table: "Products",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTagMaps_Products_ProductId",
                schema: "dbo",
                table: "ProductTagMaps",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTagMaps_ProductTag_TagId",
                schema: "dbo",
                table: "ProductTagMaps",
                column: "TagId",
                principalSchema: "dbo",
                principalTable: "ProductTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutImageMaps_Products_ProductId",
                schema: "dbo",
                table: "ProdutImageMaps",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRole_RoleId",
                schema: "dbo",
                table: "Users",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "UserRole",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeItemCategoryMaps_AttributeItems_ItemAttributeId",
                schema: "dbo",
                table: "AttributeItemCategoryMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeItemCategoryMaps_Categories_CategoryId",
                schema: "dbo",
                table: "AttributeItemCategoryMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeItems_ProductAttributes_ProductAttributeId",
                schema: "dbo",
                table: "AttributeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                schema: "dbo",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttributeMapers_Categories_CategoryId",
                schema: "dbo",
                table: "CategoryAttributeMapers");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttributeMapers_ProductAttributes_AttributeId",
                schema: "dbo",
                table: "CategoryAttributeMapers");

            migrationBuilder.DropForeignKey(
                name: "FK_DBUserInfo_Users_UserId",
                schema: "dbo",
                table: "DBUserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeItemMaps_AttributeItems_ItemAttributeId",
                schema: "dbo",
                table: "ProductAttributeItemMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeItemMaps_Products_ProductId",
                schema: "dbo",
                table: "ProductAttributeItemMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeedbacks_Products_ProductId",
                schema: "dbo",
                table: "ProductFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTagMaps_Products_ProductId",
                schema: "dbo",
                table: "ProductTagMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTagMaps_ProductTag_TagId",
                schema: "dbo",
                table: "ProductTagMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutImageMaps_Products_ProductId",
                schema: "dbo",
                table: "ProdutImageMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRole_RoleId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "dbo",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestTable",
                schema: "dbo",
                table: "TestTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SliderImages",
                schema: "dbo",
                table: "SliderImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutImageMaps",
                schema: "dbo",
                table: "ProdutImageMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTagMaps",
                schema: "dbo",
                table: "ProductTagMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                schema: "dbo",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFeedbacks",
                schema: "dbo",
                table: "ProductFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributes",
                schema: "dbo",
                table: "ProductAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeItemMaps",
                schema: "dbo",
                table: "ProductAttributeItemMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DBUserInfo",
                schema: "dbo",
                table: "DBUserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryAttributeMapers",
                schema: "dbo",
                table: "CategoryAttributeMapers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeItems",
                schema: "dbo",
                table: "AttributeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeItemCategoryMaps",
                schema: "dbo",
                table: "AttributeItemCategoryMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationAccessLogs",
                schema: "dbo",
                table: "ApplicationAccessLogs");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "USERS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "dbo",
                newName: "PRODUCTS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "dbo",
                newName: "CATEGORIES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserRole",
                schema: "dbo",
                newName: "USER_ROLE",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TestTable",
                schema: "dbo",
                newName: "TEST_TABLE",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SliderImages",
                schema: "dbo",
                newName: "SLIDER_IMAGES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProdutImageMaps",
                schema: "dbo",
                newName: "PRODUT_IMAGE_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductTagMaps",
                schema: "dbo",
                newName: "PRODUCT_TAG_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                schema: "dbo",
                newName: "PRODUCT_TAG",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductFeedbacks",
                schema: "dbo",
                newName: "PRODUCT_FEEDBACKS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductAttributes",
                schema: "dbo",
                newName: "PRODUCT_ATTRIBUTES",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductAttributeItemMaps",
                schema: "dbo",
                newName: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DBUserInfo",
                schema: "dbo",
                newName: "D_B_USER_INFO",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CategoryAttributeMapers",
                schema: "dbo",
                newName: "CATEGORY_ATTRIBUTE_MAPERS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AttributeItems",
                schema: "dbo",
                newName: "ATTRIBUTE_ITEMS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AttributeItemCategoryMaps",
                schema: "dbo",
                newName: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ApplicationAccessLogs",
                schema: "dbo",
                newName: "APPLICATION_ACCESS_LOGS",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "dbo",
                table: "USERS",
                newName: "PASSWORD");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                schema: "dbo",
                table: "USERS",
                newName: "MOBILE");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "dbo",
                table: "USERS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "USERS",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "USERS",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "dbo",
                table: "USERS",
                newName: "ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "dbo",
                table: "USERS",
                newName: "LAST_NAME");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "USERS",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "dbo",
                table: "USERS",
                newName: "FULL_NAME");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "dbo",
                table: "USERS",
                newName: "FIRST_NAME");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                schema: "dbo",
                table: "USERS",
                newName: "DATE_OF_BIRTH");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "USERS",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "USERS",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "USERS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                schema: "dbo",
                table: "USERS",
                newName: "IX_USERS_ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "Slug",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "SLUG");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "StokeAmount",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "STOKE_AMOUNT");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "SHORT_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "SKU",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "S_K_U");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "OLD_PRICE");

            migrationBuilder.RenameColumn(
                name: "NewPrice",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "NEW_PRICE");

            migrationBuilder.RenameColumn(
                name: "MinimumStokeLimit",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "MINIMUM_STOKE_LIMIT");

            migrationBuilder.RenameColumn(
                name: "IsTrackStoke",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_TRACK_STOKE");

            migrationBuilder.RenameColumn(
                name: "IsShowOnHome",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_SHOW_ON_HOME");

            migrationBuilder.RenameColumn(
                name: "IsReturnAble",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_RETURN_ABLE");

            migrationBuilder.RenameColumn(
                name: "IsOpenReview",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_OPEN_REVIEW");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_NEW");

            migrationBuilder.RenameColumn(
                name: "IsMultipleWareHouse",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_MULTIPLE_WARE_HOUSE");

            migrationBuilder.RenameColumn(
                name: "IsEnableShop",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_ENABLE_SHOP");

            migrationBuilder.RenameColumn(
                name: "IsDraft",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_DRAFT");

            migrationBuilder.RenameColumn(
                name: "IsDisplayAvaiable",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_DISPLAY_AVAIABLE");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "GTIN",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "G_T_I_N");

            migrationBuilder.RenameColumn(
                name: "FullDescription",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "FULL_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "FeatureImage",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "FEATURE_IMAGE");

            migrationBuilder.RenameColumn(
                name: "DiscountInPercent",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "DISCOUNT_IN_PERCENT");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "DISCOUNT_AMOUNT");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "CATEGORY_ID");

            migrationBuilder.RenameColumn(
                name: "AdminComment",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "ADMIN_COMMENT");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                schema: "dbo",
                table: "PRODUCTS",
                newName: "IX_PRODUCTS_CATEGORY_ID");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "PARENT_ID");

            migrationBuilder.RenameColumn(
                name: "IsShowOnHome",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "IS_SHOW_ON_HOME");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "IMAGE_URL");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                schema: "dbo",
                table: "CATEGORIES",
                newName: "IX_CATEGORIES_PARENT_ID");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "USER_ROLE",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "USER_ROLE",
                newName: "I_D");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "TEST_TABLE",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "TEST_TABLE",
                newName: "I_D");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "ImageViewer",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "IMAGE_VIEWER");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "IMAGE_URL");

            migrationBuilder.RenameColumn(
                name: "ImageTitle",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "IMAGE_TITLE");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "IMAGE_NAME");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                newName: "I_D");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                newName: "IMAGE_U_R_L");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutImageMaps_ProductId",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                newName: "IX_PRODUT_IMAGE_MAPS_PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "TagId",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                newName: "TAG_ID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTagMaps_TagId",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                newName: "IX_PRODUCT_TAG_MAPS_TAG_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTagMaps_ProductId",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                newName: "IX_PRODUCT_TAG_MAPS_PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCT_TAG",
                newName: "I_D");

            migrationBuilder.RenameColumn(
                name: "Rating",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "RATING");

            migrationBuilder.RenameColumn(
                name: "Message",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "MESSAGE");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "USER_I_D");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "ReplyID",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "REPLY_I_D");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeedbacks_ProductId",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                newName: "IX_PRODUCT_FEEDBACKS_PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                newName: "I_D");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "ItemAttributeId",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "ITEM_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeItemMaps_ProductId",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeItemMaps_ItemAttributeId",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                newName: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_ITEM_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "USER_ID");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "TemporaryState",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_STATE");

            migrationBuilder.RenameColumn(
                name: "TemporaryPostalCode",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_POSTAL_CODE");

            migrationBuilder.RenameColumn(
                name: "TemporaryCountry",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_COUNTRY");

            migrationBuilder.RenameColumn(
                name: "TemporaryCity",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_CITY");

            migrationBuilder.RenameColumn(
                name: "TemporaryApartment",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_APARTMENT");

            migrationBuilder.RenameColumn(
                name: "TemporaryAddress",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "TEMPORARY_ADDRESS");

            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PROFILE_IMAGE");

            migrationBuilder.RenameColumn(
                name: "PermanentState",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_STATE");

            migrationBuilder.RenameColumn(
                name: "PermanentPostalCode",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_POSTAL_CODE");

            migrationBuilder.RenameColumn(
                name: "PermanentCountry",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_COUNTRY");

            migrationBuilder.RenameColumn(
                name: "PermanentCity",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_CITY");

            migrationBuilder.RenameColumn(
                name: "PermanentApartment",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_APARTMENT");

            migrationBuilder.RenameColumn(
                name: "PermanentAddress",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "PERMANENT_ADDRESS");

            migrationBuilder.RenameColumn(
                name: "NIDImageFront",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "N_I_D_IMAGE_FRONT");

            migrationBuilder.RenameColumn(
                name: "NIDImageBack",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "N_I_D_IMAGE_BACK");

            migrationBuilder.RenameColumn(
                name: "NID",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "N_I_D");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_DBUserInfo_UserId",
                schema: "dbo",
                table: "D_B_USER_INFO",
                newName: "IX_D_B_USER_INFO_USER_ID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "CATEGORY_ID");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryAttributeMapers_CategoryId",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "IX_CATEGORY_ATTRIBUTE_MAPERS_CATEGORY_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryAttributeMapers_AttributeId",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                newName: "IX_CATEGORY_ATTRIBUTE_MAPERS_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "UPDATED_DATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "UPDATED_BY");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "PRODUCT_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "IS_DELETE");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "CREATED_DATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "CREATED_BY");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeItems_ProductAttributeId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                newName: "IX_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "ItemAttributeId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "ITEM_ATTRIBUTE_ID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "CATEGORY_ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "I_D");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeItemCategoryMaps_ItemAttributeId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_ITEM_ATTRIBUTE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeItemCategoryMaps_CategoryId",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                newName: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORY_ID");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "USER_NAME");

            migrationBuilder.RenameColumn(
                name: "UserIpHostName",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "USER_IP_HOST_NAME");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "USER_ID");

            migrationBuilder.RenameColumn(
                name: "ModuleName",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "MODULE_NAME");

            migrationBuilder.RenameColumn(
                name: "ActivityName",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "ACTIVITY_NAME");

            migrationBuilder.RenameColumn(
                name: "ActionType",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "ACTION_TYPE");

            migrationBuilder.RenameColumn(
                name: "AccessDateTime",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "ACCESS_DATE_TIME");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                newName: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS",
                schema: "dbo",
                table: "USERS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCTS",
                schema: "dbo",
                table: "PRODUCTS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CATEGORIES",
                schema: "dbo",
                table: "CATEGORIES",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_ROLE",
                schema: "dbo",
                table: "USER_ROLE",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEST_TABLE",
                schema: "dbo",
                table: "TEST_TABLE",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SLIDER_IMAGES",
                schema: "dbo",
                table: "SLIDER_IMAGES",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUT_IMAGE_MAPS",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_TAG_MAPS",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_TAG",
                schema: "dbo",
                table: "PRODUCT_TAG",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_FEEDBACKS",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_ATTRIBUTES",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTES",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_ATTRIBUTE_ITEM_MAPS",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_D_B_USER_INFO",
                schema: "dbo",
                table: "D_B_USER_INFO",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CATEGORY_ATTRIBUTE_MAPERS",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ATTRIBUTE_ITEMS",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ATTRIBUTE_ITEM_CATEGORY_MAPS",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                column: "I_D");

            migrationBuilder.AddPrimaryKey(
                name: "PK_APPLICATION_ACCESS_LOGS",
                schema: "dbo",
                table: "APPLICATION_ACCESS_LOGS",
                column: "I_D");

            migrationBuilder.AddForeignKey(
                name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                column: "ITEM_ATTRIBUTE_ID",
                principalSchema: "dbo",
                principalTable: "ATTRIBUTE_ITEMS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                column: "CATEGORY_ID",
                principalSchema: "dbo",
                principalTable: "CATEGORIES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTES_PRODUCT_ATTRIBUTE_ID",
                schema: "dbo",
                table: "ATTRIBUTE_ITEMS",
                column: "PRODUCT_ATTRIBUTE_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCT_ATTRIBUTES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORIES_CATEGORIES_PARENT_ID",
                schema: "dbo",
                table: "CATEGORIES",
                column: "PARENT_ID",
                principalSchema: "dbo",
                principalTable: "CATEGORIES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORY_ATTRIBUTE_MAPERS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                column: "CATEGORY_ID",
                principalSchema: "dbo",
                principalTable: "CATEGORIES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORY_ATTRIBUTE_MAPERS_PRODUCT_ATTRIBUTES_ATTRIBUTE_ID",
                schema: "dbo",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                column: "ATTRIBUTE_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCT_ATTRIBUTES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_D_B_USER_INFO_USERS_USER_ID",
                schema: "dbo",
                table: "D_B_USER_INFO",
                column: "USER_ID",
                principalSchema: "dbo",
                principalTable: "USERS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                column: "ITEM_ATTRIBUTE_ID",
                principalSchema: "dbo",
                principalTable: "ATTRIBUTE_ITEMS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                column: "PRODUCT_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCTS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_FEEDBACKS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_FEEDBACKS",
                column: "PRODUCT_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCTS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_TAG_MAPS_PRODUCT_TAG_TAG_ID",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                column: "TAG_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCT_TAG",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_TAG_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUCT_TAG_MAPS",
                column: "PRODUCT_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCTS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                schema: "dbo",
                table: "PRODUCTS",
                column: "CATEGORY_ID",
                principalSchema: "dbo",
                principalTable: "CATEGORIES",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUT_IMAGE_MAPS_PRODUCTS_PRODUCT_ID",
                schema: "dbo",
                table: "PRODUT_IMAGE_MAPS",
                column: "PRODUCT_ID",
                principalSchema: "dbo",
                principalTable: "PRODUCTS",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_USER_ROLE_ROLE_ID",
                schema: "dbo",
                table: "USERS",
                column: "ROLE_ID",
                principalSchema: "dbo",
                principalTable: "USER_ROLE",
                principalColumn: "I_D",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
