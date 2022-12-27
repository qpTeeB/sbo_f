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

        private void CreateButton_Click(object sender, EventArgs e) //Метод добавления объявления в бд, так же проверяет корректность данных, введных пользователем
        {
            if (DataIsCorrect(wts_textbox.Text, wtb_textbox.Text, phone_textbox.Text)) return;
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
        public static bool DataIsCorrect(string wts, string wtb, string num)
        {
            if (wts == "")
            {
                MessageBox.Show("Вы должны добавить описание к вашему товару.");
                return true;
            }
            if (wtb == "")
            {
                MessageBox.Show("Вы должны добавить описание к товару, который ищете.");
                return true;
            }
            if (num == "")
            {
                MessageBox.Show("Вы должны добавить свой номер телефона.");
                return true;
            }
            else if (num.Length != 11)
            {
                MessageBox.Show("Неверный введен номер телефона");
                return true;
            }
            return false;
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
