using EF.BLL.ModelBLL;
using System;
using System.Windows;
using System.Windows.Controls;


namespace UI.Dispose
{
    /// <summary>
    /// Customize.xaml 的交互逻辑
    /// </summary>
    public partial class Customize : UserControl
    {
        public Customize()
        {
            InitializeComponent();
        }

        public Customize(EF.DAL.EFModel.Dispose dis)
        {
            InitializeComponent();
            this.NameText.Text = dis.DisposeName;
            this.Text1.Text = dis.DisposeText1;
            this.Text2.Text = dis.DisposeText2;
            this.Text3.Text = dis.DisposeText3;
        }
        private readonly DisposeBLL disposebll = new DisposeBLL();

        #region 自定义操作（编辑和修改）
        private void AddorUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = disposebll.GetName(this.NameText.Text).date;
                if (model != null)
                {
                    if (MessageBox.Show("配置存在,确认执行编辑修改操作？", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        model.DisposeText1 = this.Text1.Text;
                        model.DisposeText2 = this.Text2.Text;
                        model.DisposeText3 = this.Text3.Text;
                        disposebll.UpdateOK(model, true);
                        MessageBox.Show("修改成功！", "提示:");
                    }
                }
                else
                {
                    model = new EF.DAL.EFModel.Dispose()
                    {
                        DisposeName = this.NameText.Text,
                        DisposeText1 = this.Text1.Text,
                        DisposeText2 = this.Text2.Text,
                        DisposeText3 = this.Text3.Text
                    };
                    disposebll.AddOK(model);
                    MessageBox.Show("添加成功！", "提示:");
                }
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message,"程序异常:");
            }
        }
        #endregion
    }
}
