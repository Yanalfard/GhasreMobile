using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using GhasreMobile.Utilities;
using DataLayer.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class ProductController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(ProductsInAdminVm productsInAdmin)
        {
            if (productsInAdmin.CatagoryId != 0)
            {
                if (string.IsNullOrEmpty(productsInAdmin.Search))
                {
                    if (productsInAdmin.InPageCount == 0)
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(c => c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * 18;

                        ViewBag.pageid = productsInAdmin.PageId;

                        ViewBag.PageCount = count / 18;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(18));
                    }
                    else
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(c => c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * productsInAdmin.InPageCount;

                        ViewBag.pageid = productsInAdmin.PageId;

                        ViewBag.PageCount = count / productsInAdmin.InPageCount;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(productsInAdmin.InPageCount));
                    }
                }
                else
                {
                    if (productsInAdmin.InPageCount == 0)
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(c => c.SearchText.Contains(productsInAdmin.Search) && c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * 18;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / 18;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewBag.Search = productsInAdmin.Search;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(18));
                    }
                    else
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(c => c.SearchText.Contains(productsInAdmin.Search) && c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * productsInAdmin.InPageCount;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / productsInAdmin.InPageCount;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewBag.Search = productsInAdmin.Search;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(productsInAdmin.InPageCount));
                    }
                }
            }
            else
            {

                if (string.IsNullOrEmpty(productsInAdmin.Search))
                {
                    if (productsInAdmin.InPageCount == 0)
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get().OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * 18;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / 18;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(18));
                    }
                    else
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get().OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * productsInAdmin.InPageCount;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / productsInAdmin.InPageCount;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;

                        ViewBag.Search = productsInAdmin.Search;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(productsInAdmin.InPageCount));
                    }
                }
                else
                {
                    if (productsInAdmin.InPageCount == 0)
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(p => p.Name.Contains(productsInAdmin.Search)).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * 18;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / 18;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(18));
                    }
                    else
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(p => p.Name.Contains(productsInAdmin.Search)).OrderByDescending(p => p.CatagoryId);
                        int count = products.Count();

                        var skip = (productsInAdmin.PageId - 1) * productsInAdmin.InPageCount;

                        ViewBag.PageId = productsInAdmin.PageId;

                        ViewBag.PageCount = count / productsInAdmin.InPageCount;

                        ViewBag.InPageCount = productsInAdmin.InPageCount;

                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        return View(products.Skip(skip).Take(productsInAdmin.InPageCount));
                    }
                }
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblProduct product,
                                                List<string> Keywords,
                                                List<string> Colors,
                                                List<string> ColorName,
                                                List<int> ColorsCounts,
                                                List<int?> PropertyId,
                                                List<string> Value,
                                                IFormFile MainImage,
                                                List<IFormFile> GalleryFile
            )
        {
            if (ModelState.IsValid)
            {
                if (MainImage == null)
                {
                    ModelState.AddModelError("MainImage", "تصویر الزامی میباشد . لطفا موارد را بررسی کنید");
                    ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                    ViewBag.Brands = _core.Brand.Get();
                    return await Task.FromResult(View(product));
                }
                else
                {
                    if (MainImage.Length > 3000000)
                    {
                        ModelState.AddModelError("MainImage", "حجم فایل عکس اصلی بیش از اندازه میباشد");
                        ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                        ViewBag.Brands = _core.Brand.Get();
                        return await Task.FromResult(View(product));
                    }
                    else
                    {
                        //New Prodcut
                        TblProduct NewProduct = new TblProduct();
                        NewProduct.Name = product.Name;
                        if (MainImage != null)
                        {
                            NewProduct.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                            string saveDirectory = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain");
                            string savePath = Path.Combine(saveDirectory, NewProduct.MainImage);

                            if (!Directory.Exists(saveDirectory))
                            {
                                Directory.CreateDirectory(saveDirectory);
                            }

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await MainImage.CopyToAsync(stream);
                            }
                        }
                        NewProduct.PriceBeforeDiscount = product.PriceBeforeDiscount;
                        NewProduct.DescriptionShortHtml = product.DescriptionShortHtml;
                        NewProduct.DescriptionLongHtml = product.DescriptionLongHtml;
                        NewProduct.CatagoryId = product.CatagoryId;
                        if (product.PriceAfterDiscount != null)
                        {
                            NewProduct.PriceAfterDiscount =
                                NewProduct.PriceBeforeDiscount - (NewProduct.PriceBeforeDiscount * product.PriceAfterDiscount / 100);
                        }
                        else
                        {
                            NewProduct.PriceAfterDiscount = 0;
                        }
                        NewProduct.DateCreated = DateTime.Now;
                        NewProduct.SearchText = product.SearchText;
                        NewProduct.IsFractional = product.IsFractional;
                        NewProduct.BrandId = product.BrandId;

                        _core.Product.Add(NewProduct);
                        _core.Product.Save();
                        //New Prodcut

                        TblAlbum album = new TblAlbum();
                        album.Name = NewProduct.Name;
                        _core.Album.Add(album);
                        _core.Album.Save();
                        if (GalleryFile.Count() > 0)
                        {
                            foreach (var item in GalleryFile)
                            {
                                TblImage image = new TblImage();
                                image.AlbumId = album.AlbumId;
                                image.Image = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                                string saveDirectory = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum");
                                string savePathAlbum = Path.Combine(
                                                    Directory.GetCurrentDirectory(), saveDirectory, image.Image);

                                if (!Directory.Exists(saveDirectory))
                                {
                                    Directory.CreateDirectory(saveDirectory);
                                }

                                using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                                {
                                    await item.CopyToAsync(stream);
                                }
                                _core.Image.Add(image);
                                _core.Image.Save();
                                TblProductImageRel tblProductImageRel = new TblProductImageRel();
                                tblProductImageRel.ImageId = image.ImageId;
                                tblProductImageRel.ProductId = NewProduct.ProductId;
                                _core.ProductImageRel.Add(tblProductImageRel);
                                _core.ProductImageRel.Save();
                            }
                        }

                        if (PropertyId != null)
                        {
                            for (int i = 0; i < PropertyId.Count; i++)
                            {
                                TblProductPropertyRel propertyRel = new TblProductPropertyRel();
                                propertyRel.PropertyId = PropertyId[i].Value;
                                if (Value[i] != null)
                                {
                                    propertyRel.Value = Value[i];
                                }
                                else
                                {
                                    propertyRel.Value = "";
                                }

                                propertyRel.ProductId = NewProduct.ProductId;
                                _core.ProductPropertyRel.Add(propertyRel);
                            }
                        }

                        for (int i = 0; i < Colors.Count; i++)
                        {
                            TblColor Newcolor = new TblColor();
                            Newcolor.Name = ColorName[i];
                            Newcolor.ColorCode = Colors[i];
                            Newcolor.Count = ColorsCounts[i];
                            Newcolor.ProductId = NewProduct.ProductId;
                            _core.Color.Add(Newcolor);
                        }
                        _core.Color.Save();

                        foreach (var item in Keywords)
                        {
                            if (_core.Keyword.Get().Any(k => k.Name == item.Replace(" ", "-")))
                            {
                                TblKeyword keyword = _core.Keyword.Get().Single(k => k.Name == item.Replace(" ", "-"));
                                TblProductKeywordRel keywordRel = new TblProductKeywordRel();
                                keywordRel.KeywordId = keyword.KeywordId;
                                keywordRel.ProductId = NewProduct.ProductId;
                                _core.ProductKeywordRel.Add(keywordRel);
                            }
                            else
                            {
                                TblKeyword Newkeyword = new TblKeyword();
                                Newkeyword.Name = item.Replace(" ", "-");
                                _core.Keyword.Add(Newkeyword);
                                _core.Keyword.Save();
                                TblProductKeywordRel keywordRel = new TblProductKeywordRel();
                                keywordRel.KeywordId = Newkeyword.KeywordId;
                                keywordRel.ProductId = NewProduct.ProductId;
                                _core.ProductKeywordRel.Add(keywordRel);

                            }
                        }
                        _core.ProductKeywordRel.Save();



                        return await Task.FromResult(Redirect("/Admin/Product"));
                    }
                }


            }
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return await Task.FromResult(View(product));
        }

        [HttpGet]
        public IActionResult PropertyList()
        {
            return ViewComponent("PropertyListAdmin");
        }

        public IActionResult Stock(int id)
        {
            return ViewComponent("EditStokeAdmin", new { Id = id });
        }

        [HttpPost]
        public async Task<string> EditStoke(int Id, int count)
        {
            TblColor color = _core.Color.GetById(Id);
            TblProduct product = _core.Product.GetById(color.ProductId);
            if (color.Count == 0)
            {
                if (count > 0)
                {
                    IEnumerable<TblAlertWhenReady> whenReadies = _core.AlertWhenReady.Get(a => a.ProductId == product.ProductId);
                    if (whenReadies.Count() > 0)
                    {
                        foreach (var item in whenReadies)
                        {
                            await Sms.SendSms2(item.Client.TellNo, item.Product.Name, "Https://gasrmobile2004.com/" + item.ProductId + "/" + item.Product.Name.Replace(" ", "-"), "GhasrMobileAlertWhenReady");
                            _core.AlertWhenReady.Delete(item);
                            _core.AlertWhenReady.Save();
                        }
                    }
                }
            }
            color.Count = count;
            _core.Color.Update(color);
            _core.Color.Save();
            return "true";
        }

        public void EditPrice(int Id, long Price)
        {
            TblProduct product = _core.Product.GetById(Id);
            if (product.PriceAfterDiscount != 0)
            {
                product.PriceAfterDiscount = Price;
            }
            else
            {
                product.PriceBeforeDiscount = Price;
            }
            _core.Product.Update(product);
            _core.Product.Save();
        }

        public string RemoveAlbumImage(int id)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", _core.Image.GetById(id).Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }


            TblProductImageRel tblProductImageRel = _core.ProductImageRel.Get().Where(i => i.ImageId == id).SingleOrDefault();
            _core.ProductImageRel.Delete(tblProductImageRel);
            _core.Image.DeleteById(id);
            _core.Image.Save();
            return "true";
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return View(_core.Product.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TblProduct product,
                                                List<string> Keywords,
                                                List<int?> PropertyId,
                                                List<string> Value,
                                                IFormFile MainImage,
                                                List<IFormFile> GalleryFile
            )
        {
            if (ModelState.IsValid)
            {
                TblProduct EditProduct = _core.Product.GetById(product.ProductId);
                if (MainImage != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain", EditProduct.MainImage);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    EditProduct.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain", EditProduct.MainImage
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                }

                if (_core.ProductKeywordRel.Get(k => k.ProductId == product.ProductId).Count() > 0)
                {
                    IEnumerable<TblProductKeywordRel> keywordRels = _core.ProductKeywordRel.Get(k => k.ProductId == product.ProductId);

                    foreach (var item in keywordRels)
                    {
                        _core.ProductKeywordRel.Delete(item);
                    }
                    _core.ProductKeywordRel.Save();

                    if (Keywords.Count > 0)
                    {
                        foreach (var item in Keywords)
                        {
                            if (_core.Keyword.Get().Any(k => k.Name == item.Replace("                                       ", "")))
                            {
                                TblKeyword keyword = _core.Keyword.Get(k => k.Name == item.Replace("                                       ", "")).SingleOrDefault();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.ProductId = product.ProductId;
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.ProductKeywordRel.Save();
                            }
                            else
                            {
                                TblKeyword keyword = new TblKeyword();
                                keyword.Name = item.Replace("                                       ", "");
                                _core.Keyword.Add(keyword);
                                _core.Keyword.Save();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                tblProductKeywordRel.ProductId = product.ProductId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.ProductKeywordRel.Save();
                            }
                        }
                    }
                }
                else
                {
                    if (Keywords.Count > 0)
                    {
                        foreach (var item in Keywords)
                        {
                            if (_core.Keyword.Get().Any(k => k.Name == item.Replace("                                       ", "")))
                            {
                                TblKeyword keyword = _core.Keyword.Get(k => k.Name == item.Replace("                                       ", "")).SingleOrDefault();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.ProductId = product.ProductId;
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.ProductKeywordRel.Save();
                            }
                            else
                            {
                                TblKeyword keyword = new TblKeyword();
                                keyword.Name = item.Replace("                                       ", "");
                                _core.Keyword.Add(keyword);
                                _core.Keyword.Save();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                tblProductKeywordRel.ProductId = product.ProductId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.ProductKeywordRel.Save();
                            }
                        }
                    }
                }
                if (GalleryFile.Count > 0)
                {
                    IEnumerable<TblProductImageRel> tblProductImageRel = _core.ProductImageRel.Get(p => p.ProductId == product.ProductId);

                    foreach (var galleryimage in GalleryFile)
                    {
                        TblImage NewImage = new TblImage();
                        NewImage.AlbumId = _core.ProductImageRel.Get(pi => pi.ProductId == product.ProductId).First().Image.AlbumId;
                        NewImage.Image = Guid.NewGuid().ToString() + Path.GetExtension(galleryimage.FileName);
                        string savePathAlbum = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", NewImage.Image
                                        );

                        using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                        {
                            await galleryimage.CopyToAsync(stream);
                        }
                        _core.Image.Add(NewImage);
                        _core.Image.Save();
                        TblProductImageRel imageRel = new TblProductImageRel();
                        imageRel.ProductId = EditProduct.ProductId;
                        imageRel.ImageId = NewImage.ImageId;

                        _core.ProductImageRel.Add(imageRel);
                        _core.ProductImageRel.Save();
                    }
                }
                for (int i = 0; i < PropertyId.Count; i++)
                {
                    TblProductPropertyRel propertyRel = new TblProductPropertyRel();
                    propertyRel.ProductId = EditProduct.ProductId;
                    if (Value[i] == null)
                    {
                        propertyRel.PropertyId = PropertyId[i].Value;
                        propertyRel.Value = "";
                    }
                    else
                    {
                        propertyRel.PropertyId = PropertyId[i].Value;
                        propertyRel.Value = Value[i];
                    }
                }
                EditProduct.Name = product.Name;
                EditProduct.PriceBeforeDiscount = product.PriceBeforeDiscount;
                if (product.PriceAfterDiscount != null)
                {
                    EditProduct.PriceAfterDiscount = product.PriceAfterDiscount;

                }
                else
                {
                    EditProduct.PriceAfterDiscount = 0;
                }
                EditProduct.SearchText = product.SearchText;
                EditProduct.IsFractional = product.IsFractional;
                EditProduct.BrandId = product.BrandId;

                _core.Product.Update(EditProduct);
                _core.Product.Save();

                return await Task.FromResult(Redirect("/Admin/Product"));
            }
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return View(product);
        }

        [HttpGet]
        public IActionResult StopSales(bool id)
        {
            List<TblProduct> products = _core.Product.Get().ToList();
            foreach (TblProduct i in products)
            {
                i.IsDeleted = id;
                _core.Product.Update(i);
            }

            _core.Product.Save();
            return Redirect("/Admin/Product");
        }

        public IActionResult SpecialOffer(int id)
        {
            return ViewComponent("SpecialOfferAddAdmin", new { id = id });
        }

        public string Delete(int id)
        {
            TblProduct product = _core.Product.GetById(id);
            if (product.TblOrderDetail.Count() > 0)
            {
                return "سفارشی برای این کالا وجود دارد";
            }
            else
            {
                IEnumerable<TblProductPropertyRel> propertyRels = _core.ProductPropertyRel.Get(pr => pr.ProductId == product.ProductId);
                if (propertyRels != null)
                {
                    foreach (var item in propertyRels)
                    {
                        _core.ProductPropertyRel.Delete(item);

                    }
                    _core.ProductPropertyRel.Save();
                }
                IEnumerable<TblProductKeywordRel> keywordRels = _core.ProductKeywordRel.Get(k => k.ProductId == product.ProductId);
                if (keywordRels != null)
                {
                    foreach (var item in keywordRels)
                    {
                        _core.ProductKeywordRel.Delete(item);
                    }
                    _core.ProductKeywordRel.Save();
                }
                IEnumerable<TblProductImageRel> imageRels = _core.ProductImageRel.Get(pi => pi.ProductId == product.ProductId);
                if (imageRels != null)
                {
                    foreach (var item in imageRels)
                    {
                        _core.ProductImageRel.Delete(item);
                    }
                    _core.ProductImageRel.Save();
                }

                IEnumerable<TblSpecialOffer> specialOffers = _core.SpecialOffer.Get(s => s.ProductId == product.ProductId);
                if (specialOffers != null)
                {
                    foreach (var item in specialOffers)
                    {
                        _core.SpecialOffer.Delete(item);
                    }
                    _core.SpecialOffer.Save();
                }
                _core.Product.Delete(product);
                _core.Product.Save();

                return "true";
            }
        }

        public void Selling(int id)
        {
            TblProduct product = _core.Product.GetById(id);
            product.IsDeleted = !product.IsDeleted;
            _core.Product.Update(product);
            _core.Product.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
