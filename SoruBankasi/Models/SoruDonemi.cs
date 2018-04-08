namespace SoruBankasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoruDonemi")]
    public partial class SoruDonemi
    {
       
        public SoruDonemi()
        {
            Soru = new HashSet<Soru>();
            Konu = new HashSet<Konu>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(9)]
        public string SoruDonemAdi { get; set; }

       
        public virtual ICollection<Soru> Soru { get; set; }

       
        public virtual ICollection<Konu> Konu { get; set; }
    }
}
