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

        private int? codigo_cliente = null; //

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
            lstCliente.Columns.Add("Código", 100, HorizontalAlignment.Left); //Coluna de código
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

                if (codigo_cliente == null)
                {
                    //Insert CREATE
                    cmd.CommandText = "INSERT INTO dadosdecliente(nomecompleto,nomesocial,email,cpf)" +
                    "VALUES (@nomecompleto,@nomesocial,@email,@cpf)";

                    //Adiciona parâmetros com os dados do formulário
                    cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text.Trim());
                    cmd.Parameters.AddWithValue("@nomesocial", txtNomeSocial.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cpf", cpf);

                    //Executa o comando de inserção no banco
                    cmd.ExecuteNonQuery();

                    //Mensagem de sucesso
                    MessageBox.Show("Contato inserido com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //UPDATE
                    cmd.CommandText = $"UPDATE `dadosdecliente` SET " +
                                     "nomecompleto = @nomecompleto, " +
                                     "nomesocial = @nomesocial, " +
                                     "email = @email, " +
                                     "cpf = @cpf " +
                                     "WHERE codigo = @codigo";

                    cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text.Trim());
                    cmd.Parameters.AddWithValue("@nomesocial", txtNomeSocial.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@codigo", codigo_cliente);

                    //Executa o comando de alteração no banco
                    cmd.ExecuteNonQuery();

                    //MessageBox de sucesso para dados atualizados
                    MessageBox.Show($"Os dados com o código {codigo_cliente} foram alterados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                codigo_cliente = null;

                // Limpa os campos após o sucesso
                txtNomeCompleto.Text = string.Empty;
                txtNomeSocial.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtCPF.Text = string.Empty;

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

        private void lstCliente_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection clientesSelecionados = lstCliente.SelectedItems;

                // Verifica se há itens selecionados
                if (clientesSelecionados.Count == 0)
                    return;

                // Pega o primeiro item selecionado
                ListViewItem item = clientesSelecionados[0];

                // Verifica se há subitens suficientes
                if (item.SubItems.Count < 5)
                {
                    MessageBox.Show("Dados do cliente incompletos!", "Aviso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conversão segura do código do cliente
                if (int.TryParse(item.SubItems[0].Text, out int codigo))
                {
                    codigo_cliente = codigo;

                    // Preenche os campos do formulário
                    txtNomeCompleto.Text = item.SubItems[1].Text;
                    txtNomeSocial.Text = item.SubItems[2].Text;
                    txtEmail.Text = item.SubItems[3].Text;
                    txtCPF.Text = item.SubItems[4].Text;

                    // Debug (opcional)
                    Console.WriteLine($"Cliente selecionado: ID {codigo}");
                }
                else
                {
                    MessageBox.Show("Código do cliente inválido!", "Erro",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar cliente: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Muda para a aba de cliente
            tbCadastro.SelectedIndex = 0;
        }

        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            codigo_cliente = null;

            // Limpa os campos após o sucesso
            txtNomeCompleto.Text = string.Empty;
            txtNomeSocial.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCPF.Text = string.Empty;

            txtNomeCompleto.Focus();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cliente Excluido");
        }
    }
}
