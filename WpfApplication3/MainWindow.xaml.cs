using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WpfApplication3.classlar;
using WpfApplication3.classlar.Parametreler;
using WpfApplication3.Userkontroller;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection baglanti = new SqlConnection("Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True;");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn_kapat_Click(object sender, RoutedEventArgs e)
        {
           if(Directory.Exists(Prm.Masaustu + "\\Sınav Kağıtları"))
            {
                try
                {
                    Directory.Delete(Prm.Masaustu + "\\Sınav Kağıtları", true);
                }
                catch 
                {                                      
                }
            }
            this.Close();                      
        }
        private void brd_sagust_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            DBbaglanti.BagTest();
            Versiyon.Content = DBbaglanti.BagDurum;
        }
        private void btn_SimgeDurumu_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Content_icerik_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void lbl_marmarauni_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void menubuton_ogrencigirisi_Click(object sender, RoutedEventArgs e)
        {
            uc_cagir.Uc_Ekle(Content_icerik, new Uogrencilistesi());
        }
        private void menubuton_ogrenciEkle_Click(object sender, RoutedEventArgs e)
        {
            winOgrenciEkle Ekle = new winOgrenciEkle();
            Hide();
            Ekle.ShowDialog();
            Close();
        }     
        private void menubuton_ogrencizleme_Click(object sender, RoutedEventArgs e)
        {
            winOgrenciIzleme Ekle = new winOgrenciIzleme();
            Hide();
            Ekle.ShowDialog();
            Close();
        }
        private void menubuton_ogrenciduzenle_Click(object sender, RoutedEventArgs e)
        {
            winOgrenciDuzenle Ekle = new winOgrenciDuzenle();
            Ekle.ShowDialog();        
        }
        private void menubuton_Yoklama_Click(object sender, RoutedEventArgs e)
        {
            DBbaglanti.BagTest();
            Versiyon.Content = DBbaglanti.BagDurum;
            winOgrenciYoklama Ekle = new winOgrenciYoklama();
            Hide();
            Ekle.ShowDialog();
            Close();
        }
        private void menubuton_ogrenci_imza_Click(object sender, RoutedEventArgs e)
        {
            uc_cagir.Uc_Ekle(Content_icerik, new Uogrenci_imzalistesi());
        }
    }
}