using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiejemplo.Migrations
{
    /// <inheritdoc />
    public partial class refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conjunto",
                columns: table => new
                {
                    ConjuntoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NIT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conjunto", x => x.ConjuntoId);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMantenimiento",
                columns: table => new
                {
                    TipoMantenimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMantenimiento", x => x.TipoMantenimientoId);
                });

            migrationBuilder.CreateTable(
                name: "ZonaComun",
                columns: table => new
                {
                    ZonaComunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequierePago = table.Column<bool>(type: "bit", nullable: false),
                    ValorHora = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonaComun", x => x.ZonaComunId);
                });

            migrationBuilder.CreateTable(
                name: "Torre",
                columns: table => new
                {
                    TorreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConjuntoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torre", x => x.TorreId);
                    table.ForeignKey(
                        name: "FK_Torre_Conjunto_ConjuntoId",
                        column: x => x.ConjuntoId,
                        principalTable: "Conjunto",
                        principalColumn: "ConjuntoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    MantenimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMantenimientoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    ZonaComunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.MantenimientoId);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_TipoMantenimiento_TipoMantenimientoId",
                        column: x => x.TipoMantenimientoId,
                        principalTable: "TipoMantenimiento",
                        principalColumn: "TipoMantenimientoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_ZonaComun_ZonaComunId",
                        column: x => x.ZonaComunId,
                        principalTable: "ZonaComun",
                        principalColumn: "ZonaComunId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Apartamentos",
                columns: table => new
                {
                    UnidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TorreId = table.Column<int>(type: "int", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Area = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartamentos", x => x.UnidadId);
                    table.ForeignKey(
                        name: "FK_Apartamentos_Torre_TorreId",
                        column: x => x.TorreId,
                        principalTable: "Torre",
                        principalColumn: "TorreId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BitacoraVigilancia",
                columns: table => new
                {
                    BitacoraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VigilanteId = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraVigilancia", x => x.BitacoraId);
                    table.ForeignKey(
                        name: "FK_BitacoraVigilancia_Usuario_VigilanteId",
                        column: x => x.VigilanteId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZonaComunId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_ZonaComun_ZonaComunId",
                        column: x => x.ZonaComunId,
                        principalTable: "ZonaComun",
                        principalColumn: "ZonaComunId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingreso",
                columns: table => new
                {
                    IngresoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NombrePersona = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaHoraIngreso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaHoraSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingreso", x => x.IngresoId);
                    table.ForeignKey(
                        name: "FK_Ingreso_Apartamentos_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Apartamentos",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensajeria",
                columns: table => new
                {
                    MensajeriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Guia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajeria", x => x.MensajeriaId);
                    table.ForeignKey(
                        name: "FK_Mensajeria_Apartamentos_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Apartamentos",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajeria_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parqueadero",
                columns: table => new
                {
                    ParqueaderoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parqueadero", x => x.ParqueaderoId);
                    table.ForeignKey(
                        name: "FK_Parqueadero_Apartamentos_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Apartamentos",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ResidenteUnidad",
                columns: table => new
                {
                    ResidenteUnidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    EsPropietario = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenteUnidad", x => x.ResidenteUnidadId);
                    table.ForeignKey(
                        name: "FK_ResidenteUnidad_Apartamentos_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Apartamentos",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResidenteUnidad_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParqueaderoVisitante",
                columns: table => new
                {
                    ParqueaderoVisitanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParqueaderoId = table.Column<int>(type: "int", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaHoraIngreso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaHoraSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IngresoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParqueaderoVisitante", x => x.ParqueaderoVisitanteId);
                    table.ForeignKey(
                        name: "FK_ParqueaderoVisitante_Ingreso_IngresoId",
                        column: x => x.IngresoId,
                        principalTable: "Ingreso",
                        principalColumn: "IngresoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParqueaderoVisitante_Parqueadero_ParqueaderoId",
                        column: x => x.ParqueaderoId,
                        principalTable: "Parqueadero",
                        principalColumn: "ParqueaderoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_TorreId",
                table: "Apartamentos",
                column: "TorreId");

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraVigilancia_VigilanteId",
                table: "BitacoraVigilancia",
                column: "VigilanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_UnidadId",
                table: "Ingreso",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_UsuarioId",
                table: "Ingreso",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_TipoMantenimientoId",
                table: "Mantenimiento",
                column: "TipoMantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_ZonaComunId",
                table: "Mantenimiento",
                column: "ZonaComunId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajeria_UnidadId",
                table: "Mensajeria",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajeria_UsuarioId",
                table: "Mensajeria",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Parqueadero_UnidadId",
                table: "Parqueadero",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ParqueaderoVisitante_IngresoId",
                table: "ParqueaderoVisitante",
                column: "IngresoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParqueaderoVisitante_ParqueaderoId",
                table: "ParqueaderoVisitante",
                column: "ParqueaderoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ZonaComunId",
                table: "Reserva",
                column: "ZonaComunId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidenteUnidad_UnidadId",
                table: "ResidenteUnidad",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "UQ_Residente_Unidad",
                table: "ResidenteUnidad",
                columns: new[] { "UsuarioId", "UnidadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Rol_Nombre",
                table: "Rol",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TipoMantenimiento_Nombre",
                table: "TipoMantenimiento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Torre_ConjuntoId",
                table: "Torre",
                column: "ConjuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitacoraVigilancia");

            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "Mensajeria");

            migrationBuilder.DropTable(
                name: "ParqueaderoVisitante");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "ResidenteUnidad");

            migrationBuilder.DropTable(
                name: "TipoMantenimiento");

            migrationBuilder.DropTable(
                name: "Ingreso");

            migrationBuilder.DropTable(
                name: "Parqueadero");

            migrationBuilder.DropTable(
                name: "ZonaComun");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Apartamentos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Torre");

            migrationBuilder.DropTable(
                name: "Conjunto");
        }
    }
}
