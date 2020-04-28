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
    /// Interaktionslogik für Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
        public Window3()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider1.Text = slider1.Value.ToString();
        }

        private void txtSlider1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider2.Text = slider2.Value.ToString();
        }

        private void txtSlider2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider3.Text = slider3.Value.ToString();
        }

        private void txtSlider3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
