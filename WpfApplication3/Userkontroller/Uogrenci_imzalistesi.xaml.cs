using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication3.classlar;
using System.Drawing.Imaging;
namespace WpfApplication3.Userkontroller
{
    /// <summary>
    /// Interaction logic for Uogrenci_imzalistesi.xaml
    /// </summary>
    public partial class Uogrenci_imzalistesi : System.Windows.Controls.UserControl
    {
        public Uogrenci_imzalistesi()
        {
            InitializeComponent();
        }
        private void dtg_ogrenci_imza_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            if (Ogr.globalKartList.Count != 0)
            {
                string query = "select * from tbl_ogrenci_ekleme where KartId='";
                SqlConnection con = new SqlConnection(DBbaglanti.DBadres);
                for (int i = 0; i < Ogr.globalKartList.Count; ++i)
                {
                    if (i == 0)
                    {
                        query += Ogr.globalKartList[i] + "'";
                    }
                    else
                    {
                        query += " or KartId='" + Ogr.globalKartList[i] + "'";
                    }
                }
                query += " order by Giris asc"; // Buraya asc yerine desc yazarak azalan sırada sıralayabilirsiniz :)
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataAdapter adp = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    dtg_ogrenci_imza.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Dispose();
                }
            }
        }
        private void Printdocument1_Print(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Brush black = new SolidBrush(System.Drawing.Color.Black);
            System.Drawing.Pen blackPen = new System.Drawing.Pen(black, 4);
            e.Graphics.DrawString(" İmza Listesi ", new Font("Times New Roman", 11), System.Drawing.Brushes.Black, 380, 20);
            e.Graphics.DrawString(" 2017-2018 Yılı Bahar Dönemi ", new Font("Times New Roman", 11), System.Drawing.Brushes.Black, 320, 40);
            e.Graphics.DrawString(" Fen Bilimleri Enstitüsü / Elektrik-Elektronik Mühendisliği Anabilim Dalı ", new Font("Times New Roman", 11), System.Drawing.Brushes.Black, 170, 60);
            e.Graphics.DrawString(" Ders Kodu ve Adı                       : ", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 30, 100);
            e.Graphics.DrawString(" EEM7009.1 (3 + 0) 8.0 - Akıllı Bina Sistemleri ", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 265, 100);
            e.Graphics.DrawString(" Öğretim Üyesi / Görevlisi           : ", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 30, 120);
            e.Graphics.DrawString(" Dr. Öğr. Üyesi NAZMİ EKREN ", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 265, 120);


            //int rowWidth = 180;
            int rowHeight = 22;
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
            new System.Drawing.Rectangle(90, 200, 40, rowHeight));
            e.Graphics.DrawRectangle(Pens.Black,
            new System.Drawing.Rectangle(90, 200, 40, rowHeight));
            e.Graphics.DrawString("No", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 97, 200);

            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
            new System.Drawing.Rectangle(130, 200, 140, rowHeight));
            e.Graphics.DrawRectangle(Pens.Black,
            new System.Drawing.Rectangle(130, 200, 140, rowHeight));
            e.Graphics.DrawString("Öğrenci No", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 155, 200);

            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
            new System.Drawing.Rectangle(270, 200, 210, rowHeight));
            e.Graphics.DrawRectangle(Pens.Black,
            new System.Drawing.Rectangle(270, 200, 210, rowHeight));
            e.Graphics.DrawString("Adı Soyadı", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 335, 200);

            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
            new System.Drawing.Rectangle(480, 200, 150, rowHeight));
            e.Graphics.DrawRectangle(Pens.Black,
            new System.Drawing.Rectangle(480, 200, 150, rowHeight));
            e.Graphics.DrawString("İmza", new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 530, 200);
            //for (int i = 0; i < dtg_ogrenci_imza.Columns.Count; i++)
            //{
            //  e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
            // new System.Drawing.Rectangle((i+2) * 90, 200, 240, rowHeight));
            //  e.Graphics.DrawRectangle(Pens.Black,
            // new System.Drawing.Rectangle((i+2) * 90, 200, 240, rowHeight));
            //  e.Graphics.DrawString(dtg_ogrenci_imza.Columns[i].Header.ToString(), new Font("Times New Roman", 12), System.Drawing.Brushes.Black, (i + 1) * 170, 200);
            // }
            int satir = 0;
            foreach (DataRowView row in dtg_ogrenci_imza.Items)
            {
                for (int j = 0; j <= dtg_ogrenci_imza.Columns.Count; j++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
                    new System.Drawing.Rectangle(90, (200 + rowHeight) + satir * rowHeight, 40, rowHeight));
                    e.Graphics.DrawRectangle(Pens.Black,
                    new System.Drawing.Rectangle(90, (200 + rowHeight) + satir * rowHeight, 40, rowHeight));
                    e.Graphics.DrawString((satir + 1).ToString(), new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 90, (200 + rowHeight) + satir * rowHeight);
                    e.Graphics.DrawString("Toplam :" + (satir + 1).ToString(), new Font("Times New Roman", 11), System.Drawing.Brushes.Black, 550, (225 + rowHeight) + satir * rowHeight);
                    if (j == 2)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
                          new System.Drawing.Rectangle(2 * 135, (200 + rowHeight) + satir * rowHeight, 210, rowHeight));
                        e.Graphics.DrawRectangle(Pens.Black,
                        new System.Drawing.Rectangle(2 * 135, (200 + rowHeight) + satir * rowHeight, 210, rowHeight));
                        e.Graphics.DrawString(row.Row.ItemArray[j].ToString(), new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 270, (200 + rowHeight) + satir * rowHeight);
                    }
                    if (j == 3)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
                        new System.Drawing.Rectangle(130, (200 + rowHeight) + satir * rowHeight, 140, rowHeight));
                        e.Graphics.DrawRectangle(Pens.Black,
                        new System.Drawing.Rectangle(130, (200 + rowHeight) + satir * rowHeight, 140, rowHeight));
                        e.Graphics.DrawString(row.Row.ItemArray[j].ToString(), new Font("Times New Roman", 12), System.Drawing.Brushes.Black, 130, (200 + rowHeight) + satir * rowHeight);
                    }
                    else
                    {
                        continue;
                    }
                }
                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White),
                new System.Drawing.Rectangle(3 * 160, (200 + rowHeight) + satir * rowHeight, 150, rowHeight));
                e.Graphics.DrawRectangle(Pens.Black,
                new System.Drawing.Rectangle(3 * 160, (200 + rowHeight) + satir * rowHeight, 150, rowHeight));
                satir++;
            }
            DateTime kısatarih = DateTime.Today;
            e.Graphics.DrawString((kısatarih.ToShortDateString()), new Font("Times New Roman", 11), System.Drawing.Brushes.Black, 700, 15);
        }
        private void Btn_Ogrenci_İmza_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.Printdocument1_Print);
            pd.Print();
        }
    }
}