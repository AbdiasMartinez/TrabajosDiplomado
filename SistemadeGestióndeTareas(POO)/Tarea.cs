using System;

namespace Ejercicio02POO
{
    public class Tarea : IExportable
    {
        // Contador para el ID autoincremental
        private static int contador = 1;

        // Propiedades
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public Prioridad Prioridad { get; set; }

        public string Categoria { get; set; }

        public bool Completada { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Constructor
        public Tarea(string titulo,
                     string descripcion,
                     Prioridad prioridad,
                     string categoria)
        {
            Id = contador++;
            Titulo = titulo;
            Descripcion = descripcion;
            Prioridad = prioridad;
            Categoria = categoria;
            Completada = false;
            FechaCreacion = DateTime.Now;
        }

        // Constructor vacío (necesario para algunas operaciones)
        public Tarea()
        {
        }

        // Método virtual para polimorfismo
        public virtual void MostrarInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Categoría: {Categoria}");
            Console.WriteLine($"Prioridad: {Prioridad}");
            Console.WriteLine($"Estado: {(Completada ? "Completada" : "Pendiente")}");
            Console.WriteLine($"Fecha creación: {FechaCreacion:dd/MM/yyyy HH:mm}");
        }

        // Implementación de la interfaz
        public virtual string Exportar()
        {
            return $"{Id}|{Titulo}|{Prioridad}|{Completada}";
        }

        // Actualiza el contador cuando se cargan tareas desde JSON
        public static void ActualizarContador(int ultimoId)
        {
            if (ultimoId >= contador)
            {
                contador = ultimoId + 1;
            }
        }
    }
}