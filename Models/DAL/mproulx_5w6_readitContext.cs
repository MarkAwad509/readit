using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Readit.Models.Entities;

namespace Readit.Models.DAL
{
    public partial class mproulx_5w6_readitContext : DbContext
    {
        public mproulx_5w6_readitContext()
        {
        }

        public mproulx_5w6_readitContext(DbContextOptions<mproulx_5w6_readitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Link> Links { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=mysql-mproulx.alwaysdata.net;database=mproulx_5w6_readit;port=3306;uid=mproulx;pwd=Admin22!;sslmode=Preferred", ServerVersion.Parse("10.5.16-mariadb")).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.LinkId, "Link_ID");

                entity.HasIndex(e => e.MemberId, "Member_ID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(100);

                entity.Property(e => e.LinkId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Link_ID");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Member_ID");

                entity.Property(e => e.PublicationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Publication_Date");

                entity.HasOne(d => d.Link)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.LinkId)
                    .HasConstraintName("Comment_ibfk_2");
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("Link");

                entity.HasIndex(e => e.MemberId, "Member_ID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DownVote).HasColumnType("int(11)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Member_ID");

                entity.Property(e => e.PublicationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Publication_Date");

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.UpVote).HasColumnType("int(11)");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("Link_ibfk_1");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "Username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(35);

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.Username).HasMaxLength(20);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("Vote");

                entity.HasIndex(e => e.LinkId, "Link_ID");

                entity.HasIndex(e => e.MemberId, "Member_ID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.IsUpVote).HasColumnType("bit(1)");

                entity.Property(e => e.LinkId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Link_ID");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Member_ID");

                entity.HasOne(d => d.Link)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.LinkId)
                    .HasConstraintName("Vote_ibfk_2");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("Vote_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
