using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Negocio;
public partial class NegocioContext : DbContext
{
    public NegocioContext()
    {
    }

    public NegocioContext(DbContextOptions<NegocioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditoriaPrecio> AuditoriaPrecios { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    public virtual DbSet<VistaProducto> VistaProductos { get; set; }

    public virtual DbSet<VistaVentasDetallada> VistaVentasDetalladas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriaPrecio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AuditoriaPrecio");

            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.PrecioAnterior)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioAnterior");
            entity.Property(e => e.PrecioNuevo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioNuevo");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27D9BAF68F");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__72AFBCC6021CD3ED").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC27D03D526C");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleV__3214EC27464399D3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_DetalleVenta_Producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK_DetalleVenta_Venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC270B2C6877");

            entity.ToTable("Producto", tb => tb.HasTrigger("DIS_auditoriaPrecio_actualizar"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.IdProveedor).HasColumnName("ID_Proveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioVenta");
            entity.Property(e => e.StockActual).HasColumnName("stockActual");
            entity.Property(e => e.UnidadDeMedida)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("unidadDeMedida");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Producto_Proveedor");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3214EC2722E297DA");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cuit)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("CUIT");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3214EC272BBB4EE1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.MetodoDePago)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("metodoDePago");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalVenta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Cliente");
        });

        modelBuilder.Entity<VistaProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_productos");

            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioVenta");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proveedor");
            entity.Property(e => e.StockActual).HasColumnName("stockActual");
            entity.Property(e => e.UnidadDeMedida)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("unidadDeMedida");
        });

        modelBuilder.Entity<VistaVentasDetallada>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_VentasDetalladas");

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CategoriaProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoriaProducto");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
            entity.Property(e => e.MetodoDePago)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("metodoDePago");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Producto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("producto");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.UnidadDeMedida)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("unidadDeMedida");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
