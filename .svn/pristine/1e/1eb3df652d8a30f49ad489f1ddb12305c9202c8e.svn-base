using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;//实体所在的命名空间
using WorkLogForm.Service;
using System.Collections;
using NHibernate;
using NHibernate.Cfg;
using WorkLogForm.CommonClass;
using System.Data.SqlClient;

namespace WorkLogForm
{
    public partial class shou_ye : Form
    {
        private BaseService baseService = new BaseService();
        private WkTUser user;
        private DateTime now;
        private int year;
        private int month;
        private int day;
        private string y;
        private string m;
        private string d;
        public shou_ye()
        {
            InitializeComponent();
        }

        private void shou_ye_Load(object sender, EventArgs e)
        {
            month_comboBoxEx.SelectedIndex = 0;
            year_comboBoxEx.SelectedIndex = 0;
            //每个日历小panel中显示日志的label的text都应该是从数据库中查询出来的
            string t = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色";
            if (t.Length > 10)
            {
                StringBuilder s = new StringBuilder(string.Empty);
                s.Append(t.Substring(0, 7));
                s.Append("...");
                label44.Text = s.ToString();
            }
            else
                label44.Text = t;
        }
        
        private void loadSchedule(string dd)
        {
            //从数据库中查询当前用户的日程,并在相关panel中显示相关内容
            now = DateTime.Now;//当前系统时间
            year = now.Year;
            month = now.Month;
            day = now.Day;
            y = year_comboBoxEx.Text;//要查看的日期的年份
            m = month_comboBoxEx.Text;//要查看的日期的月份
            //将要获取的日程的年,月,日拼接成日期字符串(2009-09-01)
            StringBuilder sb = new StringBuilder();
            sb.Append(y);
            sb.Append("-");
            sb.Append(m);
            sb.Append("-");
            sb.Append(dd);
            string shortdate = now.ToShortDateString().ToString();//获取当前系统时间的年月日
            //List<string> scheduleEntity = new List<string>();
            //scheduleEntity.Add("Schedule");
            //根据登录用户和日期从数据库中查询日程
            DateTime d2;
            if(int.Parse(dd)>=10)
                d2 = DateTime.ParseExact(sb.ToString(), "yyyy-mm-dd", null);//将拼接的日期字符串转换成date类型
            else
                d2 = DateTime.ParseExact(sb.ToString(), "yyyy-mm-d", null);//将拼接的日期字符串转换成date类型
            SqlConnection connection = new SqlConnection("UID=sa;PWD=iti240;Database=kjqb;server=115.24.161.202;");
            string sqlstr = "select * from LOG_T_SCHEDULE where KU_ID=90021 and LSD_DATE='" + d2 + "'";
            SqlDataReader objDataReader;
            SqlCommand objCommand = new SqlCommand(sqlstr, connection);
            try
            {
                connection.Open();
                objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (objDataReader.Read())
                {
                    if (string.Compare(shortdate, sb.ToString()) > 0)//查看的是过去的日程,将控件都设成不可编辑
                    {
                        textBox2.ReadOnly = true;
                        textBox4.ReadOnly = true;
                        checkBox1.Enabled = false;
                        comboBoxEx1.Enabled = false;
                        comboBoxEx2.Enabled = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        comboBoxEx1.Enabled = false;
                        comboBoxEx2.Enabled = false;
                    }
                    textBox4.Text = objDataReader["LSD_TITLE"].ToString();
                    textBox2.Text = objDataReader["LSD_CONTENT"].ToString();
                    if ((int)objDataReader["LSD_WARNING"] == 1)
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;
                    //comboBoxEx1,comboBoxEx2两个控件的值没有设置
                }
                else 
                {
                    if (string.Compare(shortdate, sb.ToString()) > 0)//查看的是过去的日程,不可编辑
                    {
                        textBox2.ReadOnly = true;
                        textBox4.ReadOnly = true;
                        checkBox1.Enabled = false;
                        comboBoxEx1.Enabled = false;
                        comboBoxEx2.Enabled = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        comboBoxEx1.Enabled = false;
                        comboBoxEx2.Enabled = false;
                    }
                    textBox2.Text = "";
                    textBox4.Text = "";
                    checkBox1.Checked = false;
                }
                objDataReader.Close();
            }
            finally
            {
                connection.Close();
            }
            //ArrayList scheduleList = baseService.loadEntityList(scheduleEntity, new string[,] { { "Schedule", "ku_Id", CommonStaticParameter.IS_NOT_STRING, "90021" }, { "Schedule", "date", CommonStaticParameter.IS_NOT_STRING, "2012 - 10 - 11" } });
            
            //textbox中显示已编辑的日程
            //if (scheduleList.Count != 0)
            //{
            //    ClassLibrary.Schedule schedule = (ClassLibrary.Schedule)scheduleList[0];
            //    textBox4.Text = schedule.LSD_TITLE;
            //    textBox2.Text = schedule.LSD_CONTENT;
            //   if (schedule.LSD_WARNING == 1)
            //        checkBox1.Checked = true;
            //    else
            //        checkBox1.Checked = false;
            //}
        }
        
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 172);           
            d = label1.Text;
            //系统当前时间与用户要编辑日志(日程)的当天的年月日进行比较,若不一致,编辑日志的textbox设为不可编辑
            //if ((y != year.ToString()) || (m != month.ToString()) || (d != day.ToString()))
            //{
            //    textBox3.ReadOnly = true;
            //}
            //List<string> attencelogEntity = new List<string>();
            //attencelogEntity.Add("AttenceLog");
            //ArrayList attencelogList = baseService.loadEntityList(attencelogEntity, new string[,] { { "AttenceLog", "ku_Id", CommonStaticParameter.IS_NOT_STRING, user.Id.ToString() }, { "AttenceLog", "year", CommonStaticParameter.IS_STRING, y }, { "AttenceLog", "month", CommonStaticParameter.IS_STRING, m }, { "AttenceLog", "day", CommonStaticParameter.IS_STRING, d } });
            //填写日志页面显示已填日志
            //if (attencelogList.Count != 0)
            //{
            //    ClassLibrary.AttenceLog attencelog = (ClassLibrary.AttenceLog)attencelogList[0];
            //    //textBox3.Text = attencelog.LAL_LOG;
            //}
            loadSchedule(d);     
        }

        //点击日历小方块改变panel的位置、获取日期中的day、根据日期从数据库中查询出对应的日程
        #region
        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 172);
            d = label3.Text;
            loadSchedule(d);
        }

       
        private void panel7_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 172);
            d = label7.Text;
            loadSchedule(d);
        }

        private void panel5_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 172);
            d = label5.Text;
            loadSchedule(d);
        }

        private void panel6_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 172);
            d = label6.Text;
            loadSchedule(d);
        }

        private void panel4_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 172);
            d = label4.Text;
            loadSchedule(d);
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 172);
            d = label2.Text;
            loadSchedule(d);
        }

        private void panel14_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 247);
            d = label14.Text;
            loadSchedule(d);
        }

        private void panel13_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 247);
            d = label13.Text;
            loadSchedule(d);
        }

        private void panel12_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 247);
            d = label12.Text;
            loadSchedule(d);
        }

        private void calendar_panel2_3_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 247);
            d = label11.Text;
            loadSchedule(d);
        }

        private void panel10_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 247);
            d = label10.Text;
            loadSchedule(d);
        }

        private void panel9_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 247);
            d = label9.Text;
            loadSchedule(d);
        }

        private void panel8_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 247);
            d = label8.Text;
            loadSchedule(d);
        }

        private void panel21_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label21.Text;
            loadSchedule(d);
        }

        private void panel20_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label20.Text;
            loadSchedule(d);
        }

        private void panel19_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label19.Text;
            loadSchedule(d);
        }

        private void panel18_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 322);
            d = label18.Text;
            loadSchedule(d);
        }

        private void panel17_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label17.Text;
            loadSchedule(d);
        }

        private void panel16_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label16.Text;
            loadSchedule(d);
        }

        private void panel15_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label15.Text;
            loadSchedule(d);
        }

        private void panel28_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label28.Text;
            loadSchedule(d);
        }

        private void panel27_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label27.Text;
            loadSchedule(d);
        }

        private void panel26_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label26.Text;
            loadSchedule(d);
        }

        private void panel25_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 322);
            d = label25.Text;
            loadSchedule(d);
        }

        private void panel24_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label24.Text;
            loadSchedule(d);
        }

        private void panel23_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label23.Text;
            loadSchedule(d);
        }

        private void panel22_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label22.Text;
            loadSchedule(d);
        }

        private void panel35_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label35.Text;
            loadSchedule(d);
        }

        private void panel34_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label34.Text;
            loadSchedule(d);
        }

        private void panel33_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label33.Text;
            loadSchedule(d);
        }

        private void panel32_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 322);
            d = label32.Text;
            loadSchedule(d);
        }

        private void panel31_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label31.Text;
            loadSchedule(d);
        }

        private void panel30_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label30.Text;
            loadSchedule(d);
        }

        private void panel29_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label29.Text;
            loadSchedule(d);
        }

        private void panel42_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label42.Text;
            loadSchedule(d);
        }

        private void panel41_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label41.Text;
            loadSchedule(d);
        }

        private void panel40_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label40.Text;
            loadSchedule(d);
        }

        private void panel39_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(666, 322);
            d = label39.Text;
            loadSchedule(d);
        }

        private void panel38_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(336, 322);
            d = label38.Text;
            loadSchedule(d);
        }

        private void panel37_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(446, 322);
            d = label37.Text;
            loadSchedule(d);
        }

        private void panel36_DoubleClick(object sender, EventArgs e)
        {
            panel43.Visible = true;
            panel43.Location = new Point(556, 322);
            d = label36.Text;
            loadSchedule(d);
        }
        #endregion
        
        //制定日程和查看日程中panel可见性的设置
        #region
        private void pictureBox46_Click(object sender, EventArgs e)
        {
            panel43.Visible = false;
            panel11.Visible = false;
            panel45.Visible = true;
        }

        private void label46_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel45.Visible = false;
        }

        private void label50_Click(object sender, EventArgs e)
        {
            panel11.Visible = false;
            panel45.Visible = true;
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            //制定日程中的重置按钮
            this.textBox2.Text = "";
            this.textBox4.Text = "";
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
            }
            if (comboBoxEx1.SelectedItem != null)
                comboBoxEx1.SelectedItem = null;
            if (comboBoxEx2.SelectedItem != null)
                comboBoxEx2.SelectedItem = null;
        }

        private string logToolTip(string l)
        {
            //在日志的内容中加入换行符,保证toolTip多行显示日志内容
            //toolTip控件显示的内容规定显示四行
            int s = l.Length;
            int a, b;
            string l1, l2, l3, l4, l5;
            a = s % 4;
            b = s / 4;
            if (a == 0)
            {
                l1 = l.Substring(0, b);
                l2 = l.Substring(b, b);
                l3 = l.Substring(2 * b, b);
                l4 = l.Substring(3 * b, b);
            }
            else
            {
                b++;
                l1 = l.Substring(0, b);
                l2 = l.Substring(b, b);
                l3 = l.Substring(2 * b, b);
                l4 = l.Substring(3 * b);
            }
            l5 = l1 + System.Environment.NewLine + l2 + System.Environment.NewLine + l3 + System.Environment.NewLine + l4;
            return l5;

        }
        //当鼠标知道显示日志的label时,toolTip显示日志所有内容
        #region
        private void label44_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label44, logToolTip(tt));
        }

        private void label59_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label59, logToolTip(tt));
        }

        private void label61_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label61, logToolTip(tt));
        }

        private void label63_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label63, logToolTip(tt));
        }

        private void label65_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label65, logToolTip(tt));
        }

        private void label67_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label67, logToolTip(tt));
        }

        private void label69_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label69, logToolTip(tt));
        }

        private void label71_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label71, logToolTip(tt));
        }

        private void label73_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label73, logToolTip(tt));
        }

        private void label75_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label75, logToolTip(tt));
        }

        private void label77_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label77, logToolTip(tt));
        }

        private void label79_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label79, logToolTip(tt));
        }

        private void label81_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label81, logToolTip(tt));
        }

        private void label83_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label83, logToolTip(tt));
        }

        private void label85_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label85, logToolTip(tt));
        }

        private void label87_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label87, logToolTip(tt));
        }

        private void label89_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label89, logToolTip(tt));
        }

        private void label91_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label91, logToolTip(tt));
        }

        private void label93_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label93, logToolTip(tt));
        }

        private void label95_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label95, logToolTip(tt));
        }

        private void label97_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label97, logToolTip(tt));
        }

        private void label99_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label99, logToolTip(tt));
        }

        private void label101_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label101, logToolTip(tt));
        }

        private void label103_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label103, logToolTip(tt));
        }

        private void label105_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label105, logToolTip(tt));
        }

        private void label107_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label107, logToolTip(tt));
        }

        private void label109_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label109, logToolTip(tt));
        }

        private void label111_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label111, logToolTip(tt));
        }

        private void label113_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label113, logToolTip(tt));
        }

        private void label115_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label115, logToolTip(tt));
        }

        private void label117_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label117, logToolTip(tt));
        }

        private void label119_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label119, logToolTip(tt));
        }

        private void label121_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label121, logToolTip(tt));
        }

        private void label123_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label123, logToolTip(tt));
        }

        private void label125_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label125, logToolTip(tt));
        }

        private void label127_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label127, logToolTip(tt));
        }

        private void label129_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label129, logToolTip(tt));
        }

        private void label131_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label131, logToolTip(tt));
        }

        private void label133_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label133, logToolTip(tt));
        }

        private void label135_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label135, logToolTip(tt));
        }

        private void label137_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label137, logToolTip(tt));
        }

        private void label139_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            //tt是日志的内容,应该是从数据库中获取
            string tt = "鼠标放在label的图标导航控件上是透明颜色，鼠标离开后是背景颜色77777777777777777777777777777777777777777777";
            toolTip1.SetToolTip(this.label139, logToolTip(tt));
        }
#endregion

        private void button4_Click(object sender, EventArgs e)
        {

        }



    }
}
