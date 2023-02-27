using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;
using System.Reflection;

namespace YpeuEe.Times
{
    /// <summary>
    /// 窗口显示特效
    /// </summary>
    internal class WindowShow
    {
        #region 慢慢弹出效果

        /// <summary>
        /// 在右下角慢慢弹出效果
        /// </summary>
        public static void Show(Window window)
        {
            window.Activate();
            window.Topmost = true;
            window.WindowStartupLocation = WindowStartupLocation.Manual;

            //WindowStartupLocation = WindowStartupLocation.Manual;
            //Left = 0;
            //Top = 0;
            int Index = 1;
            DispatcherTimer _timer;
            /// <summary>
            /// 结束的顶部值
            /// </summary>
            double _endTop = 0;
            //每300毫秒提升1

            window.Left = SystemParameters.WorkArea.Width - window.Width;
            _endTop = SystemParameters.WorkArea.Height - (window.Height + 2) * Index;

            window.Top = SystemParameters.WorkArea.Height + 1000;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(300);
            _timer.Tick += (object sender, EventArgs e) =>
                            {
                                while (true)
                                {
                                    if (window.Top > _endTop)
                                        window.Top -= 1;
                                    else
                                    {
                                        _timer.Stop();
                                        return;
                                    }
                                }
                            };

            _timer.Start();

        }


        #endregion

        #region 关闭窗体


        /// <summary>
        /// 逐渐关闭
        /// </summary>
        /// <param name="window"></param>
        public static void Close(Window window)
        {
            DispatcherTimer _timer1;

            double _endTop = SystemParameters.WorkArea.Height+1000;

            _timer1 = new DispatcherTimer();
            _timer1.Interval = TimeSpan.FromMilliseconds(300);
            _timer1.Tick += (object sender, EventArgs e) =>
            {
                while (true)
                {
                    if ( _endTop>= window.Top)
                        window.Top += 1;
                    else
                    {
                        _timer1.Stop();
                        window.Close();
                        return;
                    }
                }
            };
            _timer1.Start();
        }

         

        #endregion
    }
}
