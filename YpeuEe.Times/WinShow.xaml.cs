using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace YpeuEe.Times
{
    /// <summary>
    /// WinShow.xaml 的交互逻辑
    /// </summary>
    public partial class WinShow : Window
    {

        public WinShow(TaskM task)
        {
            InitializeComponent();
            WindowShow.Show(this);

            _countdown = 60;
            this.Move();
            lblMsg.Content = task.Name;
            Task = task;
            _countdown = task.Countdown;

            _dtimer = new System.Windows.Threading.DispatcherTimer();
            _dtimer.Interval = TimeSpan.FromSeconds(1);
            _dtimer.Tick += (object sender, EventArgs e) =>
            {
                lblMsg.Content = $"{task.Name},{_countdown}秒后自动休息！";
                _countdown--;

                if (_countdown <= 0)
                {
                    _dtimer.Stop();
                    _countdown = 0;
                    //打开休息页面
                    BtnClose_OnClick(sender, null);
                }
            };
            _dtimer.Start();


        }


        /// <summary>
        /// 提示的任务
        /// </summary>
        public TaskM Task { get; set; }

        /// <summary>
        /// 内部定时
        /// </summary>
        System.Windows.Threading.DispatcherTimer _dtimer;

        /// <summary>
        /// 倒计时索引
        /// </summary>
        int _countdown;

        //心领神会
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            _dtimer.Stop();
            WindowShow.Close(this);
            WinRest.ShowRest();
        }
        //等10分钟
        private void BtnDelay_OnClick(object sender, RoutedEventArgs e)
        {
            _dtimer.Stop();
            TaksManage.CreateTask("延迟10分钟", 0, 10);

            WindowShow.Close(this);

        }

    }
}
