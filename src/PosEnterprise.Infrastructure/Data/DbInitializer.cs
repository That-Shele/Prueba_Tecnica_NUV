using Microsoft.EntityFrameworkCore;
using PosEnterprise.Core.Entities;

namespace PosEnterprise.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.BlogPosts.AnyAsync())
        {
            var initialPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "5 Claves para Optimizar la Gestión de Pedidos en Tu Restaurante",
                    Slug = "5-claves-optimizar-gestion-pedidos-restaurante",
                    Summary = "Descubre cómo reducir los tiempos de espera y coordinar cocina y comanda eficientemente con un sistema POS moderno.",
                    Content = @"
                        <p>La eficiencia en el servicio de mesa y comanda digital determina la satisfacción del comensal. En este artículo analizamos cómo la integración entre el punto de venta y la pantalla de cocina (KDS) elimina errores en comanda y acelera el despacho.</p>
                        <h3>1. Comanda Móvil desde la Mesa</h3>
                        <p>Tomar el pedido en una tablet o terminal inalámbrica envía la orden directamente a la estación de cocina o barra correspondiente, reduciendo desplazamientos del personal.</p>
                        <h3>2. Integración con Plataformas de Delivery</h3>
                        <p>Sincronizar las órdenes de aplicaciones externas dentro de la misma pantalla evita ingresar tickets manualmente y previene fallos de inventario.</p>",
                    Author = "Especialista Gastronómico",
                    PublishedAt = DateTime.UtcNow.AddDays(-10),
                    IsPublished = true,
                    MetaTitle = "Optimiza la Gestión de Pedidos en Restaurantes | POS Enterprise",
                    MetaDescription = "Aprende a acelerar la atención en salón y cocina mediante comandas digitales e integración POS para restaurantes."
                },
                new BlogPost
                {
                    Title = "Control de Inventarios en Tiempo Real para Tiendas de Retail",
                    Slug = "control-inventarios-tiempo-real-retail",
                    Summary = "Evita desabastecimientos y descuadres de caja sincronizando tus ventas físicas e inventario de almacén.",
                    Content = @"
                        <p>Un control de stock deficiente provoca ventas perdidas y capital inmovilizado. Los sistemas POS actuales ofrecen trazabilidad total por código de barras, lote o variante.</p>
                        <h3>Métricas clave en el mostrador</h3>
                        <p>Monitorear el stock mínimo de forma automatizada genera alertas antes de agotar productos de alta rotación.</p>",
                    Author = "Consultor en Retail",
                    PublishedAt = DateTime.UtcNow.AddDays(-5),
                    IsPublished = true,
                    MetaTitle = "Control de Inventarios en Tiempo Real para Tiendas | POS Enterprise",
                    MetaDescription = "Estrategias de control de stock y lectura de código de barras para comercios y puntos de venta."
                }
            };

            await context.BlogPosts.AddRangeAsync(initialPosts);
            await context.SaveChangesAsync();
        }
    }
}