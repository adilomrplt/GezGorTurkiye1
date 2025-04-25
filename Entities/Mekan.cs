using System.ComponentModel.DataAnnotations;

namespace GezGorTurkiye.Entities
{
    public class Mekan
    {
        public int Id { get; set; }
        public int SehirId { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Kategori alanı gereklidir.")]
        public string Kategori { get; set; }

        [Required(ErrorMessage = "Açıklama alanı gereklidir.")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Konum alanı gereklidir.")]
        public string Konum { get; set; }

        public Sehir Sehir { get; set; }
        public int? SiraNo { get; set; }
    }
}
