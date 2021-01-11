using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Models
{
    public partial class GhasreMobileContext : DbContext
    {
        public GhasreMobileContext()
        {
        }

        public GhasreMobileContext(DbContextOptions<GhasreMobileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAd> TblAd { get; set; }
        public virtual DbSet<TblBlog> TblBlog { get; set; }
        public virtual DbSet<TblBlogCommentRel> TblBlogCommentRel { get; set; }
        public virtual DbSet<TblBlogKeywordRel> TblBlogKeywordRel { get; set; }
        public virtual DbSet<TblCatagory> TblCatagory { get; set; }
        public virtual DbSet<TblClient> TblClient { get; set; }
        public virtual DbSet<TblColor> TblColor { get; set; }
        public virtual DbSet<TblComment> TblComment { get; set; }
        public virtual DbSet<TblConfig> TblConfig { get; set; }
        public virtual DbSet<TblDiscount> TblDiscount { get; set; }
        public virtual DbSet<TblImage> TblImage { get; set; }
        public virtual DbSet<TblKeyword> TblKeyword { get; set; }
        public virtual DbSet<TblOnlineOrder> TblOnlineOrder { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblOrderDetail> TblOrderDetail { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblProductCommentRel> TblProductCommentRel { get; set; }
        public virtual DbSet<TblProductImageRel> TblProductImageRel { get; set; }
        public virtual DbSet<TblProductKeywordRel> TblProductKeywordRel { get; set; }
        public virtual DbSet<TblProductPropertyRel> TblProductPropertyRel { get; set; }
        public virtual DbSet<TblProperty> TblProperty { get; set; }
        public virtual DbSet<TblRate> TblRate { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblStore> TblStore { get; set; }
        public virtual DbSet<TblTicket> TblTicket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=103.216.62.27;Initial Catalog=GhasreMobile;User ID=Yanal;Password=1710ahmad.fard");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBlogCommentRel>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TblBlogCommentRel)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_TblBlogCommentRel_TblBlog");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.TblBlogCommentRel)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_TblBlogCommentRel_TblComment");
            });

            modelBuilder.Entity<TblBlogKeywordRel>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TblBlogKeywordRel)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_TblBlogKeywordRel_TblBlog");

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.TblBlogKeywordRel)
                    .HasForeignKey(d => d.KeywordId)
                    .HasConstraintName("FK_TblBlogKeywordRel_TblKeyword");
            });

            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.Property(e => e.IsOnFirstPage).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblCatagory_TblCatagory");
            });

            modelBuilder.Entity<TblClient>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblClient)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblClient_TblRole");
            });

            modelBuilder.Entity<TblColor>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblColor)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblColor_TblProduct");
            });

            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsValid).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblComment)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblComment_TblClient");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblComment_TblComment");
            });

            modelBuilder.Entity<TblKeyword>(entity =>
            {
                entity.HasKey(e => e.KeywordId)
                    .HasName("PK_TblKeywords");
            });

            modelBuilder.Entity<TblOnlineOrder>(entity =>
            {
                entity.HasKey(e => e.OnlineOrderId)
                    .HasName("PK_TblOrder");

                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblOnlineOrder)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblOrder_TblClient");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrdeId)
                    .HasName("PK_TblOrder_1");

                entity.Property(e => e.IsPayed).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_TblOrder_TblDiscount");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK_TblClientProductRel");

                entity.Property(e => e.Count).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblClientProductRel_TblClient");

                entity.HasOne(d => d.FinalOrder)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.FinalOrderId)
                    .HasConstraintName("FK_TblOrderDetail_TblOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrderDetail_TblProduct");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.CatagoryId)
                    .HasConstraintName("FK_TblProduct_TblCatagory");
            });

            modelBuilder.Entity<TblProductCommentRel>(entity =>
            {
                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.TblProductCommentRel)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_TblProductCommentRel_TblComment");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductCommentRel)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblProductCommentRel_TblProduct");
            });

            modelBuilder.Entity<TblProductImageRel>(entity =>
            {
                entity.HasOne(d => d.Image)
                    .WithMany(p => p.TblProductImageRel)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_TblProductImageRel_TblImage");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductImageRel)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblProductImageRel_TblProduct");
            });

            modelBuilder.Entity<TblProductKeywordRel>(entity =>
            {
                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.TblProductKeywordRel)
                    .HasForeignKey(d => d.KeywordId)
                    .HasConstraintName("FK_TblProductKeywordRel_TblKeywords");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductKeywordRel)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblProductKeywordRel_TblProduct");
            });

            modelBuilder.Entity<TblProductPropertyRel>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductPropertyRel)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblProductPropertyRel_TblProduct");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.TblProductPropertyRel)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_TblProductPropertyRel_TblProperty");
            });

            modelBuilder.Entity<TblRate>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TblRate)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblRate_TblBlog");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblRate)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblRate_TblClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblRate)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblRate_TblProduct");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<TblTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK_Ticket");

                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAnswered).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTicket)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblTicket_TblClient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
