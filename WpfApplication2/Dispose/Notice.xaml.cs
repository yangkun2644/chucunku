using EF.BLL.Model;
using EF.BLL.ModelBLL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Dispose
{
    /// <summary>
    /// Notice.xaml 的交互逻辑
    /// </summary>
    public partial class Notice : UserControl
    {
        public Notice()
        {
            InitializeComponent();
        }

        private readonly DisposeBLL disposebll = new DisposeBLL();

        #region 保存
        private void preserve_Click(object sender, RoutedEventArgs e)
        {
            string text1 = this.Text1.Text;
            string text2 = this.Text2.Text;
            string text3 = this.Text3.Text;
            string text4 = this.Text4.Text;
            string text5 = this.Text5.Text;

            string text =
                    string.IsNullOrEmpty(text1) ? "  " :        text1;
            text += string.IsNullOrEmpty(text2) ? "\n" : "\n" + text2;
            text += string.IsNullOrEmpty(text3) ? "\n" : "\n" + text3;
            text += string.IsNullOrEmpty(text4) ? "\n" : "\n" + text4;
            text += string.IsNullOrEmpty(text5) ? "\n" : "\n" + text5;

            //try
            //{
                EF.BLL.Model.ResultModel<EF.DAL.EFModel.Dispose> model = disposebll.GetNotice();
                if (model.date != null)
                {
                    model.date.DisposeText1 = "1";
                    model.date.DisposeText2 = DateTime.Now.ToString();
                    model.date.DisposeText3 = text;
                    disposebll.UpdateOK(model.date,true);
                }
                else
                {
                    disposebll.Add(new EF.DAL.EFModel.Dispose
                    {
                        DisposeName = "Notice",
                        DisposeText1 = "1",
                        DisposeText2 = DateTime.Now.ToString(),
                        DisposeText3 = text
                    });
                }
                MessageBox.Show("公告保存成功", "通知");
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show("异常信息:" + a.Message, "异常:请截图异常信息联系开发人员！");
            //}
        }
        #endregion

        #region 查看最新
        private void Newest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResultModel<EF.DAL.EFModel.Dispose> dispose = disposebll.GetNotice();
                DateTime time = Convert.ToDateTime(dispose.date.DisposeText2);
                MessageBox.Show(dispose.date.DisposeText3 + "\n\n\n发布时间:" + time, "公告");
            }
            catch (Exception g)
            {
                MessageBox.Show("异常信息:"+g.Message,"异常:请截图异常信息联系开发人员"); 
            }
        }
        #endregion


        #region 关闭和开启公告
        private void NoticeClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EF.BLL.Model.ResultModel<EF.DAL.EFModel.Dispose> model = disposebll.GetNotice();
                if (model.date != null)
                {
                    model.date.DisposeText1 = "0";
                    disposebll.UpdateOK(model.date, true);
                }
                else
                {
                    disposebll.Add(new EF.DAL.EFModel.Dispose
                    {
                        DisposeName = "Notice",
                        DisposeText1 = "0"
                    });
                }
                MessageBox.Show("公告关闭成功", "通知");
            }
            catch (Exception a)
            {
                MessageBox.Show("异常信息:" + a.Message, "异常:请截图异常信息联系开发人员！");
            }
        }

        private void NoticeOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EF.BLL.Model.ResultModel<EF.DAL.EFModel.Dispose> model = disposebll.GetNotice();
                if (model.date != null)
                {
                    model.date.DisposeText1 = "1";
                    disposebll.UpdateOK(model.date, true);
                }
                else
                {
                    disposebll.Add(new EF.DAL.EFModel.Dispose
                    {
                        DisposeName = "Notice",
                        DisposeText1 = "1",
                    });
                }
                MessageBox.Show("公告开启成功", "通知");
            }
            catch (Exception a)
            {
                MessageBox.Show("异常信息:" + a.Message, "异常:请截图异常信息联系开发人员！");
            }
        }
        #endregion
    }
}
