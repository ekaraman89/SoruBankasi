namespace SoruBankasi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
