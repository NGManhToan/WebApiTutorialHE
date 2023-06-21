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
        public virtual DbSet<ItemFeedback> ItemFeedbacks { get; set; } = null!;
        public virtual DbSet<Medium> Media { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationView> NotificationViews { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<ViolationReport> ViolationReports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=180.93.172.49;port=4786;database=SharingTogether;username=user_share_together;password=RFYsGMZq2*AK", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

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

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Category_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.CategoryLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Category_ibfk_2");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

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
                    .HasConstraintName("Comment_ibfk_3");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.CommentLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_ibfk_4");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("Comment_ibfk_2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_ibfk_1");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

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

            modelBuilder.Entity<ItemFeedback>(entity =>
            {
                entity.ToTable("ItemFeedback");

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
                    .WithMany(p => p.ItemFeedbackCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemFeedback_ibfk_2");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.ItemFeedbackLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemFeedback_ibfk_3");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ItemFeedbacks)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ItemFeedback_ibfk_1");
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasIndex(e => e.PostId, "PostId");

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.ThumbnailUrl).HasMaxLength(255);

                entity.Property(e => e.VideoUrl).HasMaxLength(255);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Media_ibfk_1");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.FromUserId, "FromUserId");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.PostId, "PostId");

                entity.HasIndex(e => e.RegistrationId, "RegistrationId");

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

                entity.Property(e => e.Type).HasColumnType("enum('Suggest giving','Suggest receiving','Registration','Approve status','Comment','Post the item from wishlist')");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NotificationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("Notification_ibfk_5");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.NotificationFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("Notification_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.NotificationLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("Notification_ibfk_6");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Notification_ibfk_4");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.RegistrationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Notification_ibfk_2");
            });

            modelBuilder.Entity<NotificationView>(entity =>
            {
                entity.ToTable("NotificationView");

                entity.HasIndex(e => e.NotificationId, "NotificationId");

                entity.HasIndex(e => e.ReceiverId, "ReceiverId");

                entity.Property(e => e.ViewedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationViews)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotificationView_ibfk_2");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.NotificationViews)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotificationView_ibfk_1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.CategoryId, "CategoryId");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

                entity.HasIndex(e => e.FromWishList, "fk_Post_wishlist");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DesiredStatus)
                    .HasColumnType("enum('Free','ForSale','Both')")
                    .HasDefaultValueSql("'Free'");

                entity.Property(e => e.EdocUrl).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status).HasColumnType("enum('Used','New')");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.Type).HasColumnType("enum('Sharing items','Wish items')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_ibfk_1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PostCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_ibfk_2");

                entity.HasOne(d => d.FromWishListNavigation)
                    .WithMany(p => p.InverseFromWishListNavigation)
                    .HasForeignKey(d => d.FromWishList)
                    .HasConstraintName("fk_Post_wishlist");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.PostLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_ibfk_3");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

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

                entity.Property(e => e.Status)
                    .HasColumnType("enum('Confirming','Accepted','Disapproved')")
                    .HasDefaultValueSql("'Confirming'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RegistrationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Registration_ibfk_2");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.RegistrationLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Registration_ibfk_3");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Registration_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

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

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("UserRole_ibfk_2"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("UserRole_ibfk_1"),
                        j =>
                        {
                            j.HasKey("RoleId", "UserId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("UserRole");

                            j.HasIndex(new[] { "UserId" }, "UserId");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.CreatedBy, "CreatedBy");

                entity.HasIndex(e => e.FacultyId, "FacultyId");

                entity.HasIndex(e => e.LastModifiedBy, "LastModifiedBy");

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
                    .HasConstraintName("User_ibfk_2");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_ibfk_1");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.InverseLastModifiedByNavigation)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("User_ibfk_3");
            });

            modelBuilder.Entity<ViolationReport>(entity =>
            {
                entity.ToTable("ViolationReport");

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
                    .WithMany(p => p.ViolationReportCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ViolationReport_ibfk_4");

                entity.HasOne(d => d.ItemFeedback)
                    .WithMany(p => p.ViolationReports)
                    .HasForeignKey(d => d.ItemFeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ViolationReport_ibfk_2");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.ViolationReportLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ViolationReport_ibfk_5");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ViolationReports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ViolationReport_ibfk_1");

                entity.HasOne(d => d.Violator)
                    .WithMany(p => p.ViolationReportViolators)
                    .HasForeignKey(d => d.ViolatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ViolationReport_ibfk_3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
