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
using System.Timers;
using System.Windows.Threading;
namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for winOgrenciDuzenle.xaml
    /// </summary>
    public partial class winOgrenciDuzenle : Window
    {
        SqlConnection baglanti = new SqlConnection("Data Source=PC-BILGISAYAR; Initial Catalog=veritabani; Integrated Security=True;");
        SqlDataAdapter adpr;
        public winOgrenciDuzenle()
        {            
           InitializeComponent();
            dtg_duzenle.BeginInit();
            string query = ("select * from tbl_ogrenci_ekleme");
            adpr = new SqlDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            adpr.Fill(dt);
            dtg_duzenle.DataContext = dt; 
            dtg_duzenle.EndInit();
        }       
            private void btn_kapat_duzenle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            winOgrenciEkle Ekle = new winOgrenciEkle();
            Ekle.ShowDialog();
        }
        private void kayıt_sil_Click(object sender, RoutedEventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed && dtg_duzenle.SelectedCells.Count != 0)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "delete from tbl_ogrenci_ekleme where KartId=@numara";

                var cellInfo = dtg_duzenle.SelectedCells[0];
                var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                var row = (DataRowView)content.DataContext;
                object[] obj = row.Row.ItemArray;

                cmd.Parameters.AddWithValue("@numara", (obj[1].ToString()));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
                listeleme();
            }
        }      
        public void kayıt_guncelle_Click(object sender, RoutedEventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommandBuilder com = new SqlCommandBuilder(adpr);
                dtg_duzenle.EndInit();
                DataTable dt = dtg_duzenle.DataContext as DataTable;
                adpr.Update(dt);
                dtg_duzenle.ItemsSource = dt.DefaultView;
                baglanti.Close();
            }
            if (txt_ara.Text.Length > 0)
            {                              
               txt_ara.Text = "";              
            }
        }    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listeleme();
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            kayıt_guncelle.RaiseEvent(newEventArgs);
        }
        private void listeleme()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = ("select * from tbl_ogrenci_ekleme");
                adpr = new SqlDataAdapter(cmd);               
                DataTable dt = new DataTable("tbl_ogrenci_ekleme");
                adpr.Fill(dt);
                dtg_duzenle.ItemsSource = dt.DefaultView;
                DataSet ds = new DataSet();
                adpr.Fill(ds,"tbl_ogrenci_ekleme");
                dtg_duzenle.DataContext = ds.Tables["tbl_ogrenci_ekleme"];
                baglanti.Close();
            }
        }
        public DataTable CreateTable()
        {
            string query = ("select * from tbl_ogrenci_ekleme");
            adpr = new SqlDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            adpr.Fill(dt);
            return dt;            
        }
        private void dtg_duzenle_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }         
        private void txt_ara_TextChanged(object sender, TextChangedEventArgs e)
        {        
           (dtg_duzenle.DataContext as DataTable).DefaultView.RowFilter =string.Format("AdSoyad LIKE '%{0}%' OR Numara LIKE '%{0}%'", txt_ara.Text);
        }
        private void duzenleme_penceresi_ust_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}