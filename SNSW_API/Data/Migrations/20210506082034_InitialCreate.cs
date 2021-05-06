using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SNSW_API.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Plate_number = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Plate_number);
                });

            migrationBuilder.CreateTable(
                name: "Insurers",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    RegistrationPlate_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurers", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Insurers_Registrations_RegistrationPlate_number",
                        column: x => x.RegistrationPlate_number,
                        principalTable: "Registrations",
                        principalColumn: "Plate_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationDetails",
                columns: table => new
                {
                    Expiry_date = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<bool>(nullable: false),
                    RegistrationPlate_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationDetails", x => x.Expiry_date);
                    table.ForeignKey(
                        name: "FK_RegistrationDetails_Registrations_RegistrationPlate_number",
                        column: x => x.RegistrationPlate_number,
                        principalTable: "Registrations",
                        principalColumn: "Plate_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Type = table.Column<string>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Vin = table.Column<string>(nullable: true),
                    Tare_weight = table.Column<int>(nullable: true),
                    Gross_mass = table.Column<int>(nullable: true),
                    RegistrationPlate_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Type);
                    table.ForeignKey(
                        name: "FK_Vehicles_Registrations_RegistrationPlate_number",
                        column: x => x.RegistrationPlate_number,
                        principalTable: "Registrations",
                        principalColumn: "Plate_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurers_RegistrationPlate_number",
                table: "Insurers",
                column: "RegistrationPlate_number");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationDetails_RegistrationPlate_number",
                table: "RegistrationDetails",
                column: "RegistrationPlate_number");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RegistrationPlate_number",
                table: "Vehicles",
                column: "RegistrationPlate_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurers");

            migrationBuilder.DropTable(
                name: "RegistrationDetails");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
