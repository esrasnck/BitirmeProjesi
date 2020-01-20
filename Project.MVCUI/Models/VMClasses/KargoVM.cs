using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class KargoVM
    {
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz."), MinLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz.")]
        public string TCKimlikNumarası { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(30, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(30, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(200, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(20, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Il { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(30, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Ilce { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), MaxLength(30, ErrorMessage = "En fazla {1} karakter girebilirsiniz.")]
        public string Mahalle { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), Phone(ErrorMessage = "Telefon Numarası Giriniz."), MaxLength(11, ErrorMessage = "Telefon numarasınızda fazla karakter bulunmaktadır"), MinLength(11, ErrorMessage = "Telefon numarasınızı eksik girdiniz")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur."), EmailAddress(ErrorMessage = "Yanlış bir e-posta adresi girdiniz."), MaxLength(60, ErrorMessage = "En fazla 30 karakter girebilirsiniz.")]
        public string Mail { get; set; }
    }
}