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
    public partial class FormSession : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        public int UserId
        {
            get { return Convert.ToInt32(comboBoxUser.SelectedValue); }
            set { comboBoxUser.SelectedValue = value; }
        }

        public int FilmId
        {
            get { return Convert.ToInt32(comboBoxFilm.SelectedValue); }
            set { comboBoxFilm.SelectedValue = value; }
        }

        public string FilmName
        {
            get { return comboBoxFilm.SelectedItem.ToString(); }
            set { comboBoxFilm.SelectedItem = value; }
        }

        private readonly FilmLogic _filmlogic;

        private readonly UserLogic _userlogic;

        private readonly SessionLogic _sessionlogic;

        public FormSession(FilmLogic filmLogic, UserLogic userLogic, SessionLogic sessionLogic)
        {
            _filmlogic = filmLogic;
            _userlogic = userLogic;
            _sessionlogic = sessionLogic;
            InitializeComponent();
        }

        SessionViewModel view;

        private void FormSession_Load(object sender, EventArgs e)
        {
            List<UserViewModel> listUsers = _userlogic.Read(null);
            if (listUsers != null)
            {
                comboBoxUser.DisplayMember = "Name";
                comboBoxUser.ValueMember = "Id";
                comboBoxUser.DataSource = listUsers;
                comboBoxUser.SelectedItem = null;
            }

            List<FilmViewModel> listFilms = _filmlogic.Read(null);
            if (listFilms != null)
            {
                comboBoxFilm.DisplayMember = "Name";
                comboBoxFilm.ValueMember = "Id";
                comboBoxFilm.DataSource = listFilms;
                comboBoxFilm.SelectedItem = null;
            }

            if (id.HasValue)
            {
                try
                {
                    view = _sessionlogic.Read(new SessionBindingModel { Id = id })?[0];

                    if (view != null)
                    {                        
                        comboBoxUser.SelectedItem = listUsers.FirstOrDefault(rec => rec.Id == view.UserId);
                        comboBoxFilm.SelectedItem = listFilms.FirstOrDefault(rec => rec.Id == view.FilmId);
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
            if (comboBoxFilm.SelectedValue == null)
            {
                MessageBox.Show("Выберите фильм", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxUser.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           
            try
            {
                if (view != null)
                {
                    _sessionlogic.CreateOrUpdate(new SessionBindingModel
                    {
                        Id = view.Id,
                        UserId = UserId,
                        FilmId = FilmId,
                        FilmName = FilmName,
                        StartOfWatchingMovie = view.StartOfWatchingMovie,
                    });
                }
                else
                {
                    _sessionlogic.CreateOrUpdate(new SessionBindingModel
                    {
                        UserId = UserId,
                        FilmId = FilmId,
                        FilmName = FilmName,
                        StartOfWatchingMovie = DateTime.Now,
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
