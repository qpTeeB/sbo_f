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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e) //Метод закрытия приложения
        {
            Application.Exit();
        }
        private void CloseButton_MouseEnter(object sender, EventArgs e) //Метод для события наведения мыши на кнопку закрытия
        {
            closeButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e) //Метод для события наведения мыши на кнопку закрытия
        {
            closeButton.ForeColor = Color.White;
        }
        Point point;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e) //Метод, добавляющий возможность перемещать окно по рабочему столу
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e) //Метод, добавляющий возможность перемещать окно по рабочему столу
        {
            point = new Point(e.X, e.Y);
        }

        private void LoginButton_Click(object sender, EventArgs e) //Метод нажатия на кнопку логина, осуществляет переход на форму авторизации
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void RegisterButton_Click(object sender, EventArgs e) //Метод нажатия на кнопку регистрации, осуществляет переход на форму регистрации
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
