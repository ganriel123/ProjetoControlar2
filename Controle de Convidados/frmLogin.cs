using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Controle_de_Convidados
{
    public partial class frmLogin : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
        public void Atuthenticacao()
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUsuario.Text;
            string senhaDigitada = txtSenha.Text;

            string usuarioCorreto = "gabriel almeida";
            string senhaCorreta = "R3boosic@tandis";

            if (usuarioDigitado == usuarioCorreto && senhaDigitada == senhaCorreta)
            {
                MessageBox.Show("Login bem-sucedido! Bem-vindo, Gabriel Almeida.");

                FrmCadastroConvidados abrir = new FrmCadastroConvidados();
                    abrir.Show();
            }
            else
            {
                MessageBox.Show("Credenciais inválidas. Tente novamente.");
            }
        }
    }
}
