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
        public virtual DbSet<TblAlbum> TblAlbum { get; set; }
        public virtual DbSet<TblAlertWhenReady> TblAlertWhenReady { get; set; }
        public virtual DbSet<TblBankAccounts> TblBankAccounts { get; set; }
        public virtual DbSet<TblBannerAndSlide> TblBannerAndSlide { get; set; }
        public virtual DbSet<TblBlog> TblBlog { get; set; }
        public virtual DbSet<TblBlogCommentRel> TblBlogCommentRel { get; set; }
        public virtual DbSet<TblBlogKeywordRel> TblBlogKeywordRel { get; set; }
        public virtual DbSet<TblBookMark> TblBookMark { get; set; }
        public virtual DbSet<TblBrand> TblBrand { get; set; }
        public virtual DbSet<TblCatagory> TblCatagory { get; set; }
        public virtual DbSet<TblClient> TblClient { get; set; }
        public virtual DbSet<TblColor> TblColor { get; set; }
        public virtual DbSet<TblComment> TblComment { get; set; }
        public virtual DbSet<TblConfig> TblConfig { get; set; }
        public virtual DbSet<TblContactUs> TblContactUs { get; set; }
        public virtual DbSet<TblDelivery> TblDelivery { get; set; }
        public virtual DbSet<TblDiscount> TblDiscount { get; set; }
        public virtual DbSet<TblImage> TblImage { get; set; }
        public virtual DbSet<TblKeyword> TblKeyword { get; set; }
        public virtual DbSet<TblNotification> TblNotification { get; set; }
        public virtual DbSet<TblOnlineOrder> TblOnlineOrder { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblOrderDetail> TblOrderDetail { get; set; }
        public virtual DbSet<TblPostOption> TblPostOption { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblProductCommentRel> TblProductCommentRel { get; set; }
        public virtual DbSet<TblProductImageRel> TblProductImageRel { get; set; }
        public virtual DbSet<TblProductKeywordRel> TblProductKeywordRel { get; set; }
        public virtual DbSet<TblProductPropertyRel> TblProductPropertyRel { get; set; }
        public virtual DbSet<TblProperty> TblProperty { get; set; }
        public virtual DbSet<TblRate> TblRate { get; set; }
        public virtual DbSet<TblRegularQuestion> TblRegularQuestion { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblSpecialOffer> TblSpecialOffer { get; set; }
        public virtual DbSet<TblStore> TblStore { get; set; }
        public virtual DbSet<TblStoreImageRel> TblStoreImageRel { get; set; }
        public virtual DbSet<TblTicket> TblTicket { get; set; }
        public virtual DbSet<TblTopic> TblTopic { get; set; }
        public virtual DbSet<TblTopicCommentRel> TblTopicCommentRel { get; set; }
        public virtual DbSet<TblVisit> TblVisit { get; set; }
        public virtual DbSet<TblWallet> TblWallet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=185.55.224.183;Initial Catalog=asamedc1_gasremobile;User ID=asamedc1_Yanal3;Password=2fjS9CYVYkgS5V8");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "asamedc1_Yanal3");

            modelBuilder.Entity<TblAlbum>(entity =>
            {
                entity.Property(e => e.IsProduct).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblAlertWhenReady>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblAlertWhenReady)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblAlertWhenReady_TblClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblAlertWhenReady)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblAlertWhenReady_TblProduct");
            });

            modelBuilder.Entity<TblBlog>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            });

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

            modelBuilder.Entity<TblBookMark>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblBookMark)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblBookMark_TblClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblBookMark)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblBookMark_TblProduct");
            });

            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblCatagory_TblCatagory");
            });

            modelBuilder.Entity<TblClient>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

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
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

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

            modelBuilder.Entity<TblDelivery>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.HasOne(d => d.Album)
                    .WithMany(p => p.TblImage)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_TblImage_TblAlbum");
            });

            modelBuilder.Entity<TblKeyword>(entity =>
            {
                entity.HasKey(e => e.KeywordId)
                    .HasName("PK_TblKeywords");
            });

            modelBuilder.Entity<TblNotification>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblNotificationClient)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblNotification_TblClient");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.TblNotificationSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblNotification_TblClient1");
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

                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentStatus).HasComment("0 is online; 1 is KartBeKart; 2 is darbe manzel ya frushgah;");

                entity.Property(e => e.SendPrice).HasComment("0");

                entity.Property(e => e.SendStatus).HasComment("0 is via post; 1 is via client comes and pics it up himselfe; 2 chapar/tipax");

                entity.Property(e => e.Status).HasComment("0 is making; 1 is on its way; 2 is done;");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrder_TblClient1");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_TblOrder_TblDiscount");

                entity.HasOne(d => d.Sent)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.SentId)
                    .HasConstraintName("FK_TblOrder_TblPostOption");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK_TblClientProductRel");

                entity.Property(e => e.Count).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_TblOrderDetail_TblColor");

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

            modelBuilder.Entity<TblPostOption>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_TblBrand");

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

            modelBuilder.Entity<TblSpecialOffer>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblSpecialOffer)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblSpecialOffer_TblProduct");
            });

            modelBuilder.Entity<TblStoreImageRel>(entity =>
            {
                entity.HasOne(d => d.Image)
                    .WithMany(p => p.TblStoreImageRel)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_TblStoreImageRel_TblImage");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblStoreImageRel)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TblStoreImageRel_TblStore");
            });

            modelBuilder.Entity<TblTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK_Ticket");

                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTicket)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblTicket_TblClient");
            });

            modelBuilder.Entity<TblTopic>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTopic)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblTopic_TblClient");
            });

            modelBuilder.Entity<TblTopicCommentRel>(entity =>
            {
                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.TblTopicCommentRel)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_TblTopicCommentRel_TblComment");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TblTopicCommentRel)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblTopicCommentRel_TblTopic");
            });

            modelBuilder.Entity<TblVisit>(entity =>
            {
                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblWallet>(entity =>
            {
                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblWallet)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblWallet_TblClient");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblWallet)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_TblWallet_TblOrder");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
