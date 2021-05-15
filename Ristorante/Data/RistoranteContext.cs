﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ristorante.Models;

#nullable disable

namespace Ristorante.Data
{
    public partial class RistoranteContext : DbContext
    {
        public RistoranteContext()
        {
        }

        public RistoranteContext(DbContextOptions<RistoranteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Piatti> Piatti { get; set; }
        public virtual DbSet<Prenotazioni> Prenotazioni { get; set; }
        public virtual DbSet<Tipo_Piatto> Tipo_Piatto { get; set; }
        public virtual DbSet<Utente> Utenti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Piatti>(entity =>
            {
                entity.Property(e => e.id)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezzo).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Tipo_piatto)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.Tipo_piattoNavigation)
                    .WithMany(p => p.Piatti)
                    .HasForeignKey(d => d.Tipo_piatto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Piatti_Tipo_piatto");
            });

            modelBuilder.Entity<Prenotazioni>(entity =>
            {
                entity.HasKey(e => e.id_prenotazione);

                entity.Property(e => e.id_prenotazione).ValueGeneratedNever();

                entity.Property(e => e.data).HasColumnType("datetime");

                entity.HasOne(d => d.id_utenteNavigation)
                    .WithMany(p => p.Prenotazioni)
                    .HasForeignKey(d => d.id_utente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prenotazioni_Utenti");
            });

            modelBuilder.Entity<Tipo_Piatto>(entity =>
            {
                entity.HasKey(e => e.Tipo_piatto1);

                entity.Property(e => e.Tipo_piatto1)
                    .HasMaxLength(25)
                    .HasColumnName("Tipo_piatto");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Utente>(entity =>
            {
                entity.HasKey(e => e.id_utente);

                entity.Property(e => e.id_utente).ValueGeneratedNever();

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}