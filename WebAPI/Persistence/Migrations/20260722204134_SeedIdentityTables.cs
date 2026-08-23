using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "026f451d-cd2e-4f1a-b7a5-38717d7aab92", "0fc72129-70fc-4f4c-8990-a8bf2610d39d", true, false, "Member", "MEMBER" },
                    { "6c164713-9761-4af3-aeab-0260c5cdf483", "52629166-8ba2-479f-9e97-b2785e5b005d", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35d90898-6a45-453b-a5c3-87de7f29d262", 0, "d838b2b7-b233-477f-ba09-7932fed689eb", "admin@WebAPI.com", true, "WebAPI", "Admin", false, null, "ADMIN@WEBAPI.COM", "ADMIN@WEBAPI.COM", "AQAAAAIAAYagAAAAED23GXUDP1Td6LixCxlcIdF87o/S6I80CZ8rLFeblUVCNp+1wVIwTEIITXnj1AlxeQ==", null, false, "900EA21DABD34E7B9A8651FE953227E8", false, "admin@WebAPI.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "polls: read", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 2, "Permissions", "polls: add", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 3, "Permissions", "polls: update", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 4, "Permissions", "polls: delete", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 5, "Permissions", "questions: read", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 6, "Permissions", "questions: add", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 7, "Permissions", "questions: update", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 8, "Permissions", "users: read", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 9, "Permissions", "users: add", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 10, "Permissions", "users: update", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 11, "Permissions", "roles: read", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 12, "Permissions", "roles: add", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 13, "Permissions", "roles: update", "6c164713-9761-4af3-aeab-0260c5cdf483" },
                    { 14, "Permissions", "results: read", "6c164713-9761-4af3-aeab-0260c5cdf483" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6c164713-9761-4af3-aeab-0260c5cdf483", "35d90898-6a45-453b-a5c3-87de7f29d262" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "026f451d-cd2e-4f1a-b7a5-38717d7aab92");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6c164713-9761-4af3-aeab-0260c5cdf483", "35d90898-6a45-453b-a5c3-87de7f29d262" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c164713-9761-4af3-aeab-0260c5cdf483");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35d90898-6a45-453b-a5c3-87de7f29d262");
        }
    }
}
