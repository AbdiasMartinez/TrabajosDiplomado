# Ejercicio 2 – Sistema de Gestión de Tareas (POO)

## Descripción

Aplicación de consola desarrollada en C# utilizando Programación Orientada a Objetos (POO) para gestionar tareas.

El sistema permite crear tareas simples y tareas con fecha de vencimiento, clasificarlas por prioridad y categoría, marcarlas como completadas, eliminarlas y guardar la información en un archivo JSON.

---

## Funcionalidades

- Agregar tareas.
- Agregar tareas con fecha de vencimiento.
- Listar todas las tareas.
- Listar tareas por categoría.
- Listar tareas por prioridad.
- Marcar tareas como completadas.
- Mostrar tareas vencidas.
- Eliminar tareas.
- Exportar y guardar tareas en formato JSON.
- Cargar automáticamente las tareas al iniciar el programa.

---

## Conceptos de Programación Orientada a Objetos implementados

- Clases.
- Objetos.
- Encapsulamiento.
- Herencia.
- Polimorfismo.
- Interfaces.
- Constructores.
- Enumeraciones (enum).
- List<T>.
- LINQ.
- Serialización y deserialización JSON.
- Manejo de excepciones mediante try-catch.

---

## Estructura del proyecto

```
Ejercicio02POO
│
├── Program.cs
├── GestorTareas.cs
├── Tarea.cs
├── TareaConVencimiento.cs
├── Categoria.cs
├── Prioridad.cs
├── IExportable.cs
├── TareaDTO.cs
├── Ejercicio02POO.csproj
└── tareas.json
```

---

## Requisitos

- Visual Studio 2022 o superior.
- .NET 8.0 (o .NET 6.0 modificando el archivo `.csproj`).

---

## Ejecución

1. Abrir la carpeta del proyecto en Visual Studio.
2. Restaurar los paquetes NuGet si es necesario.
3. Compilar la solución.
4. Ejecutar el proyecto.

---

## Menú principal

```
=== GESTOR DE TAREAS ===

1. Agregar tarea
2. Listar todas
3. Listar por categoría
4. Listar por prioridad
5. Marcar como completada
6. Mostrar tareas vencidas
7. Eliminar tarea
8. Exportar a JSON
9. Salir
```

---

## Persistencia de datos

Las tareas se almacenan automáticamente en el archivo:

```
tareas.json
```

Al iniciar el programa:

- Si el archivo existe, las tareas son cargadas automáticamente.
- Si no existe, el sistema inicia con una lista vacía.
- Si el archivo está dañado, el error es capturado mediante `try-catch`.

