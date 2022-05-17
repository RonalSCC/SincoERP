using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using School.Model.Models.SINCOdb;

namespace School.Model.Models.Context
{
    public partial class SINCOdbContext : DbContext
    {
        public SINCOdbContext()
        {
        }

        public SINCOdbContext(DbContextOptions<SINCOdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; } = null!;
        public virtual DbSet<AlumnoAsignatura> AlumnoAsignatura { get; set; } = null!;
        public virtual DbSet<AnioAcademico> AnioAcademico { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignatura { get; set; } = null!;
        public virtual DbSet<Profesor> Profesor { get; set; } = null!;
        public virtual DbSet<ProfesorAsignatura> ProfesorAsignatura { get; set; } = null!;
        public virtual DbSet<Vista_CalificacionAlumno> Vista_CalificacionAlumno { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("Alumno", "SCH");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlumnoAsignatura>(entity =>
            {
                entity.ToTable("AlumnoAsignatura", "SCH");

                entity.Property(e => e.Calificacion).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<AnioAcademico>(entity =>
            {
                entity.ToTable("AnioAcademico", "SCH");
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.ToTable("Asignatura", "SCH");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("Profesor", "SCH");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfesorAsignatura>(entity =>
            {
                entity.ToTable("ProfesorAsignatura", "SCH");
            });

            modelBuilder.Entity<Vista_CalificacionAlumno>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vista_CalificacionAlumno", "SCH");

                entity.Property(e => e.ApellidoAlumno)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Asignatura)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Calificacion).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdentificacionAlumno)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionProfesor)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NombreAlumno)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProfesor)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
