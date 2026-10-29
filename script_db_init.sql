-- ===============================================================================
-- SCRIPT DE CREACIÓN E INICIALIZACIÓN DE BASE DE DATOS SQL SERVER
-- Proyecto: POS Enterprise (Sistema Punto de Venta)
-- Compatible con: Microsoft SQL Server 2019 / 2022 / LocalDB
-- ===============================================================================

USE [master];
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'PosEnterpriseDb')
BEGIN
CREATE DATABASE [PosEnterpriseDb];
END
GO

USE [PosEnterpriseDb];
GO

IF OBJECT_ID(N'[dbo].[ContactRequests]', N'U') IS NOT NULL DROP TABLE [dbo].[ContactRequests];
IF OBJECT_ID(N'[dbo].[BlogPosts]', N'U') IS NOT NULL DROP TABLE [dbo].[BlogPosts];
GO

-- CREACIÓN DE LA TABLA [BlogPosts]
CREATE TABLE [dbo].[BlogPosts] (
[Id] INT IDENTITY(1,1) NOT NULL,
[Title] NVARCHAR(200) NOT NULL,
[Slug] NVARCHAR(200) NOT NULL,
[Summary] NVARCHAR(500) NOT NULL,
[Content] NVARCHAR(MAX) NOT NULL,
[Author] NVARCHAR(100) NOT NULL CONSTRAINT [DF_BlogPosts_Author] DEFAULT (N'Equipo POS Enterprise'),
[PublishedAt] DATETIME2 NOT NULL CONSTRAINT [DF_BlogPosts_PublishedAt] DEFAULT (SYSUTCDATETIME()),
[IsPublished] BIT NOT NULL CONSTRAINT [DF_BlogPosts_IsPublished] DEFAULT (1),
[MetaTitle] NVARCHAR(200) NULL,
[MetaDescription] NVARCHAR(300) NULL,
CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_BlogPosts_Slug] ON [dbo].[BlogPosts] ([Slug] ASC);
CREATE NONCLUSTERED INDEX [IX_BlogPosts_Published] ON [dbo].[BlogPosts] ([IsPublished] ASC, [PublishedAt] DESC);
GO

-- CREACIÓN DE LA TABLA [ContactRequests]
CREATE TABLE [dbo].[ContactRequests] (
[Id] INT IDENTITY(1,1) NOT NULL,
[FullName] NVARCHAR(150) NOT NULL,
[Email] NVARCHAR(150) NOT NULL,
[Phone] NVARCHAR(50) NULL,
[BusinessType] NVARCHAR(50) NOT NULL,
[Message] NVARCHAR(2000) NOT NULL,
[CreatedAt] DATETIME2 NOT NULL CONSTRAINT [DF_ContactRequests_CreatedAt] DEFAULT (SYSUTCDATETIME()),
CONSTRAINT [PK_ContactRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- INSERCIÓN DE DATOS INICIALES DE PRUEBA (SEED DATA)

-- A. Artículos del Blog
INSERT INTO [dbo].[BlogPosts]
([Title], [Slug], [Summary], [Content], [Author], [PublishedAt], [IsPublished], [MetaTitle], [MetaDescription])
VALUES
(
N'5 Claves para Optimizar la Gestión de Pedidos en Tu Restaurante',
N'5-claves-optimizar-gestion-pedidos-restaurante',
N'Descubre cómo reducir tiempos de espera y evitar errores de comanda mediante sistemas POS integrados con comanda digital.',
N'La importancia de la velocidad en el servicio gastronómicoEn el sector de la restauración, el tiempo de atención al cliente es determinante. La integración de comanderas móviles conectadas al sistema POS central permite que los garzones envíen los pedidos directamente a la pantalla de cocina (KDS) o impresoras térmicas en barra.Principales Beneficios:Reducción del tiempo de preparación en un 30%.Eliminación de errores por letra ilegible o comandas extraviadas.Control exacto del consumo e inventario de insumos en tiempo real.',
N'Carlos Gómez - Consultor POS',
DATEADD(DAY, -10, SYSUTCDATETIME()),
1,
N'5 Claves para Optimizar la Gestión de Pedidos en Restaurantes',
N'Guía práctica para restaurantes sobre cómo reducir tiempos y controlar comandas mediante un sistema POS moderno.'
),
(
N'Cómo Evitar Pérdidas de Inventario en Tu Tienda de Retail',
N'como-evitar-perdidas-inventario-tienda-retail',
N'Estrategias prácticas para controlar el stock, realizar auditorías rápidas y prevenir mermas imprevistas.',
N'El inventario como activo principalUn control de stock deficiente es una de las causas más comunes de pérdida financiera en comercios al por menor. Un sistema POS moderno registra cada entrada, venta y devolución al instante mediante lectura de códigos de barras.Además, establecer puntos de reorden automáticos te garantiza contar siempre con los productos de mayor rotación sin sobrestockear tu almacén.',
N'Sofía Martínez - Especialista en Retail',
DATEADD(DAY, -5, SYSUTCDATETIME()),
1,
N'Control de Inventario y Prevención de Mermas en Retail',
N'Aprende a proteger el stock de tu tienda e integrar inventarios en tiempo real con un software de punto de venta.'
),
(
N'Facturación Electrónica Integrada: Todo lo que Debes Saber',
N'facturacion-electronica-integrada-pos',
N'Simplifica la emisión de comprobantes fiscales desde el mismo terminal de cobro sin ingresar a portales externos.',
N'Emisión inmediata en punto de ventaEmitir facturas electrónicas o boletas desde el mismo flujo de cobro optimiza la atención al cliente en caja y asegura el cumplimiento con la normativa tributaria local.Al integrar tu facturación con el software POS, cada transacción queda registrada contablemente sin necesidad de reingresar datos manualmente al final de la jornada.',
N'Equipo POS Enterprise',
DATEADD(DAY, -1, SYSUTCDATETIME()),
1,
N'Facturación Electrónica Integrada en Sistemas POS',
N'Conoce los beneficios de emitir comprobantes fiscales electrónicos directamente desde tu caja registradora.'
);
GO

-- B. Solicitudes de Contacto de Ejemplo
INSERT INTO [dbo].[ContactRequests]
([FullName], [Email], [Phone], [BusinessType], [Message], [CreatedAt])
VALUES
(
N'María Fernández',
N'mfernandez@cafeejemplo.com',
N'+56912345678',
N'Restaurante',
N'Hola, me gustaría solicitar una demostración del sistema POS para una cafetería con 2 sucursales y comanderas móviles.',
DATEADD(DAY, -2, SYSUTCDATETIME())
),
(
N'Roberto Silva',
N'contacto@minimarketroberto.cl',
N'+56987654321',
N'Retail',
N'Necesito cotizar un punto de venta con lector de código de barras y balanza integrada para un minimarket.',
DATEADD(DAY, -1, SYSUTCDATETIME())
);
GO

-- ============================================================================
-- VERIFICACIÓN DE INSERCIÓN
-- ============================================================================
SELECT 'BlogPosts Registrados' AS Tabla, COUNT(*) AS Total FROM [dbo].[BlogPosts]
UNION ALL
SELECT 'ContactRequests Registrados' AS Tabla, COUNT(*) AS Total FROM [dbo].[ContactRequests];
GO