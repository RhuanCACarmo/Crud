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
using MySqlX.XDevAPI.Relational;
using Mysqlx.Resultset;

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
            //Configuração inicial do ListView para exibição dos dados dos clientes
            lstCliente.View = View.Details;         //Define a visualização como "Detalhes"
            lstCliente.LabelEdit = true;           //Permite editar os títulos das colunas
            lstCliente.AllowColumnReorder = true; //Permite reordenar as colunas
            lstCliente.FullRowSelect = true;     //Seleciona a linha inteira ao clicar
            lstCliente.GridLines = true;        //Exibe as Linhas de grade no listview

            //Definindo as colunas do ListView
            lstCliente.Columns.Add("Codigo", 100, HorizontalAlignment.Left); //Coluna de código
            lstCliente.Columns.Add("Nome Completo", 200, HorizontalAlignment.Left); //Coluna de Nome Completo
            lstCliente.Columns.Add("Nome Social", 200, HorizontalAlignment.Left); //Coluna de nome social
            lstCliente.Columns.Add("E-mail", 200, HorizontalAlignment.Left); //Coluna de e-mail
            lstCliente.Columns.Add("CPF", 200, HorizontalAlignment.Left); //Coluna de CPF

            //Carrega a lista de clientes
            carregar_clientes();
        }
        private void carregar_clientes_com_query(string query)
        {
            try
            {
                //Cria a conexão com o banco de dados
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Executa a consulta SQl fornecida
                MySqlCommand cmd = new MySqlCommand(query, Conexao);

                //Se a consulta contém o parâmetro @q, aadiciona o valor da caixa e pesquisa
                if (query.Contains("@q"))
                {
                    cmd.Parameters.AddWithValue("@q", "%" + txtbuscar.Text + "%");
                }

                //Executa o comando e obtém os resultados
                MySqlDataReader reader = cmd.ExecuteReader();

                //limpa os itens existentes no listView antes de adicionar os novos
                lstCliente.Items.Clear();

                //Preenche o listview com os dados dos clientes
                while (reader.Read())
                {
                    //Cria uma linha para cada cliente com os dados retornados da consulta
                    string[] row =
                    {
                        Convert.ToString(reader.GetInt32(0)),//Codigo
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    };

                    //Adiciona a linha ao ListView
                    lstCliente.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {
                //Trata erros relacionados ao MySQL
                MessageBox.Show("Erro " + ex.Number + " Ocorreu: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //Trata os erros relacionados ao ListView
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

        //Método para carregar todos os clientes no ListView (usando uma consulta sem parâmetros)

        private void carregar_clientes()
        {
            string query = "SELECT * FROM dadosdecliente ORDER BY codigo DESC";
            carregar_clientes_com_query(query);
        }
        
        //Validação Regex
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

                // Limpa os campos após o sucesso
                txtNomeCompleto.Text = string.Empty;
                txtNomeSocial.Text = " ";
                txtEmail.Text = " ";
                txtCPF.Text = " ";

                //Recarregar os clientes na ListView
                carregar_clientes();

                //Muda para a aba de pesquisa
                tbCadastro.SelectedIndex = 1;
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM dadosdecliente WHERE nomecompleto LIKE @q OR nomesocial LIKE @q ORDER BY codigo DESC";
            carregar_clientes_com_query(query);
        }
    }
}
