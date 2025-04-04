namespace cadastrodeclientes
{
    partial class frmCadastroClientes
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCadastroClientes = new System.Windows.Forms.Label();
            this.tbCadastro = new System.Windows.Forms.TabControl();
            this.tbDadosClientes = new System.Windows.Forms.TabPage();
            this.txtCPF = new System.Windows.Forms.MaskedTextBox();
            this.lblCPF = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtNomeSocial = new System.Windows.Forms.TextBox();
            this.lblNomeSocial = new System.Windows.Forms.Label();
            this.txtNomeCompleto = new System.Windows.Forms.TextBox();
            this.lblNomeCompleto = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tbCadastro.SuspendLayout();
            this.tbDadosClientes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.lblCadastroClientes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 100);
            this.panel1.TabIndex = 0;
            // 
            // lblCadastroClientes
            // 
            this.lblCadastroClientes.AutoSize = true;
            this.lblCadastroClientes.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCadastroClientes.ForeColor = System.Drawing.Color.White;
            this.lblCadastroClientes.Location = new System.Drawing.Point(43, 37);
            this.lblCadastroClientes.Name = "lblCadastroClientes";
            this.lblCadastroClientes.Size = new System.Drawing.Size(221, 25);
            this.lblCadastroClientes.TabIndex = 0;
            this.lblCadastroClientes.Text = "Cadastro de clientes";
            // 
            // tbCadastro
            // 
            this.tbCadastro.Controls.Add(this.tbDadosClientes);
            this.tbCadastro.Controls.Add(this.tabPage2);
            this.tbCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCadastro.Location = new System.Drawing.Point(12, 106);
            this.tbCadastro.Name = "tbCadastro";
            this.tbCadastro.SelectedIndex = 0;
            this.tbCadastro.Size = new System.Drawing.Size(776, 273);
            this.tbCadastro.TabIndex = 1;
            // 
            // tbDadosClientes
            // 
            this.tbDadosClientes.Controls.Add(this.txtCPF);
            this.tbDadosClientes.Controls.Add(this.lblCPF);
            this.tbDadosClientes.Controls.Add(this.txtEmail);
            this.tbDadosClientes.Controls.Add(this.lblEmail);
            this.tbDadosClientes.Controls.Add(this.txtNomeSocial);
            this.tbDadosClientes.Controls.Add(this.lblNomeSocial);
            this.tbDadosClientes.Controls.Add(this.txtNomeCompleto);
            this.tbDadosClientes.Controls.Add(this.lblNomeCompleto);
            this.tbDadosClientes.Location = new System.Drawing.Point(4, 29);
            this.tbDadosClientes.Name = "tbDadosClientes";
            this.tbDadosClientes.Padding = new System.Windows.Forms.Padding(3);
            this.tbDadosClientes.Size = new System.Drawing.Size(768, 240);
            this.tbDadosClientes.TabIndex = 0;
            this.tbDadosClientes.Text = "Dados de clientes";
            this.tbDadosClientes.UseVisualStyleBackColor = true;
            // 
            // txtCPF
            // 
            this.txtCPF.Culture = new System.Globalization.CultureInfo("");
            this.txtCPF.Location = new System.Drawing.Point(159, 183);
            this.txtCPF.Mask = "###.###.###-##";
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(124, 26);
            this.txtCPF.TabIndex = 7;
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCPF.Location = new System.Drawing.Point(30, 186);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(40, 20);
            this.lblCPF.TabIndex = 6;
            this.lblCPF.Text = "CPF";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(159, 141);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(429, 26);
            this.txtEmail.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblEmail.Location = new System.Drawing.Point(30, 147);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(53, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "E-Mail";
            // 
            // txtNomeSocial
            // 
            this.txtNomeSocial.Location = new System.Drawing.Point(159, 95);
            this.txtNomeSocial.Name = "txtNomeSocial";
            this.txtNomeSocial.Size = new System.Drawing.Size(429, 26);
            this.txtNomeSocial.TabIndex = 3;
            // 
            // lblNomeSocial
            // 
            this.lblNomeSocial.AutoSize = true;
            this.lblNomeSocial.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNomeSocial.Location = new System.Drawing.Point(30, 101);
            this.lblNomeSocial.Name = "lblNomeSocial";
            this.lblNomeSocial.Size = new System.Drawing.Size(98, 20);
            this.lblNomeSocial.TabIndex = 2;
            this.lblNomeSocial.Text = "Nome Social";
            // 
            // txtNomeCompleto
            // 
            this.txtNomeCompleto.Location = new System.Drawing.Point(159, 52);
            this.txtNomeCompleto.Name = "txtNomeCompleto";
            this.txtNomeCompleto.Size = new System.Drawing.Size(429, 26);
            this.txtNomeCompleto.TabIndex = 1;
            // 
            // lblNomeCompleto
            // 
            this.lblNomeCompleto.AutoSize = true;
            this.lblNomeCompleto.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNomeCompleto.Location = new System.Drawing.Point(30, 55);
            this.lblNomeCompleto.Name = "lblNomeCompleto";
            this.lblNomeCompleto.Size = new System.Drawing.Size(123, 20);
            this.lblNomeCompleto.TabIndex = 0;
            this.lblNomeCompleto.Text = "Nome Completo";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 240);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consulta";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSalvar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(39, 397);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(117, 37);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // frmCadastroClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.tbCadastro);
            this.Controls.Add(this.panel1);
            this.Name = "frmCadastroClientes";
            this.Text = "\'";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbCadastro.ResumeLayout(false);
            this.tbDadosClientes.ResumeLayout(false);
            this.tbDadosClientes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCadastroClientes;
        private System.Windows.Forms.TabControl tbCadastro;
        private System.Windows.Forms.TabPage tbDadosClientes;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblNomeCompleto;
        private System.Windows.Forms.TextBox txtNomeSocial;
        private System.Windows.Forms.Label lblNomeSocial;
        private System.Windows.Forms.TextBox txtNomeCompleto;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.MaskedTextBox txtCPF;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.Button btnSalvar;
    }
}

