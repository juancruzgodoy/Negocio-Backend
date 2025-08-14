# API para Gestión Comercial - NegocioBackend
Este proyecto es la evolución de la base de datos transaccional NegocioDB, llevando la lógica de negocio a una API RESTful robusta y escalable construida con .NET y C#.

El objetivo es demostrar la capacidad de construir una arquitectura de software limpia y profesional, aplicando patrones de diseño como Repositorio y Servicio para desacoplar responsabilidades y crear un backend seguro, eficiente y fácil de mantener.

## Características Principales
Esta API implementa una arquitectura de capas bien definida para gestionar las operaciones del negocio, demostrando conceptos clave del desarrollo de software moderno:

* Arquitectura de Capas: Separación clara entre la presentación (API Controllers), la lógica de negocio (Services) y el acceso a datos (Repositories).

* Patrón Repositorio: Abstracción de la capa de acceso a datos para centralizar y estandarizar las operaciones con la base de datos, haciendo el sistema más mantenible.

* Inyección de Dependencias (DI): Utilización del contenedor de servicios de .NET para gestionar el ciclo de vida de las dependencias (DbContext, repositorios, servicios), promoviendo un bajo acoplamiento.

* Data Transfer Objects (DTOs): Uso de DTOs para controlar con precisión los datos que se exponen en los endpoints, mejorando la seguridad y evitando la exposición del modelo de base de datos interno.

* Lógica de Negocio Transaccional:

  * Implementación de una operación de venta completa en la capa de servicio, replicando la lógica del Stored Procedure pa_Venta_insertarVentaCompleta.

  * Validación de stock en tiempo real antes de confirmar la operación.

  * Manejo de transacciones atómicas para garantizar que una venta se procese por completo o se revierta totalmente si ocurre un error, asegurando la integridad de los datos.

## Tecnologías Utilizadas
* C# y .NET

* ASP.NET Core: Para la construcción de la API RESTful.

* Entity Framework Core: Como ORM para la interacción con la base de datos.

* SQL Server: Como sistema gestor de base de datos.

* Swagger (OpenAPI): Para la documentación y prueba interactiva de los endpoints de la API.

## Estructura del Proyecto
La solución está organizada en dos proyectos principales, siguiendo el principio de separación de responsabilidades:
```

/
├── Negocio/              # Proyecto Núcleo (Biblioteca de Clases)
│   ├── Models/           # Clases de entidad generadas por EF Core
│   ├── DTOs/             # Data Transfer Objects para la API
│   └── Interfaces/       # Contratos para los repositorios y servicios
|
└── Negocio.API/          # Proyecto Principal (API de ASP.NET Core)
    ├── Controllers/      # Controladores que exponen los endpoints
    ├── Services/         # Clases que contienen la lógica de negocio
    ├── Repositories/     # Implementaciones concretas de los repositorios
    └── appsettings.json  # Archivo de configuración
```

## Instalación y Uso
Para poner en funcionamiento esta API en tu entorno local, sigue estos pasos:

1. Configurar la Base de Datos:

  * Asegúrate de tener una instancia de SQL Server en ejecución.

  * Ejecuta los scripts SQL del proyecto original en el siguiente orden para crear y poblar la base de datos NegocioDB: 01_crear_tablas.sql, 02_crear_relaciones.sql, y 03_cargar_datos.sql.

2. Configurar la API:

  * Clona este repositorio.

  * Abre el archivo appsettings.json en el proyecto Negocio.API.

  * Modifica la ConnectionString llamada DefaultConnection para que apunte a tu instancia local de SQL Server.

3. Ejecutar la API:

  * Abre la solución con Visual Studio.

  * Establece Negocio.API como el proyecto de inicio.

  * Ejecuta el proyecto. Se abrirá automáticamente una ventana del navegador con la interfaz de Swagger.

4. Probar la API:

  * Utiliza la interfaz de Swagger para probar los diferentes endpoints disponibles, como GET /api/Productos para obtener la lista de productos o POST /api/Venta para registrar una nueva venta.

## Autor:
Juan Cruz Godoy

LinkedIn: www.linkedin.com/in/juancrgodoy

GitHub: @juancruzgodoy







