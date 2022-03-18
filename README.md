# Tsc-Api

El proyecto fue desarrollado en .net 6 el code coverage es de **80%**

![image](https://user-images.githubusercontent.com/38229144/158921689-ae5ba9e8-6cfd-4b86-920a-ccae5f25dd9f.png)

## Tabla de Contenido

- [Tsc-Api](#tsc-api)
  - [Tabla de Contenido](#tabla-de-contenido)
  - [Librerías](#librerías)
  - [Arquitectura](#arquitectura)
  - [Instalación y Uso](#instalación-y-uso)
  - [Estructura del Proyecto](#estructura-del-proyecto)
  - [Contribución](#contribución)
    - [Ramas](#ramas)
    - [Reportar un Problema](#reportar-un-problema)

## Librerías

Las bibliotecas utilizadas son:

- [AutoMapper](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/)
- [EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
- [EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)
- [EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools)
- [Swagger](https://swagger.io/)
- [Newtonsoft](https://www.newtonsoft.com/json)
- [Moq](https://www.nuget.org/packages/Moq/)
- [xUnit](https://www.nuget.org/packages/xunit/2.4.2-pre.12)

## Arquitectura

- `N-CAPAS`

- `Patrón de Repositorio`

- `Denpendency Injection`

- `MVC`

- `RestApi`
- `Unit Test`

## Instalación y Uso

Para poder ejecutar el proyecto se debe de cambiar la cadena de conexion que se encuentra en el `appsettings.Development.json` con el key de `SqlServer` y colocarle la cadena de conexión correspondiente.

Luego proceder a ejecutar el proyecto este va a correr las migraciones automaticamente y va a poblar la base de datos con los datos con 12 paises incluyendo `Republica Dominicana` y `Guatemala`.

El proyecto cuenta con swagger para poder hacer las peticiones, las pruebas y para generar un token se deben utilizar las credenciales para el usuario: `test@domain.com`
y contraseña: `abc123`.

## Estructura del Proyecto

- 📁 `BusinessLayer` : Es una capa del proyecto que contiene la logica del proyecto.

  - 📁 `Core` : Es el que tiene la logica base.

    - 📁 `Repositories` : Contiene los repositorios del proyecto, especificamente el patron de repositorio.
      - cs `BaseRepository.cs`
      - cs `IBaseRepository.cs`

  - 📁 `Services` : Son los que contienen los servicios de la aplicación
    - 📁 `Auth`
      - cs `AuthService.cs`
      - cs `IAuthService.cs`
    - 📁 `Countries`
      - cs `CountryService.cs`
      - cs `ICountryService.cs`
    - 📁 `States`
      - cs `IStateService.cs`
      - cs `StateService.cs`
  - csproj `BusinessLayer.csproj`

  - cs `Usings.cs` : Contiene los globals usings de esta capa.

- 📁 `DataLayer` : Contiene la capa que maneja el acceso a los datos.

  - 📁 `Core` : Contiene los modelos base.
    - cs `BaseDtoModel.cs` : es la clase base para los DTO.
    - cs `BaseEditModel.cs` : Es la clase base para editar registros.
    - cs `BaseInputModel.cs` : Es la clase base para agregar.
    - cs `BaseModel.cs` : Representa la clase base de los modelos.
    - cs `IBaseModel.cs` : Contiene las firmas de las propiedades base.
  - 📁 `Mappings` : Contiene los mappers y la configuracion de estos.
    - 📁 `Mappers`
      - 📁 `Countries`
        - cs `CountryEditMapper.cs`
        - cs `CountryInputMapper.cs`
        - cs `CountryMapper.cs`
      - 📁 `States`
        - cs `StateEditMapper.cs`
        - cs `StateInputMapper.cs`
        - cs `StateMapper.cs`
    - 📁 `Profiles`
      - cs `CommonProfile.cs`
  - 📁 `Migrations` : contiene las migraciones del proyecto
  - 📁 `Models` :Contiene los modelos del proyecto
    - 📁 `Countries`
      - cs `Country.cs`
      - cs `CountryEFConfiguration.cs`
    - 📁 `States`
      - cs `State.cs`
      - cs `StateEFConfiguration.cs`
  - 📁 `Persistence` : Contiene la configuracion del DbContext y todo lo relacionado a este.
    - 📁 `Core`
      - cs `BaseEFConfiguration.cs`
    - cs `ApplicationDbContext.cs`
  - 📁 `Utils` : Contiene objetos que son usados para configuraciones.
    - 📁 `Configs`
      - 📁 `Auths`
        - cs `AuthConfig.cs`
        - cs `JwtConfig.cs`
    - 📁 `Paginations`
      - cs `Paginate.cs`
      - cs `PaginationResult.cs`
    - 📁 `Profiles`
      - cs `ProfileBase.cs`
  - 📁 `DataLayer`
  - csproj `DataLayer.csproj`
  - cs `Usings.cs`

- 📁 `Tsc.Api` : Contiene la capa web con los controladores.
  - 📁 `Configurations` : Contiene los metodos de extension utilizados en esta capa.
    - cs `ProgramConfiguration.cs` : Contains the github actions
  - 📁 `Controllers` : Contiene los controladores del proyecto
    - 📁 `Auth`
      - cs `AuthController.cs`
    - 📁 `Core` : Contiene los controladores con las operaciones basicas.
      - cs `CoreController.cs`
      - cs `ICoreController.cs`
    - 📁 `Countries`
      - cs `CountriesController.cs`
    - 📁 `State`
      - cs `StatesController.cs`
  - cs `Usings.cs` : Contiene los using globales de esta capa.

## Contribución

Si desea contribuir, puede crear una rama en el repositorio y enviar una solicitud de extracción. Recuerde crear excelentes pruebas unitarias y actualizaciones de documentación.

### Ramas

Recuerda usar esta estructura:

- `feature/[name]`
- `fix/[name]`
- `bug/[name]`

### Reportar un Problema

Si tienes una incidencia o un problema con la biblioteca puedes reportarlo en el [issues](https://github.com/Orbis25/Tsc-Api/issues)
