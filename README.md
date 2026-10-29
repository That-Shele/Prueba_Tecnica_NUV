# POS Enterprise - Sistema de Punto de Venta y Gestión

Plataforma web corporativa desarrollada para **POS Enterprise**, una empresa proveedora de soluciones de Punto de Venta (POS) y control de inventarios para restaurantes y tiendas retail. 

El proyecto incluye presentación corporativa, catálogo funcional por sectores, módulo de blog público con paginación, captura de clientes potenciales con validación en servidor, área administrativa protegida para la gestión de artículos mediante editor enriquecido (WYSIWYG) con subida de imágenes, y optimización SEO integral.

---

## 1. Requisitos Técnicos y Entorno Exacto

* **Runtime:** .NET 8.0 (Versión exacta de Runtime: `8.0.11`)
* **SDK:** .NET 8.0 SDK (Versión de compilación: `8.0.400` o superior)
* **Framework Web:** ASP.NET Core Razor Pages 8.0 (`Microsoft.NET.Sdk.Web`)
* **ORM:** Entity Framework Core 8.0.11 (`Microsoft.EntityFrameworkCore.SqlServer`)
* **Motor de Base de Datos:** Microsoft SQL Server 2019 / 2022 o SQL Server LocalDB (`mssqllocaldb`)
* **Seguridad / Autenticación:** Middleware de Autenticación basada en Cookies (`CookieAuthenticationDefaults`) con política restringida `AdminPolicy`.

---

## 2. Arquitectura de Software (N-Capas / Clean Architecture)

La solución está organizada en **3 proyectos desacoplados**, garantizando la separación de responsabilidades entre el modelo de dominio, el acceso a datos y la interfaz de usuario:

```text
PosEnterprise/
├── src/
│   ├── PosEnterprise.Core/           # Capa de Dominio y Contratos
│   │   ├── DTOs/                     # Objetos de transferencia de datos (ContactFormDto, BlogPostEditorDto)
│   │   ├── Entities/                 # Entidades de negocio (BlogPost, ContactRequest)
│   │   ├── Interfaces/               # Contratos de repositorios y servicios (IBlogRepository, IContactRepository, ISitemapService)
│   │   └── Specifications/           # Especificaciones de consulta (PublishedPostSpec)
│   │
│   ├── PosEnterprise.Infrastructure/ # Capa de Persistencia e Infraestructura
│   │   ├── Data/                     # ApplicationDbContext, DB Initializer y Fluent API Configurations
│   │   ├── Repositories/             # Implementación asíncrona de repositorios con EF Core
│   │   ├── Services/                 # Implementación de ISitemapService para sitemap.xml
│   │   └── Migrations/               # Historial de migraciones Entity Framework Core
│   │
│   └── PosEnterprise.Web/            # Capa de Presentación (Razor Pages)
│       ├── Areas/Admin/              # Área protegida (/Admin) para gestión CRUD del Blog y carga de imágenes
│       ├── Pages/                    # Páginas públicas (Index, Soluciones, Blog, Contacto, Sitemap, Robots.txt)
│       ├── TagHelpers/               # TagHelpers personalizados (CanonicalTagHelper)
│       └── wwwroot/                  # Recursos estáticos (CSS responsive, JS, uploads de imágenes)
└── script_db_init.sql                # Script reproducible SQL Server para creación e inserción directa
```

---

## 3. Configuración Local y Variables de Entorno

Por motivos de seguridad, las credenciales del sistema y las cadenas de conexión **no se incluyen hardcodearas** en el control de versiones.

### A. Configuración mediante Secret Manager (Entorno de Desarrollo)

Ejecuta los siguientes comandos en la terminal desde la raíz del proyecto para registrar las variables en el contenedor local de secretos de .NET:

```powershell
# 1. Inicializar User Secrets en el proyecto Web
dotnet user-secrets init --project src/PosEnterprise.Web

# 2. Configurar Cadena de Conexión a SQL Server
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\mssqllocaldb;Database=PosEnterpriseDb_Dev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" --project src/PosEnterprise.Web

# 3. Configurar Credenciales del Administrador
dotnet user-secrets set "AdminSettings:Username" "admin" --project src/PosEnterprise.Web
dotnet user-secrets set "AdminSettings:Password" "Aqui pones tu contraseña para el administrador" --project src/PosEnterprise.Web
```

### B. Configuración para Entornos de Producción (Variables de Entorno)

En servidores o servicios de alojamiento (Azure App Service, IIS, Docker), define las siguientes variables:

* `ConnectionStrings__DefaultConnection`: `"Server=tcp:tu-servidor-sql.database.windows.net,1433;Initial Catalog=PosEnterpriseDb;User Id=tu_usuario;Password=tu_password;TrustServerCertificate=True;"`
* `AdminSettings__Username`: `"admin"`
* `AdminSettings__Password`: `"Tu contraseña segura para el administrador"`

---

## 4. Base de Datos: Migraciones y Creación Reproducible

Dispones de dos métodos para crear y poblar la base de datos en SQL Server:

### Opción A: Mediante Entity Framework Core (Recomendado)

Ejecuta las migraciones automáticas desde PowerShell en la raíz de la solución:

```powershell
# Aplicar migraciones e inicializar datos semilla automáticamente
dotnet ef database update --project src/PosEnterprise.Infrastructure --startup-project src/PosEnterprise.Web
```

*(Al iniciar la aplicación, `DbInitializer.SeedAsync` insertará automáticamente los artículos de prueba y categorías iniciales si la base de datos está vacía).*

### Opción B: Ejecución de Script T-SQL Directo

Si prefieres inicializar SQL Server manualmente desde SQL Server Management Studio (SSMS), Azure Data Studio o `sqlcmd`:

1. Abre el archivo adjunto `script_db_init.sql`.
2. Conéctate a tu instancia de SQL Server.
3. Ejecuta el script completo. Creará la base de datos `PosEnterpriseDb`, las tablas `BlogPosts` y `ContactRequests`, sus índices correspondientes y los registros iniciales de prueba.

*(Es importante elegir solamente una opción, ya que ir de las dos formas puede causar inconsistencias).*

---

## 5. Instrucciones de Ejecución Local

1. **Clonar o abrir el repositorio en tu equipo.**
2. **Restaurar paquetes y compilar:**
   ```powershell
   dotnet restore
   dotnet build
   ```
3. **Aplicar la base de datos** (utilizando la Opción A o B descritas en la Sección 4).
4. **Ejecutar el proyecto Web:**
   ```powershell
   dotnet run --project src/PosEnterprise.Web
   ```
5. **Navegar en el navegador:**
   * **Sitio Público:** `https://localhost:Port`
   * **Sitemap XML:** `https://localhost:Port/sitemap.xml`
   * **Robots TXT:** `https://localhost:Port/robots.txt`
   * **Área Administrativa:** `https://localhost:Port/Admin/Login`

---

## 6. Credenciales de Acceso a la Administración

* **Ruta del Panel:** `/Admin/Login`
* **Usuario:** `admin`
* **Contraseña:** `La que estableciste en el secreto de la aplicación`

*(El panel permite redactar artículos con el editor WYSIWYG, previsualizar formatos, subir imágenes locales a `wwwroot/uploads/blog/` y publicar o editar estados).*

---

## 7. Estrategias de SEO, Accesibilidad y Rendimiento

* **Estructura HTML5 Semántica:** Uso riguroso de `<header>`, `<nav>`, `<main>`, `<article>`, `<section>` y `<footer>`.
* **Metadatos Únicos por Página:** Títulos HTML descriptivos, meta descriptions y encabezados `<h1>` coherentes en cada vista.
* **URLs Canónicas:** Uso de TagHelper personalizado `asp-canonical` para prevenir penalizaciones por contenido duplicado.
* **Datos Estructurados Schema.org:**
  * Fragmento JSON-LD `Organization` presente en la plantilla base (`_Layout.cshtml`).
  * Fragmento JSON-LD `BlogPosting` integrado en el detalle de cada artículo del blog (`/Blog/Detail`).
* **Archivos Estáticos Rastreables:**
  * `/sitemap.xml` generado dinámicamente con URLs estáticas y slugs de artículos desde SQL Server.
  * `/robots.txt` configurado para indexación pública con exclusión de las rutas privadas `/Admin/`.
* **Accesibilidad:** Enlace directo de salto al contenido (`.skip-link`), estilos visuales de foco (`:focus-visible`) y atributos `alt` en imágenes informativas.

---

## 8. Decisiones Técnicas y Limitaciones Conocidas

1. **Almacenamiento de Imágenes:** Las imágenes cargadas desde el editor del blog se guardan localmente en `wwwroot/uploads/blog/`. En un entorno distribuido o multi-instancia de nube, se recomienda sustituir el almacenamiento físico local por un proveedor de almacenamiento de objetos (ej. Azure Blob Storage o AWS S3).
2. **Autenticación Simplificada:** Se implementó autenticación por cookies para el usuario administrador sin dependencia de ASP.NET Core Identity, optimizando la agilidad de la solución.
3. **Paginación en el Blog:** El listado del blog incluye paginación con parámetro de consulta (`/Blog?p=1`), limitando la carga a 6 artículos por página con consultas eficientes usando `Skip` y `Take` sobre SQL Server.