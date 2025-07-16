# Code

This document should describe how the code is structured and how the developer implemented the logic in a clear and maintainable way.

## Project Layout

> API:
> - `Controllers/`: Maneja los HTTP endpoints de la aplicación
> - `Models/`: Entidades para las tablas de la base de datos
> - `Services/`: Logica de la aplicación
> - `Services/Implements`: Interfaces de implementación de los metodos de los servicios.
> - `Data/`: Contiene el AppDbContext, que maneja la relacion de la base de datos con Entity Framework.
	
> UI:
> - `Controllers/`: Maneja los HTTP endpoints de la aplicación
> - `Models/`: Entidades para las tablas de la base de datos
> - `Services/`: Logica de la aplicación
> - `Services/Implements`: Interfaces de implementación de los metodos de los servicios.
> - `Data/`: Contiene el AppDbContext, que maneja la relacion de la base de datos con Entity Framework.
> - `Views/`: Vistas de Razor pages.

> Database:
> - Todo se genera a través de Entity Framework, por lo que no fué necesario crear archivos.


## Expected Practices

> API:
> - Arquitectura en capas
> - Acceso a datos por Entity Framework
	
> UI:
> - Arquitectura MVC
> - Acceso a datos por medio de API

...

## Code Quality

> - Sin valores harcodeados
> - Manejo de excepciones en acceso a datos
> - Manejo de excepciones regulares en tiempo de ejecución
> - Uso de archivos de configuración
> - Uso de Linq para manejo de datos

