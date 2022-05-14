using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using School.Model.Models.SINCOdbLogs;

namespace School.Model.Models.Context
{
    public partial class SINCOdbLogsContext : DbContext
    {
        public SINCOdbLogsContext()
        {
        }

        public SINCOdbLogsContext(DbContextOptions<SINCOdbLogsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogWebApi> LogWebApi { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogWebApi>(entity =>
            {
                entity.ToTable("LogWebApi", "SCH");

                entity.Property(e => e.API)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DetalleTransaccion).IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.JsonEntrada).IsUnicode(false);

                entity.Property(e => e.JsonSalida).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
