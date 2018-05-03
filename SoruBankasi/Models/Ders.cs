namespace SoruBankasi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Ders
    {

        public Ders()
        {
            Konu = new HashSet<Konu>();
            KullaniciDers = new HashSet<KullaniciDers>();
        }

        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        public string DersAdi { get; set; }


        public virtual ICollection<Konu> Konu { get; set; }


        public virtual ICollection<KullaniciDers> KullaniciDers { get; set; }

    }
}
