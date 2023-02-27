using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows;


namespace YpeuEe.Times
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //UpdteDatabase. ReadSql();

            //CerManage.Installation();

            #region 程序退出异常处理

            //异常处理
            this.DispatcherUnhandledException += (sender, e) =>
            {
                //ToolLogs.Error(e.Exception);

                e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。

                MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止。" + e.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            #endregion

            //检测是否已有一个程序实例运行
            Startup += App_Startup;

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                //为用户组指定对应目录的完全访问权限
                //if (!MyClass.SetAccess("Users", System.Windows.Forms.Application.StartupPath))
                if (SystemTool.SetAdmin)
                    MyAdmin.SetAdmin();

                //FineEx.Update.UpdateBase objUpdate = new FineEx.Update.UpdateBase();
                ////#if DEBUG
                ////#else
                //if (!objUpdate.IsNewVersion())
                //{
                //    string upFolder = Environment.CurrentDirectory + "\\update\\";
                //    if (Directory.Exists(upFolder))
                //    {
                //        System.IO.Directory.Delete(upFolder, true);
                //    }
                //    string fileName = Environment.CurrentDirectory + "\\FineEx.Update.exe";
                //    string argu = "\"" + this.GetType().Assembly.Location + "\"";

                //    System.Diagnostics.Process.Start(fileName, argu);
                //    Environment.Exit(0);
                //    return;
                //}
                ////#endif
            }

        }


        System.Threading.Mutex _mutex;

        /// <summary>
        /// 检测是否已有一个程序实例运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                bool ret;
                _mutex = new System.Threading.Mutex(true, SystemTool.FileName, out ret);

                if (!ret)
                {
                    SystemTool.Shutdown();
                }

            }
            catch (Exception)
            {

            }


        }
    }

    class MyAdmin
    {
        /// <summary>
        /// 设置为Admin启动
        /// </summary>
        public static void SetAdmin()
        {
            try
            {
                //获得当前登录的Windows用户标示  
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                //创建Windows用户主题  

                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                //判断当前登录用户是否为管理员  
                if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //创建启动对象  
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //设置运行文件  
                    startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                    //设置启动参数  
                    string[] strs = Environment.GetCommandLineArgs();
                    startInfo.Arguments = String.Join(" ", strs);
                    //设置启动动作,确保以管理员身份运行  
                    startInfo.Verb = "runas";
                    //如果不是管理员，则启动UAC  
                    System.Diagnostics.Process.Start(startInfo);
                    //退出  
                    Environment.Exit(0);
                    return;
                }


                //如果是管理员，则直接运行  
                SetAccess("Users", System.Windows.Forms.Application.StartupPath);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 为指定用户组，授权目录指定完全访问权限
        /// </summary>
        /// <param name="user">用户组，如Users</param>
        /// <param name="folder">实际的目录</param>
        /// <returns></returns>
        public static bool SetAccess(string user, string folder)
        {
            try
            {
                //定义为完全控制的权限
                const FileSystemRights Rights = FileSystemRights.FullControl;

                //添加访问规则到实际目录
                var AccessRule = new FileSystemAccessRule(user, Rights,
                    InheritanceFlags.None,
                    PropagationFlags.NoPropagateInherit,
                    AccessControlType.Allow);

                var Info = new DirectoryInfo(folder);
                var Security = Info.GetAccessControl(AccessControlSections.Access);

                bool Result;
                Security.ModifyAccessRule(AccessControlModification.Set, AccessRule, out Result);
                if (!Result) return false;

                //总是允许再目录上进行对象继承
                const InheritanceFlags iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

                //为继承关系添加访问规则
                AccessRule = new FileSystemAccessRule(user, Rights,
                    iFlags,
                    PropagationFlags.InheritOnly,
                    AccessControlType.Allow);

                Security.ModifyAccessRule(AccessControlModification.Add, AccessRule, out Result);
                if (!Result) return false;

                Info.SetAccessControl(Security);

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }

}
