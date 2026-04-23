using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Services;

namespace TaskFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskService taskService = new TaskService();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== TaskFlow ===\n");
                Console.WriteLine("1. Crear tarea");
                Console.WriteLine("2. Listar tareas");
                Console.WriteLine("3. Salir");
                Console.Write("\nElegí una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== Crear nueva tarea ===\n");

                        Console.Write("Título (obligatorio): ");
                        string titulo = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(titulo))
                        {
                            Console.WriteLine("\n✗ El título es obligatorio.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Descripción (opcional): ");
                        string descripcion = Console.ReadLine();

                        Console.Write("Responsable: ");
                        string responsable = Console.ReadLine();

                        taskService.CrearTarea(titulo, descripcion, responsable);
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Listar tareas ===\n");
                        taskService.ListarTareas();
                        Console.ReadKey();
                        break;

                    case "3":
                        salir = true;
                    break;

                    default:
                        Console.WriteLine("\n✗ Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
