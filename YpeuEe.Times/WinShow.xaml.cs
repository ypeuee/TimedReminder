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
            this.Move();
            Label1.Content = task.Name;
            Task = task;
        }

        public TaskM Task { get; set; }

        //心领神会
        private void Btn1_OnClick(object sender, RoutedEventArgs e)
        {

            Close();
        }
        //等一分钟
        private void Btn2_OnClick(object sender, RoutedEventArgs e)
        {
            TaksManage.AddTemp(Task);
            Close();
        }
    }
}
