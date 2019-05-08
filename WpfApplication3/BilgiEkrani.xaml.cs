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
using System.Windows.Threading;
using WpfApplication3.classlar.Parametreler;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for BilgiEkrani.xaml
    /// </summary>
    public partial class BilgiEkrani : Window
    {     
        public BilgiEkrani()
        {
            InitializeComponent();
        }
        private void btn_BilgiEkrani_Kapat_Click(object sender, RoutedEventArgs e)
       {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            Hata();           
        }
        void Hata()
        {
            if(Prm.Hata == 0)
            {
                Olumlu_BilgiEkrani.Visibility = Visibility.Visible;
                Olumsuz_BilgiEkrani.Visibility = Visibility.Hidden;
                img_Olumlu.Visibility = Visibility.Visible;
                img_Olumsuz.Visibility = Visibility.Hidden;
                BilgiEkrani_Content.Content = Prm.BilgiEkraniContent;
                BilgiEkrani_Header.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF25FF02"));
                BilgiEkrani_Content.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF25FF02"));
            }
            else
            {
                Olumlu_BilgiEkrani.Visibility = Visibility.Hidden;
                Olumsuz_BilgiEkrani.Visibility = Visibility.Visible;
                img_Olumlu.Visibility = Visibility.Hidden;
                img_Olumsuz.Visibility = Visibility.Visible;
                BilgiEkrani_Content.Content = Prm.BilgiEkraniContent;
                BilgiEkrani_Header.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEE1010"));
                BilgiEkrani_Content.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEE1010"));
            }
            ///3 saniye sonra kapan
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            timer.Tick += delegate (object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (this.ShowActivated) this.Close();
            };
            timer.Start();
        }
    }
}