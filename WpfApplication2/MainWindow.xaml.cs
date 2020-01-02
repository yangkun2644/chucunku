using EF.BLL.Model;
using EF.BLL.ModelBLL;
using EF.DAL.EFModel;
using Panuon.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UI.Dispose;
using UI.FileCOS;
using UI.HomePage;
using UI.LoveBug;

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DisposeBLL disposebll = new DisposeBLL();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.Title = "测试 1.0.0.0 -----" + "请求服务器中。。。";
                ResultModel<Dispose> dispose = disposebll.GetNotice();
                this.Title = "测试 1.0.0.0  --------什么都木有发生";
            }
            catch (Exception e)
            {
                this.Title = "测试 1.0.0.0  --------毁灭性异常:"+e.Message;
            }
            //this.MenuView.Children.Add(new Control_Home());
            //加载左边导航栏
            //ObservableCollection<PUTreeViewItemModel> PutItem = new ObservableCollection<PUTreeViewItemModel>() ;
            //PUTreeViewItemModel PutViewItemModel = null;
            //List<PUTreeViewItemModel> ListPutViewItemModel = null;
            //Thickness  putViewItemPadding=new Thickness();
            //putViewItemPadding.Bottom=10;
            //putViewItemPadding.Left=0;
            //putViewItemPadding.Right=0;
            //putViewItemPadding.Top=0;
            //for (int i = 0; i < 10; i++)
            //{
            //    PutViewItemModel = new PUTreeViewItemModel();
            //    ListPutViewItemModel = new List<PUTreeViewItemModel>();
            //    for (int j = 0; j < 10; j++)
            //    {
            //        PUTreeViewItemModel PutModel = new PUTreeViewItemModel();
            //        PutModel.Header = i.ToString() + "/" + j.ToString();
            //        PutModel.Value = i .ToString()+ "/" + j.ToString();
            //        PutModel.Padding = putViewItemPadding;
            //        PutModel.ToolTip = "悬浮提示："+i.ToString() + "/" + j.ToString();
            //        ListPutViewItemModel.Add(PutModel);
            //    }
            //    PutViewItemModel.Header = i.ToString();

            //    PutViewItemModel.Items = ListPutViewItemModel;
            //    PutItem.Add(PutViewItemModel);
            //}
            //this.PUTreeView.BindingItems = PutItem;
            //this.PUTreeView.SelectedItemChanged += PUTreeView_SelectedItemChanged;
        }

        private void PUTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PUTreeView Treeview =sender as PUTreeView;
            TreeViewItem TreeviewItem = Treeview.ChoosedItem;
            try
            {
                MessageBox.Show(TreeviewItem.ToString());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }


        private void Dispose_Click(object sender, RoutedEventArgs e)
        {
            PUTabControl pUTab = new PUTabControl();
            ObservableCollection<PUTabItemModel> pUs = new ObservableCollection<PUTabItemModel>();
            pUs.Add(new PUTabItemModel { Header="公告管理", Value= "Dispose", Content =  new Notice()});
            pUs.Add(new PUTabItemModel { Header = "自定义配置", Value = "Customize", Content = new Customize() });
            pUs.Add(new PUTabItemModel { Header = "全部配置", Value = "CustomizeList", Content = new CustomizeList() });
            pUTab.BindingItems = pUs;
            pUTab.TabControlStyle = TabControlStyles.Classic;
            this.MenuView.Children.Clear();
            this.MenuView.Children.Add(pUTab);
        }

        private void Menutop_Click(object sender, RoutedEventArgs e)
        {
            this.MenuView.Children.Clear();
            this.MenuView.Children.Add(new Control_Home());
        }

        private void bugtext_Click(object sender, RoutedEventArgs e)
        {
            this.MenuView.Children.Clear();
            this.MenuView.Children.Add(new Dbug());
        }

        private void FileAdmin_Click(object sender, RoutedEventArgs e)
        {
            PUTabControl pUTab = new PUTabControl();
            ObservableCollection<PUTabItemModel> pUs = new ObservableCollection<PUTabItemModel>();
            pUs.Add(new PUTabItemModel { Header = "腾讯COS", Value = "OS", Content = new FileOS("Tencent") });
            pUs.Add(new PUTabItemModel { Header = "阿里云OSS", Value = "Customize", Content = new Customize() });
            pUTab.BindingItems = pUs;
            pUTab.TabControlStyle = TabControlStyles.Classic;
            this.MenuView.Children.Clear();
            this.MenuView.Children.Add(pUTab);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Test test = new Test();
        //    this.ActiveItem.Content = test;
        //}

        //       DateTime t1 = new DateTime(2006, 11, 13);
        //       DateTime t2 = new DateTime(2007, 11, 13);

        //       TimeSpan d3 = t2.Subtract(t1);

        //       Console.WriteLine(d3.Days.ToString()+"天"
        //+d3.Hours.ToString()+"小时"
        //+d3.Minutes.ToString()+"分钟"
        //+d3.Seconds.ToString()+"秒"
        //);
    }
}
