using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevProje.Models
{
    public class Doktor
    {
        [Key, ForeignKey("Kisi")]
        public int Id { get; set; }

        public Kisi Kisi { get; set; }

        [Display(Name = "Maaş")]
        public string Maas { get; set; }

        public Poliklinik Poliklinik { get; set; }

        [Required(ErrorMessage = "Bu alan boş olamaz.")]
        [Display(Name = "Poliklinik")]
        public int PoliklinikId { get; set; }

    }
}
