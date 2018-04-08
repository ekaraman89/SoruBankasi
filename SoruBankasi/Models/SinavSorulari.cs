namespace SoruBankasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinavSorulari")]
    public partial class SinavSorulari
    {
        [Key]
        [Column(Order = 0)]
        public int SinavID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SoruID { get; set; }

        public byte Puan { get; set; }

        public virtual Sinav Sinav { get; set; }

        public virtual Soru Soru { get; set; }
    }
}
