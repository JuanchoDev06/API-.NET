using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using WebApiejemplo.Data;
using WebApiejemplo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApiejemplo.Services
{
    public class ReporteService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReporteService> _logger;

        public ReporteService(ApplicationDbContext context, ILogger<ReporteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region REPORTES PDF

        public async Task<byte[]> GenerarReporteIngresosPdfAsync(DateTime fechaInicio, DateTime fechaFin, string? tipo)
        {
            try
            {
                var ingresos = await _context.Ingresos
                    .Include(i => i.Vigilante)
                    .Include(i => i.Unidad)
                    .Where(i => i.FechaHoraIngreso.Date >= fechaInicio.Date && i.FechaHoraIngreso.Date <= fechaFin.Date)
                    .Where(i => string.IsNullOrEmpty(tipo) || i.Tipo == tipo)
                    .OrderByDescending(i => i.FechaHoraIngreso)
                    .ToListAsync();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.Header().Column(col =>
                        {
                            col.Item().Text("REPORTE DE INGRESOS").FontSize(18).Bold().AlignCenter();
                            col.Item().Text($"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}").FontSize(10).AlignCenter();
                            col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Darken3).AlignCenter();
                            if (!string.IsNullOrEmpty(tipo))
                                col.Item().Text($"Tipo: {tipo}").FontSize(10).AlignCenter();
                            col.Item().PaddingTop(10).LineHorizontal(0.5f);
                        });

                        page.Content().Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1.5f);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Persona").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Tipo").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Documento").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Ingreso").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Vigilante").FontColor(Colors.White).Bold();
                                });

                                foreach (var ingreso in ingresos)
                                {
                                    table.Cell().Padding(5).Text(ingreso.NombrePersona ?? "-");
                                    table.Cell().Padding(5).Text(ingreso.Tipo ?? "-");
                                    table.Cell().Padding(5).Text(ingreso.Documento ?? "-");
                                    table.Cell().Padding(5).Text(ingreso.FechaHoraIngreso.ToString("dd/MM/yyyy HH:mm"));
                                    table.Cell().Padding(5).Text(ingreso.Vigilante?.Nombre ?? "-");
                                }
                            });

                            col.Item().PaddingTop(10).Text($"Total de registros: {ingresos.Count}").FontSize(9).Bold();
                        });

                        page.Footer().AlignCenter().Text("© Gestión Residencial").FontSize(8).FontColor(Colors.Grey.Darken3);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte ingresos PDF: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteResidentesPdfAsync(int? torreId, string? tipoResidente)
        {
            try
            {
                var residentes = await _context.ResidentesUnidad
                    .Include(r => r.Unidad)
                    .ThenInclude(u => u.Torre)
                    .Include(r => r.Usuario)
                    .Where(r => torreId == null || r.Unidad.TorreId == torreId)
                    .OrderBy(r => r.Unidad.Torre!.Nombre)
                    .ThenBy(r => r.Unidad.Numero)
                    .ToListAsync();

                // Filtrar por tipo de residente (Propietario/Inquilino) en memoria
                if (!string.IsNullOrEmpty(tipoResidente))
                {
                    bool esPropietario = tipoResidente.Equals("Propietario", StringComparison.OrdinalIgnoreCase);
                    residentes = residentes.Where(r => r.EsPropietario == esPropietario).ToList();
                }

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.Header().Column(col =>
                        {
                            col.Item().Text("REPORTE DE RESIDENTES").FontSize(18).Bold().AlignCenter();
                            col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Darken3).AlignCenter();
                            if (torreId.HasValue && residentes.Count > 0)
                                col.Item().Text($"Torre: {residentes.First().Unidad?.Torre?.Nombre ?? "N/A"}").FontSize(10).AlignCenter();
                            if (!string.IsNullOrEmpty(tipoResidente))
                                col.Item().Text($"Tipo: {tipoResidente}").FontSize(10).AlignCenter();
                            col.Item().PaddingTop(10).LineHorizontal(0.5f);
                        });

                        page.Content().Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Unidad").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Residente").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Documento").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Tipo").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Torre").FontColor(Colors.White).Bold();
                                });

                                foreach (var residente in residentes)
                                {
                                    table.Cell().Padding(5).Text(residente.Unidad?.Numero ?? "-");
                                    table.Cell().Padding(5).Text(residente.Usuario?.Nombre ?? "-");
                                    table.Cell().Padding(5).Text(residente.Usuario?.Documento ?? "-");
                                    table.Cell().Padding(5).Text(residente.EsPropietario ? "Propietario" : "Inquilino");
                                    table.Cell().Padding(5).Text(residente.Unidad?.Torre?.Nombre ?? "-");
                                }
                            });

                            col.Item().PaddingTop(10).Text($"Total de residentes: {residentes.Count}").FontSize(9).Bold();
                        });

                        page.Footer().AlignCenter().Text("© Gestión Residencial").FontSize(8).FontColor(Colors.Grey.Darken3);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte residentes PDF: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteMantenimientosPdfAsync(string? estado, int? tipoMantenimientoId)
        {
            try
            {
                var mantenimientos = await _context.Mantenimientos
                    .Include(m => m.TipoMantenimiento)
                    .Include(m => m.ZonaComun)
                    .Where(m => string.IsNullOrEmpty(estado) || m.Estado == estado)
                    .Where(m => tipoMantenimientoId == null || m.TipoMantenimientoId == tipoMantenimientoId)
                    .OrderByDescending(m => m.Fecha)
                    .ToListAsync();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.Header().Column(col =>
                        {
                            col.Item().Text("REPORTE DE MANTENIMIENTOS").FontSize(18).Bold().AlignCenter();
                            col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Darken3).AlignCenter();
                            if (!string.IsNullOrEmpty(estado))
                                col.Item().Text($"Estado: {estado}").FontSize(10).AlignCenter();
                            col.Item().PaddingTop(10).LineHorizontal(0.5f);
                        });

                        page.Content().Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Tipo").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Zona Común").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Fecha").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Estado").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Costo").FontColor(Colors.White).Bold();
                                });

                                foreach (var mant in mantenimientos)
                                {
                                    table.Cell().Padding(5).Text(mant.TipoMantenimiento?.Nombre ?? "-");
                                    table.Cell().Padding(5).Text(mant.ZonaComun?.Nombre ?? "-");
                                    table.Cell().Padding(5).Text(mant.Fecha.ToString("dd/MM/yyyy"));
                                    table.Cell().Padding(5).Text(mant.Estado ?? "-").FontColor(GetColorEstado(mant.Estado));
                                    table.Cell().Padding(5).Text($"${mant.Costo?.ToString("N2") ?? "-"}");
                                }
                            });

                            col.Item().PaddingTop(10).Text($"Total de registros: {mantenimientos.Count}").FontSize(9).Bold();
                        });

                        page.Footer().AlignCenter().Text("© Gestión Residencial").FontSize(8).FontColor(Colors.Grey.Darken3);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte mantenimientos PDF: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteReservasPdfAsync(int? zonaComunId, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var reservas = await _context.Reservas
                    .Include(r => r.ZonaComun)
                    .Include(r => r.Usuario)
                    .Where(r => r.Fecha >= fechaInicio && r.Fecha <= fechaFin)
                    .Where(r => zonaComunId == null || r.ZonaComunId == zonaComunId)
                    .OrderByDescending(r => r.Fecha)
                    .ToListAsync();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.Header().Column(col =>
                        {
                            col.Item().Text("REPORTE DE RESERVAS").FontSize(18).Bold().AlignCenter();
                            col.Item().Text($"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}").FontSize(10).AlignCenter();
                            col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Darken3).AlignCenter();
                            col.Item().PaddingTop(10).LineHorizontal(0.5f);
                        });

                        page.Content().Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Zona Común").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Residente").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Fecha").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Hora").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Estado").FontColor(Colors.White).Bold();
                                });

                                foreach (var res in reservas)
                                {
                                    table.Cell().Padding(5).Text(res.ZonaComun?.Nombre ?? "-");
                                    table.Cell().Padding(5).Text(res.Usuario?.Nombre ?? "-");
                                    table.Cell().Padding(5).Text(res.Fecha.ToString("dd/MM/yyyy"));
                                    table.Cell().Padding(5).Text($"{res.HoraInicio} - {res.HoraFin}");
                                    table.Cell().Padding(5).Text(res.Estado ?? "-");
                                }
                            });

                            col.Item().PaddingTop(10).Text($"Total de reservas: {reservas.Count}").FontSize(9).Bold();
                        });

                        page.Footer().AlignCenter().Text("© Gestión Residencial").FontSize(8).FontColor(Colors.Grey.Darken3);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte reservas PDF: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteVisitantesParqueaderoPdfAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var visitantes = await _context.ParqueaderosVisitantes
                    .Include(p => p.Parqueadero)
                    .Where(p => p.FechaHoraIngreso.Date >= fechaInicio.Date && p.FechaHoraIngreso.Date <= fechaFin.Date)
                    .OrderByDescending(p => p.FechaHoraIngreso)
                    .ToListAsync();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.Header().Column(col =>
                        {
                            col.Item().Text("REPORTE DE VISITANTES - PARQUEADERO").FontSize(18).Bold().AlignCenter();
                            col.Item().Text($"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}").FontSize(10).AlignCenter();
                            col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Darken3).AlignCenter();
                            col.Item().PaddingTop(10).LineHorizontal(0.5f);
                        });

                        page.Content().Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Placa").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Parqueadero").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Ingreso").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Salida").FontColor(Colors.White).Bold();
                                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Duración").FontColor(Colors.White).Bold();
                                });

                                foreach (var visit in visitantes)
                                {
                                    var duracion = visit.FechaHoraSalida.HasValue 
                                        ? (visit.FechaHoraSalida.Value - visit.FechaHoraIngreso).TotalHours.ToString("N1") + "h"
                                        : "En curso";

                                    table.Cell().Padding(5).Text(visit.Placa ?? "-");
                                    table.Cell().Padding(5).Text(visit.Parqueadero?.Numero ?? "-");
                                    table.Cell().Padding(5).Text(visit.FechaHoraIngreso.ToString("dd/MM/yyyy HH:mm"));
                                    table.Cell().Padding(5).Text(visit.FechaHoraSalida?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");
                                    table.Cell().Padding(5).Text(duracion);
                                }
                            });

                            col.Item().PaddingTop(10).Text($"Total de registros: {visitantes.Count}").FontSize(9).Bold();
                        });

                        page.Footer().AlignCenter().Text("© Gestión Residencial").FontSize(8).FontColor(Colors.Grey.Darken3);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte visitantes PDF: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region REPORTES EXCEL

        public async Task<byte[]> GenerarReporteOcupacionParqueaderoExcelAsync()
        {
            try
            {
                var parqueaderos = await _context.Parqueaderos
                    .Include(p => p.Unidad)
                    .ThenInclude(u => u.Torre)
                    .ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Ocupación Parqueadero");

                    // Headers
                    var header = worksheet.Row(1);
                    header.Cell(1).Value = "Torre";
                    header.Cell(2).Value = "Tipo";
                    header.Cell(3).Value = "Cupos Totales";
                    header.Cell(4).Value = "Cupos Asignados";
                    header.Cell(5).Value = "Cupos Disponibles";
                    header.Cell(6).Value = "% Ocupación";

                    header.Style.Fill.BackgroundColor = XLColor.Blue;
                    header.Style.Font.FontColor = XLColor.White;
                    header.Style.Font.Bold = true;

                    int row = 2;
                    decimal totalCupos = 0;
                    decimal totalAsignados = 0;

                    var agrupadosPorTipo = parqueaderos
                        .GroupBy(p => new { Torre = p.Unidad?.Torre?.Nombre ?? "Sin Torre", Tipo = p.Tipo });

                    foreach (var grupo in agrupadosPorTipo)
                    {
                        int cuposGrupo = grupo.Count();
                        int asignados = grupo.Count(p => p.UnidadId.HasValue);
                        int disponibles = cuposGrupo - asignados;
                        decimal porcentajeOcupacion = cuposGrupo > 0 ? (decimal)asignados / cuposGrupo * 100 : 0;

                        worksheet.Cell(row, 1).Value = grupo.Key.Torre;
                        worksheet.Cell(row, 2).Value = grupo.Key.Tipo;
                        worksheet.Cell(row, 3).Value = cuposGrupo;
                        worksheet.Cell(row, 4).Value = asignados;
                        worksheet.Cell(row, 5).Value = disponibles;
                        worksheet.Cell(row, 6).Value = porcentajeOcupacion.ToString("N2") + "%";

                        totalCupos += cuposGrupo;
                        totalAsignados += asignados;
                        row++;
                    }

                    // Fila de totales
                    worksheet.Cell(row, 1).Value = "TOTAL";
                    worksheet.Cell(row, 1).Style.Font.Bold = true;
                    worksheet.Cell(row, 3).Value = totalCupos;
                    worksheet.Cell(row, 3).Style.Font.Bold = true;
                    worksheet.Cell(row, 4).Value = totalAsignados;
                    worksheet.Cell(row, 4).Style.Font.Bold = true;
                    worksheet.Cell(row, 5).Value = totalCupos - totalAsignados;
                    worksheet.Cell(row, 5).Style.Font.Bold = true;
                    decimal totalPorcentaje = totalCupos > 0 ? totalAsignados / totalCupos * 100 : 0;
                    worksheet.Cell(row, 6).Value = totalPorcentaje.ToString("N2") + "%";
                    worksheet.Cell(row, 6).Style.Font.Bold = true;

                    // Ajustar ancho de columnas
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte ocupación parqueadero: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteMensajeriaExcelAsync(int? apartamentId, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var mensajeria = await _context.Mensajerias
                    .Include(m => m.Unidad)
                    .Include(m => m.Vigilante)
                    .Where(m => m.FechaRecepcion.Date >= fechaInicio.Date && m.FechaRecepcion.Date <= fechaFin.Date)
                    .Where(m => apartamentId == null || m.UnidadId == apartamentId)
                    .OrderByDescending(m => m.FechaRecepcion)
                    .ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Mensajería");

                    // Headers
                    var header = worksheet.Row(1);
                    header.Cell(1).Value = "Apartamento";
                    header.Cell(2).Value = "Empresa";
                    header.Cell(3).Value = "Guía";
                    header.Cell(4).Value = "Fecha Recepción";
                    header.Cell(5).Value = "Fecha Entrega";
                    header.Cell(6).Value = "Estado";

                    header.Style.Fill.BackgroundColor = XLColor.Blue;
                    header.Style.Font.FontColor = XLColor.White;
                    header.Style.Font.Bold = true;

                    int row = 2;
                    int entregados = 0;
                    int pendientes = 0;

                    foreach (var msg in mensajeria)
                    {
                        worksheet.Cell(row, 1).Value = msg.Unidad?.Numero;
                        worksheet.Cell(row, 2).Value = msg.Empresa ?? "-";
                        worksheet.Cell(row, 3).Value = msg.Guia ?? "-";
                        worksheet.Cell(row, 4).Value = msg.FechaRecepcion.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(row, 5).Value = msg.FechaEntrega?.ToString("dd/MM/yyyy HH:mm") ?? "-";
                        worksheet.Cell(row, 6).Value = msg.FechaEntrega.HasValue ? "Entregado" : "Pendiente";

                        if (msg.FechaEntrega.HasValue) entregados++;
                        else pendientes++;

                        row++;
                    }

                    // Totales
                    worksheet.Cell(row, 1).Value = "TOTAL";
                    worksheet.Cell(row, 1).Style.Font.Bold = true;
                    worksheet.Cell(row, 5).Value = $"Entregados: {entregados}";
                    worksheet.Cell(row, 5).Style.Font.Bold = true;
                    worksheet.Cell(row + 1, 5).Value = $"Pendientes: {pendientes}";
                    worksheet.Cell(row + 1, 5).Style.Font.Bold = true;

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte mensajería: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteIngresosPorTipoExcelAsync()
        {
            try
            {
                var ingresos = await _context.Ingresos
                    .ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Ingresos por Tipo");

                    // Headers
                    var header = worksheet.Row(1);
                    header.Cell(1).Value = "Tipo Ingreso";
                    header.Cell(2).Value = "Conteo";
                    header.Cell(3).Value = "Porcentaje";

                    header.Style.Fill.BackgroundColor = XLColor.Blue;
                    header.Style.Font.FontColor = XLColor.White;
                    header.Style.Font.Bold = true;

                    var agrupadosPorTipo = ingresos.GroupBy(i => i.Tipo)
                        .Select(g => new { Tipo = g.Key, Conteo = g.Count() })
                        .OrderByDescending(x => x.Conteo);

                    int row = 2;
                    int totalIngresos = ingresos.Count;

                    foreach (var grupo in agrupadosPorTipo)
                    {
                        decimal porcentaje = totalIngresos > 0 ? (decimal)grupo.Conteo / totalIngresos * 100 : 0;
                        worksheet.Cell(row, 1).Value = grupo.Tipo;
                        worksheet.Cell(row, 2).Value = grupo.Conteo;
                        worksheet.Cell(row, 3).Value = porcentaje.ToString("N2") + "%";
                        row++;
                    }

                    // Fila de total
                    worksheet.Cell(row, 1).Value = "TOTAL";
                    worksheet.Cell(row, 1).Style.Font.Bold = true;
                    worksheet.Cell(row, 2).Value = totalIngresos;
                    worksheet.Cell(row, 2).Style.Font.Bold = true;
                    worksheet.Cell(row, 3).Value = "100%";
                    worksheet.Cell(row, 3).Style.Font.Bold = true;

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte ingresos por tipo: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteZonasComunesExcelAsync(int? zonaComunId, int? mes, int? año)
        {
            try
            {
                var reservas = await _context.Reservas
                    .Include(r => r.ZonaComun)
                    .ToListAsync();

                if (mes.HasValue && año.HasValue)
                {
                    reservas = reservas.Where(r => r.Fecha.Month == mes && r.Fecha.Year == año).ToList();
                }

                if (zonaComunId.HasValue)
                {
                    reservas = reservas.Where(r => r.ZonaComunId == zonaComunId).ToList();
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Zonas Comunes");

                    var header = worksheet.Row(1);
                    header.Cell(1).Value = "Zona Común";
                    header.Cell(2).Value = "Reservas";
                    header.Cell(3).Value = "Requiere Pago";
                    header.Cell(4).Value = "Valor Hora";

                    header.Style.Fill.BackgroundColor = XLColor.Blue;
                    header.Style.Font.FontColor = XLColor.White;
                    header.Style.Font.Bold = true;

                    var agrupadosPorZona = reservas.GroupBy(r => r.ZonaComun)
                        .Select(g => new
                        {
                            Zona = g.Key,
                            Reservas = g.Count()
                        });

                    int row = 2;

                    foreach (var grupo in agrupadosPorZona.OrderBy(x => x.Zona?.Nombre))
                    {
                        worksheet.Cell(row, 1).Value = grupo.Zona?.Nombre;
                        worksheet.Cell(row, 2).Value = grupo.Reservas;
                        worksheet.Cell(row, 3).Value = (grupo.Zona?.RequierePago == true) ? "Sí" : "No";
                        worksheet.Cell(row, 4).Value = grupo.Zona?.ValorHora.HasValue == true ? $"${grupo.Zona.ValorHora:N2}" : "-";
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte zonas comunes: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> GenerarReporteUsuariosPorRolExcelAsync()
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .Include(u => u.Rol)
                    .ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Usuarios por Rol");

                    var header = worksheet.Row(1);
                    header.Cell(1).Value = "Nombre";
                    header.Cell(2).Value = "Documento";
                    header.Cell(3).Value = "Email";
                    header.Cell(4).Value = "Rol";
                    header.Cell(5).Value = "Estado";
                    header.Cell(6).Value = "Fecha Creación";

                    header.Style.Fill.BackgroundColor = XLColor.Blue;
                    header.Style.Font.FontColor = XLColor.White;
                    header.Style.Font.Bold = true;

                    int row = 2;
                    var agrupadosPorRol = usuarios.GroupBy(u => u.Rol?.Nombre);

                    foreach (var grupo in agrupadosPorRol.OrderBy(x => x.Key))
                    {
                        foreach (var usuario in grupo.OrderBy(u => u.Nombre))
                        {
                            worksheet.Cell(row, 1).Value = usuario.Nombre;
                            worksheet.Cell(row, 2).Value = usuario.Documento;
                            worksheet.Cell(row, 3).Value = usuario.Email;
                            worksheet.Cell(row, 4).Value = usuario.Rol?.Nombre;
                            worksheet.Cell(row, 5).Value = usuario.Activo ? "Activo" : "Inactivo";
                            worksheet.Cell(row, 6).Value = usuario.FechaCreacion.ToString("dd/MM/yyyy");
                            row++;
                        }
                    }

                    // Resumen por rol
                    int resumenRow = row + 2;
                    worksheet.Cell(resumenRow, 1).Value = "RESUMEN POR ROL";
                    worksheet.Cell(resumenRow, 1).Style.Font.Bold = true;

                    resumenRow++;
                    var header2 = worksheet.Row(resumenRow);
                    header2.Cell(1).Value = "Rol";
                    header2.Cell(2).Value = "Activos";
                    header2.Cell(3).Value = "Inactivos";
                    header2.Cell(4).Value = "Total";
                    header2.Style.Fill.BackgroundColor = XLColor.LightGray;
                    header2.Style.Font.Bold = true;

                    resumenRow++;
                    foreach (var grupo in agrupadosPorRol.OrderBy(x => x.Key))
                    {
                        int activos = grupo.Count(u => u.Activo);
                        int inactivos = grupo.Count(u => !u.Activo);
                        worksheet.Cell(resumenRow, 1).Value = grupo.Key;
                        worksheet.Cell(resumenRow, 2).Value = activos;
                        worksheet.Cell(resumenRow, 3).Value = inactivos;
                        worksheet.Cell(resumenRow, 4).Value = activos + inactivos;
                        resumenRow++;
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generando reporte usuarios por rol: {ex.Message}");
                throw;
            }
        }

        #endregion

        private Color GetColorEstado(string? estado)
        {
            return estado switch
            {
                "Pendiente" => Colors.Orange.Medium,
                "En curso" => Colors.Blue.Medium,
                "Completado" => Colors.Green.Medium,
                _ => Colors.Grey.Medium
            };
        }
    }
}
