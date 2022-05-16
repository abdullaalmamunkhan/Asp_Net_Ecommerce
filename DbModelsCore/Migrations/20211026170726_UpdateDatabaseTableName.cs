using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbModelsCore.Migrations
{
    public partial class UpdateDatabaseTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APPLICATION_ACCESS_LOGS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<long>(type: "bigint", nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MODULE_NAME = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ACTIVITY_NAME = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    USER_IP_HOST_NAME = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ACTION_TYPE = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ACCESS_DATE_TIME = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION_ACCESS_LOGS", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PARENT_ID = table.Column<long>(type: "bigint", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_SHOW_ON_HOME = table.Column<bool>(type: "bit", nullable: false),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_CATEGORIES_CATEGORIES_PARENT_ID",
                        column: x => x.PARENT_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_ATTRIBUTES",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_ATTRIBUTES", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_TAG",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_TAG", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "SLIDER_IMAGES",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IMAGE_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IMAGE_TITLE = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IMAGE_VIEWER = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLIDER_IMAGES", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "TEST_TABLE",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_TABLE", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    I_D = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => x.I_D);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLUG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHORT_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FULL_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FEATURE_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_K_U = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    G_T_I_N = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADMIN_COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_SHOW_ON_HOME = table.Column<bool>(type: "bit", nullable: false),
                    IS_OPEN_REVIEW = table.Column<bool>(type: "bit", nullable: false),
                    IS_DRAFT = table.Column<bool>(type: "bit", nullable: false),
                    OLD_PRICE = table.Column<double>(type: "float", nullable: false),
                    NEW_PRICE = table.Column<double>(type: "float", nullable: false),
                    DISCOUNT_IN_PERCENT = table.Column<double>(type: "float", nullable: false),
                    DISCOUNT_AMOUNT = table.Column<double>(type: "float", nullable: false),
                    IS_ENABLE_SHOP = table.Column<bool>(type: "bit", nullable: false),
                    IS_TRACK_STOKE = table.Column<bool>(type: "bit", nullable: false),
                    STOKE_AMOUNT = table.Column<double>(type: "float", nullable: false),
                    MINIMUM_STOKE_LIMIT = table.Column<double>(type: "float", nullable: false),
                    IS_MULTIPLE_WARE_HOUSE = table.Column<bool>(type: "bit", nullable: false),
                    IS_DISPLAY_AVAIABLE = table.Column<bool>(type: "bit", nullable: false),
                    IS_RETURN_ABLE = table.Column<bool>(type: "bit", nullable: false),
                    IS_NEW = table.Column<bool>(type: "bit", nullable: false),
                    CATEGORY_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATTRIBUTE_ITEMS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PRODUCT_ATTRIBUTE_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTRIBUTE_ITEMS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTES_PRODUCT_ATTRIBUTE_ID",
                        column: x => x.PRODUCT_ATTRIBUTE_ID,
                        principalTable: "PRODUCT_ATTRIBUTES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY_ATTRIBUTE_MAPERS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_ID = table.Column<long>(type: "bigint", nullable: false),
                    ATTRIBUTE_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY_ATTRIBUTE_MAPERS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_CATEGORY_ATTRIBUTE_MAPERS_CATEGORIES_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CATEGORY_ATTRIBUTE_MAPERS_PRODUCT_ATTRIBUTES_ATTRIBUTE_ID",
                        column: x => x.ATTRIBUTE_ID,
                        principalTable: "PRODUCT_ATTRIBUTES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOBILE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROLE_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_USERS_USER_ROLE_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "USER_ROLE",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_FEEDBACKS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_I_D = table.Column<int>(type: "int", nullable: false),
                    RATING = table.Column<double>(type: "float", nullable: false),
                    MESSAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REPLY_I_D = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_FEEDBACKS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_PRODUCT_FEEDBACKS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_TAG_MAPS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: false),
                    TAG_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_TAG_MAPS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TAG_MAPS_PRODUCT_TAG_TAG_ID",
                        column: x => x.TAG_ID,
                        principalTable: "PRODUCT_TAG",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TAG_MAPS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUT_IMAGE_MAPS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: false),
                    IMAGE_U_R_L = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUT_IMAGE_MAPS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_PRODUT_IMAGE_MAPS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ITEM_ATTRIBUTE_ID = table.Column<long>(type: "bigint", nullable: false),
                    CATEGORY_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTRIBUTE_ITEM_CATEGORY_MAPS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                        column: x => x.ITEM_ATTRIBUTE_ID,
                        principalTable: "ATTRIBUTE_ITEMS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORIES_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: false),
                    ATTRIBUTE_ID = table.Column<long>(type: "bigint", nullable: false),
                    ITEM_ATTRIBUTE_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_ATTRIBUTE_ITEM_MAPS", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_ATTRIBUTE_ITEMS_ITEM_ATTRIBUTE_ID",
                        column: x => x.ITEM_ATTRIBUTE_ID,
                        principalTable: "ATTRIBUTE_ITEMS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "D_B_USER_INFO",
                columns: table => new
                {
                    I_D = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PERMANENT_ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERMANENT_APARTMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERMANENT_CITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERMANENT_STATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERMANENT_COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERMANENT_POSTAL_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_APARTMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_CITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_STATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEMPORARY_POSTAL_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_I_D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_I_D_IMAGE_FRONT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_I_D_IMAGE_BACK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROFILE_IMAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<long>(type: "bigint", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_B_USER_INFO", x => x.I_D);
                    table.ForeignKey(
                        name: "FK_D_B_USER_INFO_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "I_D",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "USER_ROLE",
                columns: new[] { "I_D", "NAME" },
                values: new object[,]
                {
                    { 1, "Super Admin" },
                    { 2, "Admin" },
                    { 3, "Vendor" },
                    { 4, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_CATEGORY_ID",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATTRIBUTE_ITEM_CATEGORY_MAPS_ITEM_ATTRIBUTE_ID",
                table: "ATTRIBUTE_ITEM_CATEGORY_MAPS",
                column: "ITEM_ATTRIBUTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATTRIBUTE_ITEMS_PRODUCT_ATTRIBUTE_ID",
                table: "ATTRIBUTE_ITEMS",
                column: "PRODUCT_ATTRIBUTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_PARENT_ID",
                table: "CATEGORIES",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_ATTRIBUTE_MAPERS_ATTRIBUTE_ID",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                column: "ATTRIBUTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_ATTRIBUTE_MAPERS_CATEGORY_ID",
                table: "CATEGORY_ATTRIBUTE_MAPERS",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_D_B_USER_INFO_USER_ID",
                table: "D_B_USER_INFO",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_ITEM_ATTRIBUTE_ID",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                column: "ITEM_ATTRIBUTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_ATTRIBUTE_ITEM_MAPS_PRODUCT_ID",
                table: "PRODUCT_ATTRIBUTE_ITEM_MAPS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_FEEDBACKS_PRODUCT_ID",
                table: "PRODUCT_FEEDBACKS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_TAG_MAPS_PRODUCT_ID",
                table: "PRODUCT_TAG_MAPS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_TAG_MAPS_TAG_ID",
                table: "PRODUCT_TAG_MAPS",
                column: "TAG_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUT_IMAGE_MAPS_PRODUCT_ID",
                table: "PRODUT_IMAGE_MAPS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_ROLE_ID",
                table: "USERS",
                column: "ROLE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION_ACCESS_LOGS");

            migrationBuilder.DropTable(
                name: "ATTRIBUTE_ITEM_CATEGORY_MAPS");

            migrationBuilder.DropTable(
                name: "CATEGORY_ATTRIBUTE_MAPERS");

            migrationBuilder.DropTable(
                name: "D_B_USER_INFO");

            migrationBuilder.DropTable(
                name: "PRODUCT_ATTRIBUTE_ITEM_MAPS");

            migrationBuilder.DropTable(
                name: "PRODUCT_FEEDBACKS");

            migrationBuilder.DropTable(
                name: "PRODUCT_TAG_MAPS");

            migrationBuilder.DropTable(
                name: "PRODUT_IMAGE_MAPS");

            migrationBuilder.DropTable(
                name: "SLIDER_IMAGES");

            migrationBuilder.DropTable(
                name: "TEST_TABLE");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "ATTRIBUTE_ITEMS");

            migrationBuilder.DropTable(
                name: "PRODUCT_TAG");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "PRODUCT_ATTRIBUTES");

            migrationBuilder.DropTable(
                name: "CATEGORIES");
        }
    }
}
