namespace Ejercicio02POO
{
    public class Categoria
    {
        // Propiedades
        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        // Constructor vacío
        public Categoria()
        {
        }

        // Constructor con parámetros
        public Categoria(string nombre, string color, string descripcion)
        {
            Nombre = nombre;
            Color = color;
            Descripcion = descripcion;
        }

        // Método para mostrar la información de la categoría
        public void MostrarInfo()
        {
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Color: {Color}");
            Console.WriteLine($"Descripción: {Descripcion}");
        }

        // Sobrescribe ToString()
        public override string ToString()
        {
            return Nombre;
        }
    }
}