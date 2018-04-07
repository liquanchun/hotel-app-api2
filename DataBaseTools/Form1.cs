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
            try
            {
                string mysql = "select table_name from information_schema.tables where table_schema='hotel_app' and table_type='base table' order by table_name";

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

                if (checkBox1.Checked)
                {
                    CreateControllerFile(table_name, table_name_U, table_name_L);
                }
                if (checkBox2.Checked)
                {
                    CreateRepositoriesFile(table_name, table_name_U, table_name_L);
                }
                if (checkBox3.Checked)
                {
                    CreateOtherText(table_name);
                }
                string text1 = "services.AddScoped<I" + table_name_U + "Repository, "+ table_name_U + "Repository>();";
                string text2 = "public interface I"+ table_name_U + "Repository : IEntityBaseRepository<"+ table_name + "> { }";
                string text3 = "modelBuilder.Entity<"+ table_name + ">().ToTable(\""+ table_name + "\");";
                string text4 = "public DbSet<"+ table_name + "> "+ table_name_U + "s { get; set; }";

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
            MessageBox.Show("生成完成。");
        }

        private void CreateControllerFile(string table_name, string table_name_U, string table_name_L)
        {
            string sys = textBox1.Text;
            string templatefile = textBox2.Text;
            if (string.IsNullOrEmpty(templatefile)) return;
            string contorllerPath = textBox3.Text + "\\" + sys;
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
            if (string.IsNullOrEmpty(templatefile)) return;
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

        private void CreateOtherText(string table_name)
        {
            string sqlstring = string.Format(
                @"select COLUMN_NAME,IS_NULLABLE,DATA_TYPE,COLUMN_COMMENT from information_schema.columns  
            where table_schema = 'hotel_app' and table_name = '{0}' ", table_name);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Hotel.App.Model." + textBox1.Text);
            sb.AppendLine("{");
            sb.AppendLine("   using System;");
            sb.AppendLine(string.Format("   public partial class {0} : IEntityBase", table_name));
            sb.AppendLine("   {");
            try
            {
                //创建数据库连接
                myconnection.Open();
                //创建MySqlCommand对象
                MySqlCommand mycommand = new MySqlCommand(sqlstring, myconnection);
                //通过MySqlCommand的ExecuteReader()方法构造DataReader对象
                MySqlDataReader myreader = mycommand.ExecuteReader();

                while (myreader.Read())
                {
                    sb.AppendLine("      ///<summary>");
                    sb.AppendLine("      ///" + myreader.GetString("COLUMN_COMMENT"));
                    sb.AppendLine("      ///</summary>");
                    sb.AppendLine(string.Concat("      public ", GetDataType(myreader.GetString("DATA_TYPE"))," ", myreader.GetString("COLUMN_NAME"), " { get; set; }"));
                }
                myreader.Close();
                sb.AppendLine("   }");
                sb.AppendLine("}");

                string sys = textBox1.Text;
                string modelPath = textBox6.Text + "\\" + sys;
                if (!System.IO.Directory.Exists(modelPath))
                {
                    System.IO.Directory.CreateDirectory(modelPath);
                }
                string modelFile = modelPath + "\\" + table_name + ".cs";
                if (File.Exists(modelFile))
                {
                    File.Delete(modelFile);
                }
                File.WriteAllText(modelFile, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myconnection.Close();
            }
        }

        private string GetDataType(string oldtype)
        {
            switch (oldtype)
            {
                case "int":
                    return "int";
                case "varchar":
                    return "string";
                case "bit":
                    return "bool";
                case "decimal":
                    return "decimal";
                case "datetime":
                    return "DateTime";
                default:
                    return "string";
            }
        }
    }
}
