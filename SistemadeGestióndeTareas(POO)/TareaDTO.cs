using System;

namespace Ejercicio02POO
{
    public class TareaDTO
    {
        // Discriminador para identificar el tipo de tarea
        public string Tipo { get; set; }

        // Propiedades comunes
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public Prioridad Prioridad { get; set; }

        public string Categoria { get; set; }

        public bool Completada { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Solo para TareaConVencimiento
        public DateTime? FechaVencimiento { get; set; }
    }
}