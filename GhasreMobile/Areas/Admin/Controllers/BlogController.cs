﻿using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class BlogController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Search = null)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblBlog> blogs = PagingList.Create(_core.Blog.Get(b => b.Title.Contains(Search)), 30, page);

                return View(blogs);
            }
            else
            {
                IEnumerable<TblBlog> blogs = PagingList.Create(_core.Blog.Get().OrderByDescending(b => b.BlogId), 30, page);

                return View(blogs);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("BlogCreateAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblBlog blog, List<string> Keywords, IFormFile MainImage)
        {
            if (ModelState.IsValid)
            {
                if (MainImage != null && MainImage.IsImages() && MainImage.Length < 3000000)
                {
                    blog.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string saveDirectory = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs");
                    string savePathAlbum = Path.Combine(
                                        Directory.GetCurrentDirectory(), saveDirectory, blog.MainImage
                                    );
                    if (!Directory.Exists(saveDirectory))
                    {
                        Directory.CreateDirectory(saveDirectory);
                    }
                    using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                    /// #region resize Image
                    ImageConvertor imgResizer = new ImageConvertor();
                    string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs/thumb", blog.MainImage);
                    imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                    /// #endregion
                }

                _core.Blog.Add(blog);
                _core.Save();

                if (Keywords.Count > 0)
                {
                    foreach (var item in Keywords)
                    {
                        if (_core.Keyword.Get().Any(k => k.Name == item))
                        {
                            TblKeyword keyword = _core.Keyword.Get(k => k.Name == item).SingleOrDefault();
                            TblBlogKeywordRel keywordRel = new TblBlogKeywordRel();
                            keywordRel.KeywordId = keyword.KeywordId;
                            keywordRel.BlogId = blog.BlogId;
                            _core.BlogKeywordRel.Add(keywordRel);
                            _core.Save();
                        }
                        else
                        {
                            TblKeyword keyword = new TblKeyword();
                            keyword.Name = item;
                            _core.Keyword.Add(keyword);
                            _core.Save();
                            TblBlogKeywordRel keywordRel = new TblBlogKeywordRel();
                            keywordRel.KeywordId = keyword.KeywordId;
                            keywordRel.BlogId = blog.BlogId;
                            _core.BlogKeywordRel.Add(keywordRel);
                            _core.Save();
                        }
                    }
                }
                return await Task.FromResult(Redirect("/Admin/Blog"));
            }
            return View(blog);
        }

        public IActionResult Edit(int id)
        {
            return ViewComponent("BlogEditAdmin", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(TblBlog blog, List<string> Keywords, IFormFile MainImage)
        {
            if (ModelState.IsValid)
            {
                TblBlog Editblog = _core.Blog.GetById(blog.BlogId);
                if (MainImage != null && MainImage.IsImages() && MainImage.Length < 3000000)
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs", Editblog.MainImage);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }


                        var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs/thumb", Editblog.MainImage);

                        if (System.IO.File.Exists(imagePath2))
                        {
                            System.IO.File.Delete(imagePath2);
                        }

                    }
                    catch
                    {

                    }

                    string savePathAlbum = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs", blog.MainImage
                                    );

                    using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                    /// #region resize Image
                    ImageConvertor imgResizer = new ImageConvertor();
                    string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs/thumb", blog.MainImage);
                    imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                    /// #endregion
                }

                if (Keywords.Count > 0)
                {
                    IEnumerable<TblBlogKeywordRel> keywordRels = _core.BlogKeywordRel.Get(bk => bk.BlogId == blog.BlogId);

                    foreach (var item in keywordRels)
                    {
                        _core.BlogKeywordRel.Delete(item);
                    }
                    _core.Save();


                    foreach (var item in Keywords)
                    {
                        if (_core.Keyword.Get().Any(k => k.Name == item))
                        {
                            TblKeyword keyword = _core.Keyword.Get(k => k.Name == item).SingleOrDefault();
                            TblBlogKeywordRel keywordRel = new TblBlogKeywordRel();
                            keywordRel.KeywordId = keyword.KeywordId;
                            keywordRel.BlogId = blog.BlogId;
                            _core.BlogKeywordRel.Add(keywordRel);
                            _core.Save();
                        }
                        else
                        {
                            TblKeyword keyword = new TblKeyword();
                            keyword.Name = item;
                            _core.Keyword.Add(keyword);
                            _core.Save();
                            TblBlogKeywordRel keywordRel = new TblBlogKeywordRel();
                            keywordRel.KeywordId = keyword.KeywordId;
                            keywordRel.BlogId = blog.BlogId;
                            _core.BlogKeywordRel.Add(keywordRel);
                            _core.Save();
                        }
                    }
                }
                Editblog.Title = blog.Title;
                Editblog.Description = blog.Description;
                Editblog.BodyHtml = blog.BodyHtml;
                _core.Blog.Update(Editblog);
                _core.Save();
                return await Task.FromResult(Redirect("/Admin/Blog"));
            }
            return await Task.FromResult(View(blog));
        }

        public void Delete(int id)
        {
            TblBlog blog = _core.Blog.GetById(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs", blog.MainImage);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs/thumb", blog.MainImage);

            if (System.IO.File.Exists(imagePath2))
            {
                System.IO.File.Delete(imagePath2);
            }
            _core.Blog.Delete(blog);
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
    }
}
