using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           // _listTask.Add(new TaskM() { Name = "13.25戒指22专场。  14000", Time = DateTime.Parse(date + " 14:25") });


            _listTask.Add(new TaskM() { Name = "13.25戒指专场。  14000", Time = DateTime.Parse(date + " 13:25") });
            _listTask.Add(new TaskM() { Name = "15.15玉 件专场    3600", Time = DateTime.Parse(date + " 15:15") });
            _listTask.Add(new TaskM() { Name = "15.45 小挂件      1300", Time = DateTime.Parse(date + " 15:45") });
            _listTask.Add(new TaskM() { Name = "16.05戒指专      16000", Time = DateTime.Parse(date + " 16:05") });
            _listTask.Add(new TaskM() { Name = "17.05鸡血玉专场   7620", Time = DateTime.Parse(date + " 17:05") });
        }


        /// <summary>
        /// 添加临时任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="minutes"></param>
        public static void AddTemp(TaskM task, int minutes = 1)
        {
            var newTask = new TaskM()
            {
                Name = task.Name,
                Time = DateTime.Now.AddMinutes(minutes),
                IsTemp=true,
            };

            ListTask.Add(newTask);
           // ListTask.Insert(0,newTask);
        }

        /// <summary>
        /// 查找快到期的任务
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static TaskM Find(int minutes = 2)
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
        public  bool IsTemp { get; set; }
    }
}
