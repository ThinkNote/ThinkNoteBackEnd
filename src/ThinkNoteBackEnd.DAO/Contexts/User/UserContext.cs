using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThinkNoteBackEnd.DAO.User
{
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NoteFileTracker> NoteFileTracker { get; set; }
        public virtual DbSet<StaticResourceTracker> StaticResourceTracker { get; set; }
        public virtual DbSet<StudyGroupInfo> StudyGroupInfo { get; set; }
        public virtual DbSet<UserJoinGroups> UserJoinGroups { get; set; }
        public virtual DbSet<UserLoginInfo> UserLoginInfo { get; set; }
        public virtual DbSet<UserProfileInfo> UserProfileInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteFileTracker>(entity =>
            {
                entity.ToTable("note_file_tracker", "thinknote_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GenerateType)
                    .HasColumnName("generate_type")
                    .HasColumnType("int(5)");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("guid")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerUid)
                    .HasColumnName("owner_uid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Visibility)
                    .HasColumnName("visibility")
                    .HasColumnType("int(5)");
            });

            modelBuilder.Entity<StaticResourceTracker>(entity =>
            {
                entity.ToTable("static_resource_tracker", "thinknote_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssoFileGuid)
                    .HasColumnName("asso_file_guid")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("guid")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerCourseId)
                    .HasColumnName("owner_course_id")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerCourseYear)
                    .HasColumnName("owner_course_year")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OwnerSchoolCode)
                    .HasColumnName("owner_school_code")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerTeacherUid)
                    .HasColumnName("owner_teacher_uid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ResourceType)
                    .IsRequired()
                    .HasColumnName("resource_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SourcePath)
                    .IsRequired()
                    .HasColumnName("source_path")
                    .IsUnicode(false);

                entity.Property(e => e.UploaderType)
                    .HasColumnName("uploader_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudyGroupInfo>(entity =>
            {
                entity.ToTable("study_group_info", "thinknote_db");

                entity.HasIndex(e => e.Id)
                    .HasName("uid_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatorUid)
                    .HasColumnName("creator_uid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Groupname)
                    .IsRequired()
                    .HasColumnName("groupname")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserJoinGroups>(entity =>
            {
                entity.ToTable("user_join_groups", "thinknote_db");

                entity.HasIndex(e => e.JoinUserUid)
                    .HasName("join_user_uid_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JoinGroupId)
                    .HasColumnName("join_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JoinUserUid)
                    .HasColumnName("join_user_uid")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.JoinUserU)
                    .WithMany(p => p.UserJoinGroups)
                    .HasForeignKey(d => d.JoinUserUid)
                    .HasConstraintName("join_user_uid_fk");
            });

            modelBuilder.Entity<UserLoginInfo>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("user_login_info", "thinknote_db");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Uid)
                    .HasName("uid_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasColumnType("int(2)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserProfileInfo>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("user_profile_info", "thinknote_db");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Uid)
                    .HasName("uid_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RealName)
                    .HasColumnName("real_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolCode)
                    .HasColumnName("school_code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Signature)
                    .HasColumnName("signature")
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasColumnType("int(2)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.U)
                    .WithOne(p => p.UserProfileInfo)
                    .HasForeignKey<UserProfileInfo>(d => d.Uid)
                    .HasConstraintName("uid_fk");
            });
        }
    }
}
