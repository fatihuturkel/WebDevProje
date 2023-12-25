using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace WebDevProje.Models
{
    public class DoktorCalismaTakvimi
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public DateTime Tarih { get; set; }
        public Doktor Doktor { get; set; }

        // 9-17 arası çalışma saatleri, 12-13 arası öğle tatili. int deger 0 ise çalışmıyor (randevu alınamaz), 1 ise çalışıyor ve boş (randevu alınabilir), 2 ise çalışıyor ve dolu (randevu alınamaz).

        [Display(Name = "09:00 - 10:00 Arası")]
        [Range(0, 2)]
        public int dokuz_on { get; set; }

        [Display(Name = "10:00 - 11:00 Arası")]
        [Range(0, 2)]
        public int on_onbir { get; set; }

        [Display(Name = "11:00 - 12:00 Arası")]
        [Range(0, 2)]
        public int onbir_oniki { get; set; }

        [Display(Name = "13:00 - 14:00 Arası")]
        [Range(0, 2)]
        public int onuc_ondort { get; set; }

        [Display(Name = "14:00 - 15:00 Arası")]
        [Range(0, 2)]
        public int ondort_onbes { get; set; }

        [Display(Name = "15:00 - 16:00 Arası")]
        [Range(0, 2)]
        public int onbes_onalti { get; set; }

        [Display(Name = "16:00 - 17:00 Arası")]
        [Range(0, 2)]
        public int onalti_onyedi { get; set; }

    }
}
