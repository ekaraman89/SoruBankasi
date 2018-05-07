using System.Collections.Generic;

namespace SoruBankasi.Models.ViewModel
{
    public class SoruEkleJsonModel
    {


        public int KonuID { get; set; }
        public string KonuAdi { get; set; }
        public List<DonemJson> Donem = new List<DonemJson>();



        public class DonemJson
        {
            public int DonemID { get; set; }
            public string DonemAdi { get; set; }
        }
    }

}