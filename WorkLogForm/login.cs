using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkLogForm.Service;
using WorkLogForm.CommonClass;
using ClassLibrary;
using NHibernate;

namespace WorkLogForm
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        private BaseService baseService = new BaseService();
        private WkTUser user;

        public WkTUser User
        {
            get { return user; }
            set { user = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WkTUser u = new WkTUser();
            baseService.loadEntity(u, 90018);
            List<string> entityName = new List<string>(); entityName.Add("WkTUser");
            //ArrayList userList = baseService.loadEntityList(entityName, new string[,] { { "WkTUser", "Id", CommonStaticParameter.IS_NOT_STRING, textBox1.Text.Trim() }, { "WkTUser", "KuPassWD", CommonStaticParameter.IS_NOT_STRING, textBox2.Text.Trim() } });
            ISQLQuery query = baseService.loadEntityList( "select * from Wk_T_User where KU_ID=" + textBox1.Text.Trim() + " and KU_PASSWD=" + textBox2.Text.Trim());
            ArrayList userList = (ArrayList)query.List();
            //List<string> s = new List<string>(); s.Add("Schedule");
            //ArrayList userList = baseService.loadEntityList(s, new string[,] { { "Schedule", "Id", CommonStaticParameter.IS_NOT_STRING, "aaa" } });
            if (userList == null||userList.Count<=0)
            {
                MessageBox.Show("用户名或密码错误！");
            }
            else if (userList.Count > 1)
            {
                MessageBox.Show("用户异常，请联系管理员！");
            }
            else
            {
                this.User = (WkTUser)userList[0];
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
