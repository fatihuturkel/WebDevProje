using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebDevProje.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }

        //poliklinik adı
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Poliklinik adı 5 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Poliklinik Adı")]
        public string Ad { get; set; }

        //poliklinik açıklaması
        [AllowNull]
        [MaxLength(500)]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        //yönetici adı
        [AllowNull]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Yönetici alanı sadece harflerden oluşmalıdır.")]
        [Display(Name = "Yönetici Ad-Soyad")]
        public string? Yonetici { get; set; }

        //poliklinik adresi
        [Required]
        [MaxLength(100)]
        [Display(Name = "Adres")]
        public string Adres { get; set; }


        //poliklinik telefon numarası
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Telefon alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır.")]
        [StringLength(10)]
        [Display(Name = "Telefon Numarası")]
        public string TelefonNo { get; set; }

        //poliklinik fax numarası
        [AllowNull]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Fax alanı sadece sayılardan oluşmalıdır ve 10 haneli olmalıdır.")]
        [StringLength(10)]
        [Display(Name = "Fax Numarası")]
        public string? FaxNo { get; set; }

        //poliklinik eposta adresi

        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [EmailAddress(ErrorMessage = "Eposta formatında olmalıdır.")]
        [Display(Name = "Eposta Adresi")]
        public string Eposta { get; set; }

        //poliklinik kuruluş tarihi
        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [Display(Name = "Kuruluş Tarihi")]
        public DateTime KurulusTarihi { get; set; }

        //poliklinik aktiflik durumu
        [Required]
        [Display(Name = "Aktiflik Durumu")]
        public bool AktiflikDurumu { get; set; }

        //polikliniğin bağlı olduğu anabilim dalı
        [Display(Name = "Bağlı Olduğu Anabilim Dalı")]
        public AnabilimDali AnabilimDali { get; set; }

    }
}
