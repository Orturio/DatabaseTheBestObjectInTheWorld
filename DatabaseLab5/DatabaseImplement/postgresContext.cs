using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DatabaseImplement.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseImplement
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Agelimit> Agelimit { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmActor> FilmActor { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=genius;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("actor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Middlename)
                    .IsRequired()
                    .HasColumnName("middlename")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Agelimit>(entity =>
            {
                entity.ToTable("agelimit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Agelimitid).HasColumnName("agelimitid");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Agelimit)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.Agelimitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("agelimit_fkey");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("film_actor");

                entity.Property(e => e.Actorid).HasColumnName("actorid");

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.HasOne(d => d.Actor)
                    .WithMany()
                    .HasForeignKey(d => d.Actorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actor_fkey");

                entity.HasOne(d => d.Film)
                    .WithMany()
                    .HasForeignKey(d => d.Filmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Durationofpayment).HasColumnName("durationofpayment");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Filmid).HasColumnName("filmid");

                entity.Property(e => e.Startofwatchingmovie)
                    .HasColumnName("startofwatchingmovie")
                    .HasColumnType("date");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.Filmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_fkey");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Paymentid).HasColumnName("paymentid");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Paymentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payment_fkey");
            });

            modelBuilder.HasSequence("actorid");

            modelBuilder.HasSequence("agelimitid");

            modelBuilder.HasSequence("filmid");

            modelBuilder.HasSequence("paymentid");

            modelBuilder.HasSequence("sessionid");

            modelBuilder.HasSequence("usersid");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
