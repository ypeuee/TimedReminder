using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YpeuEe.Times
{
    static class WindowTool
    {
        /// <summary>
        /// 控住右键在窗体中任意地方移动
        /// </summary>
        /// <param name="window"></param>
        public static void Move(this Window window)
        {
            //移动
            window.MouseLeftButtonDown += (sender, e) =>
           {
               try
               {
                   window.DragMove();
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex);
               
               }
           };
        }

        public static void NotifyIcon(this Window window)
        {
            NotifyIconManage notifyIcon = new NotifyIconManage();
            notifyIcon.InitialTray(window);
        }

      
        }
}
