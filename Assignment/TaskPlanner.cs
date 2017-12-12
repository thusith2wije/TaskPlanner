using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class TaskPlanner
    {   
        private DateTime finishDateTime;
        private TimeSpan workdayStartTime;
        private TimeSpan workdayStopTime;
        private DateTime ret_val;
        private object tempTime_1;
        private double i;
        private int count;
        private DayOfWeek dayOfWeek;
        HashSet<DateTime> holidays;
        private object afterWeekEnd;
        private int year;
        private bool b;
        private int timeDiff;
        private double workPeriod;
        private int moreDayDiff;
        private int remMoreDayDiff;



        /*need a class constructor to access class variabels*/
        public TaskPlanner()
        {
            holidays = new HashSet<DateTime>();
            workdayStartTime = new TimeSpan(8, 0, 0);
            workdayStopTime = new TimeSpan(16, 0, 0);
        }

        /*Specify the working day start and stop time*/
        public void SetWorkdayStartAndStop(TimeSpan startTime, TimeSpan stopTime)
        {
            /*Can change workdayStartTime and workdayStopTime*/
            var workdayStartTime = new TimeSpan(8, 0, 0);
            var workdayStopTime = new TimeSpan(16, 0, 0);

        }

        /*Setting holidays for workday planner*/
        public DateTime SetHolyday(DateTime isHoliday, double day)
        {  
            if  (day >= 0)
            {                
                if (isHoliday.DayOfWeek == DayOfWeek.Saturday)
                {
                    ret_val = isHoliday.AddDays(2);
                }
                else if (isHoliday.DayOfWeek == DayOfWeek.Sunday)
                {
                    ret_val = isHoliday.AddDays(1);
                }
            }
            else
            {               
                
                if (isHoliday.DayOfWeek == DayOfWeek.Sunday)
                {
                    ret_val = isHoliday.AddDays(-2);
                }
                else if (isHoliday.DayOfWeek == DayOfWeek.Saturday)
                {
                    ret_val = isHoliday.AddDays(-1);
                }
            }

            return ret_val;
        }

        /*Fixed holidays in eveery year*/
        public bool SetRecurringHoliday(DateTime tempTime_1)
        {
            // Independence Day            
            holidays.Add(new DateTime(tempTime_1.Year, 2, 4));
            // New Years            
            holidays.Add(new DateTime(tempTime_1.Year, 12, 6));
            // Labor Day -- 1st Monday in September            
            holidays.Add(new DateTime(tempTime_1.Year, 5, 1));
            // Christmas Day            
            holidays.Add(new DateTime(tempTime_1.Year, 12, 25));
            holidays.Add(new DateTime(tempTime_1.Year, 5, 17));
            holidays.Add(new DateTime(tempTime_1.Year, 5, 27));
            // Console.WriteLine("inside SetRecurringHoliday  " + tempTime_1);
            foreach (DateTime a in holidays)
            {
                if (tempTime_1.Day == a.Day)
                {
                    //Console.WriteLine("inside SetRecurringHoliday  if   " + a);
                    b = true;
                    break;
                }
                else
                {
                    b = false;
                }
            }
            return b;
        }

        /*Find the finishing date*/
        public void GetTaskFinishingDate(DateTime start, double days)
        {            
            double workPeriod = workdayStopTime.Hours - workdayStartTime.Hours;
            
            if (days >= 0)
            {
                /*If there not starts at working hours +++++++++++++++++++++++++++++++++++++++++++++*/
                if (!(start.Hour < workdayStartTime.Hours || start.Hour > workdayStopTime.Hours))
                {
                    double timeDiff = workdayStopTime.Hours - start.Hour;
                    double a = (days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a;
                    double remMoreDayDiff = (days * workPeriod - timeDiff) % workPeriod;

                    /*Can finish Started Day*/
                    if (timeDiff > days * workPeriod)
                    {
                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start, days);
                            start = ret_val;
                            start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(1);
                            start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        
                    }
                    /*Can not finish Started Day*/
                    else if (timeDiff < days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1, days);
                                start = ret_val;                               
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(1);                                
                            }
                            else
                            {
                                start = tempTime_1;                                
                            }
                        }
                        DateTime tempTime_2 = start;
                        if (remMoreDayDiff < 0)
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                        else
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }

                    }
                }
                /*Assign works execpt working times- 1 +++++++++++++++++++++++++++++++++++++++++++++*/
                else if (start.Hour >= workdayStopTime.Hours)
                {
                    start = start.AddDays(1);
                    start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                    double timeDiff = workdayStopTime.Hours - start.Hour;
                    double a = (days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a;
                    double remMoreDayDiff = (days * workPeriod - timeDiff) % workPeriod;

                    if (timeDiff > days * workPeriod)
                    {

                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start,days);
                            start = ret_val;
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(1);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            Console.WriteLine("Show ME1 " + start);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                    }
                    else if (timeDiff < days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1,days);
                                start = ret_val;
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(1);
                            }
                            else
                            {
                                start = tempTime_1;
                            }

                        }
                        DateTime tempTime_2 = start;
                        if (remMoreDayDiff < 0)
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                        else
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                    }
                }
                /*Assign works execpt working times- 2 +++++++++++++++++++++++++++++++++++++++++++++*/
                else if (start.Hour < workdayStartTime.Hours)
                {
                    start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                    double timeDiff = workdayStopTime.Hours - start.Hour;
                    double a = (days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a;
                    double remMoreDayDiff = (days * workPeriod - timeDiff) % workPeriod;

                    if (timeDiff > days * workPeriod)
                    {
                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start, days);
                            start = ret_val;
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(1);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                    }
                    else if (timeDiff < days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1, days);
                                start = ret_val;
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(1);
                            }
                            else
                            {
                                start = tempTime_1;
                            }

                        }
                        DateTime tempTime_2 = start;
                        if (remMoreDayDiff < 0)
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                        else
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                    }
                }                
            }
            else if (days < 0)
            {   
                /*If there not starts at working hours +++++++++++++++++++++++++++++++++++++++++++++*/
                if (!(start.Hour < workdayStartTime.Hours || start.Hour > workdayStopTime.Hours))
                {   
                    double timeDiff = start.Hour - workdayStartTime.Hours;
                    double a = (-days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a; //no of days 
                    double remMoreDayDiff = (-days * workPeriod - timeDiff) % workPeriod;

                    /*Can finish Started Day*/
                    if (timeDiff > -days * workPeriod)
                    {
                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start, days);
                            start = ret_val;
                            start = start.AddHours(-(start.Hour - workdayStopTime.Hours));
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(1);
                            start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }


                    }
                    /*Can not finish Started Day*/
                    else if (timeDiff < -days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(-1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1,days);
                                start = ret_val;
                                DateTime tempTime_2 = start.AddHours(workdayStopTime.Hours - start.Hour);/*return this work day start time*/
                                DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                                Console.WriteLine("Finishing Time:  " + finishingDateTime);
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(-1);
                                DateTime tempTime_2 = start.AddHours(workdayStopTime.Hours - start.Hour);
                                DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                                Console.WriteLine("Finishing Time:  " + finishingDateTime);
                            }
                            else
                            {
                                start = tempTime_1;
                                DateTime tempTime_2 = start.AddHours((workdayStopTime.Hours-start.Hour));
                                DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                                Console.WriteLine("Finishing Time:  " + finishingDateTime);
                            }
                        }

                    }
                }
                /*Assign works execpt working times- 1 +++++++++++++++++++++++++++++++++++++++++++++*/
                else if (start.Hour >= workdayStopTime.Hours)
                {
                    //start = start.AddDays(-1);
                    start = start.AddHours(-(start.Hour - workdayStopTime.Hours));
                    double timeDiff = start.Hour - workdayStartTime.Hours;
                    double a = (-days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a;
                    double remMoreDayDiff = (-days * workPeriod - timeDiff) % workPeriod;

                    if (timeDiff > -days * workPeriod)
                    {

                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start, days);
                            start = ret_val;
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(-1);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            //Console.WriteLine("Show ME1 " + start);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                    }
                    else if (timeDiff < -days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(-1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1,days);
                                start = ret_val;
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(-1);
                            }
                            else
                            {
                                start = tempTime_1;
                            }
                        }
                        DateTime tempTime_2 = start;
                        if (remMoreDayDiff < 0)
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                        else
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                    }
                }
                /*Assign works execpt working times- 2 +++++++++++++++++++++++++++++++++++++++++++++*/
                else if (start.Hour < workdayStartTime.Hours)
                {
                    start = start.AddHours(-(start.Hour - workdayStartTime.Hours));
                    double timeDiff = workdayStopTime.Hours - start.Hour;
                    double a = (days * workPeriod - timeDiff) / workPeriod;
                    int moreDayDiff = (int)a;
                    double remMoreDayDiff = (days * workPeriod - timeDiff) % workPeriod;

                    if (timeDiff > days * workPeriod)
                    {
                        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateTime ret_val = SetHolyday(start, days);
                            start = ret_val;
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else if (SetRecurringHoliday(start))
                        {
                            start = start.AddDays(1);
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                        else
                        {
                            finishDateTime = start.AddHours(days * workPeriod);
                            Console.WriteLine(finishDateTime);
                        }
                    }
                    else if (timeDiff < days * workPeriod)
                    {
                        for (int i = 0; i <= moreDayDiff; i++)
                        {
                            DateTime tempTime_1 = start.AddDays(1);
                            if (tempTime_1.DayOfWeek == DayOfWeek.Saturday || tempTime_1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime ret_val = SetHolyday(tempTime_1, days);
                                start = ret_val;
                            }
                            else if (SetRecurringHoliday(tempTime_1))
                            {
                                start = tempTime_1.AddDays(1);
                            }
                            else
                            {
                                start = tempTime_1;
                            }

                        }
                        DateTime tempTime_2 = start;
                        if (remMoreDayDiff < 0)
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(-remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                        else
                        {
                            DateTime finishingDateTime = tempTime_2.AddHours(remMoreDayDiff);
                            Console.WriteLine("Finishing Time:  " + finishingDateTime);
                        }
                    }
                }
            }
        }
    }
}
