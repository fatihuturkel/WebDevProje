using System.ComponentModel.DataAnnotations;

namespace WebDevProje.Models
{
	public class AnabilimDalı
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		[MinLength(5)]
		[Display(Name = "Anabilim Dalı Adı")]
		public string Ad { get; set; }

		[MaxLength(250)]
		[Display(Name = "Açıklama")]
		public string? Aciklama { get; set; }

		[Display(Name = "Yönetici")]
		public string Yonetici { get; set; }

		[Display(Name ="Adres")]
		public string Adres { get; set; }

		[Display(Name = "Telefon Numarası")]
		public string Telefon { get; set; }

		[Display(Name = "E-Posta Adresi")]
		public string Eposta { get; set; }

		[Display(Name = "Faks Numarası")]
		public string Fax { get; set; }

		[Display(Name = "Kuruluş Tarihi")]
		public DateTime KurulusTarihi { get; set; }

		[Display(Name = "Aktiflik Durumu")]
		public bool Statu { get; set; }
	}
}
