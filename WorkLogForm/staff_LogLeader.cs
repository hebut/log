using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WorkLogForm
{
    public partial class staff_LogLeader : Form
    {
        private string y;
        private string m;
        public staff_LogLeader()
        {
            InitializeComponent();
        }

        private void staff_LogLeader_Load(object sender, EventArgs e)
        {
            this.month_comboBoxEx.SelectedIndex = 0;
            this.year_comboBoxEx.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            y = this.year_comboBoxEx.SelectedItem.ToString();//要查看的日志的年份
            m = this.month_comboBoxEx.SelectedItem.ToString();//要查看的日志的月份
            SqlConnection connection = new SqlConnection("UID=sa;PWD=iti240;Database=kjqb;server=115.24.161.202;");
            string sqlstr = "select a.LAL_SIGNINTIME,a.LAL_LOG,b.LLC_COMMENT from LOG_T_ATTENCELOG a,LOG_T_LOGCOMMENT b where a.LAL_ID=b.LLC_LOGID and a.KU_ID=90021 and a.LAL_YEAR='" + int.Parse(y) + "' and a.LAL_MONTH='" + int.Parse(m) + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlstr, connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds);
            connection.Close();
            try
            {
                DataTable dt = ds.Tables[0];
                int b,i = 1;
                int a = dt.Rows.Count;
                for (b = 0; b < a; b++, i++)
                {
                    //将从数据库中查询的数据显示在listview中
                    string pubtime = dt.Rows[b][0].ToString();
                    string content = dt.Rows[b][1].ToString();
                    string pingjia = dt.Rows[b][2].ToString();
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.AddRange(new string[] { pubtime, content, pingjia });
                    this.listView3.Items.Add(lvi);
                }

            }
            finally
            {
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "doc";
            sfd.Filter = "Word文件(*.doc)|*.doc";
            sfd.FileName = "员工日志";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DoWord(listView3, sfd.FileName);
            }
        }
        //将listview中的数据导出到word中
        private void DoWord(ListView listView, string strFileName) 
        {
            int rowNum = listView.Items.Count;//表格的行数
            int columnNum = listView.Items[0].SubItems.Count;//表格的列数
            int rowIndex = 1;
            int columnIndex = 0;
            if (rowNum == 0 || string.IsNullOrEmpty(strFileName))
            {
                return;
            }
            if (rowNum > 0)
            {
                object oMissing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application WordApp;
                Microsoft.Office.Interop.Word.Document WordDoc;
                WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
                WordDoc = WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                WordApp.ActiveDocument.PageSetup.PageWidth = WordApp.CentimetersToPoints(float.Parse("29.71"));//纸张宽度(A3)             
                WordApp.ActiveDocument.PageSetup.PageHeight = WordApp.CentimetersToPoints( float.Parse( "42.01" ) );//纸张高度(A3)
                string strContent = "员工日志\n ";//标题
                WordDoc.Paragraphs.First.Range.Text = strContent;
                WordDoc.Paragraphs.First.Range.Font.Size = 18;//设置标题字体大小
                WordDoc.Paragraphs.First.Range.Font.Bold = 2;//设置标题加粗
                WordDoc.Paragraphs.First.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//设置标题为居中
                //WordApp.Selection.TypeParagraph();//插入段落
                Microsoft.Office.Interop.Word.Range tableLocation = WordDoc.Paragraphs.Last.Range;//插入表格的位置
                Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add(tableLocation, rowNum+1, columnNum, ref oMissing, ref oMissing);//插入表格
                newTable.Borders.Enable = 1;
                newTable.Select();//选中表格
                WordApp.Selection.Tables[1].Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;//表格居中
                //表格的第一行存listview各列的列名
                foreach (ColumnHeader dc in listView.Columns)
                {
                    columnIndex++;
                    newTable.Cell(rowIndex, columnIndex).Range.Text = dc.Text;
                    newTable.Cell(rowIndex, columnIndex).Range.Bold = 2;//设置单元格中表头字体为粗体
                    //设置表头字体居中
                    newTable.Cell(rowIndex, columnIndex).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }
                newTable.Columns[1].Width = 40f;//设置第一列"序号"的宽度
                //将listview中的日志保存到表格中,从表格的第二行开始存
                for (int i =0; i < rowNum; i++)
                {
                    rowIndex = 2;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        columnIndex++;
                        newTable.Cell(rowIndex, columnIndex).Range.Text = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
                    }
                }
                    WordDoc.SaveAs(strFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                //关闭WordDoc文档对象     
                WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                MessageBox.Show("日志导出成功");
                //关闭WordApp组件对象   
                WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);   
            }
            
        }

        //将listview中的数据导出到excel中
        private void DoExport(ListView listView, string strFileName)
        {
            int rowNum = listView.Items.Count;//行数
            int columnNum = listView.Items[0].SubItems.Count;//列数
            int rowIndex = 1;
            int columnIndex = 0;
            if (rowNum == 0 || string.IsNullOrEmpty(strFileName))
            {
                return;
            }
            if (rowNum > 0)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                object missing = System.Reflection.Missing.Value;//作为缺省值参数传给word或excel对象的某个函数
                Microsoft.Office.Interop.Excel.Workbooks xlBooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];//取得sheet1
                Microsoft.Office.Interop.Excel.Range range = null;
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
                    return;
                }
                xlApp.DefaultFilePath = "";
                xlApp.DisplayAlerts = true;
                //将ListView的列名导入Excel表第一行
                foreach (ColumnHeader dc in listView.Columns)
                {
                    columnIndex++;
                    xlSheet.Cells[rowIndex, columnIndex] = dc.Text;
                    
                }
                //将ListView中的数据导入Excel中
                for (int i = 0; i < rowNum; i++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        columnIndex++;
                        //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                        xlSheet.Cells[rowIndex, columnIndex] = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
                        range = xlSheet.get_Range(xlSheet.Cells[rowIndex, columnIndex], xlSheet.Cells[rowIndex, columnIndex]);//表示获取要设置的单元格或单元格范围
                        range.Columns.AutoFit(); // 设置列宽为自动适应
                    }
                }
                xlSheet.SaveAs(strFileName, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                MessageBox.Show("日志导出成功");
                xlApp.Quit();
            }
        }
    }
}
