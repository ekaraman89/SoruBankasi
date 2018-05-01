namespace SoruBankasi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sinav")]
    public partial class Sinav
    {
        public Sinav()
        {
            SinavSorulari = new HashSet<SinavSorulari>();
        }

        public int ID { get; set; }

        [Required]
        public string SinavAdi { get; set; }

        public virtual ICollection<SinavSorulari> SinavSorulari { get; set; }
    }
}
