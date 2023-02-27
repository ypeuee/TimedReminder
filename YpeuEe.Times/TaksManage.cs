using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace YpeuEe.Times
{
    public static class TaksManage
    {
        private static ObservableCollection<TaskM> _listTask = new ObservableCollection<TaskM>();

        public static ObservableCollection<TaskM> ListTask
        {
            get { return _listTask; }
            set { value = _listTask; }
        }
        static TaksManage()
        {

            string date = DateTime.Now.Date.ToShortDateString();

            // 13点一次，16点一次，19点一次

            //_listTask.Add(new TaskM() { Name = "发布系统了", Time = DateTime.Parse(date + " 13:00") });
            //_listTask.Add(new TaskM() { Name = "发布系统了", Time = DateTime.Parse(date + " 16:00") });
            //_listTask.Add(new TaskM() { Name = "发布系统了", Time = DateTime.Parse(date + " 19:00") });

            CreateTask();
        }

        /// <summary>
        /// 创建任务
        /// 默认：1小时 0分钟
        /// </summary>
        /// <param name="host"></param>
        /// <param name="minute"></param>
        public static void CreateTask(int host = 1, int minute = 0)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                host = 0;
                minute = 0;
            }
            _listTask.Add(new TaskM() { Name = "休息一下", Time = DateTime.Now.AddHours(host).AddMinutes(minute) });

        }


        /// <summary>
        /// 添加临时任务
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="minutes"></param>
        public static void CreateTask(string taskName, int host, int minutes)
        {
            var newTask = new TaskM()
            {
                Name = taskName,
                Time = DateTime.Now.AddMinutes(minutes),
                IsTemp = true,
            };

            ListTask.Add(newTask);
            // ListTask.Insert(0,newTask);
        }

        /// <summary>
        /// 查找快到期的任务
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static TaskM Find(int minutes = 0)
        {
            return ListTask.FirstOrDefault(m => Math.Ceiling((m.Time - DateTime.Now).TotalMinutes) == minutes);
        }

    }

    public class TaskM
    {
        public TaskM()
        {
            ID = ++_id;
        }
        private static int _id = 0;


        public int ID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 运行的时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 是否临时 true临时 false 正常 默认false;
        /// </summary>
        public bool IsTemp { get; set; }

        /// <summary>
        /// 等待到计时（秒）
        /// </summary>
        public int Countdown { get; set; } = 60;
    }
}
