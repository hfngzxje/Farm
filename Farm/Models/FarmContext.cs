using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Farm.Models
{
    public partial class FarmContext : DbContext
    {
        public FarmContext()
        {
        }

        public FarmContext(DbContextOptions<FarmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Garden> Gardens { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Process> Processes { get; set; } = null!;
        public virtual DbSet<Produce> Produces { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = Farm; uid=sa;pwd=123456;Trusted_Connection=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garden>(entity =>
            {
                entity.ToTable("Garden");

                entity.Property(e => e.GardenId).HasColumnName("GardenID");

                entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProduceId).HasColumnName("ProduceID");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Produce)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.ProduceId)
                    .HasConstraintName("FK__History__Produce__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__History__UserID__30F848ED");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProduceId).HasColumnName("ProduceID");

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Produce)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProduceId)
                    .HasConstraintName("FK__Orders__ProduceI__31EC6D26");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProduceId).HasColumnName("ProduceID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Produce)
                    .WithMany(p => p.Processes)
                    .HasForeignKey(d => d.ProduceId)
                    .HasConstraintName("FK__Process__Produce__32E0915F");
            });

            modelBuilder.Entity<Produce>(entity =>
            {
                entity.ToTable("Produce");

                entity.Property(e => e.ProduceId).HasColumnName("ProduceID");

                entity.Property(e => e.ActualHarvestDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedHarvestDate).HasColumnType("date");

                entity.Property(e => e.GardenId).HasColumnName("GardenID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlantingDate).HasColumnType("date");

                entity.HasOne(d => d.Garden)
                    .WithMany(p => p.Produces)
                    .HasForeignKey(d => d.GardenId)
                    .HasConstraintName("FK__Produce__GardenI__33D4B598");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__RoleID__34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
