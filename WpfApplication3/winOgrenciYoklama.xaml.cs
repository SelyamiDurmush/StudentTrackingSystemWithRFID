using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.IO.Ports;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Threading;
using System.IO;
using WpfApplication3.classlar;
using WpfApplication3.classlar.Parametreler;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for winOgrenciIzleme.xaml
    /// </summary>
    public partial class winOgrenciYoklama : Window
    {
        SqlConnection baglanti = new SqlConnection("Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True");
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public winOgrenciYoklama()
        {
            InitializeComponent();
        }

        private void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            if (txt_kartId_yoklama.Text != "")
            {           
                temizle();
            }
        }

        private void txt_sinif_yoklama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void txt_bolum_yoklama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void ekleme_penceresi_ust_yoklama_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void txt_kartId_yoklama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            String tarih1 = Convert.ToString(DateTime.Now); //anlık tarih ve zaman bilgisini alır stringe çevirir
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select * from tbl_ogrenci_ekleme where KartId= '" + txt_kartId_yoklama.Text + "'", baglanti);
            cmd.Connection = baglanti;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Ogr.globalKartList.Add(txt_kartId_yoklama.Text);
                txt_adsoyad_yoklama.Text = dr["AdSoyad"].ToString();
                txt_numara_yoklama.Text = dr["Numara"].ToString();
                txt_sinif_yoklama.Text = dr["Sinif"].ToString();
                txt_bolum_yoklama.Text = dr["Bolum"].ToString();
                txt_fakulte_yoklama.Text = dr["Fakulte"].ToString();
                txt_okul_yoklama.Text = dr["Okul"].ToString();
                txt_iletisim_yoklama.Text = dr["Iletisim"].ToString();
                Prm.ResimAdi = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Resimler\\" + dr["Foto"].ToString() + ".jpg ";
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"" + Prm.ResimAdi);
                img.EndInit();
                img_OgrenciResmi1.Source = img;
            }
            baglanti.Close();

            dispatcherTimer.Tick += dispatcherTimer1_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
            dispatcherTimer.IsEnabled = true;

            //Tarih ve zaman bilgisini veri tabanına kaydetme
            if (txt_kartId_yoklama.Text != " ")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    SqlCommand ogrenciID = new SqlCommand();
                    ogrenciID.Connection = baglanti;
                    ogrenciID.CommandText = "update tbl_ogrenci_ekleme set Giris=@Giris where KartId= '" + txt_kartId_yoklama.Text + "'";
                    ogrenciID.Parameters.AddWithValue("@Giris", tarih1);
                    ogrenciID.ExecuteNonQuery();
                    ogrenciID.Dispose();
                    baglanti.Close();
                }
            }
        }
        private void txt_numara_yoklama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void txt_numara_yoklama_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void txt_iletisim_yoklama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void btn_kapat2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow Ekle = new MainWindow();
            Ekle.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             txt_kartId_yoklama.Focus();
        }
        private void temizle()
        {
            txt_kartId_yoklama.Clear();
            txt_adsoyad_yoklama.Clear();
            txt_numara_yoklama.Clear();
            txt_fakulte_yoklama.Clear();
            txt_okul_yoklama.Clear();
            txt_iletisim_yoklama.Clear();
            txt_bolum_yoklama.Clear();
            txt_sinif_yoklama.Clear();
            string DefaultResim = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Default\\izleme.png";
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"" + DefaultResim);
            img.EndInit();
            img_OgrenciResmi1.Source = img;
        }
    }
}