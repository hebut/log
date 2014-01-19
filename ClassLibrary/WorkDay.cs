using System;
using System.Collections;

namespace ClassLibrary
{
    public class WorkDay : IEntity
    {
        private DateTime workDateTime;

        public virtual DateTime WorkDateTime
        {
            get { return workDateTime; }
            set { workDateTime = value; }
        }
        private long timeAndDayId;

        public virtual long TimeAndDayId
        {
            get { return timeAndDayId; }
            set { timeAndDayId = value; }
        }
    }
}
