namespace SoruBankasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoruTipi")]
    public partial class SoruTipi
    {
                public SoruTipi()
        {
            Soru = new HashSet<Soru>();
        }

        public int ID { get; set; }

        [StringLength(15)]
        public string SoruTipAdi { get; set; }

       
        public virtual ICollection<Soru> Soru { get; set; }
    }
}
