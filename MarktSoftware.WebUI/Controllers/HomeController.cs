
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarktSoftware.BusinessLayer;
using MarktSoftware.BusinessLayer.Results;
using MarktSoftware.Entity;
using MarktSoftware.Entity.ValueObjects;
using MarktSoftware.WebUI.Filters;
using MarktSoftware.WebUI.Models;
using MarktSoftware.WebUI.ViewModels;

namespace MarktSoftware.WebUI.Controllers
{
  [Exc]
    public class HomeController : Controller
    {
        private ProductManager productManager = new ProductManager();
        private CategoryManager categoryManager = new CategoryManager();
        private MarktUserManager marktUserManager = new MarktUserManager();

        
        public ActionResult Index()
        {
            return View(productManager.ListQueryable().Where(x => x.IsHome == true).OrderByDescending(x => x.Id).ToList());

        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Product> products = productManager.ListQueryable().Where(
                x => x.Stock > 0 && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("CategoryFullWidth", products);
        }

        public ActionResult MostLiked()
        {
            return View("CategoryFullWidth", productManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }


        [Auth]
        public ActionResult ShowProfile()
        {
            BusinessLayerResult<MarktUser> res =
                marktUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        [Auth]
        public ActionResult EditProfile()
        {
            BusinessLayerResult<MarktUser> res = marktUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        [Auth]
        [HttpPost]
        public ActionResult EditProfile(MarktUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }

                BusinessLayerResult<MarktUser> res = marktUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };

                    return View("Error", errorNotifyObj);
                }


                CurrentSession.Set<MarktUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        [Auth]
        public ActionResult DeleteProfile()
        {
            BusinessLayerResult<MarktUser> res =
                marktUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<MarktUser> res = marktUserManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                CurrentSession.Set<MarktUser>("login", res.Result);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<MarktUser> res = marktUserManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                };

                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden satın alma işlemi yapamazsanız.");

                return View("Ok", notifyObj);
            }

            return View(model);
        }

        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<MarktUser> res = marktUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık satın alma işlemi yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }



        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult AccessDenied()
        {
            return View();
        }


        public ActionResult HasError()
        {
            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult ByCategorys()
        {
            return View();
        }
        public ActionResult AllProduct_Grid()
        {
            var products = productManager.ListQueryable().OrderByDescending(
                x => x.ModifiedOn);
            return View(products.ToList());
        }
        public ActionResult AllProduct_List()
        {
            var products = productManager.ListQueryable().OrderByDescending(
                x => x.ModifiedOn);
            return View(products.ToList());
        }
        public ActionResult CategoryFullWidth()
        {
            var products = productManager.ListQueryable().OrderByDescending(
                x => x.ModifiedOn);
            return View(products.ToList());
        }
    }
}