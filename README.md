## Tabla de Contenido
- [Introducción](#introducción)
- [Ejecución](#ejecución)
  - [Preparar la Base de Datos](#preparar-la-base-de-datos)

# Introducción

Este repositorio contiene un prototipo para una aplicación móvil que permite solicitar permisos por vacacaciones o por salud en una empresa.

# Ejecución

Para poder ejecutar la aplicación por completo se debe realizar los siguientes pasos:

## Preparar la Base de Datos

Para poder trabajar con la aplicación, primero debemos crear nuestra base de datos, para ellos nos dirigimos a `src\api` y ejecutamos:

```
dotnet ef database update --context ApplicationDbContext --connection "Server=localhost,1435;database=PermisosDb;User Id=SA;Password=La.Contraseña+Segura"
```

A continuación debemos ejecutar un Script de SQL que contiene los datos iniciales que se encuentra en [scripts/create-initial-data.sql](scripts/create-initial-data.sql)
