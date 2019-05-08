using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace WpfApplication3.classlar.Parametreler
{
    public class Prm
    {
        #region Static Parametreler
        public static sbyte Hata;
        public static string BilgiEkraniContent;
        public static string Belgelerim_MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
        public static string Masaustu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
        public static string ResimAdi;
        public static string ResimAdSoyad;
        public static string ResimZaman;
        public static string SınavAdi;
        public static string SınavZaman;
        #endregion

        #region ekleParametleri

        private string adSoyad;
        private string sinif;
        private string bolum;
        private string fakulte;
        private string okul;
        private string foto;
        private string giris;

        private int kartId;
        private int numara;
        private int iletisim;

        public string AdSoyad
        {
            get
            {
                return adSoyad;
            }

            set
            {
                adSoyad = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            }
        }

        public string Sinif
        {
            get
            {
                return sinif;
            }

            set
            {
                sinif = value;
            }
        }

        public string Bolum
        {
            get
            {
                return bolum;
            }

            set
            {
                bolum = value;
            }
        }

        public string Fakulte
        {
            get
            {
                return fakulte;
            }

            set
            {
                fakulte = value;
            }
        }

        public string Okul
        {
            get
            {
                return okul;
            }

            set
            {
                okul = value;
            }
        }

        public string Foto
        {
            get
            {
                return foto;
            }

            set
            {
                foto = value;
            }
        }

        public string Giris
        {
            get
            {
                return giris;
            }

            set
            {
                giris = value;
            }
        }

        public int KartId
        {
            get
            {
                return kartId;
            }

            set
            {
                kartId = value;
            }
        }

        public int Numara
        {
            get
            {
                return numara;
            }

            set
            {
                numara = value;
            }
        }

     

        public int Iletisim
        {
            get
            {
                return iletisim;
            }

            set
            {
                iletisim = value;
            }
        }

       

        #endregion
    }
}