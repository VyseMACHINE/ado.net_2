using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace HomeWork3
{
    public partial class Form1 : Form
    {
        public static string dp = "System.Data.SqlClient";
        public string cnStr = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Sales;Integrated Security=True";
        public DbProviderFactory df = DbProviderFactories.GetFactory(dp);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (DbConnection cn = df.CreateConnection())
            {
                Console.WriteLine("DbConnection");
                cn.ConnectionString = cnStr;
                cn.Open();

                DbCommand cmd = df.CreateCommand();
                Console.WriteLine("DbCommand");
                cmd.Connection = cn;
                cmd.CommandText = "Select * from "+comboBox1.Text;
                listBox1.Items.Add("Содержимое таблицы " + comboBox1.Text);
                if (comboBox1.SelectedIndex == 2)
                {
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listBox1.Items.Add("Id: "+ dr[0] + "  BuyerName: "+ dr[1] + "  SellerId: "+ dr[2] + "  Summa: "+ dr[3] + "  Date: "+dr[4]);
                        }
                    }
                }
                else
                {
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listBox1.Items.Add("Id: " + dr[0] + "  Name: " + dr[1] + "  SecondName: " + dr[2]);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DbConnection cn = df.CreateConnection())
            {
                cn.ConnectionString = cnStr;
                cn.Open();
                
                try
                {
                    //buyer
                    DbCommand cmd = df.CreateCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = "Create table Buyer(Id int primary key identity(1,1), Name nvarchar(50), SecondName nvarchar(50))";
                    int test = cmd.ExecuteNonQuery();

                    //seller
                    DbCommand cmd2 = df.CreateCommand();
                    cmd2.Connection = cn;
                    cmd2.CommandText = "Create table Seller(Id int primary key identity(1,1), Name nvarchar(50), SecondName nvarchar(50))";
                    int test2 = cmd2.ExecuteNonQuery();
                

                    //seller
                    DbCommand cmd3 = df.CreateCommand();
                    cmd3.Connection = cn;
                    cmd3.CommandText = "Create table Sale(Id int primary key identity(1,1), BuyerId int foreign key references Buyer(Id), SellerId int foreign key references Seller(Id)," +
                        "Summa int,Date DateTime)";
                    int test3 = cmd3.ExecuteNonQuery();
                    
                }
                catch (Exception) { MessageBox.Show("Таблицы уже созданы или созданы с ошибкой"); }
                cn.Close();
            }
        }
    }
}
