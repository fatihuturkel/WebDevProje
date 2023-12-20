using System.ComponentModel.DataAnnotations;

namespace WebDevProje.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        public int HastaId { get; set; }
        public int DoktorId { get; set; }
        public int PoliklinikId { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }

        [Display(Name = "Saat")]
        public string Saat { get; set; }

        public Hasta Hasta { get; set; }
        public Doktor Doktor { get; set; }
        public Poliklinik Poliklinik { get; set; }

    }
}
