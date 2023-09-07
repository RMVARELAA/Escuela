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

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Maestro> Maestros { get; set; }

    public virtual DbSet<MaestrosClase> MaestrosClases { get; set; }

    public virtual DbSet<MatriculaAlumno> MatriculaAlumnos { get; set; }

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

            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");
            entity.Property(e => e.AulaId).HasColumnName("AulaID");
            entity.Property(e => e.Clase1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Clase");
            entity.Property(e => e.HoraFinal)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Hora_Final");
            entity.Property(e => e.HoraInicial)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Hora_Inicial");
            entity.Property(e => e.Uv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UV");

            entity.HasOne(d => d.Aula).WithMany(p => p.Clases)
                .HasForeignKey(d => d.AulaId)
                .HasConstraintName("fk_Aula");

            entity.HasOne(d => d.Hora).WithMany(p => p.Clases)
                .HasForeignKey(d => new { d.HoraInicial, d.HoraFinal })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horarios");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => new { e.HoraInicial, e.HoraFinal });

            entity.ToTable("HORARIOS");

            entity.Property(e => e.HoraInicial)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Hora_Inicial");
            entity.Property(e => e.HoraFinal)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Hora_Final");
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
            entity
                .HasNoKey()
                .ToTable("MAESTROS_CLASES");

            entity.Property(e => e.IdAula).HasColumnName("Id_Aula");
            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");
            entity.Property(e => e.IdMaestros).HasColumnName("Id_Maestros");

            entity.HasOne(d => d.IdAulaNavigation).WithMany()
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK_Id_AulaMaestro");

            entity.HasOne(d => d.IdClaseNavigation).WithMany()
                .HasForeignKey(d => d.IdClase)
                .HasConstraintName("FK_Id_ClaseMaestros");

            entity.HasOne(d => d.IdMaestrosNavigation).WithMany()
                .HasForeignKey(d => d.IdMaestros)
                .HasConstraintName("FK_Id_Maestros");
        });

        modelBuilder.Entity<MatriculaAlumno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MATRICULA_ALUMNO");

            entity.Property(e => e.IdAlumno).HasColumnName("Id_Alumno");
            entity.Property(e => e.IdAula).HasColumnName("Id_Aula");
            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany()
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK_Id_Alumno");

            entity.HasOne(d => d.IdAulaNavigation).WithMany()
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK_Id_Aula");

            entity.HasOne(d => d.IdClaseNavigation).WithMany()
                .HasForeignKey(d => d.IdClase)
                .HasConstraintName("FK_Id_Clase");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
