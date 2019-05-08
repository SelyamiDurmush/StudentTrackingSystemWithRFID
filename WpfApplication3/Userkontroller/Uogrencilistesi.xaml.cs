using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfApplication3.classlar;

namespace WpfApplication3.Userkontroller
{
    /// <summary>
    /// Interaction logic for Uogrencilistesi.xaml
    /// </summary>
    public partial class Uogrencilistesi : UserControl
    {
        public Uogrencilistesi()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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

                    dtg_ogrencilistesi.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Dispose();
                }
            }
        }

        private void dtg_ogrencilistesi_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Btn_Excele_Kaydet_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Workbooks.Add();
            ExcelApp.Visible = true;
            ExcelApp.Worksheets[1].Activate();
            
            for (int i = 0; i < dtg_ogrencilistesi.Columns.Count; i++)
            {
                ExcelApp.Cells[1, i + 1].Value = dtg_ogrencilistesi.Columns[i].Header;
                ExcelApp.Cells[1, i + 1].Font.Color = System.Drawing.Color.Red;
                ExcelApp.Cells[1, i + 1].Font.Size = 9;
                ExcelApp.Cells[1, i + 1].Font.Bold = true;
                ExcelApp.Cells[1, i + 1].Font.Name = "Arial Black";
            }
            int satir = 2;
            foreach (DataRowView row in dtg_ogrencilistesi.Items)
            {
                for (int j = 1; j <= 10; j++)
                { 
                    if(j >= dtg_ogrencilistesi.Columns.Count && j < 10)
                    {
                        continue;
                    }
                    else if(j == 10)
                    {
                        string sutun = row.Row.ItemArray[j].ToString();
                        ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].Value = sutun;                    
                        ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].Font.Color = System.Drawing.Color.Black;
                        ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].Font.Size = 8;
                        ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].Font.Bold = true;
                        ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].Font.Name = "Arial Black";
                        //ExcelApp.Cells[satir, dtg_ogrencilistesi.Columns.Count].HorizontalAlignment = HorizontalAlignment.Center;                     
                    }
                   
                    else
                    {
                        string sutun = row.Row.ItemArray[j].ToString();
                        ExcelApp.Cells[satir, j].Value = sutun;
                        ExcelApp.Cells[satir, j].Font.Color = System.Drawing.Color.Black;
                        ExcelApp.Cells[satir, j].Font.Size = 8;                                        
                        ExcelApp.Cells[satir, j].Font.Bold = true;
                       // ExcelApp.Cells[satir, j].HorizontalAlignment = HorizontalAlignment.Left;
                        ExcelApp.Cells[satir, j].Font.Name = "Arial Black";
                      
                    }
                  }
                satir++;
            }
            ExcelApp.Worksheets[1].Columns.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ExcelApp.Worksheets[1].Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int a = 1; a <= dtg_ogrencilistesi.Columns.Count; a++)
            {
                for (int b = 0; b <= dtg_ogrencilistesi.Items.Count; b++)
                {
                    ExcelApp.Worksheets[1].Cells[b + 1, a].BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone, null);
                    ExcelApp.Worksheets[1].Cells[b + 1, a].Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;
                    ExcelApp.Worksheets[1].Cells[b + 1, a].Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    ExcelApp.Worksheets[1].Cells[b + 1, a].Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;
                }
            }
            ExcelApp.Worksheets[1].Columns.AutoFit();
        }
      }
  }