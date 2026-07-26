using System;

namespace Ejercicio02POO
{
    public class TareaConVencimiento : Tarea
    {
        // Fecha límite de la tarea
        public DateTime FechaVencimiento { get; set; }

        // Propiedad calculada (solo lectura)
        public int DiasRestantes
        {
            get
            {
                return (FechaVencimiento.Date - DateTime.Now.Date).Days;
            }
        }

        // Constructor que llama al constructor de la clase base
        public TareaConVencimiento(
            string titulo,
            string descripcion,
            Prioridad prioridad,
            string categoria,
            DateTime fechaVencimiento)
            : base(titulo, descripcion, prioridad, categoria)
        {
            FechaVencimiento = fechaVencimiento;
        }

        // Constructor vacío para facilitar la deserialización si es necesario
        public TareaConVencimiento()
        {
        }

        // Sobrescribe el método MostrarInfo()
        public override void MostrarInfo()
        {
            base.MostrarInfo();

            Console.WriteLine($"Fecha de vencimiento: {FechaVencimiento:dd/MM/yyyy}");

            if (DiasRestantes > 0)
            {
                Console.WriteLine($"Días restantes: {DiasRestantes}");
            }
            else if (DiasRestantes == 0)
            {
                Console.WriteLine("La tarea vence hoy.");
            }
            else
            {
                Console.WriteLine($"Vencida hace {Math.Abs(DiasRestantes)} día(s).");
            }
        }

        // También sobrescribimos Exportar para incluir el tipo de tarea
        public override string Exportar()
        {
            return $"{Id}|{Titulo}|{Prioridad}|{Completada}|{FechaVencimiento:dd/MM/yyyy}";
        }
    }
}