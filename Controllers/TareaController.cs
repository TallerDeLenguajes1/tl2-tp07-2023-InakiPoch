using Microsoft.AspNetCore.Mvc;
using tp7.DataModels;
using tp7.Models;

namespace tp7.Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase {
    private readonly ILogger<TareaController> _logger;
    private ManejoDeDatos manejoTareas;

    public TareaController(ILogger<TareaController> logger) {
        var acceso = new AccesoADatos();
        manejoTareas = new ManejoDeDatos(acceso);
        _logger = logger;
    }

    [HttpPost("CrearTarea")]
    public ActionResult<Tarea> CrearTarea(Tarea tarea) {
        if(!manejoTareas.CrearTarea(tarea))
            return BadRequest("No se pudo crear la tarea");
        return Ok(tarea);
    }

    [HttpGet("GetTarea")]
    public ActionResult<Tarea> GetTarea(int id) {
        var tarea = manejoTareas.ObtenerTarea(id);
        if(tarea == null)
            return BadRequest("La tarea especificada no se encontro");
        return Ok(tarea);
    }

    [HttpPut("ActualizarTarea")]
    public ActionResult ActualizarTarea(Tarea tarea) {
        if(!manejoTareas.ActualizarTarea(tarea))
            return BadRequest("No se pudo actualizar la tarea");
        return Ok("Tarea actualizada con exito");
    }

    [HttpDelete("EliminarTarea")]
    public ActionResult EliminarTarea(int id) {
        if(!manejoTareas.EliminarTarea(id))
            return BadRequest("No se pudo eliminar la tarea");
        return Ok("Tarea eliminada con exito");
    }

    [HttpGet]
    public ActionResult<List<Tarea>> GetTareas() => Ok(manejoTareas.GetTareas());

    [HttpGet("GetTareasCompletadas")]
    public ActionResult<List<Tarea>> GetTareasCompletadas() => Ok(manejoTareas.GetTareasCompletadas());
}
