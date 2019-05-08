using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApplication3.classlar
{
    public class DBbaglanti
    {
        public static string DBadres = @"Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True;";
        public static string BagDurum;
        public static void BagTest()
        {
            using (SqlConnection  conn = new SqlConnection(DBadres))
            {
                if (conn.State == ConnectionState.Closed)

                {
                    try
                    {
                        conn.Open();
                        BagDurum = "Veritabanına Bağlanıldı...";
                    }
                    catch (Exception)
                    {
                        BagDurum = "Veritabanı Bağlantı Hatası...";
                    }
                }
                else { BagDurum = "Veritabanına Bağlanıldı..."; }
            }
        }
    }
}