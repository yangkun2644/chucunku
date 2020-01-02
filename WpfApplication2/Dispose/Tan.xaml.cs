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

namespace UI.Dispose
{
    /// <summary>
    /// Tan.xaml 的交互逻辑
    /// </summary>
    public partial class Tan : Window
    {
        public Tan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 自定义配置修改
        /// </summary>
        /// <param name="dis"></param>
        public Tan(EF.DAL.EFModel.Dispose dis)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Customize view = new Customize(dis);
            this.Views.Children.Add(view);
        }

        public Tan(string menu)
        {
            InitializeComponent();
            switch (menu)
            {
                case "DisposeAdd":  //自定义配置添加
                    Customize view = new Customize();
                    this.Views.Children.Add(view);
                    break;
                default:
                    break;
            }
        }
    }
}
