using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Models;

namespace PWIII_Gestion_Defensa_Tesis.Data;

public partial class DbtesisContext : DbContext
{
    public DbtesisContext()
    {
    }

    public DbtesisContext(DbContextOptions<DbtesisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityProfessional> ActivityProfessionals { get; set; }

    public virtual DbSet<Audience> Audiences { get; set; }

    public virtual DbSet<DefenseActivity> DefenseActivities { get; set; }

    public virtual DbSet<Professional> Professionals { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Thesis> Theses { get; set; }

    public virtual DbSet<TypeThesis> TypeTheses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityProfessional>(entity =>
        {
            entity.HasKey(e => e.IdActivityProfessional);

            entity.ToTable("ActivityProfessional");

            entity.Property(e => e.IdActivityProfessional).HasColumnName("idActivityProfessional");
            entity.Property(e => e.IdActivity).HasColumnName("idActivity");
            entity.Property(e => e.IdProfessional).HasColumnName("idProfessional");

            entity.HasOne(d => d.IdActivityNavigation).WithMany(p => p.ActivityProfessionals)
                .HasForeignKey(d => d.IdActivity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityProfessional_Activity");

            entity.HasOne(d => d.IdProfessionalNavigation).WithMany(p => p.ActivityProfessionals)
                .HasForeignKey(d => d.IdProfessional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityProfessional_Professional");
        });

        modelBuilder.Entity<Audience>(entity =>
        {
            entity.ToTable("Audience");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<DefenseActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Activity");

            entity.ToTable("DefenseActivity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DefenseDate)
                .HasColumnType("datetime")
                .HasColumnName("defenseDate");
            entity.Property(e => e.Description)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdAudience).HasColumnName("idAudience");
            entity.Property(e => e.IdStudent).HasColumnName("idStudent");
            entity.Property(e => e.IdThesis).HasColumnName("idThesis");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");

            entity.HasOne(d => d.IdAudienceNavigation).WithMany(p => p.DefenseActivities)
                .HasForeignKey(d => d.IdAudience)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Audience");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.DefenseActivities)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Student");

            entity.HasOne(d => d.IdThesisNavigation).WithMany(p => p.DefenseActivities)
                .HasForeignKey(d => d.IdThesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Thesis");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.ToTable("Professional");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Career)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("career");
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SecondLastName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("secondLastName");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SecondLastName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("secondLastName");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Thesis>(entity =>
        {
            entity.ToTable("Thesis");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdTypeThesis).HasColumnName("idTypeThesis");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");

            entity.HasOne(d => d.IdTypeThesisNavigation).WithMany(p => p.Theses)
                .HasForeignKey(d => d.IdTypeThesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Thesis_TypeThesis");
        });

        modelBuilder.Entity<TypeThesis>(entity =>
        {
            entity.ToTable("TypeThesis");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(34)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasColumnName("status");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
