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
using Microsoft.EntityFrameworkCore;

namespace DatabaseLab5
{
    public partial class FormAuthorization : Form
    {
        private readonly UserLogic _userLogic;

        private readonly PaymentLogic logic;

        public int Id
        {
            get { return Convert.ToInt32(comboBoxPayment.SelectedValue); }
            set { comboBoxPayment.SelectedValue = value; }
        }

        public FormAuthorization(PaymentLogic paymentLogic, UserLogic userLogic)
        {
            logic = paymentLogic;
            _userLogic = userLogic;
            InitializeComponent();

            List<PaymentViewModel>  list = logic.Read(null);

            if (list != null)
            {
                comboBoxPayment.DisplayMember = "DurationOfPayment";
                comboBoxPayment.ValueMember = "Id";
                comboBoxPayment.DataSource = list;
                comboBoxPayment.SelectedItem = null;
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните Login", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните поле Пароль", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                UserBindingModel model = new UserBindingModel
                {
                    Name = textBoxLogin.Text,
                    Password = textBoxPassword.Text,
                    PaymentId = Id,
                };
                _userLogic.CreateOrUpdate(model);
                Program.User = _userLogic.Read(model)?[0];
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DbUpdateException exe)
            {
                MessageBox.Show(exe?.InnerException?.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
