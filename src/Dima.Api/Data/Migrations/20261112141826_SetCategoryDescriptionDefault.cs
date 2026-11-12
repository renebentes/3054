using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetCategoryDescriptionDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """UPDATE "Category" SET "Description" = '' WHERE "Description" IS NULL;""");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "text",
                maxLength: 255,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 255,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "text",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
