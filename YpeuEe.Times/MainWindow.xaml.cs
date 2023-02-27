using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YpeuEe.Times
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Move();
            this.NotifyIcon();
            this.Loaded += MainWindow_Loaded;
        }
        System.Windows.Threading.DispatcherTimer dtimer;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            dtimer = new System.Windows.Threading.DispatcherTimer();
            dtimer.Interval = TimeSpan.FromSeconds(1);
            dtimer.Tick += dtimer_Tick;
            dtimer.Start();
        }

        private TaskM tempRunTask = null;
        void dtimer_Tick(object sender, EventArgs e)
        {
            Label1.Content = DateTime.Now.ToString();
            //查找到期任务
            var task = TaksManage.Find();

            if (task != null && (tempRunTask == null || !task.Equals(tempRunTask)))
            {
                var winShow = new WinShow(task);
             
                winShow.Show();
                tempRunTask = task;
            }
        }


        #region 改绑定的值自动更新值

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
