using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using DatabaseBusinessLogic.ViewModels;
using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.BusinessLogics;
using System.Windows.Forms;

namespace DatabaseLab5
{
    public partial class FormSignIn : Form
    {
        private readonly UserLogic _userLogic;

        public FormSignIn(UserLogic userLogic)
        {
            _userLogic = userLogic;
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните поле Login", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните поле пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var user = _userLogic.Read(new UserBindingModel { Name = textBoxLogin.Text, Password = textBoxPassword.Text })?[0];
            if (user == null)
            {
                MessageBox.Show("Неверный Login или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Program.User = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
