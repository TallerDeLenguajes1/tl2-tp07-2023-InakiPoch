using System.Text.Json;
using System.Text.Json.Serialization;
using tp7.Models;

namespace tp7.DataModels;

public class AccesoADatos {
    private static string route = "DataFiles/tareas.json";

    public List<Tarea> Read() {
        if(!File.Exists(route)) File.Create(route);
        using(var reader = new StreamReader(route)) {
            var data = reader.ReadToEnd();
            if(string.IsNullOrEmpty(data))
                return new List<Tarea>();
            var tareas = JsonSerializer.Deserialize<List<Tarea>>(data);
            return tareas;        
        }
    }

    public void Save(List<Tarea> tareas) {
        var data = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(route, data);
    }
}