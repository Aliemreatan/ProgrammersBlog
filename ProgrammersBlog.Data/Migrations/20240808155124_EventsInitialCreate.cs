using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EventsInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 98,
                column: "ConcurrencyStamp",
                value: "81b2405f-47cf-4851-8a85-58709ceffb61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99,
                column: "ConcurrencyStamp",
                value: "35b55b0d-b4ae-4b69-afea-b08746738c67");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "839038be-b079-4695-86ec-8cf842560708", "AQAAAAIAAYagAAAAENTe1/bYJ19Nht1c8nvlhZC4Ld1SUZnhl/Xs9maSu1qBqjpa02c8Neh+1gQu0wFaUg==", "20272608-36c1-4c27-b2ef-45fe9f654aa7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16b56ae0-f494-47ca-b92a-52bd02cf8989", "AQAAAAIAAYagAAAAENejYXAQ/pvnwI4HZrwmxWPSC8J9pkXcmRclV8Ea7AsnH3Jk2eFVNgrESJAnquzHAQ==", "353f4a93-c43d-4006-a0ea-caa769b139c1" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8867), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8869) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8873), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8873) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8876), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(8877) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9987), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9988) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9991), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9992) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9994), new DateTime(2024, 8, 8, 18, 51, 24, 387, DateTimeKind.Local).AddTicks(9995) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 98,
                column: "ConcurrencyStamp",
                value: "a06caa5c-08fc-49f6-b2ad-147ec23b0765");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99,
                column: "ConcurrencyStamp",
                value: "78efb8ce-021e-4ef1-8d34-76b8c9c72f90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "163d26ac-b8a9-4f69-abef-d124dfffa75e", "AQAAAAIAAYagAAAAEPJ5aJ17DZNsBoATjY6yhD72++FK7KWwokTco/bgs1MoOTQADrJuFbhOZ6/y2gw6DQ==", "9b60eaf2-51ac-44be-9db3-2686384a2d94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a337e364-8701-4f94-97fc-e7f528160a95", "AQAAAAIAAYagAAAAEEGO9aC3LaCOaK1IYzxjHRoUYzktP+uyzJQiZeMCs5T82BEs5jeADzgobQ5NCkR/gg==", "64495ffb-e541-4b26-b2ab-02df0b96d28e" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5836), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5837) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5839), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5842), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(5843) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6845), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6846) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6848), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6849) });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6851), new DateTime(2024, 8, 7, 11, 31, 19, 199, DateTimeKind.Local).AddTicks(6852) });
        }
    }
}
