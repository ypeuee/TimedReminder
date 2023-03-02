using System;
using System.Windows;

namespace YpeuEe.Times
{
    /// <summary>
    /// NotifyIcon
    /// </summary>
    public class NotifyIconManage
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon = null;
        private Window _window;

        public void InitialTray(Window window)
        {
            if (_notifyIcon != null)
                return;

            _window = window;
            //设置托盘的各个属性
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            //_notifyIcon.BalloonTipText = @"程序开始运行";
            _notifyIcon.Text = window.Title;
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);// new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + "\\Facebook.ico");
            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(_notifyIcon_MouseClick);

            //窗体状态改变时候触发
            _window.StateChanged += new EventHandler(_window_StateChanged);
            _window.Closed += _window_Closed;
            _window.Closing += _window_Closing;
            AddMenuItem();
        }

        /// <summary>
        /// 程序关闭中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SystemTool.IsShutdown == false)
            {
                e.Cancel = true;
                SetCollapsed();
            }
        }

        /// <summary>
        /// 程序关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _window_Closed(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
        }

        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _window_StateChanged(object sender, EventArgs e)
        {
            //if (_window.WindowState == WindowState.Minimized )
            //{
            //    SetCollapsed();
            //}
            //else if (_window.WindowState == WindowState.Normal)
            //{
            //    _window.Visibility = Visibility.Visible;
            //    _window.Activate();
            //    _window.Focus();
            //}
        }


        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if (_window.WindowState != WindowState.Normal && _window.Visibility == Visibility.Visible)
                {
                    _window.WindowState = WindowState.Normal;
                    return;
                }
                if (_window.Visibility == Visibility.Visible)
                {
                    SetCollapsed();
                }
                else
                {
                    //显示主窗体
                    _window.Visibility = Visibility.Visible;
                    _window.WindowState = WindowState.Normal;
                    _window.Activate();
                    _window.Focus();
                }
            }
        }

        /// <summary>
        /// 添加左键菜单
        /// </summary>
        private void AddMenuItem()
        {
            ////设置菜单项
            //System.Windows.Forms.MenuItem menu1 = new System.Windows.Forms.MenuItem("菜单项1");
            //System.Windows.Forms.MenuItem menu2 = new System.Windows.Forms.MenuItem("菜单项2");
            //System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem("菜单", new System.Windows.Forms.MenuItem[] { menu1, menu2 });

            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += (sender, e) =>
            {
                if (MessageBox.Show("确定要关闭吗?", "退出", MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    _notifyIcon.Visible = false;
                    SystemTool.Shutdown();
                }
            };

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = { exit };
            _notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

        }

        /// <summary>
        /// 获取主窗体
        /// </summary>
        private void SetCollapsed()
        {
            _window.Visibility = Visibility.Collapsed;
            _window.WindowState = WindowState.Normal;
            //_notifyIcon.BalloonTipText = @"我在这里！";
            //_notifyIcon.ShowBalloonTip(2000);
        }

    }
}
