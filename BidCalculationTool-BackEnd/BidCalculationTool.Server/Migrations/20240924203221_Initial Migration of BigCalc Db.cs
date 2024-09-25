using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BidCalculationTool.Server.Migrations
{
    public partial class InitialMigrationofBigCalcDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociationFeeRules",
                columns: table => new
                {
                    AssociationFeeRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationFeeRules", x => x.AssociationFeeRuleId);
                });

            migrationBuilder.CreateTable(
                name: "StorageFees",
                columns: table => new
                {
                    StorageFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFees", x => x.StorageFeeId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerFeeRules",
                columns: table => new
                {
                    BuyerFeeRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerFeeRules", x => x.BuyerFeeRuleId);
                    table.ForeignKey(
                        name: "FK_BuyerFeeRules_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerFeeRules",
                columns: table => new
                {
                    SellerFeeRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerFeeRules", x => x.SellerFeeRuleId);
                    table.ForeignKey(
                        name: "FK_SellerFeeRules_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssociationFeeRules",
                columns: new[] { "AssociationFeeRuleId", "FeeAmount", "MaxPrice", "MinPrice" },
                values: new object[,]
                {
                    { 1, 5m, 500m, 1m },
                    { 2, 10m, 1000m, 501m },
                    { 3, 15m, 3000m, 1001m },
                    { 4, 20m, null, 3001m }
                });

            migrationBuilder.InsertData(
                table: "StorageFees",
                columns: new[] { "StorageFeeId", "FeeAmount" },
                values: new object[] { 1, 100m });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Common" },
                    { 2, "Luxury" }
                });

            migrationBuilder.InsertData(
                table: "BuyerFeeRules",
                columns: new[] { "BuyerFeeRuleId", "MaxFee", "MinFee", "Percentage", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, 50m, 10m, 10m, 1 },
                    { 2, 200m, 25m, 10m, 2 }
                });

            migrationBuilder.InsertData(
                table: "SellerFeeRules",
                columns: new[] { "SellerFeeRuleId", "Percentage", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, 2m, 1 },
                    { 2, 4m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerFeeRules_VehicleTypeId",
                table: "BuyerFeeRules",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerFeeRules_VehicleTypeId",
                table: "SellerFeeRules",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationFeeRules");

            migrationBuilder.DropTable(
                name: "BuyerFeeRules");

            migrationBuilder.DropTable(
                name: "SellerFeeRules");

            migrationBuilder.DropTable(
                name: "StorageFees");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
