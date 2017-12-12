using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var workdayStartTime = new TimeSpan(8, 0, 0);
            var workdayStopTime = new TimeSpan(16, 0, 0);
            //DateTime GetTaskFinishingDate(DateTime start, double days);
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Enter Date: YYYY-MM-DD HH:MM:SS");
                DateTime InputDateTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Work: ");
                string work_1 = Console.ReadLine();
                double work_2 = Convert.ToDouble(work_1);               
                TaskPlanner ans = new TaskPlanner();
                ans.GetTaskFinishingDate(InputDateTime, work_2);                

            }
            /*Hold console*/
            Console.ReadLine();

        }


    }
}
