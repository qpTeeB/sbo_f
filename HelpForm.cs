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
    public partial class HelpForm : Form
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
        public HelpForm(string login)
        {
            LoginUser = login;
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

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage mainPage = new MainPage(LoginUser);
            mainPage.Show();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            label3.Text = "1. Всплывающее окно с сообщением \"Ошибка\" - возникает при возникновении проблемы \n при подключении к базе данных";
            label4.Text = "2. Всплывающие ошибки связанные с заполнением полей - требуется ввод достоверной информации, \n также вам не стоит оставлять поля пустыми.";
            label5.Text = "Если вашей проблемы нет в списке, вы можете связаться с нашей службой поддержки.";


            contactsLabel.Text = "Контакты для связи со службой поддержки: \nНомер телефона: +79126634874 \nEmail: sbo@ramble.com";
        }
    }
}
