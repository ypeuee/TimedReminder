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
using System.Windows.Shapes;

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
        }



        public static void ShowRest()
        {
            WinRest winRest = new WinRest();
            winRest.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            TaksManage.CreateTask();
        }

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