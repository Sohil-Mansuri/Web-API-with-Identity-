using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addManytomany_relationShip_For_Portfolios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e23bc7a-7ddc-4988-a475-dcdebef99faf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "442f0de8-306a-45a8-9719-86bb3f0543ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59339a49-afce-4ce4-b962-636cd10bbd6a");

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => new { x.AppUserID, x.StockID });
                    table.ForeignKey(
                        name: "FK_Portfolios_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portfolios_Stock_StockID",
                        column: x => x.StockID,
                        principalTable: "Stock",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91a6efcf-b46c-4dee-b7e1-75047e223285", null, "User", "USER" },
                    { "cd70e084-b70d-4d3f-89a4-3beed6462fd9", null, "Admin", "ADMIN" },
                    { "dec53084-31b3-42c1-ab23-a0a69972e712", null, "Staff", "STAFF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_StockID",
                table: "Portfolios",
                column: "StockID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91a6efcf-b46c-4dee-b7e1-75047e223285");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd70e084-b70d-4d3f-89a4-3beed6462fd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dec53084-31b3-42c1-ab23-a0a69972e712");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e23bc7a-7ddc-4988-a475-dcdebef99faf", null, "Admin", "ADMIN" },
                    { "442f0de8-306a-45a8-9719-86bb3f0543ed", null, "User", "USER" },
                    { "59339a49-afce-4ce4-b962-636cd10bbd6a", null, "Staff", "STAFF" }
                });
        }
    }
}
