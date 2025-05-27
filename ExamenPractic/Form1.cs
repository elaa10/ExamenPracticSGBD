using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ExamenPractic
{
    public partial class Form1 : Form
    {
        SqlDataAdapter daParent, daChild;
        DataSet ds = new DataSet();
        BindingSource bsParent = new BindingSource();
        BindingSource bsChild = new BindingSource();
        string connectionString = @"Server=DELFI\SQLEXPRESS;Database=Problema1;Integrated Security=true; TrustServerCertificate=true;";

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btnAddChild.Click += btnAddChild_Click;
            btnDeleteChild.Click += btnDeleteChild_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnUpdateChild.Click += btnUpdateChild_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    daParent = new SqlDataAdapter("SELECT * FROM Cofetarii;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Briose;", con);

                    daParent.Fill(ds, "Cofetarii");
                    daChild.Fill(ds, "Briose");

                    DataColumn pkColumn = ds.Tables["Cofetarii"].Columns["cod_cofetarie"];
                    DataColumn fkColumn = ds.Tables["Briose"].Columns["cod_cofetarie"];

                    DataRelation relation = new DataRelation("FK_Cofetarii_Briose", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Cofetarii"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Cofetarii_Briose";
                    dataGridViewChild.DataSource = bsChild;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la incarcarea datelor: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    daParent.SelectCommand.Connection = con;
                    daChild.SelectCommand.Connection = con;

                    if (ds.Tables.Contains("Briose")) ds.Tables["Briose"].Clear();
                    if (ds.Tables.Contains("Cofetarii")) ds.Tables["Cofetarii"].Clear();

                    daParent.Fill(ds, "Cofetarii");
                    daChild.Fill(ds, "Briose");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la reincarcarea datelor: " + ex.Message);
            }
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeBriosa.Text))
                {
                    MessageBox.Show("Numele brioșei nu poate fi gol.");
                    return;
                }

                if (numPret.Value <= 0)
                {
                    MessageBox.Show("Prețul trebuie să fie mai mare decât 0.");
                    return;
                }

                DataRow selectedRow = ((DataRowView)bsParent.Current).Row;
                int codCofetarie = Convert.ToInt32(selectedRow["cod_cofetarie"]);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Briose (cod_cofetarie, nume_briosa, descriere, pret) " +
                        "VALUES (@cod_cofetarie, @nume_briosa, @descriere, @pret)", con);

                    cmd.Parameters.AddWithValue("@cod_cofetarie", codCofetarie);
                    cmd.Parameters.AddWithValue("@nume_briosa", txtNumeBriosa.Text);
                    cmd.Parameters.AddWithValue("@descriere", txtDescriere.Text);
                    cmd.Parameters.AddWithValue("@pret", Convert.ToDouble(numPret.Value));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Brioșa a fost adăugată cu succes!");
                    btnRefresh_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea brioșei: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    if (selectedRow["cod_briosa"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codBriosa = Convert.ToInt32(selectedRow["cod_briosa"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Briose WHERE cod_briosa = @cod_briosa", con);
                        cmd.Parameters.AddWithValue("@cod_briosa", codBriosa);
                        cmd.ExecuteNonQuery();

                        btnRefresh_Click(sender, e);
                        MessageBox.Show("Brioșa a fost ștearsă cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio brioșă pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea brioșei: " + ex.Message);
            }
        }

        private void btnUpdateChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        DataRow selectedRow = ((DataRowView)bsChild.Current).Row;

                        if (selectedRow["cod_briosa"] == DBNull.Value)
                        {
                            MessageBox.Show("ID invalid.");
                            return;
                        }

                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Briose SET cod_cofetarie = @cod_cofetarie, nume_briosa = @nume_briosa, descriere = @descriere, pret = @pret " +
                            "WHERE cod_briosa = @cod_briosa", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_cofetarie", Convert.ToInt32(selectedRow["cod_cofetarie"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_briosa", Convert.ToInt32(selectedRow["cod_briosa"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume_briosa", selectedRow["nume_briosa"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@descriere", selectedRow["descriere"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@pret", Convert.ToDouble(selectedRow["pret"]));

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Brioșa a fost actualizată cu succes!");
                        }

                        btnRefresh_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio brioșă pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea brioșei: " + ex.Message);
            }
        }
    }
}

