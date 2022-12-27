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
    public partial class TradeForm : Form
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
        public TradeForm(string login)
        {
            LoginUser = login;
            InitializeComponent();
        }
        Point point;
        private void TradeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void TradeForm_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }
        
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (wts_textbox.Text == "")
            {
                MessageBox.Show("Вы должны добавить описание к вашему товару.");
                return;
            }
            if (wtb_textbox.Text == "")
            {
                MessageBox.Show("Вы должны добавить описание к товару, который ищете.");
                return;
            }
            if (phone_textbox.Text == "")
            {
                MessageBox.Show("Вы должны добавить свой номер телефона.");
                return;
            }
            else if (phone_textbox.Text.Length != 11)
            {
                MessageBox.Show("Неверный введен номер телефона");
                return;
            }
            DataBase db = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `orders` (`creator`, `wts`, `wtb`, `contacts`) VALUES (@uLogin, @WTS, @WTB, @uContacts)", db.getConnection());

            command.Parameters.Add("@uLogin", MySqlDbType.VarChar).Value = LoginUser;
            command.Parameters.Add("@WTS", MySqlDbType.VarChar).Value = wts_textbox.Text;
            command.Parameters.Add("@WTB", MySqlDbType.VarChar).Value = wtb_textbox.Text;
            command.Parameters.Add("@uContacts", MySqlDbType.Int32).Value = phone_textbox.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Объявление успешно создано");
                this.Hide();
                MainPage mainPage = new MainPage(LoginUser);
                mainPage.Show();
            }
            else MessageBox.Show("Ошибка");


            db.closeConnection();
        }

        private void LKbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLK lkPage = new FormLK(loginUser);
            lkPage.Show();
        }

        private void returnToMain_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage mainPage = new MainPage(loginUser);
            mainPage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            HelpForm help = new HelpForm(LoginUser);
            help.Show();
        }
    }
}
