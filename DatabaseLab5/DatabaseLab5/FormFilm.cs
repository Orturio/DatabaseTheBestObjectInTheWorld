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
    public partial class FormFilm : Form
    {
        public int Id { set { id = value; } }

        public int AgeLimitId 
        {
            get { return Convert.ToInt32(comboBoxAgeLimit.SelectedValue); }
            set { comboBoxAgeLimit.SelectedValue = value; }
        }

        private int? id;
        private readonly FilmLogic logic;

        private readonly AgeLimitLogic logicA;
        public FormFilm(FilmLogic filmLogic, AgeLimitLogic logicAgeL)
        {
            logicA = logicAgeL;
            logic = filmLogic;
            InitializeComponent();
        }

        FilmViewModel view;

        private void FormFilm_Load(object sender, EventArgs e)
        {
            List<AgeLimitViewModel> list = logicA.Read(null);
            if (list != null)
            {
                comboBoxAgeLimit.DisplayMember = "Name";
                comboBoxAgeLimit.ValueMember = "Id";
                comboBoxAgeLimit.DataSource = list;
                comboBoxAgeLimit.SelectedItem = null;
            }

            if (id.HasValue)
            {
                try
                {
                    view = logic.Read(new FilmBindingModel { Id = id })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxDescription.Text = view.Description;
                        textBoxRating.Text = view.Rating.ToString();
                        comboBoxAgeLimit.SelectedItem = list.FirstOrDefault(rec => rec.Id == view.AgeLimitId);
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
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxDescription.Text))
            {
                MessageBox.Show("Заполните описание", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRating.Text))
            {
                MessageBox.Show("Заполните рэйтинг", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new FilmBindingModel
                    {
                        Id = view.Id,
                        AgeLimitId = AgeLimitId,
                        Name = textBoxName.Text,
                        Description = textBoxDescription.Text,
                        Rating = Convert.ToInt32(textBoxRating.Text)
                    });
                }
                else
                {
                    logic.CreateOrUpdate(new FilmBindingModel
                    {
                        AgeLimitId = AgeLimitId,
                        Name = textBoxName.Text,
                        Description = textBoxDescription.Text,
                        Rating = Convert.ToInt32(textBoxRating.Text)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
