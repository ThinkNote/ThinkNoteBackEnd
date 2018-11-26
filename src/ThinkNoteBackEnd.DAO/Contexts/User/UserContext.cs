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

        public virtual DbSet<UserLoginInfo> UserLoginInfo { get; set; }
        public virtual DbSet<UserProfileInfo> UserProfileInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("");
            }
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
