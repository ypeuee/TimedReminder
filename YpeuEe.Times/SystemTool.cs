using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YpeuEe.Times
{
    public class SystemTool
    {
        /// <summary>
        /// true关闭系统
        /// </summary>
        public static bool IsShutdown { get; set; }

        /// <summary>
        /// 通知并关闭系统
        /// </summary>
        public static void Shutdown()
        {
            IsShutdown = true;

            try
            {
                //Application.Current.Shutdown();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static string _fileName;
        /// <summary>
        /// 程序名称
        /// 没有扩展名，没有路径，只有一个名称
        /// </summary>
        public static string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                    _fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                return _fileName;
            }
        }

        //下载版本不同的文件
        private static string currentPath;
        private static string iisBinPath;
        /// <summary>
        /// 系统路径
        /// </summary>
        public static string Path
        {
            get
            {
                if (string.IsNullOrEmpty(iisBinPath) && string.IsNullOrEmpty(currentPath))
                {
                    currentPath = AppDomain.CurrentDomain.BaseDirectory;
                    iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;
                }

                return string.IsNullOrEmpty(iisBinPath) ? currentPath : iisBinPath;
            }
        }

        private static readonly string _systemName = "FineExCLoud";
        /// <summary>
        /// 系统的名称
        /// </summary>
        public static string SystemName
        {
            get { return _systemName; }
        }

        private static string _shortcut;
        /// <summary>
        /// 桌面图标包括路径
        /// </summary>
        public static string Shortcut
        {
            get
            {
                if (string.IsNullOrEmpty(_shortcut))
                {
                    //获取当前系统用户桌面目录
                    string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                    _shortcut = System.IO.Path.Combine(desktopPath, SystemName + ".lnk");

                }
                return _shortcut;
            }

        }

        private static readonly string _previewDir = "PrintPreview";
        /// <summary>
        /// 打印预览生成文件夹名称
        /// </summary>
        public static string PreviewDir
        {
            get { return _previewDir; }
        }

        private static string _previewPath;
        /// <summary>
        /// 打印预览生成文件路径
        /// </summary>
        public static string PreviewPath
        {
            get
            {
                if (string.IsNullOrEmpty(_previewPath))
                {
                    _previewPath = Path + PreviewDir;

                    if (!Directory.Exists(_previewPath))
                        Directory.CreateDirectory(_previewPath);
                }

                return _previewPath;
            }
        }


        private static readonly string _previewExtension = ".jpg";
        /// <summary>
        /// 打印预览扩展名
        /// </summary>
        public static string PreviewExtension
        {
            get { return _previewExtension; }
        }

        private static readonly string _previewHost = "localhost";
        /// <summary>
        /// 打印预览主机
        /// </summary>
        public static string PreviewHost
        {
            get { return _previewHost; }
        }


        private static readonly string _previewPort = "8889";
        /// <summary>
        /// 打印预览主机端口
        /// </summary>
        public static string PreviewPort
        {
            get { return _previewPort; }
        }

        private static int _previewCount = 20;
        /// <summary>
        /// 默认打印预览数 10
        /// </summary>
        public static int PreviewCount
        {
            get { return _previewCount = 10; }
        }


        private static string _previewUrl;
        /// <summary>
        /// 打印预览地址
        /// </summary>
        public static string PreviewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_previewUrl))
                {
                    _previewUrl = "http://" + PreviewHost + ":" + PreviewPort + "/";
                }
                return _previewUrl;
            }
        }


        private static bool _notAdmin;
        /// <summary>
        /// 是否按管理员运行
        /// </summary>
        public static bool NotAdmin
        {
            get
            {
                var con = ConfigurationManager.AppSettings["NotAdmin"];
                bool.TryParse(con, out _notAdmin);
                return _notAdmin;
            }

        }


    }
}
