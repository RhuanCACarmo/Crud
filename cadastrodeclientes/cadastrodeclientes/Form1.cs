using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace cadastrodeclientes
{
    public partial class frmCadastroClientes: Form
    {
        //Conexão com o banco de dados SQL
        MySqlConnection Conexao;
        string data_source = "datasource =localhost; username=root;password=;database=db_cadastro";

        public frmCadastroClientes()
        {
            InitializeComponent();
        }

        private bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private bool isValidCPFLegth(string cpf)
        {
            //Remover quaisquer caracteres não numéricos (como pontos e traços)
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if(cpf.Length !=11 || !cpf.All(char.IsDigit))
            {
                return false;
            }
            return true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validação de campos obrigatórios
                if (string.IsNullOrEmpty(txtNomeCompleto.Text.Trim()) ||
                    string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                    string.IsNullOrEmpty(txtCPF.Text.Trim()))
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cpf = txtCPF.Text.Trim();
                if (!isValidCPFLegth(cpf))
                {
                    MessageBox.Show("CPF inválido. Certifique-se de que o CPF tenha 11 dígitos numéricos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validação do E-mail.   
                string email = txtEmail.Text.Trim();
                if (!isValidEmail(email))
                {
                    MessageBox.Show("Email Inválido. Certifique-se de preencheu corretamente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Cria conexão com o banco de dados.
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //MessageBox.Show("Conexão aberta com sucesso.");

                //Comando SQL para inserir um novo cliente no banco de dados
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Conexao
                };

                cmd.Prepare();

                cmd.CommandText = "INSERT INTO dadosdecliente(nomecompleto,nomesocial,email,cpf)" +
                    "VALUES (@nomecompleto,@nomesocial,@email,@cpf)";

                //Adiciona parâmetros com os dados do formulário
                cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text.Trim());
                cmd.Parameters.AddWithValue("@nomesocial", txtNomeSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@cpf",cpf);

                //Executa o comando de inserção no banco
                cmd.ExecuteNonQuery();

                //Mensagem de sucesso
                MessageBox.Show("Contato inserido com sucesso.","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }

            catch (MySqlException ex)
            {
                //Trata erros relacionados ao MySQL
                MessageBox.Show("Erro " + ex.Number + " Ocorreu: " + ex.Message,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                //Trata outro tipos de erros.
                MessageBox.Show("Ocorreu: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                //Garante que a conexão com o banco de dados será fechada, mesmo se ocorrer um erro.
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();

                   // MessageBox.Show("Conexão fechada com sucesso.");
                }
            }
        }
    }
}
