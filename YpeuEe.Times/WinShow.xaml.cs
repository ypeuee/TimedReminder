﻿using System;
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
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            WinRest.ShowRest();
        }
        //等10分钟
        private void BtnDelay_OnClick(object sender, RoutedEventArgs e)
        {
            TaksManage.CreateTask("延迟10分钟", 0, 1);
            Close();
        }
    }
}
