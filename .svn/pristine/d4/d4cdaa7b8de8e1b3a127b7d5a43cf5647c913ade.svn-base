using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkLogForm.WindowUiClass
{
    class CalendarClass
    {
        public static void initialCalendar(PictureBox[] dayList,int year,int month)
        {
            DateTime firstDay = new DateTime(year, month, 1);
            int dayOfWeek = 0;
            switch (firstDay.DayOfWeek.ToString())
            {
                case "Sunday":{dayOfWeek=0;break;}
                case "Monday":{dayOfWeek=1;break;}
                case "Tuesday":{dayOfWeek=2;break;}
                case "Wednesday":{dayOfWeek=3;break;}
                case "Thursday":{dayOfWeek=4;break;}
                case "Friday":{dayOfWeek=5;break;}
                case "Saturday":{dayOfWeek=6;break;}
            }
            CommonClass.CNDate dateUtil = new CommonClass.CNDate(firstDay);
        }
    }
}
