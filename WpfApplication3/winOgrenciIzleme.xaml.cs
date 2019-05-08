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
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace WpfApplication3{
    /// <summary>
    /// Interaction logic for winOgrenciIzleme.xaml
    /// </summary>
    /// 
    public partial class winOgrenciIzleme : Window
    {
        SqlConnection baglanti = new SqlConnection("Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True");
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        System.Drawing.Image sınav_word;
        System.Drawing.Image sınav_image;
        string pngTarget;
        string image_target;
        private bool image_sinav_ekleWasClicked ;
        private bool word_ekleWasClicked;
        public winOgrenciIzleme()
        {
            InitializeComponent();                     
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (txt_kartId_izleme.Text != "")
            {
                PrintDocument Printdocument1 = new PrintDocument();
                Printdocument1.PrintPage += new PrintPageEventHandler(this.Printdocument1_Print);
                Printdocument1.Print();
                Thread.Sleep(1000);
                temizle();
            }
        }     
        private void Printdocument1_Print(object sender, PrintPageEventArgs e)
        {
            int x = 0;
            int y = 125;
            int width = 830;
            int height = 1020;
            try
            {
                if (image_sinav_ekleWasClicked)
                {
                    sınav_image = System.Drawing.Image.FromFile(image_target);
                    e.Graphics.DrawImage(sınav_image, x, y, width, height);

                    if (word_ekleWasClicked)
                    {
                        Directory.Delete(image_target);
                        sınav_word = System.Drawing.Image.FromFile(pngTarget);
                        e.Graphics.DrawImage(sınav_word, x, y, width, height);
                    }
                }
                try
                {
                if (word_ekleWasClicked)
                {
                    sınav_word = System.Drawing.Image.FromFile(pngTarget);
                    e.Graphics.DrawImage(sınav_word, x, y, width, height);

                    if (image_sinav_ekleWasClicked)
                    {                      
                        Directory.Delete(pngTarget);
                        sınav_image = System.Drawing.Image.FromFile(image_target);
                        e.Graphics.DrawImage(sınav_image, x, y, width, height);
                    }
                }  
            }
            catch (Exception)
            {
            }
            finally
            {
                System.Drawing.Brush black = new SolidBrush(System.Drawing.Color.Black);
                System.Drawing.Pen blackPen = new System.Drawing.Pen(black, 4);
                e.Graphics.DrawString(" ADI SOYADI  : " + txt_adsoyad_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 20);
                e.Graphics.DrawString(" NUMARASI    : " + txt_numara_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 55);
                e.Graphics.DrawString(" SINIFI              : " + txt_sinif_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 85);
                e.Graphics.DrawString(" BÖLÜM          : " + txt_bolum_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 20);
                e.Graphics.DrawString(" ALDIĞI NOT : ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 55);
                e.Graphics.DrawString(" İMZA              : ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 85);
                e.Graphics.DrawRectangle(blackPen, 15, 15, 765, 95);
                    //e.Graphics.DrawString(" SORULAR ", new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, 350, 125);
                    //e.Graphics.DrawString(" 1-) ", new Font("Times New Roman", 15), System.Drawing.Brushes.Black, 30, 155);
                    //e.Graphics.DrawString(" 2-) ", new Font("Times New Roman", 15), System.Drawing.Brushes.Black, 30, 180);
                    //e.Graphics.DrawString(" Başarılar... ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 600, 200);
                    //e.Graphics.DrawString(" Dr.Öğr.Üyesi Nazmi EKREN ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 530, 220);
                }
          }
            catch (Exception)
            { }
            finally
            {
                System.Drawing.Brush black = new SolidBrush(System.Drawing.Color.Black);
                System.Drawing.Pen blackPen = new System.Drawing.Pen(black, 4);
                e.Graphics.DrawString(" ADI SOYADI  : " + txt_adsoyad_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 20);
                e.Graphics.DrawString(" NUMARASI    : " + txt_numara_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 55);
                e.Graphics.DrawString(" SINIFI              : " + txt_sinif_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 20, 85);
                e.Graphics.DrawString(" BÖLÜM          : " + txt_bolum_izleme.Text.ToString(), new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 20);
                e.Graphics.DrawString(" ALDIĞI NOT : ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 55);
                e.Graphics.DrawString(" İMZA              : ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 410, 85);
                e.Graphics.DrawRectangle(blackPen, 15, 15, 765, 95);
                //e.Graphics.DrawString(" SORULAR ", new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, 350, 125);
                //e.Graphics.DrawString(" 1-) ", new Font("Times New Roman", 15), System.Drawing.Brushes.Black, 30, 155);
                //e.Graphics.DrawString(" 2-) ", new Font("Times New Roman", 15), System.Drawing.Brushes.Black, 30, 180);
                //e.Graphics.DrawString(" Başarılar... ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 600, 200);
                //e.Graphics.DrawString(" Dr.Öğr.Üyesi Nazmi EKREN ", new Font("Times New Roman", 14), System.Drawing.Brushes.Black, 530, 220);
            }
         }
        private void txt_sinif_izleme_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void txt_bolum_izleme_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void ekleme_penceresi_ust_izleme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void txt_kartId_izleme_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            String tarih1 = Convert.ToString(DateTime.Now); //anlık tarih ve zaman bilgisini alır stringe çevirir
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            baglanti.Open();// veri tabanından kart ıd ye göre ilgili textbox lara verileri çekme işlemi
            SqlCommand cmd = new SqlCommand("select * from tbl_ogrenci_ekleme where KartId= '" + txt_kartId_izleme.Text + "'", baglanti);
            cmd.Connection = baglanti;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Ogr.globalKartList.Add(txt_kartId_izleme.Text);
                txt_adsoyad_izleme.Text = dr["AdSoyad"].ToString();
                txt_numara_izleme.Text = dr["Numara"].ToString();
                txt_sinif_izleme.Text = dr["Sinif"].ToString();
                txt_bolum_izleme.Text = dr["Bolum"].ToString();
                txt_fakulte_izleme.Text = dr["Fakulte"].ToString();
                txt_okul_izleme.Text = dr["Okul"].ToString();
                txt_iletisim_izleme.Text = dr["Iletisim"].ToString();
                Prm.ResimAdi = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Resimler\\" + dr["Foto"].ToString() + ".jpg";
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"" + Prm.ResimAdi);
                img.EndInit();
                img_OgrenciResmi.Source = img;
            }
            baglanti.Close();

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0,100);
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);          
            dispatcherTimer.IsEnabled = true;
                  
            //Tarih ve zaman bilgisini veri tabanına kaydetme
            if (txt_kartId_izleme.Text != " ") {            
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    SqlCommand ogrenciID = new SqlCommand();
                    ogrenciID.Connection = baglanti;
                    ogrenciID.CommandText = "update tbl_ogrenci_ekleme set Giris=@Giris where KartId= '"+txt_kartId_izleme.Text+"'";                   
                    ogrenciID.Parameters.AddWithValue("@Giris", tarih1);                  
                    ogrenciID.ExecuteNonQuery();
                    ogrenciID.Dispose();
                    baglanti.Close();               
                }
            }           
        }
        private void txt_numara_izleme_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }
        private void txt_numara_izleme_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void txt_iletisim_izleme_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            txt_kartId_izleme.Focus();
        }
        private void temizle()
        {
            txt_kartId_izleme.Clear();
            txt_adsoyad_izleme.Clear();
            txt_numara_izleme.Clear();
            txt_fakulte_izleme.Clear();
            txt_okul_izleme.Clear();
            txt_iletisim_izleme.Clear();
            txt_bolum_izleme.Clear();
            txt_sinif_izleme.Clear();
            string DefaultResim = Prm.Belgelerim_MyDocuments + "\\OgrenciTakipPro\\Default\\izleme.png";
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"" + DefaultResim);
            img.EndInit();
            img_OgrenciResmi.Source = img;
        }
        string secilenword;    
        public void word_ekle_Click(object sender, RoutedEventArgs e)
        {
            word_ekleWasClicked = true;
            try
            {
                if (!Directory.Exists(Prm.Masaustu + "\\Sınav Kağıtları\\Word Formatı"))
                {
                    Directory.CreateDirectory(Prm.Masaustu + "\\Sınav Kağıtları\\Word Formatı");
                }
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Sınav Kağıdı Seç";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dlg.Filter = "Word Files (*.docx)|*.docx|Word Files (*.doc)|*.doc";
                dlg.FilterIndex = 1;
                dlg.Multiselect = false;

                if (Convert.ToBoolean(dlg.ShowDialog()))
                {
                   secilenword = dlg.FileName;
                    if (secilenword != "")
                    {                       
                        var docPath = secilenword;
                        var app = new Microsoft.Office.Interop.Word.Application();
                        app.Visible = true;
                        var doc = app.Documents.Open(secilenword);
                        doc.ShowGrammaticalErrors = false;
                        doc.ShowRevisions = false;
                        doc.ShowSpellingErrors = false;

                        foreach (Microsoft.Office.Interop.Word.Window window in doc.Windows)
                        {
                            foreach (Microsoft.Office.Interop.Word.Pane pane in window.Panes)
                            {
                                for (var i = 1; i <= pane.Pages.Count; i++)
                                {
                                    var page = pane.Pages[i];
                                    var bits = page.EnhMetaFileBits;
                                    //var target = System.IO.Path.Combine(Prm.Masaustu + "\\Sınav Kağıtları\\" + secilenword.Split('.')[0], string.Format("{1}_Sayfa_{0}", i, secilenword.Split('.')[0])); // Çoklu sayfa çevirme
                                    using (var ms = new MemoryStream((byte[])(bits)))
                                    {
                                        DateTime zaman = DateTime.Now;
                                        string format = "dd-MM-yyyy_hh-mm-ss";
                                        Prm.ResimZaman = zaman.ToString(format);
                                        var target = (Prm.Masaustu + "\\Sınav Kağıtları\\Word Formatı\\" + Prm.ResimZaman);
                                        var image = System.Drawing.Image.FromStream(ms);                                       
                                        pngTarget = System.IO.Path.ChangeExtension(target, "png");
                                        image.Save(pngTarget, ImageFormat.Png);
                                    }
                                }
                            }
                        }
                        doc.Close(Type.Missing, Type.Missing, Type.Missing);
                        app.Quit(Type.Missing, Type.Missing, Type.Missing);

                        Prm.Hata = 0;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        txt_kartId_izleme.Focus();
                        Prm.BilgiEkraniContent = "Sınav Ekleme İşlemi Başarılı";
                        bilgi.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        txt_kartId_izleme.Focus();
                        Prm.BilgiEkraniContent = "Sınav Ekleme İşlemi Başarısız";
                        bilgi.Show();
                    }
                }                               
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }           
        }      
        private void image_sinav_ekle_Click(object sender, RoutedEventArgs e)
        {
            image_sinav_ekleWasClicked = true;
            try
            {
                if (!Directory.Exists(Prm.Masaustu + "\\Sınav Kağıtları\\Resim Formatı"))
                {
                    Directory.CreateDirectory(Prm.Masaustu + "\\Sınav Kağıtları\\Resim Formatı");
                }
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Sınav Kağıdı Seç";
                dlg.InitialDirectory = "";
                dlg.Filter = "Image Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp";
                dlg.FilterIndex = 1;

                if (Convert.ToBoolean(dlg.ShowDialog()))
                {
                    String SecilenSınav = dlg.FileName;
                    if (SecilenSınav != "")
                    {
                        DateTime zaman = DateTime.Now;
                        string format = "dd-MM-yyyy_hh-mm-ss";
                        Prm.ResimZaman = zaman.ToString(format);
                        Prm.ResimAdi = Prm.Masaustu + "\\Sınav Kağıtları\\Resim Formatı\\" + Prm.ResimZaman + ".jpg";
                        File.Copy(SecilenSınav, Prm.ResimAdi, true);
                        image_target = Prm.ResimAdi;

                        Prm.Hata = 0;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        txt_kartId_izleme.Focus();
                        Prm.BilgiEkraniContent = "Sınav Ekleme işlemi Başarılı";
                        bilgi.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        BilgiEkrani bilgi = new BilgiEkrani();
                        txt_kartId_izleme.Focus();
                        Prm.BilgiEkraniContent = "Sınav Ekleme işlemi Başarısız";
                        bilgi.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }        
        }
    }
}
