using tp7.Models;

namespace tp7.DataModels;

public class ManejoDeDatos {
    private AccesoADatos datos;
    private List<Tarea> tareas;

    public ManejoDeDatos(AccesoADatos acceso) {
        datos = acceso; 
        tareas = new List<Tarea>(); 
    }

    public bool CrearTarea(Tarea tarea) {
        tareas = datos.Read();
        tareas.Add(tarea);
        tarea.Id = tareas.Count();
        datos.Save(tareas);
        return true;
    }

    public bool ActualizarTarea(Tarea tarea) {
        tareas = datos.Read();
        var tareaSeleccionada = tareas.FirstOrDefault(t => t.Id == tarea.Id);
        tareas.Remove(tareaSeleccionada);
        if(tareaSeleccionada == null)
            return false;
        tareaSeleccionada = tarea;
        tareas.Add(tareaSeleccionada);
        datos.Save(tareas);
        return true;
    }

    public bool EliminarTarea(int id) {
        tareas = datos.Read();
        var tareaSeleccionada = tareas.FirstOrDefault(t => t.Id == id);
        if(tareas.Remove(tareaSeleccionada)) {
            datos.Save(tareas);
            return true;
        }  
        return false;
    }

    public List<Tarea> GetTareasCompletadas() {
        tareas = datos.Read();
        var tareasCompletadas = tareas.Where(t => t.Estado == Estado.Completada).ToList();
        return tareasCompletadas;
    }

    public List<Tarea> GetTareas() => datos.Read();
    public Tarea ObtenerTarea(int id) {
        tareas = datos.Read();
        return tareas.FirstOrDefault(t => t.Id == id);
    }
}