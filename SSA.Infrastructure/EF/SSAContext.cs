using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSA.Infrastructure.EF
{
    public partial class SSAContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Coworker> Coworker { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectCoworker> ProjectCoworker { get; set; }
        public virtual DbSet<RolCoworkerProject> RolCoworkerProject { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolOption> RolOption { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskReviewLog> TaskReviewLog { get; set; }

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

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.IdActitivity);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdated).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdAssignedToNavigation)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.IdAssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_ProjectCoworker");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Category");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Status");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.Property(e => e.CategoryType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'.')");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("nchar(10)")
                    .HasDefaultValueSql("(N'fff')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
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

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject);

                entity.Property(e => e.DateFinish).HasColumnType("datetime");

                entity.Property(e => e.DateSart).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectCoworker>(entity =>
            {
                entity.HasKey(e => e.IdProjectCoworker);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.IdCoworkerNavigation)
                    .WithMany(p => p.ProjectCoworker)
                    .HasForeignKey(d => d.IdCoworker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCoworker_Coworker");

                entity.HasOne(d => d.IdRolCoworkerProjectNavigation)
                    .WithMany(p => p.ProjectCoworker)
                    .HasForeignKey(d => d.IdRolCoworkerProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCoworker_RolCoworkerProject");
            });

            modelBuilder.Entity<RolCoworkerProject>(entity =>
            {
                entity.HasKey(e => e.IdRolCoworkerProject);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdated).HasColumnType("datetime");
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

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'fff')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask);

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.IdStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecordUpdate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Activity");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Status");
            });

            modelBuilder.Entity<TaskReviewLog>(entity =>
            {
                entity.HasKey(e => e.IdTaskReviewLog);

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordStatus).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdCoworkerReviewNavigation)
                    .WithMany(p => p.TaskReviewLog)
                    .HasForeignKey(d => d.IdCoworkerReview)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskReviewLog_ProjectCoworker");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TaskReviewLog)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskReviewLog_Task");
            });
        }
    }
}