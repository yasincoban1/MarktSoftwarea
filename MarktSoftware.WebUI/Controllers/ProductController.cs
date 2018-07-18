using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarktSoftware.BusinessLayer;
using MarktSoftware.DAL.EntityFramework;
using MarktSoftware.Entity;
using MarktSoftware.WebUI.Filters;
using MarktSoftware.WebUI.Models;

namespace MarktSoftware.WebUI.Controllers
{

    public class ProductController : Controller
    {
        private ProductManager productManager = new ProductManager();
        private CategoryManager categoryManager = new CategoryManager();
        private LikedManager likedManager = new LikedManager();

     [Exc]
        public ActionResult Index()
        {
            var products = productManager.ListQueryable().Include("Owner").Where(
                x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(
                x => x.ModifiedOn);

            return View(products.ToList());
        }


        [Auth]
        public ActionResult MyLikedProducts()
        {
            var products = likedManager.ListQueryable().Include("LikedUser").Include("Product").Where(
                x => x.LikedUser.Id == CurrentSession.User.Id).Select(
                x => x.Product).Include("Category").Include("Owner").OrderByDescending(
                x => x.ModifiedOn);

            return View("Index", products.ToList());
        }

        [AuthAdmin]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productManager.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [AuthAdmin]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                product.Owner = CurrentSession.User;
                productManager.Insert(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", product.CategoryId);
            return View(product);
        }

        [AuthAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productManager.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", product.CategoryId);
            return View(product);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Product db_product = productManager.Find(x => x.Id == product.Id);
                db_product.Stock = product.Stock;
                db_product.CategoryId = product.CategoryId;
                db_product.Name = product.Name;
                db_product.Brand = product.Brand;

                productManager.Update(db_product);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", product.CategoryId);
            return View(product);
        }

        [AuthAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productManager.Find(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [AuthAdmin]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productManager.Find(x => x.Id == id);
            productManager.Delete(product);
            return RedirectToAction("Index");
        }

        [Auth]
        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (CurrentSession.User != null)
            {
                List<int> likedProductIds = likedManager.List(
                    x => x.LikedUser.Id == CurrentSession.User.Id && ids.Contains(x.Product.Id)).Select(
                    x => x.Product.Id).ToList();

                return Json(new { result = likedProductIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }
        [Auth]
        [HttpPost]
        public ActionResult SetLikeState(int productid, bool liked)
        {
            int res = 0;

            if (CurrentSession.User == null)
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi için giriş yapmalısınız.", result = 0 });

            Liked like =
                likedManager.Find(x => x.Product.Id == productid && x.LikedUser.Id == CurrentSession.User.Id);

            Product product = productManager.Find(x => x.Id == productid);

            if (like != null && liked == false)
            {
                res = likedManager.Delete(like);
            }
            else if (like == null && liked == true)
            {
                res = likedManager.Insert(new Liked()
                {
                    LikedUser = CurrentSession.User,
                    Product = product
                });
            }

            if (res > 0)
            {
                if (liked)
                {
                    product.LikeCount++;
                }
                else
                {
                    product.LikeCount--;
                }

                res = productManager.Update(product);

                return Json(new { hasError = false, errorMessage = string.Empty, result = product.LikeCount });
            }

            return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = product.LikeCount });
        }


        public ActionResult GetProductText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = productManager.Find(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialProductText", product);
        }
    }
}
