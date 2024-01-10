using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTRS.Migrations
{
    public partial class fm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passenger",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passenger", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusNum = table.Column<int>(type: "int", nullable: false),
                    dest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_Admin_Trip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trip_admin_FK_Admin_Trip",
                        column: x => x.FK_Admin_Trip,
                        principalTable: "admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaptinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Num_of_S = table.Column<int>(type: "int", nullable: false),
                    FK_Bus_Trip = table.Column<int>(type: "int", nullable: false),
                    FK_Bus_Admin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bus_admin_FK_Bus_Admin",
                        column: x => x.FK_Bus_Admin,
                        principalTable: "admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bus_trip_FK_Bus_Trip",
                        column: x => x.FK_Bus_Trip,
                        principalTable: "trip",
                        principalColumn: "Id"
                       /* onDelete: ReferentialAction.Cascade*/);
                });

            migrationBuilder.CreateTable(
                name: "Trip_Passenger",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Passenger = table.Column<int>(type: "int", nullable: false),
                    FK_Trip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_Passenger", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trip_Passenger_passenger_FK_Passenger",
                        column: x => x.FK_Passenger,
                        principalTable: "passenger",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Passenger_trip_FK_Trip",
                        column: x => x.FK_Trip,
                        principalTable: "trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bus_FK_Bus_Admin",
                table: "bus",
                column: "FK_Bus_Admin");

            migrationBuilder.CreateIndex(
                name: "IX_bus_FK_Bus_Trip",
                table: "bus",
                column: "FK_Bus_Trip");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_email",
                table: "passenger",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trip_FK_Admin_Trip",
                table: "trip",
                column: "FK_Admin_Trip");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Passenger_FK_Passenger",
                table: "Trip_Passenger",
                column: "FK_Passenger");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Passenger_FK_Trip",
                table: "Trip_Passenger",
                column: "FK_Trip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bus");

            migrationBuilder.DropTable(
                name: "Trip_Passenger");

            migrationBuilder.DropTable(
                name: "passenger");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "admin");
        }
    }
}
