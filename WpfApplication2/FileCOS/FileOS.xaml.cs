using Microsoft.Win32;
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
using TencentCos;
using TencentCos.Model;

namespace UI.FileCOS
{
    /// <summary>
    /// FileOS.xaml 的交互逻辑
    /// </summary>
    public partial class FileOS : UserControl
    {
        public FileOS()
        {
            InitializeComponent();
        }
        // <param name="secretId">云 API 密钥 SecretId</param>
        // <param name="secretKey">云 API 密钥 SecretKey</param>
        // <param name="durationSecond">每次请求签名有效时长，单位为秒</param>
        // <param name="setAppid">设置腾讯云账户的账户标识 APPID</param>
        // <param name="SetRegion">设置一个默认的存储桶地域</param>
        public string SecretId = "AKIDgAPaeTrMSKxQn1elHZcQzRkocBWc36pQ";
        public string SecretKey = "Xprf2LQUN0UjkOp186oGTRBft0yg9Wkp";
        public string SetAppid = "1300493532";
        public string SetRegion = "yuanguhl";
        TencentCosFun fun;
        public FileOS(string OStype)
        {
            InitializeComponent();
            switch (OStype)
            {
                case "Tencent": //腾讯COS
                    fun= new TencentCosFun(
                        secretId: SecretId,
                        secretKey: SecretKey,
                        durationSecond: 60,
                        setAppid: SetAppid,
                        SetRegion: SetRegion
                        );
                    //TenCentCos(this.Bardchucunto,this.Chucunto);
                    TencentCosWenjian();
                    break;
                case "Aliyun" : //阿里云OSS
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 查询储存桶
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="chucunto"></param>
        //private async void TenCentCos(PUProgressBar bt,DataGrid chucunto)
        //{
        //    await Task.Run(()=>{
        //        tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "开始连接远程服务器。。。\n\n" + tishi.Text; }));
        //        var Chucunto = fun.GetChucunto();
        //        if (Chucunto.success)
        //        {
        //            bt.Dispatcher.Invoke(new Action(() =>{bt.Percent = 0.66;}));
        //            List<Bucket> listbucket = (from a in Chucunto.date.buckets
        //                                       select new Bucket { CreationDate = Convert.ToDateTime(a.createDate), Location = a.location, Name = a.name }).ToList();
        //            bt.Dispatcher.Invoke(new Action(() =>{bt.Percent = 1;}));
        //            chucunto.Dispatcher.Invoke(new Action(() => { chucunto.DataContext = listbucket; }));
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "-------------------------------------------------------------------------------\n\n" + tishi.Text; }));
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = ( "存储桶加载完成\n" + DateTime.Now + "\n") + tishi.Text; }));
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "+++++++++++++存储桶+++++++++++++\n" + tishi.Text; }));

        //        }
        //        else
        //        {
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "-------------------------------------------------------------------------------\n\n" + tishi.Text; }));
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = (Chucunto.message + "\n" + DateTime.Now + "\n") + tishi.Text; }));
        //            tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "+++++++++++++存储桶+++++++++++++\n" + tishi.Text; }));
        //        }
        //    });
        ////}

        private void shuaxin_click(object sender, RoutedEventArgs e) 
        {
            TencentCosWenjian();
        }

        /// <summary>
        /// 查询全部文件
        /// </summary>
        private async void TencentCosWenjian() 
        {
             await Task.Run(()=>{
                 wenjianbar.Dispatcher.Invoke(new Action(() => { wenjianbar.Percent = 0.1; }));
                 tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "开始连接远程服务器。。。\n\n" + tishi.Text; }));
                 var Wenjian = fun.GetBucketResult("19/Add/");
                 wenjianbar.Dispatcher.Invoke(new Action(() => { wenjianbar.Percent = 0.6; }));
                 if (Wenjian.success)
                 {
                     List<Contents> listcontents = (from a in Wenjian.date.contentsList where a.size!=0 select new Contents { key = a.key, size =(float)a.size /1024, lastModified = a.lastModified }).ToList();
                     shuliang.Dispatcher.Invoke(new Action(()=> { shuliang.Text = "文件数量:" +listcontents.Count; }));
                     Wenjianjia.Dispatcher.Invoke(new Action(() => { Wenjianjia.DataContext = listcontents; }));
                     wenjianbar.Dispatcher.Invoke(new Action(() => { wenjianbar.Percent = 1; }));
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "-------------------------------------------------------------------------------\n\n" + tishi.Text; }));
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = ("加载完成......\n" + DateTime.Now + "\n") + tishi.Text; }));
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "+++++++++++++存储桶->对象+++++++++++++\n" + tishi.Text; }));
                 }
                 else
                 {
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "-------------------------------------------------------------------------------\n\n" + tishi.Text; }));
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = (Wenjian.message + "\n" + DateTime.Now + "\n") + tishi.Text; }));
                     tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "+++++++++++++存储桶->对象+++++++++++++\n" + tishi.Text; }));
                     wenjianbar.Dispatcher.Invoke(new Action(() => { wenjianbar.Percent = 0; }));
                 }
             });
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void xiazai_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "开始下载。。。\n\n" + tishi.Text; }));
                        Contents model = new Contents();
                        Wenjianjia.Dispatcher.Invoke(new Action(() => { model = Wenjianjia.SelectedItem as Contents; }));
                        if (model != null)
                        {
                            string filelujin = model.key;
                            string name = GetKeyName(filelujin);
                            fun.GetFileDow(filelujin, name, delegate (long completed, long total)
                            {
                                xiazaijindubar.Dispatcher.Invoke(new Action(() => { xiazaijindubar.Percent = (completed * 100.0 / total) / 100.0; }));
                            }, openFileDialog.SelectedPath);
                        }
                        else
                        {
                            MessageBox.Show("请选择一个文件下载", "提示:");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "异常:");
                    }
                });
            }
        }

        public string GetKeyName(string key)
        {
            string name = string.Empty;
            string[] strArray = key.Split('/');
            name = strArray[strArray.Length-1];
            return name;
        }

        private async void shangchuan_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        tishi.Dispatcher.Invoke(new Action(() => { tishi.Text = "开始上传。。。\n\n" + tishi.Text; }));
                        Contents model = new Contents();
                        Wenjianjia.Dispatcher.Invoke(new Action(() => { model = Wenjianjia.SelectedItem as Contents; }));
                        fun.AddTencentCos(openFileDialog.FileName, delegate (long completed, long total)
                        {
                            xiazaijindubar.Dispatcher.Invoke(new Action(() => { xiazaijindubar.Percent = (completed * 100.0 / total) / 100.0; }));
                        },openFileDialog.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"异常:");
                    }
                });
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            for (;;)
            {
                MessageBox.Show("想删？😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡😡");
            }
        }
    }
}
