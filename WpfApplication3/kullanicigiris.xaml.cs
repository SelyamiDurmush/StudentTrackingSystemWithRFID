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

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for kullanicigiris.xaml
    /// </summary>
    public partial class kullanicigiris : Window
    {
        public kullanicigiris()
        {
            InitializeComponent();
        }

        private void btn_kullanicigirisi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Ekle = new MainWindow();
            Ekle.ShowDialog();
        }
    }
}
