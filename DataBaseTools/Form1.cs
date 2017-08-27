using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseTools
{
    public partial class Form1 : Form
    {
        MySql.Data.MySqlClient.MySqlConnection myconnection = new MySql.Data.MySqlClient.MySqlConnection();
        public Form1()
        {
            InitializeComponent();
            myconnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string mysql = "select table_name from information_schema.tables where table_schema='hotel-app' and table_type='base table' order by table_name";
            try
            {
                //创建数据库连接
                myconnection.Open();
                //创建MySqlCommand对象
                MySqlCommand mycommand = new MySqlCommand(mysql, myconnection);
                //通过MySqlCommand的ExecuteReader()方法构造DataReader对象
                MySqlDataReader myreader = mycommand.ExecuteReader();

                while (myreader.Read())
                {
                    checkedListBox1.Items.Add(myreader.GetString(0));
                }
                myreader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myconnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2= new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();

            foreach (var item in checkedListBox1.CheckedItems)
            {
                
                string table_name = item.ToString();

                string[] names = table_name.Split("_".ToArray());
                List<string> nameUList = new List<string>();
                List<string> nameLList = new List<string>();
                for (int i = 0; i < names.Length; i++)
                {
                    string temp = names[i];
                    string first = temp.Substring(0, 1);
                    nameUList.Add(first.ToUpper() + temp.Substring(1));
                    if (i == 0)
                    {
                        nameLList.Add(first.ToLower() + temp.Substring(1));
                    }
                    else
                    {
                        nameLList.Add(first.ToUpper() + temp.Substring(1));
                    }
                }

                string table_name_U = string.Join("", nameUList);
                string table_name_L = string.Join("", nameLList);

                Console.WriteLine("table_name:" + table_name);
                Console.WriteLine("table_name_U:" + table_name_U);
                Console.WriteLine("table_name_L:" + table_name_L);

                string text1 = string.Format("services.AddScoped<I{0}Repository, {0}Repository>();", table_name_U);
                string text2 = string.Format("public interface I{0}Repository : IEntityBaseRepository<{1}> { }", table_name_U, table_name);
                string text3 = string.Format("modelBuilder.Entity<{0}>().ToTable(\"{0}\");", table_name);
                string text4 = string.Format("public DbSet<{0}> {1}s { get; set; }", table_name, table_name_U);

                sb1.AppendLine(text1);
                sb2.AppendLine(text2);
                sb3.AppendLine(text3);
                sb4.AppendLine(text4);
            }

            richTextBox1.AppendText(sb1.ToString());
            richTextBox1.AppendText(System.Environment.NewLine);
            richTextBox1.AppendText(sb2.ToString());
            richTextBox1.AppendText(System.Environment.NewLine);
            richTextBox1.AppendText(sb3.ToString());
            richTextBox1.AppendText(System.Environment.NewLine);
            richTextBox1.AppendText(sb4.ToString());
            richTextBox1.AppendText(System.Environment.NewLine);
        }

        private void CreateControllerFile(string table_name, string table_name_U, string table_name_L)
        {
            string sys = textBox1.Text;
            string templatefile = textBox2.Text;
            string contorllerPath = textBox3.Text;
            if (!System.IO.Directory.Exists(contorllerPath))
            {
                System.IO.Directory.CreateDirectory(contorllerPath);
            }

            string controllerName = table_name_U + "Controller";
            string controllerFile = contorllerPath + "\\" + controllerName + ".cs";

            File.Copy(templatefile, controllerFile);
            string controllertext = File.ReadAllText(controllerFile);
            controllertext = controllertext.Replace("{table_name}", table_name)
                .Replace("{table_name_U}", table_name_U)
                .Replace("{table_name_L}", table_name_L)
                .Replace("{sys}", sys);

            File.WriteAllText(controllerFile, controllertext);
        }
        private void CreateRepositoriesFile(string table_name, string table_name_U, string table_name_L)
        {
            string sys = textBox1.Text;
            string templatefile = textBox5.Text;
            string contorllerPath = textBox4.Text + "\\" + sys;

            if(!System.IO.Directory.Exists(contorllerPath))
            {
                System.IO.Directory.CreateDirectory(contorllerPath);
            }

            string controllerName = table_name_U + "Repository";
            string controllerFile = contorllerPath + "\\" + controllerName + ".cs";

            File.Copy(templatefile, controllerFile);
            string controllertext = File.ReadAllText(controllerFile);
            controllertext = controllertext.Replace("{table_name}", table_name)
                .Replace("{table_name_U}", table_name_U)
                .Replace("{table_name_L}", table_name_L)
                .Replace("{sys}", sys);

            File.WriteAllText(controllerFile, controllertext);
        }

        private void CreateOtherText(string table_name, string table_name_U, string table_name_L)
        {

        }
    }
}
