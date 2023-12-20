using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevProje.Models
{
    public class Hasta
    {
        [Key, ForeignKey("Kisi")]
        public int Id { get; set; }
        
        public Kisi Kisi { get; set; }
    }
}
