using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.Repositories;

namespace Services.Services
{
    public class Core : IDisposable
    {
        private GhasreMobileContext _context = new GhasreMobileContext();

        private MainRepo<TblAd> _ad;
        private MainRepo<TblBlog> _blog;
        private MainRepo<TblBlogCommentRel> _blogCommentRel;
        private MainRepo<TblBlogKeywordRel> _blogKeywordRel;
        private MainRepo<TblCatagory> _catagory;
        private MainRepo<TblClient> _client;
        private MainRepo<TblColor> _color;
        private MainRepo<TblComment> _comment;
        private MainRepo<TblConfig> _config;
        private MainRepo<TblDiscount> _discount;
        private MainRepo<TblImage> _image;
        private MainRepo<TblKeyword> _keyword;
        private MainRepo<TblOnlineOrder> _onlineOrder;
        private MainRepo<TblOrder> _order;
        private MainRepo<TblOrderDetail> _orderDetail;
        private MainRepo<TblProduct> _product;
        private MainRepo<TblProductCommentRel> _productCommentRel;
        private MainRepo<TblProductImageRel> _productImageRel;
        private MainRepo<TblProductKeywordRel> _productKeywordRel;
        private MainRepo<TblProductPropertyRel> _productPropertyRel;
        private MainRepo<TblProperty> _property;
        private MainRepo<TblRate> _rate;
        private MainRepo<TblRole> _role;
        private MainRepo<TblStore> _store;
        private MainRepo<TblTicket> _ticket;

        public MainRepo<TblAd> Ad => _ad ??= new MainRepo<TblAd>(_context);
        public MainRepo<TblBlog> Blog => _blog ??= new MainRepo<TblBlog>(_context);
        public MainRepo<TblBlogCommentRel> BlogCommentRel => _blogCommentRel ??= new MainRepo<TblBlogCommentRel>(_context);
        public MainRepo<TblBlogKeywordRel> BlogKeywordRel => _blogKeywordRel ??= new MainRepo<TblBlogKeywordRel>(_context);
        public MainRepo<TblCatagory> Catagory => _catagory ??= new MainRepo<TblCatagory>(_context);
        public MainRepo<TblClient> Client => _client ??= new MainRepo<TblClient>(_context);
        public MainRepo<TblColor> Color => _color ??= new MainRepo<TblColor>(_context);
        public MainRepo<TblComment> Comment => _comment ??= new MainRepo<TblComment>(_context);
        public MainRepo<TblConfig> Config => _config ??= new MainRepo<TblConfig>(_context);
        public MainRepo<TblDiscount> Discount => _discount ??= new MainRepo<TblDiscount>(_context);
        public MainRepo<TblImage> Image => _image ??= new MainRepo<TblImage>(_context);
        public MainRepo<TblKeyword> Keyword => _keyword ??= new MainRepo<TblKeyword>(_context);
        public MainRepo<TblOnlineOrder> OnlineOrder => _onlineOrder ??= new MainRepo<TblOnlineOrder>(_context);
        public MainRepo<TblOrder> Order => _order ??= new MainRepo<TblOrder>(_context);
        public MainRepo<TblOrderDetail> OrderDetail => _orderDetail ??= new MainRepo<TblOrderDetail>(_context);
        public MainRepo<TblProduct> Product => _product ??= new MainRepo<TblProduct>(_context);
        public MainRepo<TblProductCommentRel> ProductCommentRel => _productCommentRel ??= new MainRepo<TblProductCommentRel>(_context);
        public MainRepo<TblProductImageRel> ProductImageRel => _productImageRel ??= new MainRepo<TblProductImageRel>(_context);
        public MainRepo<TblProductKeywordRel> ProductKeywordRel => _productKeywordRel ??= new MainRepo<TblProductKeywordRel>(_context);
        public MainRepo<TblProductPropertyRel> ProductPropertyRel => _productPropertyRel ??= new MainRepo<TblProductPropertyRel>(_context);
        public MainRepo<TblProperty> Property => _property ??= new MainRepo<TblProperty>(_context);
        public MainRepo<TblRate> Rate => _rate ??= new MainRepo<TblRate>(_context);
        public MainRepo<TblRole> Role => _role ??= new MainRepo<TblRole>(_context);
        public MainRepo<TblStore> Store => _store ??= new MainRepo<TblStore>(_context);
        public MainRepo<TblTicket> Ticket => _ticket ??= new MainRepo<TblTicket>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
