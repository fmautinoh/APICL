using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICL.Modelos;

public partial class DbBolivarContext : DbContext
{
    public DbBolivarContext()
    {
    }

    public DbBolivarContext(DbContextOptions<DbBolivarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comprobante> Comprobantes { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Presentacion> Presentacions { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<TipoComprobante> TipoComprobantes { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__auditori__3213E83F7A80AAE9");

            entity.ToTable("auditoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accion).HasColumnName("accion");
            entity.Property(e => e.Detalles).HasColumnName("detalles");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.TablaAfectada).HasColumnName("tabla_afectada");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCli);

            entity.ToTable("cliente", tb => tb.HasTrigger("registro_auditoria_clientes"));

            entity.Property(e => e.IdCli).HasColumnName("id_cli");
            entity.Property(e => e.AmatCli)
                .HasMaxLength(60)
                .HasColumnName("amat_cli");
            entity.Property(e => e.ApatCli)
                .HasMaxLength(60)
                .HasColumnName("apat_cli");
            entity.Property(e => e.CorCli)
                .HasMaxLength(70)
                .HasColumnName("cor_cli");
            entity.Property(e => e.DirCli)
                .HasMaxLength(80)
                .HasColumnName("dir_cli");
            entity.Property(e => e.DniCli)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("dni_cli");
            entity.Property(e => e.NomCli)
                .HasMaxLength(50)
                .HasColumnName("nom_cli");
            entity.Property(e => e.RucCli)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("ruc_cli");
            entity.Property(e => e.TelCli)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("tel_cli");
        });

        modelBuilder.Entity<Comprobante>(entity =>
        {
            entity.HasKey(e => e.IdComp);

            entity.ToTable("comprobante", tb => tb.HasTrigger("registro_auditoria_comprobante"));

            entity.Property(e => e.IdComp)
                .ValueGeneratedNever()
                .HasColumnName("id_comp");
            entity.Property(e => e.IdTipComp).HasColumnName("id_tip_comp");
            entity.Property(e => e.Igv)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("igv");
            entity.Property(e => e.NumeComp)
                .HasMaxLength(50)
                .HasColumnName("nume_comp");

            entity.HasOne(d => d.IdTipCompNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.IdTipComp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobante_tipo_comprobante");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("detalle_venta", tb => tb.HasTrigger("registro_auditoria_detalle_venta"));

            entity.Property(e => e.CanVent).HasColumnName("can_vent");
            entity.Property(e => e.IdInv).HasColumnName("id_inv");
            entity.Property(e => e.IdVent).HasColumnName("id_vent");

            entity.HasOne(d => d.IdInvNavigation).WithMany()
                .HasForeignKey(d => d.IdInv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_venta_inventario");

            entity.HasOne(d => d.IdVentNavigation).WithMany()
                .HasForeignKey(d => d.IdVent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_venta_venta");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInv).HasName("PK__inventar__D62AAC3382F1E325");

            entity.ToTable("inventario", tb =>
                {
                    tb.HasTrigger("registro_auditoria_inventario");
                    tb.HasTrigger("tr_descontar_stock");
                });

            entity.Property(e => e.IdInv).HasColumnName("id_inv");
            entity.Property(e => e.EstadoInv).HasColumnName("estado_inv");
            entity.Property(e => e.FechProduc)
                .HasColumnType("date")
                .HasColumnName("fech_produc");
            entity.Property(e => e.IdPresent).HasColumnName("id_present");
            entity.Property(e => e.IdProd).HasColumnName("id_prod");
            entity.Property(e => e.StokPro).HasColumnName("stok_pro");

            entity.HasOne(d => d.IdPresentNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdPresent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventario_presentacion");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventario_producto");
        });

        modelBuilder.Entity<Presentacion>(entity =>
        {
            entity.HasKey(e => e.IdPres);

            entity.ToTable("presentacion", tb => tb.HasTrigger("registro_auditoria_presentacion"));

            entity.Property(e => e.IdPres).HasColumnName("id_pres");
            entity.Property(e => e.TipPres)
                .HasMaxLength(50)
                .HasColumnName("tip_pres");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProd);

            entity.ToTable("producto", tb =>
                {
                    tb.HasTrigger("registro_auditoria_producto");
                    tb.HasTrigger("tr_no_eliminar_producto");
                });

            entity.Property(e => e.IdProd).HasColumnName("id_prod");
            entity.Property(e => e.DurProd).HasColumnName("dur_prod");
            entity.Property(e => e.IdTipPro).HasColumnName("id_tip_pro");
            entity.Property(e => e.NomProd)
                .HasMaxLength(70)
                .HasColumnName("nom_prod");
            entity.Property(e => e.PrecioProd)
                .HasColumnType("decimal(9, 1)")
                .HasColumnName("precio_prod");

            entity.HasOne(d => d.IdTipProNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipPro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_producto_tipo_producto");
        });

        modelBuilder.Entity<TipoComprobante>(entity =>
        {
            entity.HasKey(e => e.IdTipComp);

            entity.ToTable("tipo_comprobante", tb => tb.HasTrigger("registro_auditoria_tipo_comprobante"));

            entity.Property(e => e.IdTipComp).HasColumnName("id_tip_comp");
            entity.Property(e => e.TipoComp)
                .HasMaxLength(20)
                .HasColumnName("tipo_comp");
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipPro);

            entity.ToTable("tipo_producto", tb => tb.HasTrigger("registro_auditoria_tipo_producto"));

            entity.Property(e => e.IdTipPro).HasColumnName("id_tip_pro");
            entity.Property(e => e.TipoPro)
                .HasMaxLength(20)
                .HasColumnName("tipo_pro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsu);

            entity.ToTable("usuario", tb => tb.HasTrigger("registro_auditoria_usuario"));

            entity.Property(e => e.IdUsu)
                .ValueGeneratedNever()
                .HasColumnName("id_usu");
            entity.Property(e => e.Pasw).HasColumnName("pasw");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .HasColumnName("tipo_usuario");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVent);

            entity.ToTable("venta", tb => tb.HasTrigger("registro_auditoria_venta"));

            entity.Property(e => e.IdVent)
                .ValueGeneratedNever()
                .HasColumnName("id_vent");
            entity.Property(e => e.FechVent)
                .HasColumnType("date")
                .HasColumnName("fech_vent");
            entity.Property(e => e.IdCli).HasColumnName("id_cli");
            entity.Property(e => e.IdComp).HasColumnName("id_comp");
            entity.Property(e => e.TotPre)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("tot_pre");

            entity.HasOne(d => d.IdCompNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdComp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_venta_comprobante");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
