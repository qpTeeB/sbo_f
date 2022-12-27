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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.passField.Size = new Size(this.passField.Size.Width,this.loginField.Size.Height);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }
        Point point;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X,e.Y);
        }

        private void ButtonLogin_Click(object sender, EventArgs e) //Осуществляет сравнение данных, которые ввел пользователь и которые находятся в бд
        {
            String loginUser = loginField.Text;
            String passUser = passField.Text;

            if (CheckUserData(loginUser, passUser)) return;

            DataBase db = new DataBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uLogin AND `password` = @uPass",db.getConnection());
            command.Parameters.Add("@uLogin", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uPass", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь авторизован");
                this.Hide();
                MainPage mainPage = new MainPage(loginUser);
                mainPage.Show();
            }
            else MessageBox.Show("Неверный логин или пароль");
        }
        public static bool CheckUserData(string login, string pass)
        {
            if(login == "")
            {
                MessageBox.Show("Поле логина не может быть пустым!");
                return true;
            }
            if(pass == "")
            {
                MessageBox.Show("Поле пароля не может быть пустым!");
                return true;
            }
            return false;
        }
    }
}
