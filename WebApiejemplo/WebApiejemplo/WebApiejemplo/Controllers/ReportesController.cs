using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiejemplo.Services;

namespace WebApiejemplo.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        // ==================== REPORTES PDF ====================

        [HttpGet("ingresos-pdf")]
        public async Task<IActionResult> GenerarReporteIngresosPdf(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromQuery] string? tipo)
        {
            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio debe ser menor a la fecha fin");

            var pdf = await _reporteService.GenerarReporteIngresosPdfAsync(fechaInicio, fechaFin, tipo);
            return File(pdf, "application/pdf", $"Reporte_Ingresos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        [HttpGet("residentes-pdf")]
        public async Task<IActionResult> GenerarReporteResidentesPdf(
            [FromQuery] int? torreId,
            [FromQuery] string? tipoResidente)
        {
            var pdf = await _reporteService.GenerarReporteResidentesPdfAsync(torreId, tipoResidente);
            return File(pdf, "application/pdf", $"Reporte_Residentes_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        [HttpGet("mantenimientos-pdf")]
        public async Task<IActionResult> GenerarReporteMantenimientosPdf(
            [FromQuery] string? estado,
            [FromQuery] int? tipoMantenimientoId)
        {
            var pdf = await _reporteService.GenerarReporteMantenimientosPdfAsync(estado, tipoMantenimientoId);
            return File(pdf, "application/pdf", $"Reporte_Mantenimientos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        [HttpGet("reservas-pdf")]
        public async Task<IActionResult> GenerarReporteReservasPdf(
            [FromQuery] int? zonaComunId,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio debe ser menor a la fecha fin");

            var pdf = await _reporteService.GenerarReporteReservasPdfAsync(zonaComunId, fechaInicio, fechaFin);
            return File(pdf, "application/pdf", $"Reporte_Reservas_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        [HttpGet("visitantes-parqueadero-pdf")]
        public async Task<IActionResult> GenerarReporteVisitantesParqueaderoPdf(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio debe ser menor a la fecha fin");

            var pdf = await _reporteService.GenerarReporteVisitantesParqueaderoPdfAsync(fechaInicio, fechaFin);
            return File(pdf, "application/pdf", $"Reporte_Visitantes_Parqueadero_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        // ==================== REPORTES EXCEL ====================

        [HttpGet("ocupacion-parqueadero-excel")]
        public async Task<IActionResult> GenerarReporteOcupacionParqueaderoExcel()
        {
            var excel = await _reporteService.GenerarReporteOcupacionParqueaderoExcelAsync();
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Reporte_Ocupacion_Parqueadero_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet("mensajeria-excel")]
        public async Task<IActionResult> GenerarReporteMensajeriaExcel(
            [FromQuery] int? apartamentId,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio debe ser menor a la fecha fin");

            var excel = await _reporteService.GenerarReporteMensajeriaExcelAsync(apartamentId, fechaInicio, fechaFin);
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Reporte_Mensajeria_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet("ingresos-por-tipo-excel")]
        public async Task<IActionResult> GenerarReporteIngresosPorTipoExcel()
        {
            var excel = await _reporteService.GenerarReporteIngresosPorTipoExcelAsync();
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Reporte_Ingresos_Por_Tipo_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet("zonas-comunes-excel")]
        public async Task<IActionResult> GenerarReporteZonasComunesExcel(
            [FromQuery] int? zonaComunId,
            [FromQuery] int? mes,
            [FromQuery] int? año)
        {
            var excel = await _reporteService.GenerarReporteZonasComunesExcelAsync(zonaComunId, mes, año);
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Reporte_Zonas_Comunes_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet("usuarios-por-rol-excel")]
        public async Task<IActionResult> GenerarReporteUsuariosPorRolExcel()
        {
            var excel = await _reporteService.GenerarReporteUsuariosPorRolExcelAsync();
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Reporte_Usuarios_Por_Rol_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }
    }
}
