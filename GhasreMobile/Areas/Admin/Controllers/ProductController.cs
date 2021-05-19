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
using GhasreMobile.Utilities;
using DataLayer.Utilities;
using System.IO;

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
            try
            {
                if (productsInAdmin.CatagoryId != 0)
                {
                    if (string.IsNullOrEmpty(productsInAdmin.Search))
                    {
                        IEnumerable<TblProduct> products = _core.Product.Get(c => c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.ProductId);
                        int count = products.Count();
                        var skip = 0;
                        ViewBag.pageid = productsInAdmin.PageId;
                        ViewBag.InPageCount = productsInAdmin.InPageCount;
                        ViewBag.CatagoryId = productsInAdmin.CatagoryId;
                        ViewData["isStop"] = products.Any(i => !i.IsDeleted);
                        ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);
                        if (productsInAdmin.InPageCount == 0)
                        {
                            skip = (productsInAdmin.PageId - 1) * 18;
                            ViewBag.PageCount = count / 18;
                            return View(products.Skip(skip).Take(18));
                        }
                        else
                        {
                            skip = (productsInAdmin.PageId - 1) * productsInAdmin.InPageCount;
                            ViewBag.PageCount = count / productsInAdmin.InPageCount;
                            return View(products.Skip(skip).Take(productsInAdmin.InPageCount));
                        }
                    }
                    else
                    {
                        if (productsInAdmin.InPageCount == 0)
                        {
                            IEnumerable<TblProduct> products = _core.Product.Get(c => c.SearchText.Contains(productsInAdmin.Search) && c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.ProductId);
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
                            IEnumerable<TblProduct> products = _core.Product.Get(c => c.SearchText.Contains(productsInAdmin.Search) && c.CatagoryId == productsInAdmin.CatagoryId).OrderByDescending(p => p.ProductId);
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
                            IEnumerable<TblProduct> products = _core.Product.Get().OrderByDescending(p => p.ProductId);
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
                            IEnumerable<TblProduct> products = _core.Product.Get().OrderByDescending(p => p.ProductId);
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
                            IEnumerable<TblProduct> products = _core.Product.Get(p => p.Name.Contains(productsInAdmin.Search)).OrderByDescending(p => p.ProductId);
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
                            IEnumerable<TblProduct> products = _core.Product.Get(p => p.Name.Contains(productsInAdmin.Search)).OrderByDescending(p => p.ProductId);
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
            catch
            {
                return Redirect("/");
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                ViewBag.Brands = _core.Brand.Get();
                List<string> keywords = new List<string>();
                ViewBag.keywords = keywords;
                return View();
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblProduct product,
                                                List<string> Keywords,
                                                List<string> Colors,
                                                List<string> ColorName,
                                                List<int> ColorsCounts,
                                                IFormFile MainImage
            )
        {

            try
            {
                ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                ViewBag.Brands = _core.Brand.Get();
                ViewBag.keywords = Keywords;
                if (ModelState.IsValid)
                {
                    if (MainImage == null && MainImage.IsImages())
                    {
                        ModelState.AddModelError("MainImage", "تصویر الزامی میباشد . لطفا موارد را بررسی کنید");
                        return await Task.FromResult(View(product));
                    }
                    else
                    {
                        if (MainImage.Length > 3000000)
                        {
                            ModelState.AddModelError("MainImage", "حجم فایل عکس اصلی بیش از اندازه میباشد");
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
                                /// #region resize Image
                                ImageConvertor imgResizer = new ImageConvertor();
                                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/thumb", NewProduct.MainImage);
                                imgResizer.Image_resize(savePath, thumbPath, 300);
                                /// #endregion

                            }
                            NewProduct.PriceBeforeDiscount = product.PriceBeforeDiscount;
                            NewProduct.DescriptionShortHtml = product.DescriptionShortHtml;
                            NewProduct.DescriptionLongHtml = product.DescriptionLongHtml;
                            NewProduct.CatagoryId = product.CatagoryId;
                            if (product.PriceAfterDiscount != null)
                            {
                                NewProduct.PriceAfterDiscount = product.PriceAfterDiscount;
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
                            _core.Save();
                            //New Prodcut

                            TblAlbum album = new TblAlbum();
                            album.Name = NewProduct.Name;
                            album.IsProduct = true;
                            _core.Album.Add(album);
                            _core.Save();

                            for (int i = 0; i < Colors.Count; i++)
                            {
                                TblColor Newcolor = new TblColor();
                                Newcolor.Name = ColorName[i];
                                Newcolor.ColorCode = Colors[i];
                                Newcolor.Count = ColorsCounts[i];
                                Newcolor.ProductId = NewProduct.ProductId;
                                _core.Color.Add(Newcolor);
                            }
                            _core.Save();

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
                                    _core.Save();
                                    TblProductKeywordRel keywordRel = new TblProductKeywordRel();
                                    keywordRel.KeywordId = Newkeyword.KeywordId;
                                    keywordRel.ProductId = NewProduct.ProductId;
                                    _core.ProductKeywordRel.Add(keywordRel);

                                }
                            }
                            _core.Save();



                            return await Task.FromResult(Redirect("/Admin/Product"));
                        }
                    }


                }

                return await Task.FromResult(View(product));
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }


        public IActionResult CreateColor(int id)
        {
            return ViewComponent("CreateColorAdmin", new { id = id });
        }
        [HttpPost]
        public IActionResult CreateColor(CreateColorVm createColorVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblColor color = new TblColor();
                    color.ProductId = createColorVm.id;
                    color.Name = createColorVm.Name;
                    color.ColorCode = createColorVm.Code;
                    color.Count = createColorVm.count;
                    _core.Color.Add(color);
                    _core.Save();
                    return Redirect("/Admin/Product");
                }
                return View(createColorVm);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public IActionResult PropertyList()
        {
            return ViewComponent("PropertyListAdmin");
        }
        public IActionResult AddProperty(int id)
        {
            try
            {
                ViewBag.id = id;
                List<TblProperty> list = _core.ProductPropertyRel.Get(i => i.ProductId == id).Select(i => i.Property).ToList();
                return View(_core.Product.GetById(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult AddProperty(int id, List<int?> PropertyId, List<string> Value)
        {
            try
            {
                List<TblProductPropertyRel> pros = new List<TblProductPropertyRel>();
                for (int i = 0; i < PropertyId.Count; i++)
                {
                    TblProductPropertyRel propertyRel = new TblProductPropertyRel();
                    propertyRel.ProductId = id;
                    if (Value[i] != null)
                    {
                        //propertyRel.PropertyId = PropertyId[i].Value;
                        //propertyRel.Value = "";
                        propertyRel.PropertyId = PropertyId[i].Value;
                        propertyRel.Value = Value[i];
                        pros.Add(propertyRel);
                    }
                }
                _core.ProductPropertyRel.Get(i => i.ProductId == id).ToList().ForEach(j => _core.ProductPropertyRel.Delete(j));
                _core.Save();
                foreach (var item in pros)
                {
                    if (!_core.ProductPropertyRel.Get().Any(i => i.ProductId == item.ProductId && i.PropertyId == item.PropertyId))
                    {
                        _core.ProductPropertyRel.Add(item);
                        _core.Save();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        public IActionResult AddGallery(int id)
        {
            try
            {
                ViewBag.id = id;
                List<TblProductImageRel> list = _core.ProductImageRel.Get(i => i.ProductId == id).ToList();
                return View(list);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddGallery(int id, List<IFormFile> GalleryFile)
        {
            try
            {

                if (GalleryFile.Count > 0)
                {
                    TblProduct product = _core.Product.GetById(id);
                    foreach (var galleryimage in GalleryFile)
                    {
                        if (galleryimage.IsImages() && galleryimage.Length < 3000000)
                        {
                            TblImage NewImage = new TblImage();
                            if (_core.ProductImageRel.Get(pi => pi.ProductId == product.ProductId).Count() == 0)
                            {
                                TblAlbum album = new TblAlbum();
                                album.Name = product.Name;
                                album.IsProduct = true;
                                _core.Album.Add(album);
                                _core.Save();
                                NewImage.AlbumId = album.AlbumId;

                            }
                            else
                            {
                                NewImage.AlbumId = _core.ProductImageRel.Get(pi => pi.ProductId == product.ProductId).First().Image.AlbumId;
                            }
                            NewImage.Image = Guid.NewGuid().ToString() + Path.GetExtension(galleryimage.FileName);
                            string savePathAlbum = Path.Combine(
                                                Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum/", NewImage.Image
                                            );
                            using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                            {
                                await galleryimage.CopyToAsync(stream);
                            }
                            /// #region resize Image
                            ImageConvertor imgResizer = new ImageConvertor();
                            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum/thumb/", NewImage.Image);
                            imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                            /// #endregion
                            _core.Image.Add(NewImage);
                            _core.Save();
                            TblProductImageRel imageRel = new TblProductImageRel();
                            imageRel.ProductId = product.ProductId;
                            imageRel.ImageId = NewImage.ImageId;

                            _core.ProductImageRel.Add(imageRel);
                            _core.Save();
                        }

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

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
                            await Sms.SendSms2(item.Client.TellNo, item.Product.Name, "https://gasremobile2004.com/Product/" + item.ProductId + "/" + item.Product.Name.Replace(" ", "-").Replace("/", "-"), "GhasrMobileAlertWhenReady");
                            _core.AlertWhenReady.Delete(item);
                            _core.Save();
                        }
                    }
                }
            }
            color.Count = count;
            _core.Color.Update(color);
            _core.Save();
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
            _core.Save();
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
            _core.Save();
            if (!_core.Image.Get().Any(i => i.AlbumId == tblProductImageRel.Image.AlbumId))
            {
                TblAlbum selectedAlbum = _core.Album.GetById(tblProductImageRel.Image.AlbumId);

                _core.Album.Delete(selectedAlbum);
                _core.Save();
            }
            return "true";
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                ViewBag.Brands = _core.Brand.Get();
                ViewBag.CatagoryName = _core.Product.GetById(id).Catagory.Name;
                ViewBag.keywords = _core.Product.GetById(id).TblProductKeywordRel;
                return View(_core.Product.GetById(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult DeleteProperty(int id)
        {
            TblProductPropertyRel rel = _core.ProductPropertyRel.GetById(id);
            bool isDeleted = _core.ProductPropertyRel.Delete(rel);
            _core.Save();
            return Ok(isDeleted);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TblProduct product,
                                                List<string> Keywords,
                                                IFormFile MainImage
            )
        {
            try
            {
                ViewBag.CatagoryName = _core.Catagory.GetById(product.CatagoryId).Name;
                ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
                ViewBag.Brands = _core.Brand.Get();
                ViewBag.keywords = _core.Product.GetById(product.ProductId).TblProductKeywordRel;
                if (ModelState.IsValid)
                {
                    TblProduct EditProduct = _core.Product.GetById(product.ProductId);

                    if (MainImage != null && MainImage.IsImages() && MainImage.Length < 3000000)
                    {
                        try
                        {
                            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/", EditProduct.MainImage);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/thumb/", EditProduct.MainImage);
                            if (System.IO.File.Exists(imagePath2))
                            {
                                System.IO.File.Delete(imagePath2);
                            }
                        }
                        catch
                        {

                        }


                        EditProduct.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                        string saveDirectory = Path.Combine(
                                                Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/");
                        string savePath = Path.Combine(saveDirectory, EditProduct.MainImage);

                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await MainImage.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/thumb", EditProduct.MainImage);
                        imgResizer.Image_resize(savePath, thumbPath, 300);
                        /// #endregion
                    }
                    IEnumerable<TblProductKeywordRel> keywordRels = _core.ProductKeywordRel.Get(k => k.ProductId == product.ProductId);
                    if (keywordRels.Count() > 0)
                    {
                        foreach (var item in keywordRels)
                        {
                            _core.ProductKeywordRel.Delete(item);
                        }
                        _core.Save();
                    }
                    if (Keywords.Count > 0)
                    {
                        foreach (var item in Keywords)
                        {
                            if (_core.Keyword.Get().Any(k => k.Name == item.Replace(" ", "")))
                            {
                                TblKeyword keyword = _core.Keyword.Get().Where(k => k.Name == item.Replace(" ", "")).SingleOrDefault();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.ProductId = product.ProductId;
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.Save();
                            }
                            else
                            {
                                TblKeyword keyword = new TblKeyword();
                                keyword.Name = item.Replace(" ", "");
                                _core.Keyword.Add(keyword);
                                _core.Save();
                                TblProductKeywordRel tblProductKeywordRel = new TblProductKeywordRel();
                                tblProductKeywordRel.KeywordId = keyword.KeywordId;
                                tblProductKeywordRel.ProductId = product.ProductId;
                                _core.ProductKeywordRel.Add(tblProductKeywordRel);
                                _core.Save();
                            }
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
                    EditProduct.DescriptionShortHtml = product.DescriptionShortHtml;
                    EditProduct.DescriptionLongHtml = product.DescriptionLongHtml;
                    EditProduct.CatagoryId = product.CatagoryId;
                    _core.Product.Update(EditProduct);
                    _core.Save();
                    return await Task.FromResult(Redirect("/Admin/Product"));
                }

                return View(product);
            }
            catch
            {
                return RedirectToAction("Index");
            }


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

            _core.Save();
            return Redirect("/Admin/Product");
        }

        public IActionResult SpecialOffer(int id)
        {
            return ViewComponent("SpecialOfferAddAdmin", new { id = id });
        }

        public string Delete(int id)
        {
            try
            {
                TblProduct product = _core.Product.GetById(id);
                if (product.TblOrderDetail.Count() > 0)
                {
                    return "سفارشی برای این کالا وجود دارد";
                }
                else
                {
                    IEnumerable<TblProductImageRel> imageRels = _core.ProductImageRel.Get(pi => pi.ProductId == product.ProductId);
                    if (imageRels.Count() > 0)
                    {
                        TblAlbum selectedAlbum = imageRels.First().Image.Album;
                        foreach (var item in imageRels)
                        {
                            var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", item.Image.Image);
                            if (System.IO.File.Exists(deleteImagePath))
                            {
                                System.IO.File.Delete(deleteImagePath);
                            }
                            var deleteImagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum/thumb/", item.Image.Image);
                            if (System.IO.File.Exists(deleteImagePath2))
                            {
                                System.IO.File.Delete(deleteImagePath2);
                            }
                            _core.Image.Delete(item.Image);
                            _core.Save();
                        }
                        if (selectedAlbum != null)
                        {
                            _core.Album.Delete(selectedAlbum);
                            _core.Save();
                        }
                        _core.Save();
                    }
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain", product.MainImage);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain/thumb/", product.MainImage);
                    if (System.IO.File.Exists(imagePath2))
                    {
                        System.IO.File.Delete(imagePath2);
                    }
                    _core.Product.Delete(product);
                    _core.Save();
                    return "true";
                }
            }
            catch
            {
                return "خطا در حذف";
            }

        }

        public void Selling(int id)
        {
            TblProduct product = _core.Product.GetById(id);
            product.IsDeleted = !product.IsDeleted;
            _core.Product.Update(product);
            _core.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }

        public string DeleteCountProduct(int id)
        {
            try
            {
                TblProduct product = _core.Product.GetById(id);
                foreach (var item in product.TblColor)
                {
                    item.Count = 0;
                    _core.Color.Update(item);
                }
                _core.Save();
                return "true";
            }
            catch
            {
                return "خطا در حذف";
            }

        }
    }
}
