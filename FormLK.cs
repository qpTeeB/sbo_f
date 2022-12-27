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
    public partial class FormLK : Form
    {
        private string loginUser;
        
        public string LoginUser 
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
        public FormLK(string login)
        {
            LoginUser = login;
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void returnToMain_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage mainPage = new MainPage(LoginUser);
            mainPage.Show();
        }
        Point point;
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void FormLK_Load(object sender, EventArgs e) //Загрузка объявлений определенного пользователя из бд и доавление их на ListView
        {
            LKbutton.Enabled = false;
            LKbutton.Visible = false;
            Username_lable.Text = LoginUser;

            DataBase db = new DataBase();

            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `creator` = @uLogin", db.getConnection());
            command.Parameters.Add("@uLogin", MySqlDbType.VarChar).Value = LoginUser;

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[4]);
                for (int i = 0; i < 4; i++)
                {
                    data[data.Count - 1][i] = reader[i].ToString();
                }
            }

            reader.Close();

            db.closeConnection();


            for (int i = data.Count; i > 0; i--)
            {
                ListViewGroup group = new ListViewGroup(data[i-1][0]);
                listView1.Items.Add(new ListViewItem(data[i - 1][1], group));
                listView1.Items.Add(new ListViewItem(data[i - 1][2], group));
                listView1.Items.Add(new ListViewItem("Контактный номер: "+ data[i - 1][3], group));
                listView1.Groups.Add(group);
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HelpForm help = new HelpForm(LoginUser);
            help.Show();
        }
    }
}
