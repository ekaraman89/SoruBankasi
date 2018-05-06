namespace SoruBankasi.Models.ViewModel
{
    public class SinavHazirlamaViewModel
    {
        public int Ders { get; set; }
        public int Donem { get; set; }
        public int KlasikSoruAdet { get; set; }
        public int BoslukSoruAdet { get; set; }
        public int TestSoruAdet { get; set; }

        public int KlasikSoruPuan { get; set; }
        public int BoslukSoruPuan { get; set; }
        public int TestSoruPuan { get; set; }

    }
}