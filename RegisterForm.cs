﻿using MySqlConnector;
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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
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

        private void RegisterButton_Click(object sender, EventArgs e)  //Осуществляет проверку данных, введеных пользователем и заносит их в бд
        {
            if (DataIsCorrect(loginField.Text, passField.Text, userAgeField.Text)) return;
            DataBase db = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `age`) VALUES (@uLogin, @uPass, @uAge)", db.getConnection());

            command.Parameters.Add("@uLogin", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@uPass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@uAge", MySqlDbType.Int32).Value = userAgeField.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно зарегестрирован");
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
            else MessageBox.Show("Ошибка");


            db.closeConnection();
        }

        public static bool checkUser(string login) // Проверяет, существует ли такой пользователь в бд
        {
            DataBase db = new DataBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uLogin", db.getConnection());
            command.Parameters.Add("@uLogin", MySqlDbType.VarChar).Value = login;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже зарегистрирован, введите другой");
                return true;
            }
            else return false;
        }
        public static bool DataIsCorrect(string login, string pass, string age)
        {
            if (login == "")
            {
                MessageBox.Show("Поле логин не может быть пустым!");
                return true;
            }
            if (pass == "")
            {
                MessageBox.Show("Поле пароль не может быть пустым!");
                return true;
            }
            if (int.Parse(age) < 18)
            {
                MessageBox.Show("Для регистрации в системе вы должны быть совершеннолетним");
                return true;
            }
            if (checkUser(login))
            {
                return true;
            }
            return false;
        }
    }
}
