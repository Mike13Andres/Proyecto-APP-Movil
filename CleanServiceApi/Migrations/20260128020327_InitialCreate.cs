using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmailEmpleado = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Contrasena = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmailEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    EmailUsuario = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Contrasena = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.EmailUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Resenias",
                columns: table => new
                {
                    IdResenia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    EmailEmpleado = table.Column<string>(type: "text", nullable: false),
                    EmailUsuario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenias", x => x.IdResenia);
                    table.ForeignKey(
                        name: "FK_Resenias_Empleados_EmailEmpleado",
                        column: x => x.EmailEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "EmailEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resenias_Usuarios_EmailUsuario",
                        column: x => x.EmailUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "EmailUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrecioHora = table.Column<double>(type: "double precision", nullable: false),
                    Disponibilidad = table.Column<int>(type: "integer", nullable: false),
                    TipoServicio = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<string>(type: "text", nullable: false),
                    hora = table.Column<string>(type: "text", nullable: false),
                    ImagenUrl = table.Column<string>(type: "text", nullable: true),
                    EmailUsuario = table.Column<string>(type: "text", nullable: false),
                    EmailEmpleado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Empleados_EmailEmpleado",
                        column: x => x.EmailEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "EmailEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_EmailUsuario",
                        column: x => x.EmailUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "EmailUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resenias_EmailEmpleado",
                table: "Resenias",
                column: "EmailEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Resenias_EmailUsuario",
                table: "Resenias",
                column: "EmailUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_EmailEmpleado",
                table: "Solicitudes",
                column: "EmailEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_EmailUsuario",
                table: "Solicitudes",
                column: "EmailUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resenias");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
