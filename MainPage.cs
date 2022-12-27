using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBO_f
{
    public partial class MainPage : Form
    {
        private string loginUser; //Поле с логином пользователя

        public string LoginUser  //Свойства для поля с логином
        { 
            get 
            { 
                return loginUser; 
            } 
            set 
            { 
                loginUser = value; 
            } 
        }
        public MainPage(string login)
        {
            LoginUser = login;
            InitializeComponent();
        }
        Point point;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }
        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainPage_Load(object sender, EventArgs e) //Метод для события загрузки формы, считывает объявления из бд и заносит их в ListView
        {
            
            DataBase db = new DataBase();
            
            db.openConnection();
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM `orders`", db.getConnection());
            
            MySqlDataReader reader = command.ExecuteReader();
            
            List<string[]> data = new List<string[]>();

            while(reader.Read())
            {
                data.Add(new string[4]);
                for(int i = 0; i < 4; i++)
                {
                    data[data.Count - 1][i] = reader[i].ToString();
                }
            }

            reader.Close();

            db.closeConnection();


            for (int i = data.Count; i > 0;i--)
            {
                ListViewGroup group = new ListViewGroup(data[i - 1][0]);
                listView1.Items.Add(new ListViewItem(data[i - 1][1], group));
                listView1.Items.Add(new ListViewItem(data[i - 1][2], group));
                listView1.Items.Add(new ListViewItem("Контактный номер: +" + data[i - 1][3], group));
                listView1.Groups.Add(group);
            }           
        }

        private void LKbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLK lkPage = new FormLK(loginUser);
            lkPage.Show();
        }

        private void createOrdButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            TradeForm tradeForm = new TradeForm(loginUser);
            tradeForm.Show();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HelpForm help = new HelpForm(LoginUser);
            help.Show();
        }
    }
}