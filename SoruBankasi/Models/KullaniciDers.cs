using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoruBankasi.Models
{
    [Table("KullaniciDers")]
    public partial class KullaniciDers
    {
        public int ID { get; set; }

        public int DersID { get; set; }

        public int KullaniciID { get; set; }

        public virtual Ders Ders { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}