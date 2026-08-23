using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35d90898-6a45-453b-a5c3-87de7f29d262",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAED23GXUDP1Td6LixCxlcIdF87o/S6I80CZ8rLFeblUVCNp+1wVIwTEIITXnj1AlxeQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35d90898-6a45-453b-a5c3-87de7f29d262",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECyotUh84SE3/QGEZVc8iJtocX8ltShTD5iUciubTfFX8cjVz8YUBeWvNkKZ109Uyg==");
        }
    }
}
