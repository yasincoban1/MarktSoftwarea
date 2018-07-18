using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarktSoftware.BusinessLayer;
using MarktSoftware.Entity;
using MarktSoftware.WebUI.Filters;
using MarktSoftware.WebUI.Models;

namespace MarktSoftware.WebUI.Controllers
{
    [Exc]
    public class CommentController : Controller
    {
        private ProductManager productManager = new ProductManager();
        private CommentManager commentManager = new CommentManager();

        public ActionResult ShowProductComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            Product product = productManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", product.Comments);
        }

        [Auth]
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = commentManager.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }

            comment.Text = text;

            if (commentManager.Update(comment) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = commentManager.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }

            if (commentManager.Delete(comment) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        [HttpPost]
        public ActionResult Create(Comment comment, int? productid)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (productid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Product product = productManager.Find(x => x.Id == productid);

                if (product == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Product = product;
                comment.Owner = CurrentSession.User;

                if (commentManager.Insert(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
