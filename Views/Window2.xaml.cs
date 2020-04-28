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

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void Buttonbuch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Buttonfilm_Click(object sender, RoutedEventArgs e)
        {
            Window3 rate = new Window3();
            //signUp.Show();
            rate.Owner = this;
            this.Hide();
            rate.ShowDialog();

        }

        private void Buttonsong_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
