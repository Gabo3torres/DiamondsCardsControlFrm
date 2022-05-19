using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiamondsCardsControlFrm
{
    public partial class LogginFrm : Form
    {
        public SalaVIP CurrentRoom { get; set; }
        public LogginFrm()
        {
            var _salas = new RoomVipRepository();
            InitializeComponent();
            try
            {
                Viproomcmbx.DataSource = _salas.ConsultarSalaVip().ToList();
                Viproomcmbx.DisplayMember = "Nombre";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(IsValidateText(this.Passwordtxt.Text) && IsValidateText(this.Usertxt.Text))
            {
                
                var userRepository = new UserRepository();
                var confirmUser = userRepository.ConsultarUsuario(this.Usertxt.Text, this.Passwordtxt.Text);
                if (confirmUser != null)
                {
                    MessageBox.Show("Bienvenido " + confirmUser.Name, "Mensaje");
                    Form1 form1 = new Form1(Transform(confirmUser));
                    form1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrecta", "Alert", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Por favor llene todos los campos", "Alert", MessageBoxButtons.OK);
            }
        }

        private bool IsValidateText(string sendText) {
            if (!string.IsNullOrEmpty(sendText))
                return true;
            return false;
        }

        private void LogginFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private UserTransversal Transform(User user)
        {
            if(user == null)
            return null;

            return new UserTransversal
            {
                Id = user.Id,
                Name = user.Name,
                RolId = user.RolId,
                ShortName = user.ShortName,
                UserName = user.UserName,
                Sala = CurrentRoom,
                Admin = user.Admin

            };
        }

        private void LogginFrm_Load(object sender, EventArgs e)
        {

        }

        private void Viproomcmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentRoom = Viproomcmbx.SelectedItem as SalaVIP;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to Close this window?", "Close Window", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!WindowState.Equals(FormWindowState.Minimized))
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void Passwordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveBtn_Click(sender, e);
            }
        }
    }
}
