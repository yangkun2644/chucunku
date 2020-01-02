using EF.BLL.ModelBLL;
using Panuon.UI;
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

namespace UI.Dispose
{
    /// <summary>
    /// CustomizeList.xaml 的交互逻辑
    /// </summary>
    public partial class CustomizeList : UserControl
    {
        private readonly DisposeBLL disposebll = new DisposeBLL();

        public CustomizeList()
        {
            InitializeComponent();
            DataContexBd();
        }

        public void DataContexBd()
        {
            this.DisposeList.DataContext = disposebll.FindList(x=>true).ToList();
        }

        
        #region 删除配置
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (disposebll.Delete(this.DisposeList.SelectedItem as EF.DAL.EFModel.Dispose))
                    MessageBox.Show("删除成功", "提醒：");
                else
                    MessageBox.Show("删除失败,配置可能不存在", "提醒：");
                this.DataContexBd();
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message,"异常:");
            }
        }
        #endregion

        #region 编辑自定义配置
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.DisposeList.SelectedItem as EF.DAL.EFModel.Dispose != null)
            {
                Tan tan = new Tan(this.DisposeList.SelectedItem as EF.DAL.EFModel.Dispose);
                tan.Show();
            }
            else
                MessageBox.Show("请选择一列","提醒：");
        }
        #endregion

        #region 模糊查询（名称）
        private void Sosuo_Click(object sender, RoutedEventArgs e)
        {
            string name= this.SosuoText.Text;
            this.SeDis(name);
        }

        public void SeDis(string name)
        {
            this.DisposeList.DataContext = disposebll.FindList(x=>x.DisposeName.Contains(name)).ToList();
        }
        #endregion

        #region 添加自定义配置
        private void AddModel_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                Tan tan = new Tan("DisposeAdd");
                tan.Show();
            //}
            //catch (Exception g)
            //{
            //    MessageBox.Show(g.Message,"异常:");
            //}
        }
        #endregion
    }
}
