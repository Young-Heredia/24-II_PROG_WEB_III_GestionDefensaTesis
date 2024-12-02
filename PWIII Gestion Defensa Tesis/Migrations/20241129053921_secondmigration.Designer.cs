﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PWIII_Gestion_Defensa_Tesis.Data;

#nullable disable

namespace PWIII_Gestion_Defensa_Tesis.Migrations
{
    [DbContext(typeof(DbtesisContext))]
    [Migration("20241129053921_secondmigration")]
    partial class secondmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.ActivityProfessional", b =>
                {
                    b.Property<int>("IdActivityProfessional")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idActivityProfessional");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdActivityProfessional"));

                    b.Property<int>("IdActivity")
                        .HasColumnType("int")
                        .HasColumnName("idActivity");

                    b.Property<short>("IdProfessional")
                        .HasColumnType("smallint")
                        .HasColumnName("idProfessional");

                    b.HasKey("IdActivityProfessional");

                    b.HasIndex("IdActivity");

                    b.HasIndex("IdProfessional");

                    b.ToTable("ActivityProfessional", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Audience", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("latitude");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Audience", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.DefenseActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DefenseDate")
                        .HasColumnType("datetime")
                        .HasColumnName("defenseDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("description");

                    b.Property<byte>("IdAudience")
                        .HasColumnType("tinyint")
                        .HasColumnName("idAudience");

                    b.Property<short>("IdStudent")
                        .HasColumnType("smallint")
                        .HasColumnName("idStudent");

                    b.Property<int>("IdThesis")
                        .HasColumnType("int")
                        .HasColumnName("idThesis");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("StatusThesis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .HasName("PK_Activity");

                    b.HasIndex("IdAudience");

                    b.HasIndex("IdStudent");

                    b.HasIndex("IdThesis");

                    b.ToTable("DefenseActivity", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Professional", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("Career")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("career");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("lastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("name");

                    b.Property<string>("SecondLastName")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("secondLastName");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("ci")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Professional", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Student", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("lastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("name");

                    b.Property<string>("SecondLastName")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("secondLastName");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("ci")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Thesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("description");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("IdTypeThesis")
                        .HasColumnType("tinyint")
                        .HasColumnName("idTypeThesis");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("name");

                    b.Property<double>("Note")
                        .HasColumnType("float");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdTypeThesis");

                    b.ToTable("Thesis", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.TypeThesis", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("name");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TypeThesis", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.User", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("email");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varbinary(34)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("role");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.ActivityProfessional", b =>
                {
                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.DefenseActivity", "IdActivityNavigation")
                        .WithMany("ActivityProfessionals")
                        .HasForeignKey("IdActivity")
                        .IsRequired()
                        .HasConstraintName("FK_ActivityProfessional_Activity");

                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.Professional", "IdProfessionalNavigation")
                        .WithMany("ActivityProfessionals")
                        .HasForeignKey("IdProfessional")
                        .IsRequired()
                        .HasConstraintName("FK_ActivityProfessional_Professional");

                    b.Navigation("IdActivityNavigation");

                    b.Navigation("IdProfessionalNavigation");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.DefenseActivity", b =>
                {
                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.Audience", "IdAudienceNavigation")
                        .WithMany("DefenseActivities")
                        .HasForeignKey("IdAudience")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_Audience");

                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.Student", "IdStudentNavigation")
                        .WithMany("DefenseActivities")
                        .HasForeignKey("IdStudent")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_Student");

                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.Thesis", "IdThesisNavigation")
                        .WithMany("DefenseActivities")
                        .HasForeignKey("IdThesis")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_Thesis");

                    b.Navigation("IdAudienceNavigation");

                    b.Navigation("IdStudentNavigation");

                    b.Navigation("IdThesisNavigation");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Thesis", b =>
                {
                    b.HasOne("PWIII_Gestion_Defensa_Tesis.Models.TypeThesis", "IdTypeThesisNavigation")
                        .WithMany("Theses")
                        .HasForeignKey("IdTypeThesis")
                        .IsRequired()
                        .HasConstraintName("FK_Thesis_TypeThesis");

                    b.Navigation("IdTypeThesisNavigation");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Audience", b =>
                {
                    b.Navigation("DefenseActivities");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.DefenseActivity", b =>
                {
                    b.Navigation("ActivityProfessionals");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Professional", b =>
                {
                    b.Navigation("ActivityProfessionals");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Student", b =>
                {
                    b.Navigation("DefenseActivities");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.Thesis", b =>
                {
                    b.Navigation("DefenseActivities");
                });

            modelBuilder.Entity("PWIII_Gestion_Defensa_Tesis.Models.TypeThesis", b =>
                {
                    b.Navigation("Theses");
                });
#pragma warning restore 612, 618
        }
    }
}
