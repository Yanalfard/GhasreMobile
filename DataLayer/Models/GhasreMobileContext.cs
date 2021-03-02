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
        public virtual DbSet<TblDelivery> TblDelivery { get; set; }
        public virtual DbSet<TblDiscount> TblDiscount { get; set; }
        public virtual DbSet<TblImage> TblImage { get; set; }
        public virtual DbSet<TblKeyword> TblKeyword { get; set; }
        public virtual DbSet<TblNotification> TblNotification { get; set; }
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
        public virtual DbSet<TblRegularQuestion> TblRegularQuestion { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblSpecialOffer> TblSpecialOffer { get; set; }
        public virtual DbSet<TblStore> TblStore { get; set; }
        public virtual DbSet<TblTicket> TblTicket { get; set; }
        public virtual DbSet<TblTopic> TblTopic { get; set; }
        public virtual DbSet<TblTopicCommentRel> TblTopicCommentRel { get; set; }
        public virtual DbSet<TblWallet> TblWallet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
           .UseLazyLoadingProxies()
           .UseSqlServer("Data Source=103.216.62.27;Initial Catalog=GhasreMobile;User ID=Yanal;Password=1710ahmad.fard");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAd>(entity =>
            {
                entity.HasKey(e => e.AdId);

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Label).HasMaxLength(500);

                entity.Property(e => e.Link).IsRequired();
            });

            modelBuilder.Entity<TblAlbum>(entity =>
            {
                entity.HasKey(e => e.AlbumId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblAlertWhenReady>(entity =>
            {
                entity.HasKey(e => e.AlertWhenReady);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblAlertWhenReady)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblAlertWhenReady_TblClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblAlertWhenReady)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblAlertWhenReady_TblProduct");
            });

            modelBuilder.Entity<TblBankAccounts>(entity =>
            {
                entity.HasKey(e => e.BankAccountId);

                entity.Property(e => e.AccountNo)
                    .IsRequired()
                    .HasMaxLength(19);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblBannerAndSlide>(entity =>
            {
                entity.HasKey(e => e.BannerAndSlideId);

                entity.Property(e => e.ActiveTill).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TblBlog>(entity =>
            {
                entity.HasKey(e => e.BlogId);

                entity.Property(e => e.BodyHtml).IsRequired();

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.MainImage).HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TblBlogCommentRel>(entity =>
            {
                entity.HasKey(e => e.BlogCommentRelId);

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
                entity.HasKey(e => e.BlogKeywordRelId);

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
                entity.HasKey(e => e.BookMarkId);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblBookMark)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblBookMark_TblClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblBookMark)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblBookMark_TblProduct");
            });

            modelBuilder.Entity<TblBrand>(entity =>
            {
                entity.HasKey(e => e.BrandId);

                entity.Property(e => e.MainImage).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.HasKey(e => e.CatagoryId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblCatagory_TblCatagory");
            });

            modelBuilder.Entity<TblClient>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MainImage).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.TellNo)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblClient)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblClient_TblRole");
            });

            modelBuilder.Entity<TblColor>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.ColorCode).HasMaxLength(7);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblColor)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblColor_TblProduct");
            });

            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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

            modelBuilder.Entity<TblConfig>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key).HasMaxLength(128);

                entity.Property(e => e.Value).HasMaxLength(500);
            });

            modelBuilder.Entity<TblDelivery>(entity =>
            {
                entity.HasKey(e => e.DeliveryId);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TellNo)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TblDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ValidTill).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.Image).IsRequired();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.TblImage)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_TblImage_TblAlbum");
            });

            modelBuilder.Entity<TblKeyword>(entity =>
            {
                entity.HasKey(e => e.KeywordId)
                    .HasName("PK_TblKeywords");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50);

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

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateSubmited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblOnlineOrder)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblOrder_TblClient");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrdeId)
                    .HasName("PK_TblOrder_1");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DateSubmited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentStatus).HasComment("0 is online; 1 is KartBeKart; 2 is darbe manzel ya frushgah;");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SendPrice).HasComment("0");

                entity.Property(e => e.SendStatus).HasComment("0 is via post; 1 is via client comes and pics it up himselfe; 2 chapar/tipax");

                entity.Property(e => e.Status).HasComment("0 is making; 1 is on its way; 2 is done;");

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
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MainImage)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.SearchText).HasMaxLength(500);

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
                entity.HasKey(e => e.ProductCommentRelId);

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
                entity.HasKey(e => e.ProductImageRelId);

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
                entity.HasKey(e => e.ProductKeywordRelId);

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
                entity.HasKey(e => e.ProductPropertyRelId);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductPropertyRel)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblProductPropertyRel_TblProduct");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.TblProductPropertyRel)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_TblProductPropertyRel_TblProperty");
            });

            modelBuilder.Entity<TblProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblRate>(entity =>
            {
                entity.HasKey(e => e.RateId);

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(15);

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

            modelBuilder.Entity<TblRegularQuestion>(entity =>
            {
                entity.HasKey(e => e.RegularQuestionId);

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<TblSpecialOffer>(entity =>
            {
                entity.HasKey(e => e.SpecialOfferId);

                entity.Property(e => e.ValidTill).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblSpecialOffer)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_TblSpecialOffer_TblProduct");
            });

            modelBuilder.Entity<TblStore>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Lat).HasMaxLength(50);

                entity.Property(e => e.Lon).HasMaxLength(50);

                entity.Property(e => e.MainImage)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TellNo).HasMaxLength(200);
            });

            modelBuilder.Entity<TblTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK_Ticket");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.DateSubmited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTicket)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblTicket_TblClient");
            });

            modelBuilder.Entity<TblTopic>(entity =>
            {
                entity.HasKey(e => e.TopicId);

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTopic)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_TblTopic_TblClient");
            });

            modelBuilder.Entity<TblTopicCommentRel>(entity =>
            {
                entity.HasKey(e => e.TopicCommentRelId);

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

            modelBuilder.Entity<TblWallet>(entity =>
            {
                entity.HasKey(e => e.WalletId);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblWallet)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblWallet_TblClient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
