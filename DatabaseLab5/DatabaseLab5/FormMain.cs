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
using Unity;

namespace DatabaseLab5
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly UserLogic logic;
        public FormMain(UserLogic userLogic)
        {
            logic = userLogic;
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);

                if (list != null && Program.User != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSignIn>();
            form.ShowDialog();
        }

        private void зарегатьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAuthorization>();
            form.ShowDialog();
        }

        private void справочникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.User != null)
                {
                    var form = Container.Resolve<FormFilms>();
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Куда лезешь! Пока не зарегистрируешься не пущу =)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void актёрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.User != null)
                {
                    var form = Container.Resolve<FormActors>();
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Куда лезешь! Пока не зарегистрируешься не пущу =)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void отчётыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.User != null)
                {
                    var form = Container.Resolve<FormSessions>();
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Куда лезешь! Пока не зарегистрируешься не пущу =)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
