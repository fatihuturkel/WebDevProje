﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebDevProje.Models
{
    // Kisi sınıfı hastanede çalışan kişileri içerir. Bu sınıfın alt sınıfları doktor, hemşire, hasta, işçi ve yönetici sınıflarıdır.
    public class Kisi
    {
        public int Id { get; set; }

        // string length should be bewteen 5 and 50
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad alanı 2 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        // string length should be bewteen 5 and 50
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Soyad alanı 5 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        // cinsiyet alanı char olmalıdır ve erkek için 'e' , kadın için 'k' değerini almalıdır
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [Display(Name = "Cinsiyet")]
        public char Cinsiyet { get; set; }

        // doğum tarihi
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [Display(Name = "Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        // telefon alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Telefon alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır.")]
        [StringLength(10)]
        [Display(Name = "Telefon Numarası")]
        public string TelefonNo { get; set; }

        // eposta alanı
        [AllowNull]
        [EmailAddress(ErrorMessage = "Eposta formatında olmalıdır.")]
        [Display(Name = "E-posta Adresi")]
        public string? Eposta { get; set; }

        // tc kimlik numarası
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Tc kimlik numarası sadece sayılardan oluşmalıdır ve 11 haneli olmalıdır.")]
        [StringLength(11)]
        [Display(Name = "Tc Kimlik Numarası")]
        public string TcKimlikNo { get; set; }

        // bu kişi doktor, hasta , hemşire, işçi veya olabilir

        [Display(Name = "Doktor")]
        public bool Doktor { get; set; }

        [Display(Name = "Hasta")]
        public bool Hasta { get; set; }

        [Display(Name = "Hemşire")]
        public bool Hemsire { get; set; }

        [Display(Name = "İşçi")]
        public bool Isci { get; set; }

        [Display(Name = "Yönetici")]
        public bool Yonetici { get; set; }

        // kullanıcı türü. Bu alan admin için true, diğer kullanıcılar için false değerini alır. default değeri false'tur.
        [DefaultValue(false)]
        [Display(Name = "Admin")]
        public bool adminMi { get; set; }

        // password should be between 6 and 15 characters
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Şifre alanı 6 ile 15 karakter arasında olmalıdır.")]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
    }
}
