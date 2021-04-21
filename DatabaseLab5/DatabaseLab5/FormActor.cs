using DatabaseBusinessLogic.BindingModels;
using DatabaseBusinessLogic.BusinessLogics;
using DatabaseBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Unity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseLab5
{
    public partial class FormActor : Form
    {

        public int Id { set { id = value; } }
        private int? id;

        private readonly ActorLogic logic;

        public FormActor(ActorLogic logicА)
        {
            logic = logicА;
            InitializeComponent();
        }

        ActorViewModel view;

        private void FormActor_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    view = logic.Read(new ActorBindingModel { Id = id })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxSurname.Text = view.Surname;
                        textBoxMiddlename.Text = view.MiddleName;                       
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxSurname.Text))
            {
                MessageBox.Show("Заполните фамилию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMiddlename.Text))
            {
                MessageBox.Show("Заполните отчество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new ActorBindingModel
                    {
                        Id = view.Id,
                        Name = textBoxName.Text,
                        Surname = textBoxSurname.Text,
                        MiddleName = textBoxMiddlename.Text,
                    });
                }
                else
                {
                    logic.CreateOrUpdate(new ActorBindingModel
                    {
                        Name = textBoxName.Text,
                        Surname = textBoxSurname.Text,
                        MiddleName = textBoxMiddlename.Text,
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
