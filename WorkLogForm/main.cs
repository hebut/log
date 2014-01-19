﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;
using WorkLogForm.WindowUiClass;
using WorkLogForm.Service;
using NHibernate;
using NHibernate.Cfg;
using System.Collections;
using WorkLogForm.CommonClass;

namespace WorkLogForm
{
    public partial class main : Form
    {
        private EventHandler mouseLeave;
        private EventHandler mouseEnter;
        private int height,width;
        private BaseService baseService = new BaseService();
        private WkTUser user;

        public WkTUser User
        {
            get { return user; }
            set { user = value; }
        }

        public main()
        {
            InitializeComponent();
            initialWindow();
            //List<string> s = new List<string>();s.Add("Course1");
            //ArrayList wktUserList = baseService.loadEntityList(s, new string[,] { { "Course1", "CourseName", CommonStaticParameter.IS_STRING, "aaa" } });
            //creatPersonPanel(wktUserList);
          
        }

        #region 自定义窗体初始化方法
        /// <summary>
        /// 初始化window（界面效果）
        /// </summary>
        private void initialWindow()
        {
            mouseLeave = new System.EventHandler(main_MouseLeave);
            mouseEnter = new System.EventHandler(main_MouseEnter);
            height = this.Height;
            width = this.Width;
            creatWindow.SetFormRoundRectRgn(this, 15);
            creatWindow.SetFormShadow(this);
        }

        /// <summary>
        /// 创建人员列表panel
        /// </summary>
        /// <param name="personList">wktuserList</param>
        public void creatPersonPanel(ArrayList personList,int Y)
        {
            Panel childPanel = new Panel();
            childPanel.Width = 268;
            childPanel.Height = 20 * personList.Count;
            childPanel.Left = 0;
            childPanel.Top = Y;
            childPanel.Parent = panel1;
            for (int i = 0; i < personList.Count; i++)
            {
                WkTUser u = (WkTUser)personList[0];
                Button user = new Button();
                user.Parent = childPanel;
                user.Width = 500;
                user.Height = 20;
                user.Left = 0;
                user.Top = 20 * i;
                user.TextAlign = ContentAlignment.MiddleLeft;
                user.FlatStyle = FlatStyle.Flat;
                user.FlatAppearance.BorderSize = 0;
                user.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
                user.Text = "    "+u.KuName;
            }
        }

        /// <summary>
        /// 创建部门列表panel
        /// </summary>
        /// <param name="personList">wktuserList</param>
        public void creatDeptPanel(ArrayList deptList)
        {
            for (int i = 0; i < deptList.Count; i++)
            {
                WkTDept d = (WkTDept)deptList[0];
                Button dept = new Button();
                dept.Parent = panel1;
                dept.Width = 500;
                dept.Height = 20;
                dept.Left = 0;
                dept.Top = 20 * i;
                dept.TextAlign = ContentAlignment.MiddleLeft;
                dept.FlatStyle = FlatStyle.Flat;
                dept.FlatAppearance.BorderSize = 0;
                dept.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
                dept.Text = d.KdName;
                dept.Name = d.Id.ToString();
                dept.Click += new EventHandler(onClickEventHandler);
            }
        }

        private void onClickEventHandler(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            List<string> s = new List<string>(); s.Add("WkTUser");
            ArrayList wktUserList = baseService.loadEntityList(s, new string[,] { { "WkTUser", "kdid", CommonStaticParameter.IS_NOT_STRING, b.Name } });
            creatPersonPanel(wktUserList,b.Top+b.Height);
        }

        #endregion

        #region 窗体特效事件
        private int x, y;
        private void main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.x = e.X;
                this.y = e.Y;
            }
        }

        private void main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&this.Location.Y>0)
            {
                Top = MousePosition.Y - y;
                Left = MousePosition.X - x;
            }
            else if (e.Button == MouseButtons.Left && e.Y > this.y)
            {
                Top = MousePosition.Y - y;
                Left = MousePosition.X - x;
            }
        }

        private void main_Move(object sender, EventArgs e)
        {
            if (this.Location.Y <= 0)
            {
                this.MouseLeave += mouseLeave;
                this.MouseEnter += mouseEnter;
                this.Top = 0;
            }
        }
        
        private void main_MouseLeave(object sender, EventArgs e)
        {

            if (this.Location.Y == 0 && (MousePosition.Y >= this.Location.Y + this.height || MousePosition.X <= this.Location.X || this.PointToClient(new Point(MousePosition.X, MousePosition.Y)).X >= this.width))
            {
                timer2.Stop();
                timer1.Start();
            }
        }

        private void main_MouseEnter(object sender, EventArgs e)
        {
            if (this.Location.Y == 0&&(MousePosition.Y<=5||MousePosition.X>=this.Location.X||MousePosition.X<=this.Location.X+this.width))
            {
                timer1.Stop();
                timer2.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Height < 10)
            {
                this.Height = 5;
                timer1.Stop();
                return;
            }
            this.Height = this.Height / 2;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int h = Height * 5;
            if (h >= this.height)
            {
                this.Height = this.height;
                timer2.Stop();
            }
            else
            {
                this.Height = h;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = WorkLogForm.Properties.Resources.最小化2;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = WorkLogForm.Properties.Resources.最小化渐变;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = WorkLogForm.Properties.Resources.关闭渐变;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = WorkLogForm.Properties.Resources.关闭1;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = false;
            notifyIcon1.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 业务逻辑控件事件
        private void ri_zhi_button_Click(object sender, EventArgs e)
        {
            ri_zhi_textBox.Visible = true;
            ri_zhi_button.Visible = false;
            if (!ri_zhi_button.Text.Trim().Equals("点击填写"))
            {
                ri_zhi_textBox.Text = ri_zhi_button.Text;
            }
            toolTip1.SetToolTip(ri_zhi_textBox, ri_zhi_textBox.Text);
        }

        private void ri_zhi_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ri_zhi_textBox.Focused&&e.KeyChar.ToString().Equals("\r"))
            {
                ri_zhi_textBox.Visible = false;
                ri_zhi_button.Text = ri_zhi_textBox.Text;
                toolTip1.SetToolTip(ri_zhi_button, ri_zhi_button.Text);
                ri_zhi_button.Visible = true;
            }
        }

        private void ri_zhi_textBox_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(ri_zhi_textBox, ri_zhi_textBox.Text);
        }

        private void qjgl_pictureBox_Click(object sender, EventArgs e)
        {
            Leave leaveForm = new Leave();
            leaveForm.Show();
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            lxr_pictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main联系人副本;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            lxr_pictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main联系人;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            rc_spictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main文件夹副本;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            rc_spictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main日程;
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            rz_pictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main日志副本;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            rz_pictureBox.BackgroundImage = WorkLogForm.Properties.Resources.main日志;
        }
        #endregion
    }
}
