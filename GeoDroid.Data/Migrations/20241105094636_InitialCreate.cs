using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoDroid.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AveriaEstado",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    color = table.Column<int>(type: "INTEGER", nullable: false),
                    fin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AveriaEstado", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CacheImportes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    numeroRecaudaciones = table.Column<int>(type: "INTEGER", nullable: false),
                    neto = table.Column<decimal>(type: "TEXT", nullable: false),
                    tasas = table.Column<decimal>(type: "TEXT", nullable: false),
                    cargas = table.Column<decimal>(type: "TEXT", nullable: false),
                    recuperacionesCargas = table.Column<decimal>(type: "TEXT", nullable: false),
                    prestamos = table.Column<decimal>(type: "TEXT", nullable: false),
                    recuperacionesPrestamos = table.Column<decimal>(type: "TEXT", nullable: false),
                    gastos = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheImportes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoAveria",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoAveria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoGastoEstablecimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    activoEnTerminales = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoGastoEstablecimiento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoMotivoMaquinaNoRecaudada",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoMotivoMaquinaNoRecaudada", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoPrestamo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    fondoPerdido = table.Column<bool>(type: "INTEGER", nullable: false),
                    cargasHopper = table.Column<bool>(type: "INTEGER", nullable: false),
                    activoEnTerminales = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoPrestamo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    unitNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ip = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    port = table.Column<int>(type: "INTEGER", maxLength: 5, nullable: false),
                    printerMAC = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    syncToken = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    tempSyncToken = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    printSignature = table.Column<bool>(type: "INTEGER", nullable: false),
                    printTwoCopies = table.Column<bool>(type: "INTEGER", nullable: false),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: false),
                    serverDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    syncCompressionEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationGEO",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    valor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationGEO", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DesgloseBruto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idRecaudacion = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_50 = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_20 = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_10 = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_5 = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_2 = table.Column<int>(type: "INTEGER", nullable: false),
                    eur_1 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_50 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_20 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_10 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_5 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_2 = table.Column<int>(type: "INTEGER", nullable: false),
                    cent_1 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesgloseBruto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    poblacion = table.Column<string>(type: "TEXT", nullable: false),
                    direccion = table.Column<string>(type: "TEXT", nullable: false),
                    cif = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaCompetidora",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaCompetidora", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Establecimientos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    codigo = table.Column<string>(type: "TEXT", nullable: true),
                    nombre = table.Column<string>(type: "TEXT", nullable: true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    direccion = table.Column<string>(type: "TEXT", nullable: true),
                    poblacion = table.Column<string>(type: "TEXT", nullable: true),
                    codigoPostal = table.Column<string>(type: "TEXT", nullable: true),
                    telefono1 = table.Column<string>(type: "TEXT", nullable: true),
                    telefono2 = table.Column<string>(type: "TEXT", nullable: true),
                    fax = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    web = table.Column<string>(type: "TEXT", nullable: true),
                    contacto = table.Column<string>(type: "TEXT", nullable: true),
                    esAlmacen = table.Column<bool>(type: "INTEGER", nullable: false),
                    permiso = table.Column<string>(type: "TEXT", nullable: true),
                    comentarios = table.Column<string>(type: "TEXT", nullable: true),
                    avisoRecaudacion = table.Column<string>(type: "TEXT", nullable: true),
                    nombreTitular = table.Column<string>(type: "TEXT", nullable: true),
                    nifTitular = table.Column<string>(type: "TEXT", nullable: true),
                    envioFacturasEmail = table.Column<bool>(type: "INTEGER", nullable: false),
                    fechaDesdeTitular = table.Column<DateTime>(type: "TEXT", nullable: false),
                    comercial = table.Column<string>(type: "TEXT", nullable: true),
                    recaudador = table.Column<string>(type: "TEXT", nullable: true),
                    mecanico = table.Column<string>(type: "TEXT", nullable: true),
                    tieneLlave = table.Column<bool>(type: "INTEGER", nullable: false),
                    horaApertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    horaCierre = table.Column<DateTime>(type: "TEXT", nullable: false),
                    comentariosHorario = table.Column<string>(type: "TEXT", nullable: true),
                    comentariosContrato = table.Column<string>(type: "TEXT", nullable: true),
                    diasDescanso = table.Column<string>(type: "TEXT", nullable: true),
                    fechaProximaRecaudacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    maxDiasSinArquear = table.Column<int>(type: "INTEGER", nullable: false),
                    tecnausaCountersServidor = table.Column<string>(type: "TEXT", nullable: true),
                    tecnausaCountersUsuario = table.Column<string>(type: "TEXT", nullable: true),
                    tecnausaCountersPassword = table.Column<string>(type: "TEXT", nullable: true),
                    fechaPermiso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaCaducidadPermiso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaPresentacionPermiso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    estadoPermiso = table.Column<string>(type: "TEXT", nullable: true),
                    fechaContrato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaFinContrato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    recaudacion = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establecimientos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LecturaDetalleAuto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idLecturaContadoresAuto = table.Column<int>(type: "INTEGER", nullable: false),
                    tipoContadoresProtocolos = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturaDetalleAuto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaHora = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    latitud = table.Column<double>(type: "REAL", nullable: false),
                    longitud = table.Column<double>(type: "REAL", nullable: false),
                    estado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloMaquina",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloMaquina", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MotivoAccionComercial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoAccionComercial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MotivoVisitaComercial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoVisitaComercial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PatContDetalle",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idPatronContadores = table.Column<int>(type: "INTEGER", nullable: false),
                    tipoContadores = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    valorPaso = table.Column<decimal>(type: "TEXT", nullable: false),
                    tipoBasico = table.Column<int>(type: "INTEGER", nullable: false),
                    tipoProtocolos = table.Column<int>(type: "INTEGER", nullable: false),
                    orden = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatContDetalle", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PatronesContador",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronesContador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Poblacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poblacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAccionComercial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAccionComercial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCombustible",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCombustible", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDistribucionConceptoRecaudacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idTipoDistribucion = table.Column<int>(type: "INTEGER", nullable: false),
                    idConceptoRecaudacionGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    concepto = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    pctBruto = table.Column<int>(type: "INTEGER", nullable: false),
                    pctEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    pctEstablecimiento = table.Column<int>(type: "INTEGER", nullable: false),
                    establecimiento = table.Column<bool>(type: "INTEGER", nullable: false),
                    facturable = table.Column<bool>(type: "INTEGER", nullable: false),
                    esIngreso = table.Column<bool>(type: "INTEGER", nullable: false),
                    tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    orden = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDistribucionConceptoRecaudacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", nullable: false),
                    matricula = table.Column<string>(type: "TEXT", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    idEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    maxKm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CacheImportesConceptosRecaudacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idCacheImportes = table.Column<int>(type: "INTEGER", nullable: false),
                    idConceptoRecaudacionGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    CacheImportesid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheImportesConceptosRecaudacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_CacheImportesConceptosRecaudacion_CacheImportes_CacheImportesid",
                        column: x => x.CacheImportesid,
                        principalTable: "CacheImportes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cambio",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idPersonalEntrega = table.Column<int>(type: "INTEGER", nullable: false),
                    idPersonalRecuperacion = table.Column<int>(type: "INTEGER", nullable: false),
                    idEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaRecuperacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaEntrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", nullable: false),
                    bloqueado = table.Column<bool>(type: "INTEGER", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idEstablecimientos = table.Column<int>(type: "INTEGER", nullable: true),
                    establecimientoid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambio", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cambio_Establecimientos_establecimientoid",
                        column: x => x.establecimientoid,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "GastoEstablecimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: false),
                    baseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    pctIva = table.Column<decimal>(type: "TEXT", nullable: false),
                    iva = table.Column<decimal>(type: "TEXT", nullable: false),
                    total = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idConceptoGastoEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    idEmpresas = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoEstablecimiento", x => x.id);
                    table.ForeignKey(
                        name: "FK_GastoEstablecimiento_ConceptoGastoEstablecimiento_idConceptoGastoEstablecimiento",
                        column: x => x.idConceptoGastoEstablecimiento,
                        principalTable: "ConceptoGastoEstablecimiento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_GastoEstablecimiento_Empresa_idEmpresas",
                        column: x => x.idEmpresas,
                        principalTable: "Empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_GastoEstablecimiento_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Ruta",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    bloqueada = table.Column<bool>(type: "INTEGER", nullable: false),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ruta_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VisitaComercial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: false),
                    personaContacto = table.Column<string>(type: "TEXT", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    valoracion = table.Column<int>(type: "INTEGER", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    idMotivoVisitaComercial = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitaComercial", x => x.id);
                    table.ForeignKey(
                        name: "FK_VisitaComercial_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VisitaComercial_MotivoVisitaComercial_idMotivoVisitaComercial",
                        column: x => x.idMotivoVisitaComercial,
                        principalTable: "MotivoVisitaComercial",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Maquina",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", nullable: true),
                    pctEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    fechaUltimoArqueo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ultimoArqueo = table.Column<decimal>(type: "TEXT", nullable: true),
                    arqueoEstablecido = table.Column<decimal>(type: "TEXT", nullable: true),
                    diferenciaArqueo = table.Column<decimal>(type: "TEXT", nullable: true),
                    redondeoValor = table.Column<decimal>(type: "TEXT", nullable: true),
                    redondeoParaEmpresa = table.Column<bool>(type: "INTEGER", nullable: true),
                    fechaUltimaRecaudacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    empresaSaldo = table.Column<decimal>(type: "TEXT", nullable: true),
                    establecimientoSaldo = table.Column<decimal>(type: "TEXT", nullable: true),
                    protocolosMaquinas = table.Column<int>(type: "INTEGER", nullable: true),
                    idInstalaciones = table.Column<int>(type: "INTEGER", nullable: true),
                    idPatronContadores = table.Column<int>(type: "INTEGER", nullable: true),
                    patronContadorid = table.Column<int>(type: "INTEGER", nullable: true),
                    fechaInstalacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    idTipoDistribucion = table.Column<int>(type: "INTEGER", nullable: true),
                    descripcionDistribucion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    modeloMaquina = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    permiso = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Empresaid = table.Column<int>(type: "INTEGER", nullable: true),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquina", x => x.id);
                    table.ForeignKey(
                        name: "FK_Maquina_Empresa_Empresaid",
                        column: x => x.Empresaid,
                        principalTable: "Empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Maquina_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquina_PatronesContador_patronContadorid",
                        column: x => x.patronContadorid,
                        principalTable: "PatronesContador",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PatronesContadoresDetalle",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: true),
                    tipoContadores = table.Column<string>(type: "TEXT", nullable: false),
                    valorPaso = table.Column<decimal>(type: "TEXT", nullable: false),
                    tipoBasico = table.Column<int>(type: "INTEGER", nullable: false),
                    tipoProtocolos = table.Column<int>(type: "INTEGER", nullable: false),
                    orden = table.Column<int>(type: "INTEGER", nullable: false),
                    idPatronContador = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronesContadoresDetalle", x => x.id);
                    table.ForeignKey(
                        name: "FK_PatronesContadoresDetalle_PatronesContador_idPatronContador",
                        column: x => x.idPatronContador,
                        principalTable: "PatronesContador",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EstablecimientoPotencial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    direccion = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    codigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    telefono1 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    telefono2 = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    idComercial = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idPoblacion = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstablecimientoPotencial", x => x.id);
                    table.ForeignKey(
                        name: "FK_EstablecimientoPotencial_Poblacion_idPoblacion",
                        column: x => x.idPoblacion,
                        principalTable: "Poblacion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AccionComercial",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: false),
                    personaContacto = table.Column<string>(type: "TEXT", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    valoracion = table.Column<int>(type: "INTEGER", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    idTipoAccionComercial = table.Column<int>(type: "INTEGER", nullable: true),
                    idMotivoAccionComercial = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionComercial", x => x.id);
                    table.ForeignKey(
                        name: "FK_AccionComercial_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccionComercial_MotivoAccionComercial_idMotivoAccionComercial",
                        column: x => x.idMotivoAccionComercial,
                        principalTable: "MotivoAccionComercial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccionComercial_TipoAccionComercial_idTipoAccionComercial",
                        column: x => x.idTipoAccionComercial,
                        principalTable: "TipoAccionComercial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiculoCombustible",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idVehiculos = table.Column<int>(type: "INTEGER", nullable: true),
                    idTipoCombustible = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoCombustible", x => x.id);
                    table.ForeignKey(
                        name: "FK_VehiculoCombustible_TipoCombustible_idTipoCombustible",
                        column: x => x.idTipoCombustible,
                        principalTable: "TipoCombustible",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VehiculoCombustible_Vehiculo_idVehiculos",
                        column: x => x.idVehiculos,
                        principalTable: "Vehiculo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VehiculoKm",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    km = table.Column<int>(type: "INTEGER", nullable: false),
                    tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    idVehiculos = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoKm", x => x.id);
                    table.ForeignKey(
                        name: "FK_VehiculoKm_Vehiculo_idVehiculos",
                        column: x => x.idVehiculos,
                        principalTable: "Vehiculo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VehiculoRepostaje",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: false),
                    baseImponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    pctIva = table.Column<decimal>(type: "TEXT", nullable: false),
                    iva = table.Column<decimal>(type: "TEXT", nullable: false),
                    total = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    precioUnidadCombustible = table.Column<decimal>(type: "TEXT", nullable: false),
                    km = table.Column<int>(type: "INTEGER", nullable: false),
                    foto = table.Column<byte[]>(type: "BLOB", nullable: false),
                    idTipoCombustible = table.Column<int>(type: "INTEGER", nullable: true),
                    idEmpresas = table.Column<int>(type: "INTEGER", nullable: true),
                    idVehiculos = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoRepostaje", x => x.id);
                    table.ForeignKey(
                        name: "FK_VehiculoRepostaje_Empresa_idEmpresas",
                        column: x => x.idEmpresas,
                        principalTable: "Empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VehiculoRepostaje_TipoCombustible_idTipoCombustible",
                        column: x => x.idTipoCombustible,
                        principalTable: "TipoCombustible",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VehiculoRepostaje_Vehiculo_idVehiculos",
                        column: x => x.idVehiculos,
                        principalTable: "Vehiculo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Incidencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idRutaRecaudaciones = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idMaquinas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Incidencia_Maquina_idMaquinas",
                        column: x => x.idMaquinas,
                        principalTable: "Maquina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinaConceptoRecaudacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idConceptoRecaudacionGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fijo = table.Column<decimal>(type: "TEXT", nullable: false),
                    diario = table.Column<decimal>(type: "TEXT", nullable: false),
                    semanal = table.Column<decimal>(type: "TEXT", nullable: false),
                    pct = table.Column<int>(type: "INTEGER", nullable: false),
                    acumular = table.Column<bool>(type: "INTEGER", nullable: false),
                    pendiente = table.Column<decimal>(type: "TEXT", nullable: false),
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaConceptoRecaudacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_MaquinaConceptoRecaudacion_Maquina_idMaquina",
                        column: x => x.idMaquina,
                        principalTable: "Maquina",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ModuloCaptura",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    mac = table.Column<string>(type: "TEXT", nullable: false),
                    imei = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    idMaquinas = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloCaptura", x => x.id);
                    table.ForeignKey(
                        name: "FK_ModuloCaptura_Maquina_idMaquinas",
                        column: x => x.idMaquinas,
                        principalTable: "Maquina",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Prestamo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idPersonalEntrega = table.Column<int>(type: "INTEGER", nullable: false),
                    importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    saldo = table.Column<decimal>(type: "TEXT", nullable: false),
                    importePorRecuperacion = table.Column<decimal>(type: "TEXT", nullable: false),
                    pctPorRecuperacion = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaUltimaRecuperacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idEmpresa = table.Column<int>(type: "INTEGER", nullable: true),
                    idEstablecimiento = table.Column<int>(type: "INTEGER", nullable: true),
                    idConceptoPrestamo = table.Column<int>(type: "INTEGER", nullable: true),
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prestamo_ConceptoPrestamo_idConceptoPrestamo",
                        column: x => x.idConceptoPrestamo,
                        principalTable: "ConceptoPrestamo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prestamo_Empresa_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prestamo_Establecimientos_idEstablecimiento",
                        column: x => x.idEstablecimiento,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prestamo_Maquina_idMaquina",
                        column: x => x.idMaquina,
                        principalTable: "Maquina",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UltimaLecturaRecaudacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<long>(type: "INTEGER", nullable: false),
                    ajustePostLectura = table.Column<long>(type: "INTEGER", nullable: false),
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: true),
                    idPatContDetalles = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UltimaLecturaRecaudacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_UltimaLecturaRecaudacion_Maquina_idMaquina",
                        column: x => x.idMaquina,
                        principalTable: "Maquina",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UltimaLecturaRecaudacion_PatContDetalle_idPatContDetalles",
                        column: x => x.idPatContDetalles,
                        principalTable: "PatContDetalle",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EstablecimientoPotencialMaquina",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaPermiso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaRenovacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idEstablecimientoPotencial = table.Column<int>(type: "INTEGER", nullable: true),
                    idEmpresaCompetidora = table.Column<int>(type: "INTEGER", nullable: true),
                    idModelo = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstablecimientoPotencialMaquina", x => x.id);
                    table.ForeignKey(
                        name: "FK_EstablecimientoPotencialMaquina_EmpresaCompetidora_idEmpresaCompetidora",
                        column: x => x.idEmpresaCompetidora,
                        principalTable: "EmpresaCompetidora",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_EstablecimientoPotencialMaquina_EstablecimientoPotencial_idEstablecimientoPotencial",
                        column: x => x.idEstablecimientoPotencial,
                        principalTable: "EstablecimientoPotencial",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_EstablecimientoPotencialMaquina_ModeloMaquina_idModelo",
                        column: x => x.idModelo,
                        principalTable: "ModeloMaquina",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Averia",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: true),
                    idPersonal = table.Column<int>(type: "INTEGER", nullable: true),
                    observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    detalle = table.Column<string>(type: "TEXT", nullable: true),
                    comentarios = table.Column<string>(type: "TEXT", nullable: true),
                    fechaFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    foto = table.Column<byte[]>(type: "BLOB", nullable: true),
                    estado = table.Column<string>(type: "TEXT", nullable: true),
                    idConceptos = table.Column<int>(type: "INTEGER", nullable: true),
                    idIncidencias = table.Column<int>(type: "INTEGER", nullable: false),
                    idAveriaEstados = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Averia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Averia_AveriaEstado_idAveriaEstados",
                        column: x => x.idAveriaEstados,
                        principalTable: "AveriaEstado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Averia_ConceptoAveria_idConceptos",
                        column: x => x.idConceptos,
                        principalTable: "ConceptoAveria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Averia_Incidencia_idIncidencias",
                        column: x => x.idIncidencias,
                        principalTable: "Incidencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instalacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaInstalacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaInstalacionPrevista = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaTransporteGet = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaTransportePut = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    arqueo = table.Column<decimal>(type: "TEXT", nullable: false),
                    arqueoEstablecido = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idIncidencia = table.Column<int>(type: "INTEGER", nullable: false),
                    idMaquinas = table.Column<int>(type: "INTEGER", nullable: false),
                    idEstablecimientoOrigen = table.Column<int>(type: "INTEGER", nullable: true),
                    idEstablecimientoDestino = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instalacion_Establecimientos_idEstablecimientoDestino",
                        column: x => x.idEstablecimientoDestino,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Instalacion_Establecimientos_idEstablecimientoOrigen",
                        column: x => x.idEstablecimientoOrigen,
                        principalTable: "Establecimientos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Instalacion_Incidencia_idIncidencia",
                        column: x => x.idIncidencia,
                        principalTable: "Incidencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instalacion_Maquina_idMaquinas",
                        column: x => x.idMaquinas,
                        principalTable: "Maquina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturaContador",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    idIncidencias = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturaContador", x => x.id);
                    table.ForeignKey(
                        name: "FK_LecturaContador_Incidencia_idIncidencias",
                        column: x => x.idIncidencias,
                        principalTable: "Incidencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "LecturaContadorAuto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipoLecturaContadores = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<int>(type: "INTEGER", nullable: false),
                    idIncidencias = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturaContadorAuto", x => x.id);
                    table.ForeignKey(
                        name: "FK_LecturaContadorAuto_Incidencia_idIncidencias",
                        column: x => x.idIncidencias,
                        principalTable: "Incidencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MotivoMaquinaNoRecaudada",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    idIncidencia = table.Column<int>(type: "INTEGER", nullable: true),
                    idConceptoMotivoMaquinaNoRecaudada = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoMaquinaNoRecaudada", x => x.id);
                    table.ForeignKey(
                        name: "FK_MotivoMaquinaNoRecaudada_ConceptoMotivoMaquinaNoRecaudada_idConceptoMotivoMaquinaNoRecaudada",
                        column: x => x.idConceptoMotivoMaquinaNoRecaudada,
                        principalTable: "ConceptoMotivoMaquinaNoRecaudada",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_MotivoMaquinaNoRecaudada_Incidencia_idIncidencia",
                        column: x => x.idIncidencia,
                        principalTable: "Incidencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Recaudacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bruto = table.Column<decimal>(type: "TEXT", nullable: false),
                    recaudacion = table.Column<decimal>(type: "TEXT", nullable: false),
                    netoEmpresa = table.Column<decimal>(type: "TEXT", nullable: false),
                    netoEstablecimiento = table.Column<decimal>(type: "TEXT", nullable: false),
                    arqueoRealizado = table.Column<bool>(type: "INTEGER", nullable: false),
                    valorArqueo = table.Column<decimal>(type: "TEXT", nullable: false),
                    observaciones = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    comentario = table.Column<string>(type: "TEXT", nullable: false),
                    idRuta = table.Column<int>(type: "INTEGER", nullable: true),
                    idIncidencia = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recaudacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recaudacion_Incidencia_idIncidencia",
                        column: x => x.idIncidencia,
                        principalTable: "Incidencia",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Recaudacion_Ruta_idRuta",
                        column: x => x.idRuta,
                        principalTable: "Ruta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PrestamoRecuperacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idPersonalRecuperacion = table.Column<int>(type: "INTEGER", nullable: false),
                    importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", nullable: false),
                    idRuta = table.Column<int>(type: "INTEGER", nullable: false),
                    idCarga = table.Column<int>(type: "INTEGER", nullable: false),
                    idPrestamos = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestamoRecuperacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_PrestamoRecuperacion_Prestamo_idPrestamos",
                        column: x => x.idPrestamos,
                        principalTable: "Prestamo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Carga",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: false),
                    cargaEmpresa = table.Column<decimal>(type: "TEXT", nullable: false),
                    cargaEstablecimiento = table.Column<decimal>(type: "TEXT", nullable: false),
                    recuperacionEmpresa = table.Column<decimal>(type: "TEXT", nullable: false),
                    recuperacionEstablecimiento = table.Column<decimal>(type: "TEXT", nullable: false),
                    ajusteEmpresa = table.Column<decimal>(type: "TEXT", nullable: false),
                    ajusteEstablecimiento = table.Column<decimal>(type: "TEXT", nullable: false),
                    saldoEmpresaChanged = table.Column<decimal>(type: "TEXT", nullable: false),
                    saldoEstablecimientoChanged = table.Column<decimal>(type: "TEXT", nullable: false),
                    idIncidencia = table.Column<int>(type: "INTEGER", nullable: true),
                    idAverias = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carga", x => x.id);
                    table.ForeignKey(
                        name: "FK_Carga_Averia_idAverias",
                        column: x => x.idAverias,
                        principalTable: "Averia",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Carga_Incidencia_idIncidencia",
                        column: x => x.idIncidencia,
                        principalTable: "Incidencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "LecturaDetalle",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idGeo = table.Column<int>(type: "INTEGER", nullable: true),
                    valor = table.Column<long>(type: "INTEGER", nullable: false),
                    valorAntes = table.Column<long>(type: "INTEGER", nullable: false),
                    tieneAjuste = table.Column<bool>(type: "INTEGER", nullable: false),
                    idLecturaContadores = table.Column<int>(type: "INTEGER", nullable: true),
                    idPatContDetalles = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturaDetalle", x => x.id);
                    table.ForeignKey(
                        name: "FK_LecturaDetalle_LecturaContador_idLecturaContadores",
                        column: x => x.idLecturaContadores,
                        principalTable: "LecturaContador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturaDetalle_PatronesContadoresDetalle_idPatContDetalles",
                        column: x => x.idPatContDetalles,
                        principalTable: "PatronesContadoresDetalle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecaudacionDetalles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idConceptoRecaudacion = table.Column<int>(type: "INTEGER", nullable: false),
                    importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    importeDefecto = table.Column<decimal>(type: "TEXT", nullable: false),
                    idRecaudacion = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecaudacionDetalles", x => x.id);
                    table.ForeignKey(
                        name: "FK_RecaudacionDetalles_Recaudacion_idRecaudacion",
                        column: x => x.idRecaudacion,
                        principalTable: "Recaudacion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccionComercial_idEstablecimiento",
                table: "AccionComercial",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_AccionComercial_idMotivoAccionComercial",
                table: "AccionComercial",
                column: "idMotivoAccionComercial");

            migrationBuilder.CreateIndex(
                name: "IX_AccionComercial_idTipoAccionComercial",
                table: "AccionComercial",
                column: "idTipoAccionComercial");

            migrationBuilder.CreateIndex(
                name: "IX_Averia_idAveriaEstados",
                table: "Averia",
                column: "idAveriaEstados");

            migrationBuilder.CreateIndex(
                name: "IX_Averia_idConceptos",
                table: "Averia",
                column: "idConceptos");

            migrationBuilder.CreateIndex(
                name: "IX_Averia_idIncidencias",
                table: "Averia",
                column: "idIncidencias");

            migrationBuilder.CreateIndex(
                name: "IX_CacheImportesConceptosRecaudacion_CacheImportesid",
                table: "CacheImportesConceptosRecaudacion",
                column: "CacheImportesid");

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_establecimientoid",
                table: "Cambio",
                column: "establecimientoid");

            migrationBuilder.CreateIndex(
                name: "IX_Carga_idAverias",
                table: "Carga",
                column: "idAverias");

            migrationBuilder.CreateIndex(
                name: "IX_Carga_idIncidencia",
                table: "Carga",
                column: "idIncidencia");

            migrationBuilder.CreateIndex(
                name: "IX_EstablecimientoPotencial_idPoblacion",
                table: "EstablecimientoPotencial",
                column: "idPoblacion");

            migrationBuilder.CreateIndex(
                name: "IX_EstablecimientoPotencialMaquina_idEmpresaCompetidora",
                table: "EstablecimientoPotencialMaquina",
                column: "idEmpresaCompetidora");

            migrationBuilder.CreateIndex(
                name: "IX_EstablecimientoPotencialMaquina_idEstablecimientoPotencial",
                table: "EstablecimientoPotencialMaquina",
                column: "idEstablecimientoPotencial");

            migrationBuilder.CreateIndex(
                name: "IX_EstablecimientoPotencialMaquina_idModelo",
                table: "EstablecimientoPotencialMaquina",
                column: "idModelo");

            migrationBuilder.CreateIndex(
                name: "IX_GastoEstablecimiento_idConceptoGastoEstablecimiento",
                table: "GastoEstablecimiento",
                column: "idConceptoGastoEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_GastoEstablecimiento_idEmpresas",
                table: "GastoEstablecimiento",
                column: "idEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_GastoEstablecimiento_idEstablecimiento",
                table: "GastoEstablecimiento",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencia_idMaquinas",
                table: "Incidencia",
                column: "idMaquinas");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_idEstablecimientoDestino",
                table: "Instalacion",
                column: "idEstablecimientoDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_idEstablecimientoOrigen",
                table: "Instalacion",
                column: "idEstablecimientoOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_idIncidencia",
                table: "Instalacion",
                column: "idIncidencia");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_idMaquinas",
                table: "Instalacion",
                column: "idMaquinas");

            migrationBuilder.CreateIndex(
                name: "IX_LecturaContador_idIncidencias",
                table: "LecturaContador",
                column: "idIncidencias");

            migrationBuilder.CreateIndex(
                name: "IX_LecturaContadorAuto_idIncidencias",
                table: "LecturaContadorAuto",
                column: "idIncidencias");

            migrationBuilder.CreateIndex(
                name: "IX_LecturaDetalle_idLecturaContadores",
                table: "LecturaDetalle",
                column: "idLecturaContadores");

            migrationBuilder.CreateIndex(
                name: "IX_LecturaDetalle_idPatContDetalles",
                table: "LecturaDetalle",
                column: "idPatContDetalles");

            migrationBuilder.CreateIndex(
                name: "IX_Maquina_Empresaid",
                table: "Maquina",
                column: "Empresaid");

            migrationBuilder.CreateIndex(
                name: "IX_Maquina_idEstablecimiento",
                table: "Maquina",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Maquina_patronContadorid",
                table: "Maquina",
                column: "patronContadorid");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaConceptoRecaudacion_idMaquina",
                table: "MaquinaConceptoRecaudacion",
                column: "idMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloCaptura_idMaquinas",
                table: "ModuloCaptura",
                column: "idMaquinas");

            migrationBuilder.CreateIndex(
                name: "IX_MotivoMaquinaNoRecaudada_idConceptoMotivoMaquinaNoRecaudada",
                table: "MotivoMaquinaNoRecaudada",
                column: "idConceptoMotivoMaquinaNoRecaudada");

            migrationBuilder.CreateIndex(
                name: "IX_MotivoMaquinaNoRecaudada_idIncidencia",
                table: "MotivoMaquinaNoRecaudada",
                column: "idIncidencia");

            migrationBuilder.CreateIndex(
                name: "IX_PatronesContadoresDetalle_idPatronContador",
                table: "PatronesContadoresDetalle",
                column: "idPatronContador");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_idConceptoPrestamo",
                table: "Prestamo",
                column: "idConceptoPrestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_idEmpresa",
                table: "Prestamo",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_idEstablecimiento",
                table: "Prestamo",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_idMaquina",
                table: "Prestamo",
                column: "idMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamoRecuperacion_idPrestamos",
                table: "PrestamoRecuperacion",
                column: "idPrestamos");

            migrationBuilder.CreateIndex(
                name: "IX_Recaudacion_idIncidencia",
                table: "Recaudacion",
                column: "idIncidencia");

            migrationBuilder.CreateIndex(
                name: "IX_Recaudacion_idRuta",
                table: "Recaudacion",
                column: "idRuta");

            migrationBuilder.CreateIndex(
                name: "IX_RecaudacionDetalles_idRecaudacion",
                table: "RecaudacionDetalles",
                column: "idRecaudacion");

            migrationBuilder.CreateIndex(
                name: "IX_Ruta_idEstablecimiento",
                table: "Ruta",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_UltimaLecturaRecaudacion_idMaquina",
                table: "UltimaLecturaRecaudacion",
                column: "idMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_UltimaLecturaRecaudacion_idPatContDetalles",
                table: "UltimaLecturaRecaudacion",
                column: "idPatContDetalles");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoCombustible_idTipoCombustible",
                table: "VehiculoCombustible",
                column: "idTipoCombustible");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoCombustible_idVehiculos",
                table: "VehiculoCombustible",
                column: "idVehiculos");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoKm_idVehiculos",
                table: "VehiculoKm",
                column: "idVehiculos");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoRepostaje_idEmpresas",
                table: "VehiculoRepostaje",
                column: "idEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoRepostaje_idTipoCombustible",
                table: "VehiculoRepostaje",
                column: "idTipoCombustible");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoRepostaje_idVehiculos",
                table: "VehiculoRepostaje",
                column: "idVehiculos");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaComercial_idEstablecimiento",
                table: "VisitaComercial",
                column: "idEstablecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaComercial_idMotivoVisitaComercial",
                table: "VisitaComercial",
                column: "idMotivoVisitaComercial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccionComercial");

            migrationBuilder.DropTable(
                name: "CacheImportesConceptosRecaudacion");

            migrationBuilder.DropTable(
                name: "Cambio");

            migrationBuilder.DropTable(
                name: "Carga");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "ConfigurationGEO");

            migrationBuilder.DropTable(
                name: "DesgloseBruto");

            migrationBuilder.DropTable(
                name: "EstablecimientoPotencialMaquina");

            migrationBuilder.DropTable(
                name: "GastoEstablecimiento");

            migrationBuilder.DropTable(
                name: "Instalacion");

            migrationBuilder.DropTable(
                name: "LecturaContadorAuto");

            migrationBuilder.DropTable(
                name: "LecturaDetalle");

            migrationBuilder.DropTable(
                name: "LecturaDetalleAuto");

            migrationBuilder.DropTable(
                name: "Localizacion");

            migrationBuilder.DropTable(
                name: "MaquinaConceptoRecaudacion");

            migrationBuilder.DropTable(
                name: "ModuloCaptura");

            migrationBuilder.DropTable(
                name: "MotivoMaquinaNoRecaudada");

            migrationBuilder.DropTable(
                name: "PrestamoRecuperacion");

            migrationBuilder.DropTable(
                name: "RecaudacionDetalles");

            migrationBuilder.DropTable(
                name: "TipoDistribucionConceptoRecaudacion");

            migrationBuilder.DropTable(
                name: "UltimaLecturaRecaudacion");

            migrationBuilder.DropTable(
                name: "VehiculoCombustible");

            migrationBuilder.DropTable(
                name: "VehiculoKm");

            migrationBuilder.DropTable(
                name: "VehiculoRepostaje");

            migrationBuilder.DropTable(
                name: "VisitaComercial");

            migrationBuilder.DropTable(
                name: "MotivoAccionComercial");

            migrationBuilder.DropTable(
                name: "TipoAccionComercial");

            migrationBuilder.DropTable(
                name: "CacheImportes");

            migrationBuilder.DropTable(
                name: "Averia");

            migrationBuilder.DropTable(
                name: "EmpresaCompetidora");

            migrationBuilder.DropTable(
                name: "EstablecimientoPotencial");

            migrationBuilder.DropTable(
                name: "ModeloMaquina");

            migrationBuilder.DropTable(
                name: "ConceptoGastoEstablecimiento");

            migrationBuilder.DropTable(
                name: "LecturaContador");

            migrationBuilder.DropTable(
                name: "PatronesContadoresDetalle");

            migrationBuilder.DropTable(
                name: "ConceptoMotivoMaquinaNoRecaudada");

            migrationBuilder.DropTable(
                name: "Prestamo");

            migrationBuilder.DropTable(
                name: "Recaudacion");

            migrationBuilder.DropTable(
                name: "PatContDetalle");

            migrationBuilder.DropTable(
                name: "TipoCombustible");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "MotivoVisitaComercial");

            migrationBuilder.DropTable(
                name: "AveriaEstado");

            migrationBuilder.DropTable(
                name: "ConceptoAveria");

            migrationBuilder.DropTable(
                name: "Poblacion");

            migrationBuilder.DropTable(
                name: "ConceptoPrestamo");

            migrationBuilder.DropTable(
                name: "Incidencia");

            migrationBuilder.DropTable(
                name: "Ruta");

            migrationBuilder.DropTable(
                name: "Maquina");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Establecimientos");

            migrationBuilder.DropTable(
                name: "PatronesContador");
        }
    }
}
