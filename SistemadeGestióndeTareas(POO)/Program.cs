using System;
using System.Collections.Generic;

namespace Ejercicio02POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestorTareas gestor = new GestorTareas();

            // Cargar tareas si existe el archivo
            gestor.CargarDeJSON("tareas.json");

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("      GESTOR DE TAREAS");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Listar todas");
                Console.WriteLine("3. Listar por categoría");
                Console.WriteLine("4. Listar por prioridad");
                Console.WriteLine("5. Marcar como completada");
                Console.WriteLine("6. Mostrar tareas vencidas");
                Console.WriteLine("7. Eliminar tarea");
                Console.WriteLine("8. Exportar a JSON");
                Console.WriteLine("9. Salir");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AgregarTarea(gestor);
                        break;

                    case "2":
                        ListarTodas(gestor);
                        break;

                    case "3":
                        ListarCategoria(gestor);
                        break;

                    case "4":
                        ListarPrioridad(gestor);
                        break;

                    case "5":
                        CompletarTarea(gestor);
                        break;

                    case "6":
                        MostrarVencidas(gestor);
                        break;

                    case "7":
                        EliminarTarea(gestor);
                        break;

                    case "8":
                        gestor.GuardarEnJSON("tareas.json");
                        Console.WriteLine("\nArchivo exportado correctamente.");
                        Pausa();
                        break;

                    case "9":
                        gestor.GuardarEnJSON("tareas.json");
                        salir = true;
                        Console.WriteLine("\nDatos guardados.");
                        break;

                    default:
                        Console.WriteLine("\nOpción inválida.");
                        Pausa();
                        break;
                }
            }
        }

        static void AgregarTarea(GestorTareas gestor)
        {
            Console.Clear();

            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            Console.Write("Categoría: ");
            string categoria = Console.ReadLine();

            Console.WriteLine("\nPrioridad:");
            Console.WriteLine("1. Baja");
            Console.WriteLine("2. Media");
            Console.WriteLine("3. Alta");
            Console.WriteLine("4. Crítica");
            Console.Write("Seleccione: ");

            Prioridad prioridad = (Prioridad)(Convert.ToInt32(Console.ReadLine()) - 1);

            Console.Write("\n¿Tiene fecha de vencimiento? (S/N): ");
            string respuesta = Console.ReadLine().ToUpper();

            if (respuesta == "S")
            {
                Console.Write("Fecha (dd/MM/yyyy): ");
                DateTime fecha = DateTime.Parse(Console.ReadLine());

                gestor.Agregar(new TareaConVencimiento(
                    titulo,
                    descripcion,
                    prioridad,
                    categoria,
                    fecha));
            }
            else
            {
                gestor.Agregar(new Tarea(
                    titulo,
                    descripcion,
                    prioridad,
                    categoria));
            }

            Console.WriteLine("\nTarea agregada correctamente.");
            Pausa();
        }

        static void ListarTodas(GestorTareas gestor)
        {
            Console.Clear();

            List<Tarea> tareas = gestor.ObtenerTodas();

            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas.");
            }
            else
            {
                Console.WriteLine("=== LISTADO POLIMÓRFICO ===\n");

                foreach (Tarea tarea in tareas)
                {
                    tarea.MostrarInfo();
                    Console.WriteLine(new string('-', 40));
                }
            }

            Pausa();
        }

        static void ListarCategoria(GestorTareas gestor)
        {
            Console.Clear();

            Console.Write("Categoría: ");
            string categoria = Console.ReadLine();

            var lista = gestor.ListarPorCategoria(categoria);

            foreach (var tarea in lista)
            {
                tarea.MostrarInfo();
                Console.WriteLine();
            }

            Pausa();
        }

        static void ListarPrioridad(GestorTareas gestor)
        {
            Console.Clear();

            Console.WriteLine("1. Baja");
            Console.WriteLine("2. Media");
            Console.WriteLine("3. Alta");
            Console.WriteLine("4. Crítica");

            Console.Write("Seleccione: ");

            Prioridad prioridad = (Prioridad)(Convert.ToInt32(Console.ReadLine()) - 1);

            var lista = gestor.ListarPorPrioridad(prioridad);

            foreach (var tarea in lista)
            {
                tarea.MostrarInfo();
                Console.WriteLine();
            }

            Pausa();
        }

        static void CompletarTarea(GestorTareas gestor)
        {
            Console.Clear();

            Console.Write("ID de la tarea: ");
            int id = Convert.ToInt32(Console.ReadLine());

            gestor.Completar(id);

            Console.WriteLine("\nTarea completada.");
            Pausa();
        }

        static void MostrarVencidas(GestorTareas gestor)
        {
            Console.Clear();

            var lista = gestor.ObtenerVencidas();

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay tareas vencidas.");
            }
            else
            {
                foreach (var tarea in lista)
                {
                    tarea.MostrarInfo();
                    Console.WriteLine();
                }
            }

            Pausa();
        }

        static void EliminarTarea(GestorTareas gestor)
        {
            Console.Clear();

            Console.Write("ID de la tarea: ");
            int id = Convert.ToInt32(Console.ReadLine());

            gestor.Eliminar(id);

            Console.WriteLine("\nTarea eliminada.");
            Pausa();
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}