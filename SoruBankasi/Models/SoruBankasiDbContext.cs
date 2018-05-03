namespace SoruBankasi.Models
{
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SoruBankasiDbContext : DbContext
    {
        public SoruBankasiDbContext()
            : base("name=SoruBankasiDbContext")
        {
            Database.SetInitializer<SoruBankasiDbContext>(new MyInitializer());
        }

        public virtual DbSet<Cevaplar> Cevaplar { get; set; }
        public virtual DbSet<Ders> Ders { get; set; }
        public virtual DbSet<Konu> Konu { get; set; }
        public virtual DbSet<KonuSoruDonemi> KonuSoruDonemi { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<KullaniciDers> KullaniciDers { get; set; }
        public virtual DbSet<Sinav> Sinav { get; set; }
        public virtual DbSet<SinavSorulari> SinavSorulari { get; set; }
        public virtual DbSet<Soru> Soru { get; set; }
        public virtual DbSet<SoruDonemi> SoruDonemi { get; set; }
        public virtual DbSet<SoruTipi> SoruTipi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ders>()
                .HasMany(e => e.Konu)
                .WithRequired(e => e.Ders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sinav>()
                .HasMany(e => e.SinavSorulari)
                .WithRequired(e => e.Sinav)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Soru>()
                .HasMany(e => e.Cevaplar)
                .WithRequired(e => e.Soru)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Soru>()
                .HasMany(e => e.SinavSorulari)
                .WithRequired(e => e.Soru)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SoruDonemi>()
                .HasMany(e => e.KonuSoruDonemi)
                .WithRequired(e => e.SoruDonemi)
                .HasForeignKey(e => e.SoruDonemID);

            modelBuilder.Entity<SoruDonemi>()
                .HasMany(e => e.Soru)
                .WithRequired(e => e.SoruDonemi)
                .HasForeignKey(e => e.SoruDonemID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SoruTipi>()
                .HasMany(e => e.Soru)
                .WithRequired(e => e.SoruTipi)
                .HasForeignKey(e => e.SoruTipID)
                .WillCascadeOnDelete(false);
        }


        public class MyInitializer : CreateDatabaseIfNotExists<SoruBankasiDbContext>
        {
            protected override void Seed(SoruBankasiDbContext context)
            {
                base.Seed(context);
                context.Kullanici.Add(new Kullanici
                {
                    KullaniciAdi = "emrah",
                    Mail = "emrah@mail.com",
                    Sifre = "123",
                    Adi = "emrah",
                    Soyadi = "karaman",
                    YoneticiMi = true
                });

                context.SoruTipi.AddRange(new List<SoruTipi> {
                    new SoruTipi { SoruTipAdi = "Klasik" },
                    new SoruTipi { SoruTipAdi = "Test" },
                    new SoruTipi { SoruTipAdi = "Bosluk Doldurma" }}
                );

                context.SoruDonemi.AddRange(new List<SoruDonemi> {
                    new SoruDonemi { SoruDonemAdi = "Vize" },
                    new SoruDonemi { SoruDonemAdi = "Final" },
                    new SoruDonemi { SoruDonemAdi = "Bütünleme" } }
                );

                context.SaveChanges();
            }
        }


    }
}
