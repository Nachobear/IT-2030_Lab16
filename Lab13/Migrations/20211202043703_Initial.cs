using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab13.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfHire = table.Column<DateTime>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quarter = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_Sales_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfHire", "FirstName", "LastName", "ManagerId" },
                values: new object[] { 1, new DateTime(1956, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ada", "Lovelace", 0 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfHire", "FirstName", "LastName", "ManagerId" },
                values: new object[] { 2, new DateTime(1990, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jimbo", "Magilicutty", 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfHire", "FirstName", "LastName", "ManagerId" },
                values: new object[] { 3, new DateTime(1984, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mike", "Wazowski", 1 });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SalesId", "Amount", "EmployeeId", "Quarter", "Year" },
                values: new object[,]
                {
                    { 5, 12344.0, 1, 3, 2020 },
                    { 1, 23456.0, 2, 4, 2019 },
                    { 2, 34567.0, 2, 1, 2020 },
                    { 3, 43221.0, 3, 2, 2019 },
                    { 4, 53444.0, 3, 2, 2020 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
