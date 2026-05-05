using System;
using System.Collections.Generic;
using System.Linq;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private List<TaskItem> _tasks = new List<TaskItem>();

        public void CrearTarea(string titulo, string descripcion, string responsable)
        {
            var tarea = new TaskItem
            {
                Id = _tasks.Count + 1,
                Titulo = titulo,
                Descripcion = descripcion,
                Responsable = responsable,
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now
            };
            _tasks.Add(tarea);
            Console.WriteLine($"\n Tarea '{titulo}' creada correctamente con ID {tarea.Id}");
        }

        public void ListarTareas(string filtro = "todas")
        {
            var tareasFiltradas = filtro.ToLower() switch
            {
                "pendiente"   => _tasks.Where(t => t.Estado == "Pendiente").ToList(),
                "en progreso" => _tasks.Where(t => t.Estado == "En progreso").ToList(),
                "completada"  => _tasks.Where(t => t.Estado == "Completada").ToList(),
                _             => _tasks
            };

            if (!tareasFiltradas.Any())
            {
                Console.WriteLine("\nNo hay tareas para mostrar.");
                return;
            }

            Console.WriteLine("\n===== LISTADO DE TAREAS =====");
            foreach (var t in tareasFiltradas)
            {
                Console.WriteLine($"ID: {t.Id} | Titulo: {t.Titulo} | Responsable: {t.Responsable} | Estado: {t.Estado}");
                Console.WriteLine($"   Creada: {t.FechaCreacion:dd/MM/yyyy HH:mm} | Modificada: {(t.FechaActualizacion.HasValue ? t.FechaActualizacion.Value.ToString("dd/MM/yyyy HH:mm") : "Sin modificaciones")}");
                Console.WriteLine(new string('-', 60));
            }
        }

        public void EliminarTarea(int id)
        {
            var tarea = _tasks.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"\n✗ No se encontró una tarea con ID {id}.");
                return;
            }
            _tasks.Remove(tarea);
            Console.WriteLine($"\n✓ Tarea '{tarea.Titulo}' eliminada correctamente.");
        }
    }
}