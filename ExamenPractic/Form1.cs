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
        string connectionString = @"Server=DELFI\SQLEXPRESS;Database=ExamenPractic;Integrated Security=true; TrustServerCertificate=true;";

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

                    daParent = new SqlDataAdapter("SELECT * FROM Echipe;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Premii;", con);

                    daParent.Fill(ds, "Echipe");
                    daChild.Fill(ds, "Premii");

                    DataColumn pkColumn = ds.Tables["Echipe"].Columns["cod_echipa"];
                    DataColumn fkColumn = ds.Tables["Premii"].Columns["cod_echipa"];

                    DataRelation relation = new DataRelation("FK_Echipe_Premii", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Echipe"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Echipe_Premii";
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

                    if (ds.Tables.Contains("Premii")) ds.Tables["Premii"].Clear();
                    if (ds.Tables.Contains("Echipe")) ds.Tables["Echipe"].Clear();

                    daParent.Fill(ds, "Echipe");
                    daChild.Fill(ds, "Premii");
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
                if (string.IsNullOrWhiteSpace(txtNumePremiu.Text))
                {
                    MessageBox.Show("Numele premiului nu poate fi gol.");
                    return;
                }

                if (numSuma.Value < 0)
                {
                    MessageBox.Show("Suma trebuie să fie mai mare decât 0.");
                    return;
                }

                DataRow selectedRow = ((DataRowView)bsParent.Current).Row;
                int codEchipa = Convert.ToInt32(selectedRow["cod_echipa"]);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Premii (cod_echipa, nume, an_decernare, suma) " +
                        "VALUES (@cod_echipa, @nume, @an_decernare, @suma)", con);

                    cmd.Parameters.AddWithValue("@cod_echipa", codEchipa);
                    cmd.Parameters.AddWithValue("@nume", txtNumePremiu.Text);
                    cmd.Parameters.AddWithValue("@an_decernare", txtAn.Text);
                    cmd.Parameters.AddWithValue("@suma", Convert.ToDouble(numSuma.Value));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Premiul a fost adăugat cu succes!");
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
                    if (selectedRow["cod_premiu"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codPremiu = Convert.ToInt32(selectedRow["cod_premiu"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Premii WHERE cod_premiu = @cod_premiu", con);
                        cmd.Parameters.AddWithValue("@cod_premiu", codPremiu);
                        cmd.ExecuteNonQuery();

                        btnRefresh_Click(sender, e);
                        MessageBox.Show("Premiul a fost ștears cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun premiu pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea premiului: " + ex.Message);
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

                        if (selectedRow["cod_premiu"] == DBNull.Value)
                        {
                            MessageBox.Show("ID invalid.");
                            return;
                        }

                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Premii SET cod_echipa = @cod_echipa, nume = @nume, an_decernare = @an_decernare, suma = @suma " +
                            "WHERE cod_premiu = @cod_premiu", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_echipa", Convert.ToInt32(selectedRow["cod_echipa"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_premiu", Convert.ToInt32(selectedRow["cod_premiu"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume", selectedRow["nume"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@an_decernare", Convert.ToInt32(selectedRow["an_decernare"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@suma", Convert.ToDouble(selectedRow["suma"]));

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Premiul a fost actualizat cu succes!");
                        }

                        btnRefresh_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun premiu pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea premiului: " + ex.Message);
            }
        }
    }
}

