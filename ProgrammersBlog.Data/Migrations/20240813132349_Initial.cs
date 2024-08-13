using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(1609), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(1604), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(1609) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 98,
                column: "ConcurrencyStamp",
                value: "2c3e6062-e802-47bf-b0b3-bf5b07fab765");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99,
                column: "ConcurrencyStamp",
                value: "fa9dc03f-1127-45a0-93b4-4ce1f0b856f6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff5fe12d-22d1-4678-af89-1609a07b8d48", "AQAAAAIAAYagAAAAEBCTSYOecMSrbpT2j9FUn3p4//Py69XUa/YKRUmPD0YnsX1Kz4DiKJMxidtj7BsV6g==", "1cb8c007-f53e-4af4-9fa2-3ba0e50dd149" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bb01180-6fdc-4406-bcf9-aa2fc6acd29c", "AQAAAAIAAYagAAAAEO7TAhk7w+pTplt+9gi4t5CjOLcz+D2jeQHJzm1hQFqpsp/QKIpUHlR6PIq4v1Uo/Q==", "b389702b-6d7f-4548-99ff-e640cf22112e" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3120), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3121) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3124), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3125) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3127), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(3128) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4099), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4099) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4102), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4103) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4105), new DateTime(2024, 8, 13, 16, 23, 48, 597, DateTimeKind.Local).AddTicks(4106) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Date", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(5294), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(5290), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(5295) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 98,
                column: "ConcurrencyStamp",
                value: "c1d55d3f-57ad-4ac8-974c-a24d8ba842da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99,
                column: "ConcurrencyStamp",
                value: "8056d64b-5a82-40fa-9efe-82b616c401a3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec8448c5-4755-42e5-8bfa-b7092df9de47", "AQAAAAIAAYagAAAAEG8BCPRznrkuxlsVv3U7exdIiQrPw1ze5JlG1o0QPKndpXNF0a1/zLWWTPWuKe7EFA==", "e3edd7aa-668e-4dbc-8351-91b95a4a5656" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba523a2b-3764-413f-9e6a-e13b40ef300a", "AQAAAAIAAYagAAAAEJoQzbeK5Wv6ZgTdiiWuaPKutlN9ArrYkg0nlLswtoqZRUi5e0xKebfCVxAilvPmYQ==", "106700b4-bb0a-4560-b3c1-3dad8c36f052" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6383), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6386), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6387) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6395), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(6396) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7238), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7239) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7241), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7242) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7244), new DateTime(2024, 8, 13, 16, 12, 13, 40, DateTimeKind.Local).AddTicks(7244) });
        }
    }
}
