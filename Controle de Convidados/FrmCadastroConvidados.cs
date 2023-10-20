using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Controle_de_Convidados
{
    public partial class FrmCadastroConvidados : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        private MySqlConnection connection;
        private string connectionString;
        
        public List<ResultadoPesquisa> GetConvidadosPorNome(string nomeParaPesquisar)
        {
            List<ResultadoPesquisa> resultados = new List<ResultadoPesquisa>();

            // Use um comando parametrizado para evitar SQL injection.
            string query = "SELECT * FROM tbConvidados WHERE nome = @Nome";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nome", nomeParaPesquisar);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ResultadoPesquisa resultado = new ResultadoPesquisa
                {
                    CodConvidado = reader.GetInt32("codConvidado"),
                    Nome = reader.GetString("nome"),
                    Cpf = reader.GetString("cpf"),
                    Endereco = reader.GetString("endereco"),
                    Bairro = reader.GetString("bairro"),
                    Cep = reader.GetString("Cep"),
                    Email = reader.GetString("E-mail")
                };
                resultados.Add(resultado);
            }

            reader.Close();
            return resultados;
        }

        public FrmCadastroConvidados()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string cpf = maskCPF.Text;
            string endereco = txtEndereco.Text;
            string bairro = txtBairro.Text;
            string cep = maskCep.Text;
            string email = txtemail1.Text;
            string codigo = txtCodigo.Text;

            InserirDadosNoBanco(codigo, email, cpf, endereco, bairro, cep, nome);

            
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPesquisar.Enabled = false;
            btnCadastrar.Enabled = true;
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
            btnPesquisar.Enabled = true;
            btnCadastrar.Enabled = false;

            txtCodigo.Enabled = true;
            txtemail1.Enabled = true;
            maskCPF.Enabled = true;
            txtEndereco.Enabled = true;
            txtBairro.Enabled = true;
            maskCep.Enabled = true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Coletar dados dos campos
            string codigo = txtCodigo.Text;
            string email = txtemail1.Text;
            string cpf = maskCPF.Text;
            string endereco = txtEndereco.Text;
            string bairro = txtBairro.Text;
            string cep = maskCep.Text;

            // Inserir dados no banco de dados
            // Limpar campos
            txtCodigo.Clear();
            txtemail1.Clear();
            maskCPF.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            maskCep.Clear();
        }
        public void InserirDadosNoBanco(string codigo, string email, string cpf, string endereco, string bairro, string cep, string nome)
        {
            string query = "INSERT INTO tbConvidados (nome, cpf, endereco, bairro, Cep, E-mail) " +
                          "VALUES (@Nome, @Cpf, @Endereco, @Bairro, @Cep, @Email)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Cpf", cpf);
            cmd.Parameters.AddWithValue("@Endereco", endereco);
            cmd.Parameters.AddWithValue("@Bairro", bairro);
            cmd.Parameters.AddWithValue("@Cep", cep);
            cmd.Parameters.AddWithValue("@Email", email);

            cmd.ExecuteNonQuery();
        }


        // metodos criados do crud do banco de dados
        // metodo de deletar informações de banco de dados
        public void DeleteConvidado(int codConvidado)
        {
            string query = "DELETE FROM tbConvidados WHERE codConvidado = @CodConvidado";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CodConvidado", codConvidado);

            cmd.ExecuteNonQuery();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, insira um nome para pesquisar.");
                return;
            }

            string nomePesquisado = txtNome.Text;

            
            List<ResultadoPesquisa> resultados = GetConvidadosPorNome(nomePesquisado);

            if (resultados.Count > 0)
            {
                
                btnAtualizar.Enabled = true;
                btnExcluir.Enabled = true;

                
                txtCodigo.Enabled = false;
                txtemail1.Enabled = false;
                maskCPF.Enabled = false;
                txtEndereco.Enabled = false;
                txtBairro.Enabled = false;
                maskCep.Enabled = false;
                btnCadastrar.Enabled = false;
                btnNovo.Enabled = false;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }

        private void FrmCadastroConvidados_Load(object sender, EventArgs e)
        {

            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }
        
        public void DesabilitarCampos1()
        {
            txtCodigo.Enabled = false;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            txtemail1.Enabled = false;
            txtBairro.Enabled = false;

         
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
