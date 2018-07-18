using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity
{
    public class MarktUser:MyEntityBase
    {
        [DisplayName("İsim"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrar alanı gereklidir")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [DisplayName("Şifre Tekrar")]
        public string ConfirmPassword { get; set; }

        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }
        [DisplayName("Telefon Numarası")]
        public string Phone { get; set; }
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }

        [DisplayName("Yönetici")]
        public bool IsAdmin { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }


        public virtual List<Product> Products { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
      
    }
}
