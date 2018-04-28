namespace SoruBankasi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Soru")]
    public partial class Soru
    {
       
        public Soru()
        {
            Cevaplar = new HashSet<Cevaplar>();
            SinavSorulari = new HashSet<SinavSorulari>();
        }

        public int ID { get; set; }

        [Required]
        public string Sorular { get; set; }

        public int SoruTipID { get; set; }

        public int SoruDonemID { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        public virtual ICollection<Cevaplar> Cevaplar { get; set; }

        public virtual ICollection<SinavSorulari> SinavSorulari { get; set; }

        public virtual SoruDonemi SoruDonemi { get; set; }

        public virtual SoruTipi SoruTipi { get; set; }
    }
}
