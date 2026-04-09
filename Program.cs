using System;
using System.Collections.Generic;

namespace TaskFlow
{
    class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        public Tarea(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaCreacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Descripción: {Descripcion}, Fecha de Creación: {FechaCreacion}";
        }
    }

    class GestorTareas
    {
        private int contador = 1;

        public void CrearTarea(string nombre, string descripcion)
        {
            Tarea nuevaTarea = new Tarea(contador++, nombre, descripcion);
            tareas.Add(nuevaTarea);
            Console.WriteLine("Tarea creada exitosamente.");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            GestorTareas gestor = new GestorTareas();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("----- Menú -----");
                Console.WriteLine("1. Crear Tarea");
                Console.WriteLine("2. Listar Tareas");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Ingrese el nombre de la tarea: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Ingrese la descripción de la tarea: ");
                        string descripcion = Console.ReadLine();
                        gestor.CrearTarea(nombre, descripcion);
                        break;

                    case "2":
                        gestor.ListarTareas();
                        break;

                    case "3":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
