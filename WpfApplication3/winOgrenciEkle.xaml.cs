using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApplication3.classlar;
using WpfApplication3.classlar.Parametreler;
using System.Data.SqlClient;
using System.Data;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for winOgrenciEkle.xaml
    /// </summary>
    public partial class winOgrenciEkle : Window
    {
        SqlConnection baglanti = new SqlConnection("Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True;");      
        
        public winOgrenciEkle()
        {
            InitializeComponent();
        }
        private void btn_kapat1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow Ekle = new MainWindow();
            Ekle.ShowDialog();
        }
        private void txt_adsoyad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txt_numara_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        int sayi1 = 11;
        private void txt_iletisim_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            if(txt_iletisim.Text.Length == sayi1)
            {
                MessageBox.Show("Lütfen 11 haneli sayı giriniz!");
            }
        }
        int sayi = 11;
        private void txt_kartId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            if (txt_kartId.Text.Length == sayi)
            {
                MessageBox.Show("Lütfen 10 haneli sayı giriniz!");
            }
        }
        private void ekleme_penceresi_ust_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void btn_kaydet_Click(object sender, RoutedEventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                if (txt_kartId.Text != "" && txt_adsoyad.Text != "" && txt_numara.Text != "" && cmb_sinif.Text != "" && Prm.ResimAdSoyad + Prm.ResimZaman !="")
                {
                    SqlCommand ogrenciID = new SqlCommand();
                    ogrenciID.Connection = baglanti;
                    ogrenciID.CommandText = "SELECT COUNT(*) FROM tbl_ogrenci_ekleme where KartId='" + txt_kartId.Text + "'";
                    int kayitSayisi = (int)ogrenciID.ExecuteScalar();
                    ogrenciID.Dispose();
                    if (kayitSayisi == 0)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = baglanti;
                        cmd.CommandText = "SELECT COUNT(*) FROM tbl_ogrenci_ekleme";
                        Int32 count = (Int32)cmd.ExecuteScalar();
                        count++;
                        cmd.CommandText = "insert into tbl_ogrenci_ekleme(Sira,KartId,AdSoyad,Numara,Sinif,Bolum,Fakulte,Okul,Iletisim,Foto) Values ('" + count.ToString() + "','" + txt_kartId.Text + "','" + txt_adsoyad.Text + "','" + (txt_numara.Text) + "','" + cmb_sinif.Text + "','" + cmb_bolum.Text + "','" + cmb_fakulte.Text + "','" + cmb_okul.Text + "','" + (txt_iletisim.Text) + "','" + Prm.ResimAdSoyad + Prm.ResimZaman + "')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        temizle();
                        string DefaultResim = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Default\\fotogrenci.png";
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"" + DefaultResim);
                        img.EndInit();
                        img_OgrenciResmi.Source = img;
                        Prm.Hata = 0;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        Prm.BilgiEkraniContent = "Kayıt işlemi Başarılı :)";
                        bilgi.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        Prm.BilgiEkraniContent = "Bu Kart ID Zaten Kayıtlı !";
                        bilgi.Show();
                    }
                }
                else
                {
                    Prm.Hata = 1;
                    BilgiEkrani bilgi = new BilgiEkrani();
                    Prm.BilgiEkraniContent = "Lütfen Zorunlu Alanları Doldurunuz! :(  \n Ad Soyad, Numara, Sınıf veya Foto";
                    bilgi.Show();
                }
                baglanti.Close();
            }                        
        }  
        private void temizle()
        {
            txt_kartId.Clear();
            txt_adsoyad.Clear();
            txt_numara.Clear();
            cmb_fakulte.SelectedItem = null;
            cmb_okul.SelectedItem = null;
            txt_iletisim.Clear();
            cmb_bolum.SelectedItem = null;
            cmb_sinif.SelectedItem = null;            
            string DefaultResim = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Default\\fotogrenci.png";
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"" + DefaultResim);
            img.EndInit();
            img_OgrenciResmi.Source = img;
        }

        //Öğrenci Ekleme- temizle butonu
        #region
        private void btn_temizle_Click(object sender, RoutedEventArgs e)
        {
            temizle();
        }
        #endregion

        //Öğrenci fotoğrafı ekleme-Resim Ekleme
        #region
        string SecilenResim;

        private void btn_ResimEkle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Resimler"))
                {
                    Directory.CreateDirectory(Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Resimler");
                }
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Resim Seç";
                dlg.InitialDirectory = "";
                dlg.Filter = "Image Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
                dlg.FilterIndex = 1;

                if ((bool)dlg.ShowDialog())
                {
                    SecilenResim = dlg.FileName;
                    DateTime zaman = DateTime.Now;
                    string format = "dd-MM-yyyy_hh-mm-ss";
                    Prm.ResimZaman = zaman.ToString(format);
                    Prm.ResimAdi = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Resimler\\" + Prm.ResimAdSoyad + Prm.ResimZaman + ".jpg";

                    File.Copy(SecilenResim, Prm.ResimAdi, true);
                    
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(@"" + Prm.ResimAdi);
                    img.EndInit();
                    img_OgrenciResmi.Source = img;

                   Prm.Hata = 0;
                    BilgiEkrani bilgi = new BilgiEkrani();
                    Prm.BilgiEkraniContent = "Resim Ekleme işlemi Başarılı";
                    bilgi.Show();
                }
                else
                {
                   Prm.Hata = 1;
                    BilgiEkrani bilgi = new BilgiEkrani();
                    Prm.BilgiEkraniContent = "Resim Ekleme işlemi Başarısız";
                    bilgi.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void btn_duzenle_Click(object sender, RoutedEventArgs e)
        {
            winOgrenciDuzenle Ekle = new winOgrenciDuzenle();
            Hide();
            Ekle.ShowDialog();
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_kartId.Focus();
        }
      }
  }
