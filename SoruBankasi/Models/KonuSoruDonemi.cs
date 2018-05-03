using System.ComponentModel.DataAnnotations.Schema;

namespace SoruBankasi.Models
{
    [Table("KonuSoruDonemi")]
    public partial class KonuSoruDonemi
    {
        public int ID { get; set; }

        public int KonuID { get; set; }

        public int SoruDonemID { get; set; }

        public virtual Konu Konu { get; set; }

        public virtual SoruDonemi SoruDonemi { get; set; }
    }
}