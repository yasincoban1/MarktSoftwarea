using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarktSoftware.BusinessLayer;
using MarktSoftware.BusinessLayer.Results;
using MarktSoftware.DAL.EntityFramework;
using MarktSoftware.Entity;
using MarktSoftware.WebUI.Filters;

namespace MarktSoftware.WebUI.Controllers
{
    [AuthAdmin]
    public class MarktUserController : Controller
    {
        private MarktUserManager marktUserManager = new MarktUserManager();


        public ActionResult Index()
        {
            return View(marktUserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MarktUser marktUser = marktUserManager.Find(x => x.Id == id.Value);

            if (marktUser == null)
            {
                return HttpNotFound();
            }

            return View(marktUser);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarktUser marktUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<MarktUser> res = marktUserManager.Insert(marktUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(marktUser);
                }

                return RedirectToAction("Index");
            }

            return View(marktUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MarktUser evernoteUser = marktUserManager.Find(x => x.Id == id.Value);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }

            return View(evernoteUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MarktUser evernoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<MarktUser> res = marktUserManager.Update(evernoteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(evernoteUser);
                }

                return RedirectToAction("Index");
            }
            return View(evernoteUser);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MarktUser marktUser = marktUserManager.Find(x => x.Id == id.Value);

            if (marktUser == null)
            {
                return HttpNotFound();
            }

            return View(marktUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarktUser marktUser = marktUserManager.Find(x => x.Id == id);
            marktUserManager.Delete(marktUser);

            return RedirectToAction("Index");
        }
    }
}
