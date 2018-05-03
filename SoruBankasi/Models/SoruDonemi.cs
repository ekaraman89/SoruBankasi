namespace SoruBankasi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SoruDonemi")]
    public partial class SoruDonemi
    {

        public SoruDonemi()
        {
            KonuSoruDonemi = new HashSet<KonuSoruDonemi>();
            Soru = new HashSet<Soru>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(9)]
        public string SoruDonemAdi { get; set; }

        public virtual ICollection<KonuSoruDonemi> KonuSoruDonemi { get; set; }

        public virtual ICollection<Soru> Soru { get; set; }

    }
}
