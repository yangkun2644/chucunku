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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.LoveBug
{
    /// <summary>
    /// Dbug.xaml 的交互逻辑
    /// </summary>
    public partial class Dbug : UserControl
    {
        public Dbug()
        {
            InitializeComponent();
        }

        private void PUButton_Click(object sender, RoutedEventArgs e)
        {
            Bug1 bug1 = new Bug1();
            views.Children.Clear();
            views.Children.Add(bug1);
        }
    }
}
