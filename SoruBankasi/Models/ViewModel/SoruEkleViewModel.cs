namespace SoruBankasi.Models.ViewModel
{
    public class SoruEkleViewModel
    {
        public int KonuID { get; set; }
        public int DonemID { get; set; }
        public int SoruTipiID { get; set; }
        public string Soru { get; set; }
        public Cevap[] Cevaplar { get; set; }

        public class Cevap
        {
            public bool Val { get; set; }
            public string CevapIcerik { get; set; }
        }
    }
}