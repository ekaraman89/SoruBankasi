namespace SoruBankasi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Konu")]
    public partial class Konu
    {
        
        public Konu()
        {
            KonuSoruDonemi = new HashSet<KonuSoruDonemi>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string KonuAdi { get; set; }

        public int DersID { get; set; }

        public virtual Ders Ders { get; set; }


        public virtual ICollection<KonuSoruDonemi> KonuSoruDonemi { get; set; }

    }
}
