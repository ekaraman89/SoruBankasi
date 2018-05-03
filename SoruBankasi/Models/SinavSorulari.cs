namespace SoruBankasi.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SinavSorulari")]
    public partial class SinavSorulari
    {
        public int ID { get; set; }

        public int SinavID { get; set; }

        public int SoruID { get; set; }

        public byte Puan { get; set; }

        public virtual Sinav Sinav { get; set; }

        public virtual Soru Soru { get; set; }
    }
}
