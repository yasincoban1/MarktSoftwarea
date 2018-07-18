using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktSoftware.BusinessLayer.Abstract;
using MarktSoftware.BusinessLayer.Results;
using MarktSoftware.Common.Helpers;
using MarktSoftware.Entity;
using MarktSoftware.Entity.Messages;
using MarktSoftware.Entity.ValueObjects;


namespace MarktSoftware.BusinessLayer
{
    public class MarktUserManager : ManagerBase<MarktUser>
    {
        public BusinessLayerResult<MarktUser> RegisterUser(RegisterViewModel data)
        {
            // Kullanıcı username kontrolü..
            // Kullanıcı e-posta kontrolü..
            // Kayıt işlemi..
            // Aktivasyon e-postası gönderimi.
            MarktUser user = Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = base.Insert(new MarktUser()
                {
                    Username = data.Username,
                    Email = data.EMail,
                    ProfileImageFilename = "user.png",
                    Password = data.Password,
                    ConfirmPassword=data.ConfirmPassword,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.EMail && x.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, res.Result.Email, "Shepherd Markt Hesap Aktifleştirme");
                }
            }

            return res;
        }

        public BusinessLayerResult<MarktUser> GetUserById(int id)
        {
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<MarktUser> LoginUser(LoginViewModel data)
        {
            // Giriş kontrolü
            // Hesap aktive edilmiş mi?
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre uyuşmuyor.");
            }

            return res;
        }

        public BusinessLayerResult<MarktUser> UpdateProfile(MarktUser data)
        {
            MarktUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;

            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }

        public BusinessLayerResult<MarktUser> RemoveUserById(int id)
        {
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();
            MarktUser user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<MarktUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }


        // Method hiding..
        public new BusinessLayerResult<MarktUser> Insert(MarktUser data)
        {
            MarktUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();

            res.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user_boy.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı eklenemedi.");
                }
            }

            return res;
        }

        public new BusinessLayerResult<MarktUser> Update(MarktUser data)
        {
            MarktUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<MarktUser> res = new BusinessLayerResult<MarktUser>();
            res.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı güncellenemedi.");
            }

            return res;
        }



    }
}
