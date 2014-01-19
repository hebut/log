using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class IEntity : BaseEntity
    {
        public static string ENTITY_NORMAL = "0";
        public static string ENTITY_OLD = "1";

        #region 成员
        private string state;
        private DateTime timeStamp;
        #endregion
        

        #region 属性
        
        public virtual DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        public virtual string State
        {
            get { return state; }
            set { state = value; }
        }
        #endregion
        
    }
}
