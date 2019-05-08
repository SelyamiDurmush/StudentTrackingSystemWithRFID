using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApplication3.classlar.Parametreler;
using System.Data.SqlClient;
namespace WpfApplication3.classlar
{
    public class DBislemci
    {
        public static bool EklemeIslemi(Prm veri)
        {
            sbyte i = 0;
            SqlConnection con = new SqlConnection(DBbaglanti.DBadres);
            SqlCommand com = new SqlCommand("insert into tbl_ogrenci_ekleme (KartId,AdSoyad,Numara,Sinif,Bolum,Fakulte,Okul,Iletisim,Foto,Giris) values(@KartId,@AdSoyad,@Numara,@Sinif,@Bolum,@Fakulte,@Okul,@Iletisim,@Foto,@Giris)", con);

            com.Parameters.AddWithValue("@KartId", veri.KartId);
            com.Parameters.AddWithValue("@AdSoyad", veri.AdSoyad);
            com.Parameters.AddWithValue("@Numara", veri.Numara);
            com.Parameters.AddWithValue("@Sinif", veri.Sinif);
            com.Parameters.AddWithValue("@Bolum", veri.Bolum);
            com.Parameters.AddWithValue("@Fakulte", veri.Fakulte);
            com.Parameters.AddWithValue("@Okul", veri.Okul);
            com.Parameters.AddWithValue("@Iletisim", veri.Iletisim);
            com.Parameters.AddWithValue("@Foto", veri.Foto);
            com.Parameters.AddWithValue("@Giris", veri.Giris);
            try
            {
                con.Open();
                i = (sbyte)com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
    }
}