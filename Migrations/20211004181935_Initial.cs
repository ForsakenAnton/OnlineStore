using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "AspNetRoles",
        //        columns: table => new
        //        {
        //            Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetRoles", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUsers",
        //        columns: table => new
        //        {
        //            Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        //            EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
        //            PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
        //            TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
        //            LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
        //            LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
        //            AccessFailedCount = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUsers", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Category",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
        //            Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
        //            CategoryLevel = table.Column<int>(type: "int", nullable: false),
        //            Order = table.Column<int>(type: "int", nullable: false),
        //            IsTop = table.Column<bool>(type: "bit", nullable: false),
        //            ParentCategoryId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Category", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Category_Category_ParentCategoryId",
        //                column: x => x.ParentCategoryId,
        //                principalTable: "Category",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Manufacturer",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Manufacturer", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetRoleClaims",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
        //                column: x => x.RoleId,
        //                principalTable: "AspNetRoles",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUserClaims",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUserLogins",
        //        columns: table => new
        //        {
        //            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
        //            table.ForeignKey(
        //                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUserRoles",
        //        columns: table => new
        //        {
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
        //            table.ForeignKey(
        //                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
        //                column: x => x.RoleId,
        //                principalTable: "AspNetRoles",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "AspNetUserTokens",
        //        columns: table => new
        //        {
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
        //            Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
        //            table.ForeignKey(
        //                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Order",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            OrderNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        //            DateOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            OrderState = table.Column<int>(type: "int", nullable: false),
        //            TotalPrice = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Order", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Order_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Product",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Feature = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
        //            IsTop = table.Column<bool>(type: "bit", nullable: false),
        //            Discount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
        //            Count = table.Column<int>(type: "int", nullable: false),
        //            CommodityCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
        //            Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
        //            ManufacturerId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Product", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Product_Manufacturer_ManufacturerId",
        //                column: x => x.ManufacturerId,
        //                principalTable: "Manufacturer",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Delivery",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            City = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            OrderId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Delivery", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Delivery_Order_OrderId",
        //                column: x => x.OrderId,
        //                principalTable: "Order",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "CategoryProducts",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            CategoryId = table.Column<int>(type: "int", nullable: false),
        //            ProductId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_CategoryProducts", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_CategoryProducts_Category_CategoryId",
        //                column: x => x.CategoryId,
        //                principalTable: "Category",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_CategoryProducts_Product_ProductId",
        //                column: x => x.ProductId,
        //                principalTable: "Product",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Comment",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Virtues = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Shortcomings = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Rating = table.Column<int>(type: "int", nullable: false),
        //            IsModerated = table.Column<bool>(type: "bit", nullable: false),
        //            Date = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
        //            ProductId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Comment", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Comment_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "FK_Comment_Product_ProductId",
        //                column: x => x.ProductId,
        //                principalTable: "Product",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "FavoriteProduct",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            ProductId = table.Column<int>(type: "int", nullable: false),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_FavoriteProduct", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_FavoriteProduct_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "FK_FavoriteProduct_Product_ProductId",
        //                column: x => x.ProductId,
        //                principalTable: "Product",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "OrderProducts",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Quantity = table.Column<int>(type: "int", nullable: false),
        //            PriceOfOneProduct = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
        //            OrderId = table.Column<int>(type: "int", nullable: false),
        //            ProductId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_OrderProducts", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_OrderProducts_Order_OrderId",
        //                column: x => x.OrderId,
        //                principalTable: "Order",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_OrderProducts_Product_ProductId",
        //                column: x => x.ProductId,
        //                principalTable: "Product",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Answer",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Date = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            IsModerated = table.Column<bool>(type: "bit", nullable: false),
        //            CommentId = table.Column<int>(type: "int", nullable: false),
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Answer", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Answer_AspNetUsers_UserId1",
        //                column: x => x.UserId1,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "FK_Answer_Comment_CommentId",
        //                column: x => x.CommentId,
        //                principalTable: "Comment",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Like",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            IsLike = table.Column<bool>(type: "bit", nullable: false),
        //            IsUnlike = table.Column<bool>(type: "bit", nullable: false),
        //            CommentId = table.Column<int>(type: "int", nullable: false),
        //            UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
        //            Liking = table.Column<bool>(type: "bit", nullable: false),
        //            Unliking = table.Column<bool>(type: "bit", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Like", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Like_AspNetUsers_UserId",
        //                column: x => x.UserId,
        //                principalTable: "AspNetUsers",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "FK_Like_Comment_CommentId",
        //                column: x => x.CommentId,
        //                principalTable: "Comment",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Answer_CommentId",
        //        table: "Answer",
        //        column: "CommentId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Answer_UserId1",
        //        table: "Answer",
        //        column: "UserId1");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_AspNetRoleClaims_RoleId",
        //        table: "AspNetRoleClaims",
        //        column: "RoleId");

        //    migrationBuilder.CreateIndex(
        //        name: "RoleNameIndex",
        //        table: "AspNetRoles",
        //        column: "NormalizedName",
        //        unique: true,
        //        filter: "[NormalizedName] IS NOT NULL");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_AspNetUserClaims_UserId",
        //        table: "AspNetUserClaims",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_AspNetUserLogins_UserId",
        //        table: "AspNetUserLogins",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_AspNetUserRoles_RoleId",
        //        table: "AspNetUserRoles",
        //        column: "RoleId");

        //    migrationBuilder.CreateIndex(
        //        name: "EmailIndex",
        //        table: "AspNetUsers",
        //        column: "NormalizedEmail");

        //    migrationBuilder.CreateIndex(
        //        name: "UserNameIndex",
        //        table: "AspNetUsers",
        //        column: "NormalizedUserName",
        //        unique: true,
        //        filter: "[NormalizedUserName] IS NOT NULL");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Category_ParentCategoryId",
        //        table: "Category",
        //        column: "ParentCategoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CategoryProducts_CategoryId",
        //        table: "CategoryProducts",
        //        column: "CategoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CategoryProducts_ProductId",
        //        table: "CategoryProducts",
        //        column: "ProductId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Comment_ProductId",
        //        table: "Comment",
        //        column: "ProductId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Comment_UserId",
        //        table: "Comment",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Delivery_OrderId",
        //        table: "Delivery",
        //        column: "OrderId",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_FavoriteProduct_ProductId",
        //        table: "FavoriteProduct",
        //        column: "ProductId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_FavoriteProduct_UserId",
        //        table: "FavoriteProduct",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Like_CommentId",
        //        table: "Like",
        //        column: "CommentId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Like_UserId",
        //        table: "Like",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Order_UserId",
        //        table: "Order",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_OrderProducts_OrderId",
        //        table: "OrderProducts",
        //        column: "OrderId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_OrderProducts_ProductId",
        //        table: "OrderProducts",
        //        column: "ProductId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Product_CommodityCode",
        //        table: "Product",
        //        column: "CommodityCode",
        //        unique: true,
        //        filter: "[CommodityCode] IS NOT NULL");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Product_ManufacturerId",
        //        table: "Product",
        //        column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoryProducts");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "FavoriteProduct");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Manufacturer");
        }
    }
}
