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

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<ImgItem> ImgItems { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemFeedback> ItemFeedbacks { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationView> NotificationViews { get; set; } = null!;
        public virtual DbSet<Proposal> Proposals { get; set; } = null!;
        public virtual DbSet<Registation> Registations { get; set; } = null!;
        public virtual DbSet<TypeNotification> TypeNotifications { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3307;database=sharingtogether;user=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.NameCategory)
                    .HasMaxLength(100)
                    .HasColumnName("name_category");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("faculties");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.FacultyName)
                    .HasMaxLength(50)
                    .HasColumnName("faculty_name");
            });

            modelBuilder.Entity<ImgItem>(entity =>
            {
                entity.HasKey(e => e.IdImgItem)
                    .HasName("PRIMARY");

                entity.ToTable("img_items");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdImgItem).HasColumnName("id_img_item");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.FileEdoc)
                    .HasMaxLength(255)
                    .HasColumnName("file_edoc");

                entity.Property(e => e.ImageItem)
                    .HasMaxLength(255)
                    .HasColumnName("image_item");

                entity.Property(e => e.NameItem)
                    .HasMaxLength(50)
                    .HasColumnName("name_item");

                entity.Property(e => e.NoteItem)
                    .HasColumnType("text")
                    .HasColumnName("note_item");

                entity.Property(e => e.PostDate)
                    .HasColumnType("datetime")
                    .HasColumnName("post_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("items_ibfk_1");
            });

            modelBuilder.Entity<ItemFeedback>(entity =>
            {
                entity.ToTable("item_feedbacks");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ItemId, "item_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.ItemFeedbackId).HasColumnName("item_feedback_id");

                entity.Property(e => e.FeedbackContent)
                    .HasColumnType("text")
                    .HasColumnName("feedback_content");

                entity.Property(e => e.FeedbackDate)
                    .HasColumnType("datetime")
                    .HasColumnName("feedback_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemFeedbacks)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("item_feedbacks_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ItemFeedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("item_feedbacks_ibfk_2");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotifyId)
                    .HasName("PRIMARY");

                entity.ToTable("notifications");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.FromUserId, "user_id");

                entity.Property(e => e.NotifyId).HasColumnName("notify_id");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.IdRegistation).HasColumnName("id_registation");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.NotifyContent)
                    .HasColumnType("text")
                    .HasColumnName("notify_content");

                entity.Property(e => e.NotifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("notify_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("notifications_ibfk_2");
            });

            modelBuilder.Entity<NotificationView>(entity =>
            {
                entity.ToTable("notification_views");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserIdView, "fk_notification_views_users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.StatusView)
                    .HasColumnName("status_view")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserIdView).HasColumnName("user_id_view");

                entity.Property(e => e.ViewedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("viewed_at");

                entity.HasOne(d => d.UserIdViewNavigation)
                    .WithMany(p => p.NotificationViews)
                    .HasForeignKey(d => d.UserIdView)
                    .HasConstraintName("fk_notification_views_users");
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.ToTable("proposals");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.ProposalId).HasColumnName("proposal_id");

                entity.Property(e => e.ContentProposal)
                    .HasColumnType("text")
                    .HasColumnName("content_proposal");

                entity.Property(e => e.ImageProposal)
                    .HasMaxLength(255)
                    .HasColumnName("image_proposal");

                entity.Property(e => e.ProposalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("proposal_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.SubjectProposal)
                    .HasMaxLength(255)
                    .HasColumnName("subject_proposal");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("proposals_ibfk_1");
            });

            modelBuilder.Entity<Registation>(entity =>
            {
                entity.ToTable("registation");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ItemId, "item_id");

                entity.HasIndex(e => e.RegisterId, "register_id");

                entity.Property(e => e.RegistationId).HasColumnName("registation_id");

                entity.Property(e => e.ApprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("approval_date");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.RegistationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registation_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RegisterId).HasColumnName("register_id");

                entity.Property(e => e.RegisterNotifi).HasColumnName("register_notifi");

                entity.Property(e => e.RegisterStatus).HasColumnName("register_status");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Registations)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_registation_id_item");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.Registations)
                    .HasForeignKey(d => d.RegisterId)
                    .HasConstraintName("registation_ibfk_2");
            });

            modelBuilder.Entity<TypeNotification>(entity =>
            {
                entity.HasKey(e => e.IdTypeNotification)
                    .HasName("PRIMARY");

                entity.ToTable("type_notification");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdTypeNotification).HasColumnName("id_type_notification");

                entity.Property(e => e.NameTypeNotification)
                    .HasMaxLength(255)
                    .HasColumnName("name_type_notification");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AccountId, "account_id");

                entity.HasIndex(e => e.FacultyId, "faculty_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Class)
                    .HasMaxLength(15)
                    .HasColumnName("class");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("full_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .HasColumnName("phone_number");

                entity.Property(e => e.StudentCode)
                    .HasMaxLength(10)
                    .HasColumnName("student_code")
                    .IsFixedLength();

                entity.Property(e => e.UrlAvartar)
                    .HasColumnType("text")
                    .HasColumnName("url_avartar");

                entity.Property(e => e.YearAdmission).HasColumnName("year_admission");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("users_ibfk_1");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("users_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
