using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "选择路径";
            button2.Text = "注册";
            button3.Text = "删除";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                textBox1.Text = fileName;
               

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isOk = IsSetDesktop(textBox1.Text,true);
            if (isOk)
            {
                MessageBox.Show("注册成功");
            }
            else
            {
                MessageBox.Show("注册失败");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isOk = IsSetDesktop(null, false);
            if (!isOk)
            {
                MessageBox.Show("删除成功");
            }
           
        }
        private bool IsSetDesktop(string appPath, bool isSetDesktop)
        {
            bool ret = false;
            RegistryKey reg = null;

            try
            {
                reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                if (isSetDesktop)
                {
                    if (null == reg)
                    {
                        reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                    }
                    reg.SetValue("Shell", appPath);
                    ret = true;
                }
                else
                {
                    reg.SetValue("Shell", "explorer.exe");//explorer.exe为操作系统显示桌面的exe程序
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err:"+e);
            }
            finally
            {
                if (null != reg)
                {
                    reg.Close();
                }
            }
            return ret;
        }



    }
}
