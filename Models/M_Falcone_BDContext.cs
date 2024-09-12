using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MFalcone_WEBAPI.Models
{
    public partial class M_Falcone_BDContext : DbContext
    {
        public M_Falcone_BDContext()
        {
        }

        public M_Falcone_BDContext(DbContextOptions<M_Falcone_BDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<M_FalconeSolicitud> M_FalconeSolicituds { get; set; } = null!;
        public virtual DbSet<M_FalconeUsuario> M_FalconeUsuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<M_FalconeSolicitud>(entity =>
            {
                entity.ToTable("M-Falcone_Solicitudes");

                entity.Property(e => e.DescripcionSolicitud).HasMaxLength(255);

                entity.Property(e => e.DetalleGestion).HasMaxLength(255);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaGestion).HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.HasOne(d => d.UsuarioCreador)
                    .WithMany(p => p.M_FalconeSolicitudUsuarioCreadors)
                    .HasForeignKey(d => d.UsuarioCreadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_UsuarioCreador");

                entity.HasOne(d => d.UsuarioGestor)
                    .WithMany(p => p.M_FalconeSolicitudUsuarioGestors)
                    .HasForeignKey(d => d.UsuarioGestorId)
                    .HasConstraintName("FK_Solicitud_UsuarioGestor");
            });

            modelBuilder.Entity<M_FalconeUsuario>(entity =>
            {
                entity.ToTable("M-Falcone_Usuarios");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Rol).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
