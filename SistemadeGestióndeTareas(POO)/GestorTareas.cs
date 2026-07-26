using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Ejercicio02POO
{
    public class GestorTareas
    {
        private List<Tarea> tareas;

        public GestorTareas()
        {
            tareas = new List<Tarea>();
        }

        public void Agregar(Tarea tarea)
        {
            tareas.Add(tarea);
        }

        public List<Tarea> ObtenerTodas()
        {
            return tareas;
        }

        public void Completar(int id)
        {
            Tarea tarea = tareas.FirstOrDefault(t => t.Id == id);

            if (tarea != null)
            {
                tarea.Completada = true;
            }
        }

        public List<Tarea> ListarPorCategoria(string categoria)
        {
            return tareas
                .Where(t => t.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Tarea> ListarPorPrioridad(Prioridad prioridad)
        {
            return tareas
                .Where(t => t.Prioridad == prioridad)
                .ToList();
        }

        public List<Tarea> ObtenerVencidas()
        {
            return tareas
                .Where(t =>
                    t is TareaConVencimiento tv &&
                    !tv.Completada &&
                    tv.FechaVencimiento.Date < DateTime.Now.Date)
                .ToList();
        }

        public void Eliminar(int id)
        {
            Tarea tarea = tareas.FirstOrDefault(t => t.Id == id);

            if (tarea != null)
            {
                tareas.Remove(tarea);
            }
        }

        public void GuardarEnJSON(string archivo)
        {
            try
            {
                List<TareaDTO> datos = new List<TareaDTO>();

                foreach (Tarea tarea in tareas)
                {
                    TareaDTO dto = new TareaDTO
                    {
                        Tipo = tarea is TareaConVencimiento
                            ? "TareaConVencimiento"
                            : "Tarea",

                        Id = tarea.Id,
                        Titulo = tarea.Titulo,
                        Descripcion = tarea.Descripcion,
                        Categoria = tarea.Categoria,
                        Prioridad = tarea.Prioridad,
                        Completada = tarea.Completada,
                        FechaCreacion = tarea.FechaCreacion
                    };

                    if (tarea is TareaConVencimiento tv)
                    {
                        dto.FechaVencimiento = tv.FechaVencimiento;
                    }

                    datos.Add(dto);
                }

                JsonSerializerOptions opciones = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(datos, opciones);

                File.WriteAllText(archivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar:");
                Console.WriteLine(ex.Message);
            }
        }

        public List<Tarea> CargarDeJSON(string archivo)
        {
            tareas.Clear();

            try
            {
                if (!File.Exists(archivo))
                    return tareas;

                string json = File.ReadAllText(archivo);

                List<TareaDTO> datos =
                    JsonSerializer.Deserialize<List<TareaDTO>>(json);

                if (datos == null)
                    return tareas;

                foreach (TareaDTO dto in datos)
                {
                    Tarea tarea;

                    if (dto.Tipo == "TareaConVencimiento")
                    {
                        tarea = new TareaConVencimiento(
                            dto.Titulo,
                            dto.Descripcion,
                            dto.Prioridad,
                            dto.Categoria,
                            dto.FechaVencimiento.Value);
                    }
                    else
                    {
                        tarea = new Tarea(
                            dto.Titulo,
                            dto.Descripcion,
                            dto.Prioridad,
                            dto.Categoria);
                    }

                    tarea.Id = dto.Id;
                    tarea.Completada = dto.Completada;
                    tarea.FechaCreacion = dto.FechaCreacion;

                    tareas.Add(tarea);
                }

                // Actualiza el contador de IDs para que continúe correctamente
                if (tareas.Count > 0)
                {
                    Tarea.ActualizarContador(
                        tareas.Max(t => t.Id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No fue posible cargar el archivo JSON.");
                Console.WriteLine(ex.Message);
            }

            return tareas;
        }
    }
}