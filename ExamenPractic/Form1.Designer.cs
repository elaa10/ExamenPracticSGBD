namespace ExamenPractic
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridViewParent = new DataGridView();
            dataGridViewChild = new DataGridView();
            txtNumePremiu = new TextBox();
            txtAn = new TextBox();
            numSuma = new NumericUpDown();
            btnAddChild = new Button();
            btnDeleteChild = new Button();
            btnUpdateChild = new Button();
            btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)dataGridViewParent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewChild).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSuma).BeginInit();
            SuspendLayout();

            // 
            // dataGridViewParent
            // 
            dataGridViewParent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewParent.Location = new Point(38, 103);
            dataGridViewParent.Name = "dataGridViewParent";
            dataGridViewParent.RowHeadersWidth = 51;
            dataGridViewParent.Size = new Size(300, 188);
            dataGridViewParent.TabIndex = 0;

            // 
            // dataGridViewChild
            // 
            dataGridViewChild.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewChild.Location = new Point(433, 103);
            dataGridViewChild.Name = "dataGridViewChild";
            dataGridViewChild.RowHeadersWidth = 51;
            dataGridViewChild.Size = new Size(300, 188);
            dataGridViewChild.TabIndex = 1;

            // 
            // txtNumePremiu
            // 
            txtNumePremiu.Location = new Point(63, 321);
            txtNumePremiu.Name = "txtNumePremiu";
            txtNumePremiu.Size = new Size(125, 27);
            txtNumePremiu.TabIndex = 2;
            txtNumePremiu.PlaceholderText = "Nume Premiu";

            // 
            // txtAn
            // 
            txtAn.Location = new Point(63, 366);
            txtAn.Name = "txtAn";
            txtAn.Size = new Size(125, 27);
            txtAn.TabIndex = 3;
            txtAn.PlaceholderText = "An decernare";

            // 
            // numSuma
            // 
            numSuma.DecimalPlaces = 2;
            numSuma.Location = new Point(466, 321);
            numSuma.Maximum = 1000;
            numSuma.Name = "numSuma";
            numSuma.Size = new Size(150, 27);
            numSuma.TabIndex = 4;

            // 
            // btnAddChild
            // 
            btnAddChild.Location = new Point(185, 42);
            btnAddChild.Name = "btnAddChild";
            btnAddChild.Size = new Size(94, 29);
            btnAddChild.TabIndex = 5;
            btnAddChild.Text = "Add";
            btnAddChild.UseVisualStyleBackColor = true;

            // 
            // btnDeleteChild
            // 
            btnDeleteChild.Location = new Point(433, 42);
            btnDeleteChild.Name = "btnDeleteChild";
            btnDeleteChild.Size = new Size(94, 29);
            btnDeleteChild.TabIndex = 6;
            btnDeleteChild.Text = "Delete";
            btnDeleteChild.UseVisualStyleBackColor = true;

            // 
            // btnUpdateChild
            // 
            btnUpdateChild.Location = new Point(600, 42);
            btnUpdateChild.Name = "btnUpdateChild";
            btnUpdateChild.Size = new Size(94, 29);
            btnUpdateChild.TabIndex = 7;
            btnUpdateChild.Text = "Update";
            btnUpdateChild.UseVisualStyleBackColor = true;

            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(38, 42);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRefresh);
            Controls.Add(btnUpdateChild);
            Controls.Add(btnDeleteChild);
            Controls.Add(btnAddChild);
            Controls.Add(numSuma);
            Controls.Add(txtAn);
            Controls.Add(txtNumePremiu);
            Controls.Add(dataGridViewChild);
            Controls.Add(dataGridViewParent);
            Name = "Form1";
            Text = "Examen Practic SGBD";

            ((System.ComponentModel.ISupportInitialize)dataGridViewParent).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewChild).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSuma).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewParent;
        private DataGridView dataGridViewChild;
        private TextBox txtNumePremiu;
        private TextBox txtAn;
        private NumericUpDown numSuma;
        private Button btnAddChild;
        private Button btnDeleteChild;
        private Button btnUpdateChild;
        private Button btnRefresh;
    }
}
