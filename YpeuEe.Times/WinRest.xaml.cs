using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace YpeuEe.Times
{
    /// <summary>
    /// WinRest.xaml 的交互逻辑
    /// </summary>
    public partial class WinRest : Window
    {
        public WinRest()
        {
            InitializeComponent();

            // 显示休息页面
            ReplaceImage();

            //休息倒计时
            CountDown();
        }

        /// <summary>
        /// 休息倒计时
        /// </summary>
        void CountDown()
        {
            int totalSecond = 60 * 5;
            var time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += (object sender, EventArgs e) =>
            {
                if (totalSecond <= 0)
                {
                    time.Stop();
                    LblMsg.Content = "元气满满";
                }


                //获取分钟显示值
                var m = String.Format("{0:D2}", (totalSecond % 3600) / 60);
                //获取秒显示值
                var s = String.Format("{0:D2}", totalSecond % 60);

                LblMsg.Content = $"{m}:{s}";
                totalSecond--;
            };
            time.Start();
        }

        /// <summary>
        /// 更换背景图
        /// </summary>
        void ReplaceImage()
        {

            this.Activate();
            this.Topmost = true;

            //获取基目录即当前工作目录
            string str_1 = System.AppDomain.CurrentDomain.BaseDirectory;
            var files = System.IO.Directory.GetFiles(str_1 + "\\4k");
            int index = 0;

            var _timer = new DispatcherTimer();
           // _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Interval = TimeSpan.FromSeconds(10);
            BitmapImage lassImage = null;
            _timer.Tick += (object sender, EventArgs e) =>
            {
                try
                {
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                    bi.BeginInit();
                    StreamResourceInfo info = Application.GetRemoteStream(new Uri(files[index], UriKind.Relative));
                    bi.StreamSource = info.Stream;
                    bi.EndInit();
                    // Set the image source.
                    Img.Source = bi;

                    lassImage = bi;
                }
                catch
                {
                    if (lassImage != null)
                        Img.Source = lassImage;
                }
                finally
                {
                    if (index >= files.LongLength)
                        index = 0;
                }
            };

            _timer.Start();
        }

        /// <summary>
        /// 显示休息页面
        /// </summary>
        public static void ShowRest()
        {
            WinRest winRest = new WinRest();
            winRest.ShowDialog();
        }

        /// <summary>
        /// 关闭时添加新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            TaksManage.CreateTask();
        }

        /// <summary>
        /// btn－关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region 快速捷
        private void CommandBinding_ToolCapClick_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_ToolCapClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BtnClose_OnClick(sender, e);
        }

        #endregion

    }
}