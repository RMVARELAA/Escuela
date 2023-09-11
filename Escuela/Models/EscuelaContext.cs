using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Models;

public partial class EscuelaContext : DbContext
{
    public EscuelaContext()
    {
    }

    public EscuelaContext(DbContextOptions<EscuelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Maestro> Maestros { get; set; }

    public virtual DbSet<MaestrosClase> MaestrosClases { get; set; }

    public virtual DbSet<MatriculaAlumno> MatriculaAlumnos { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ESCUELA;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumnos).HasName("PK__ALUMNOS__AE016685CC704F49");

            entity.ToTable("ALUMNOS");

            entity.HasIndex(e => e.Telefono, "UQ__ALUMNOS__4EC5048038B7E484").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__ALUMNOS__75E3EFCF18C824BC").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__ALUMNOS__A9D10534A3FE3DA0").IsUnique();

            entity.Property(e => e.IdAlumnos).HasColumnName("Id_Alumnos");
            entity.Property(e => e.Codigo)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasComputedColumnSql("('ALUM'+right('000'+CONVERT([varchar],[Id_Alumnos]),(3)))", true);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.IdAula);

            entity.ToTable("AULAS");

            entity.Property(e => e.IdAula).HasColumnName("Id_Aula");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasComputedColumnSql("('AU'+right('00'+CONVERT([varchar],[Id_Aula]),(3)))", false);
            entity.Property(e => e.NombreAula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nombre_Aula");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PK_Id_Clase");

            entity.ToTable("CLASES");

            entity.HasIndex(e => e.NombreClase, "UQ__CLASES__1ECC051AF6A1C18F").IsUnique();

            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");
            entity.Property(e => e.CodigoClase)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComputedColumnSql("(upper(substring([Nombre_Clase],(1),(3))))", true)
                .HasColumnName("Codigo_Clase");
            entity.Property(e => e.NombreClase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Clase");
            entity.Property(e => e.Uv).HasColumnName("UV");
        });

        modelBuilder.Entity<Maestro>(entity =>
        {
            entity.HasKey(e => e.IdMaestros).HasName("PK__MAESTROS__7FC94C8BBA5952BD");

            entity.ToTable("MAESTROS");

            entity.HasIndex(e => e.Telefono, "UQ__MAESTROS__4EC504807F950D41").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__MAESTROS__75E3EFCF432086BD").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__MAESTROS__A9D105344AA2EFD0").IsUnique();

            entity.Property(e => e.IdMaestros).HasColumnName("Id_Maestros");
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComputedColumnSql("('MAE'+right('000'+CONVERT([varchar],[Id_Maestros]),(3)))", true);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaestrosClase>(entity =>
        {
            entity.HasKey(e => e.IdClaseMaestro).HasName("PK_Id_Clase_Maestro");

            entity.ToTable("MAESTROS_CLASES");

            entity.Property(e => e.IdClaseMaestro).HasColumnName("Id_Clase_Maestro");
            entity.Property(e => e.IdMaestros).HasColumnName("Id_Maestros");
            entity.Property(e => e.IdSeccion).HasColumnName("Id_Seccion");

            entity.HasOne(d => d.IdMaestrosNavigation).WithMany(p => p.MaestrosClases)
                .HasForeignKey(d => d.IdMaestros)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_Maestros");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.MaestrosClases)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_Seccion_Maestros");
        });

        modelBuilder.Entity<MatriculaAlumno>(entity =>
        {
            entity.HasKey(e => e.IdMatriculaAlumno).HasName("PK_Id_Matricula_Alumno");

            entity.ToTable("MATRICULA_ALUMNO");

            entity.Property(e => e.IdMatriculaAlumno).HasColumnName("Id_Matricula_Alumno");
            entity.Property(e => e.IdAlumno).HasColumnName("Id_Alumno");
            entity.Property(e => e.IdSeccion).HasColumnName("Id_Seccion");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.MatriculaAlumnos)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK_Id_Alumno");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.MatriculaAlumnos)
                .HasForeignKey(d => d.IdSeccion)
                .HasConstraintName("FK_Id_Seccion");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion).HasName("Id_Seccion");

            entity.ToTable("SECCION");

            entity.Property(e => e.IdSeccion).HasColumnName("Id_Seccion");
            entity.Property(e => e.IdAula).HasColumnName("Id_Aula");
            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.IdAula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AulaId");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_Clase");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
