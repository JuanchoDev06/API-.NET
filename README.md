# Guía de Migraciones de Entity Framework Core

Este documento explica cómo crear y aplicar migraciones de base de datos en los proyectos de esta solución usando Entity Framework Core.

## Requisitos Previos

*   Tener instalado el SDK de .NET.
*   Tener instaladas las herramientas de Entity Framework Core CLI. Si no las tienes, instálalas desde tu terminal con el siguiente comando:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

## Opción 1: Usando Visual Studio (Consola del Administrador de Paquetes) (Recomendado)

Dado que se codifica en Visual Studio tradicional:

1. Abre Visual Studio. Ve al menú superior y selecciona **Herramientas (Tools)** > **Administrador de paquetes NuGet (NuGet Package Manager)** > **Consola del administrador de paquetes (Package Manager Console)**.
2. En la consola que se abre abajo, asegúrate de que en el menú desplegable **Proyecto predeterminado (Default project)** esté seleccionado el proyecto que tiene tu base de datos (Ej: `WebApiejemplo`).
3. **Crea una nueva migración**:
   ```powershell
   Add-Migration NombreDeTuMigracion
   ```
4. **Aplica la migración a la base de datos**:
   ```powershell
   Update-Database
   ```

## Opción 2: Usando la CLI de .NET (Terminal / Línea de Comandos)

1. **Abre la terminal** (CMD, PowerShell o la terminal de VS Code) y navega hasta la carpeta del proyecto que contiene el archivo `DbContext` (donde está tu `ApplicationDbContext.cs`).
   Ejemplo:
   ```bash
   cd "WebApiejemplo\WebApiejemplo\WebApiejemplo"
   ```

2. **Crea una nueva migración**:
   Ejecuta el siguiente comando cada vez que modifiques tus clases de modelo (como agregar las propiedades `Email` y `Password` a `Cliente`). Esto generará los archivos C# con los cambios para la base de datos.
   ```bash
   dotnet ef migrations add NombreDeTuMigracion
   ```
   *(Reemplaza `NombreDeTuMigracion` con un nombre descriptivo, por ejemplo `AddAuthToCliente` o `Inicial`)*

3. **Aplica la migración a la base de datos**:
   Este comando actualizará (o creará) las tablas en la base de datos SQL Server configurada en tu cadena de conexión.
   ```bash
   dotnet ef database update
   ```

## Configuración de la Cadena de Conexión (Connection String)

Antes de aplicar cualquier migración a la base de datos (o para que la API funcione), debes asegurarte de que tu cadena de conexión esté configurada correctamente apuntando a tu servidor SQL Server o LocalDB.

Esta configuración se encuentra en el archivo **`appsettings.json`** ubicado en la ruta principal del proyecto de tu API (`WebApiejemplo\WebApiejemplo\WebApiejemplo\appsettings.json`).

Debes buscar la sección `"ConnectionStrings"` y modificar el valor de `"DefaultConnection"`:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Profundizacion;User ID=sa;Password=tu_contraseña_aqui;TrustServerCertificate=True"
  }
```

- **Data Source**: Indica el servidor (ej: `(localdb)\MSSQLLocalDB` para bases de datos de desarrollo locales o el nombre del PC y la instancia).
- **Initial Catalog**: Es el nombre de la base de datos a crear/conectar (ej: `Profundizacion`).
- **User ID y Password**: Las credenciales de tu servidor SQL (ej: usuario `sa`).

## Solución de problemas / Consideraciones

- **Error de conexión al aplicar la base de datos**: Asegúrate de que en tu archivo `appsettings.json` la propiedad `"DefaultConnection"` (o como se llame tu connection string) apunte correctamente a tu servidor de base de datos SQL Server / LocalDB y tenga las credenciales adecuadas.
- Si te aparece un error sobre que la cuenta o base de datos no existe, revisa si instalaste correctamenta tu LocalDB o si creaste manualmente la base de datos si así lo requiriera tu proveedor.
