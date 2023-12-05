using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebDevProje.Models
{
    public class AnabilimDali
    {
        public int Id { get; set; }

        // string length should be bewteen 5 and 50
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Anabilim dalı adı 2 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Anabilim Dalı Adı")]
        public string Ad { get; set; }

        [AllowNull]
        [MaxLength(500)]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        // yonetici alanı sadece harflerden oluşmalıdır
        [AllowNull]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Yönetici alanı sadece harflerden oluşmalıdır.")]
        [Display(Name = "Yönetici Ad-Soyad")]
        public string? Yonetici { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Adres")]
        public string Adres { get; set; }

        // telefon alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Telefon alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır.")]
        [StringLength(10)]
        [Display(Name = "Telefon Numarası")]
        public string TelefonNo { get; set; }

        // fax alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır
        [AllowNull]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Fax alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır.")]
        [StringLength(10)]
        [Display(Name = "Fax Numarası")]
        public string? FaxNo { get; set; }

        // eposta alanı 
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [EmailAddress(ErrorMessage = "Eposta formatında olmalıdır.")]
        [Display(Name = "Eposta Adresi")]
        public string Eposta { get; set; }

        // Kuruluş tarihi
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [Display(Name = "Kuruluş Tarihi")]
        public DateTime KurulusTarihi { get; set; }

        // aktiflik durumu
        [Required]
        [Display(Name = "Aktiflik Durumu")]
        public bool AktiflikDurumu { get; set; }

    }
}