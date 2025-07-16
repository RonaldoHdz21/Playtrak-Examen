# Implementation

This document should describe how to run the solution locally. It is not intended for production deployment.

## Environment Requirements

> - Windows 10 o superior
> - .NET 6.0 SDK o superior
> - Visual Studio o Visual Studio Code

...

## Project Structure

> - `api/`: Proyecto ASP.NET Core Web API
> - `ui/`: Proyecto Web UI

...

## API Setup

> Para generar las tablas de la base de datos desde Entity Framework, debes ejecutar las siguientes instrucciones en la consola de Administrador de paquetes:
> ```bash
> Add-Migration InitialCreate
> Update-Database
> ```


## Configuration

> API:
> - `appsettings.json`: Default values
	 
> UI:
> - `Web.config`: Default values

...

## Notes

> API:
> - Todas las tablas necesarias se generan por medio de Entity Framework

> UI:
> - Todos los endpoints se registraron en HomeController, ya que Visual Studio no me permitió agregar mas controladores