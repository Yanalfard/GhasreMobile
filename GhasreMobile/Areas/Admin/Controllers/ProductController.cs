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

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class ProductController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(int page = 1, string Search = null)
        {
            if (string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblProduct> products = PagingList.Create(_core.Product.Get(c => !c.IsDeleted), 30, page);
                return View(products);
            }
            else
            {
                IEnumerable<TblProduct> products = PagingList.Create(_core.Product.Get(c => !c.IsDeleted && c.SearchText.Contains(Search)), 30, page);
                ViewBag.Search = Search;
                return View(products);

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
                            string savePath = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/ProductMain", NewProduct.MainImage
                                                );

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
                                string savePathAlbum = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", image.Image
                                                );

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
                                propertyRel.Value = Value[i];
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

        public IActionResult PropertyList()
        {
            return ViewComponent("PropertyListAdmin");
        }

        public IActionResult Stock(int id)
        {
            return ViewComponent("EditStokeAdmin", new { Id = id });
        }

        [HttpPost]
        public void EditStoke(int Id, int count)
        {
            TblColor color = _core.Color.GetById(Id);
            color.Count = count;
            _core.Color.Update(color);
            _core.Color.Save();
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
                                keyword.Name = item.Replace(" ","-");
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
                            if (_core.Keyword.Get().Any(k => k.Name == item.Replace("                                       ","")))
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
                    foreach (var item in tblProductImageRel)
                    {
                        TblImage image = _core.Image.GetById(item.ImageId);
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", image.Image);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        _core.ProductImageRel.DeleteById(item.ProductImageRelId);
                        _core.Image.Delete(image);
                        _core.ProductImageRel.Save();
                        _core.Image.Save();

                        foreach (var galleryimage in GalleryFile)
                        {
                            TblAlbum album = new TblAlbum();
                            album.Name = product.Name;
                            _core.Album.Add(album);
                            _core.Album.Save();
                            TblImage NewImage = new TblImage();
                            NewImage.AlbumId = album.AlbumId;
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
                }
                if (ColorName.Count > 0)
                {
                    IEnumerable<TblColor> LastColors = _core.Color.Get(c => c.ProductId == EditProduct.ProductId);
                    foreach (var item in LastColors)
                    {
                        _core.Color.DeleteById(item.ColorId);
                    }
                    _core.Color.Save();
                    for (int i = 0; i < ColorName.Count; i++)
                    {
                        TblColor color = new TblColor();
                        color.ColorCode = Colors[i];
                        color.Name = ColorName[i];
                        color.ProductId = product.ProductId;
                        color.Count = ColorsCounts[i];
                        _core.Color.Add(color);
                    }
                    _core.Color.Save();
                }
                for (int i = 0; i < PropertyId.Count; i++)
                {
                    TblProductPropertyRel propertyRel = new TblProductPropertyRel();
                    propertyRel.ProductId = EditProduct.ProductId;
                    if (Value[i] == null)
                    {
                        propertyRel.PropertyId = PropertyId[i].Value;
                        propertyRel.Value = null;
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

    }
}
