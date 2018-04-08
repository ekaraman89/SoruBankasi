namespace SoruBankasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ders
    {
       
        public Ders()
        {
            Konu = new HashSet<Konu>();
            Kullanici = new HashSet<Kullanici>();
        }

        public int ID { get; set; }

        [StringLength(10)]
        public string DersAdi { get; set; }


        public virtual ICollection<Konu> Konu { get; set; }


        public virtual ICollection<Kullanici> Kullanici { get; set; }
    }
}
