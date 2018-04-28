namespace SoruBankasi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cevaplar")]
    public partial class Cevaplar
    {
        public int ID { get; set; }

        public int SoruID { get; set; }

        [Required]
        public string Cevap { get; set; }

        public bool DogruMu { get; set; }

        public virtual Soru Soru { get; set; }
    }
}
