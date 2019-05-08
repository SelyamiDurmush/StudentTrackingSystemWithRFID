using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApplication3.Userkontroller;

namespace WpfApplication3.classlar
{
    public class uc_cagir
    {
        public static void Uc_Ekle(Grid grd, UserControl uc) 
        {
         if(grd.Children.Count>0)
            {
                //grd.Children.Clear();
                grd.Children.Add(uc);
            }
            else
            {
                grd.Children.Add(uc);
            }
        
        }
    }
}