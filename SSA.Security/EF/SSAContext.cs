using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSA.Security.EF
{
    public partial class SSASecurityContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Coworker> Coworker { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolOption> RolOption { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-RNJUCNR;Database=SSA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_User");

                entity.ToTable("Account", "security");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(900);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Coworker>(entity =>
            {
                entity.HasKey(e => e.IdCoworker);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ProfilePicture).IsRequired();

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.IdOption);

                entity.ToTable("Option", "security");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role", "security");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RolOption>(entity =>
            {
                entity.HasKey(e => e.IdRolOption);

                entity.ToTable("RolOption", "security");

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdOptionNavigation)
                    .WithMany(p => p.RolOption)
                    .HasForeignKey(d => d.IdOption)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolOption_Option");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolOption)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolOption_Role");
            });

        }
    }
}