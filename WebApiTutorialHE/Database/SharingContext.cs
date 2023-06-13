using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Database
{
    public partial class SharingContext : DbContext
    {
        public SharingContext()
        {
        }

        public SharingContext(DbContextOptions<SharingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Itemfeedback> Itemfeedbacks { get; set; } = null!;
        public virtual DbSet<Medium> Media { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Notificationtype> Notificationtypes { get; set; } = null!;
        public virtual DbSet<Notificationview> Notificationviews { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Violationreport> Violationreports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3307;database=sharingtogether1;user=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.CreatedBy, "FK_Category_CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "FK_Category_LastModifiedBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_CreatedBy");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.CategoryLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_LastModifiedBy");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.ParentCommentId, "ParentCommentId");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CommentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_ibfk_3");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.CommentLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_ibfk_4");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("comment_ibfk_2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_ibfk_1");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("faculty");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Itemfeedback>(entity =>
            {
                entity.ToTable("itemfeedback");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ItemfeedbackCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("itemfeedback_ibfk_2");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.ItemfeedbackLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("itemfeedback_ibfk_3");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Itemfeedbacks)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("itemfeedback_ibfk_1");
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.ToTable("media");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.ThumbnailUrl).HasMaxLength(255);

                entity.Property(e => e.VideoUrl).HasMaxLength(255);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("media_ibfk_1");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.FromUserId, "FromUserId");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.NotificationTypeId, "NotificationTypeId");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.HasIndex(e => e.RegistationId, "RegistationId");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NotificationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("notification_ibfk_5");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.NotificationFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("notification_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.NotificationLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("notification_ibfk_6");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notification_ibfk_3");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notification_ibfk_4");

                entity.HasOne(d => d.Registation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.RegistationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notification_ibfk_2");
            });

            modelBuilder.Entity<Notificationtype>(entity =>
            {
                entity.ToTable("notificationtype");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NotificationtypeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificationtype_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.NotificationtypeLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificationtype_ibfk_2");
            });

            modelBuilder.Entity<Notificationview>(entity =>
            {
                entity.ToTable("notificationview");

                entity.HasIndex(e => e.NotificationId, "NotificationId");

                entity.Property(e => e.ViewedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.Notificationviews)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificationview_ibfk_1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.CategoryId, "CategoryId");

                entity.HasIndex(e => e.CreatedBy, "FK_Post_CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "FK_Post_LastModifiedBy");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DesiredStatus)
                    .HasColumnType("enum('Free','ForSale','Both')")
                    .HasDefaultValueSql("'Free'");

                entity.Property(e => e.EdocUrl).HasMaxLength(255);

                entity.Property(e => e.Hastag).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_ibfk_1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PostCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_CreatedBy");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.PostLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_LastModifiedBy");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registration");

                entity.HasIndex(e => e.CreatedBy, "FK_Registation_CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "FK_Registation_LastModifiedBy");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                    .HasColumnType("enum('Confirming','Accepted','Disapproved')")
                    .HasDefaultValueSql("'Confirming'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RegistrationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registation_CreatedBy");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.RegistrationLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registation_LastModifiedBy");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registration_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.CreatedBy, "FK_Role_CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "FK_Role_LastModifiedBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Role_CreatedBy");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.RoleLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("FK_Role_LastModifiedBy");

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "Userrole",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("userrole_ibfk_2"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("userrole_ibfk_1"),
                        j =>
                        {
                            j.HasKey("RoleId", "UserId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("userrole");

                            j.HasIndex(new[] { "UserId" }, "UserId");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.LastModifiedBy, "FK_User_LastModifiedBy");

                entity.HasIndex(e => e.CreatedBy, "FK_User_User");

                entity.HasIndex(e => e.FacultyId, "FacultyId");

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.PhoneNumber).HasMaxLength(12);

                entity.Property(e => e.StudentCode)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.UrlAvatar).HasColumnType("text");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_User_User");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.InverseLastModifiedByNavigation)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("FK_User_LastModifiedBy");
            });

            modelBuilder.Entity<Violationreport>(entity =>
            {
                entity.ToTable("violationreport");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.ItemFeedbackId, "ItemFeedbackId");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.HasIndex(e => e.ViolatorId, "ViolatorId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ViolationreportCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("violationreport_ibfk_4");

                entity.HasOne(d => d.ItemFeedback)
                    .WithMany(p => p.Violationreports)
                    .HasForeignKey(d => d.ItemFeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("violationreport_ibfk_2");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.ViolationreportLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("violationreport_ibfk_5");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Violationreports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("violationreport_ibfk_1");

                entity.HasOne(d => d.Violator)
                    .WithMany(p => p.ViolationreportViolators)
                    .HasForeignKey(d => d.ViolatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("violationreport_ibfk_3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
