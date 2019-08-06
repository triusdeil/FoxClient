using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FoxClient
{
    public partial class Usuariook : Form
    {
        public Usuariook()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                    con.Open();
                    //procedimientos almacenados
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insertar_usuario", con);
                    //insertar parametros
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", txtRol.Text);
                    //procesar imagenes para memorizarlas y transformarlas para sqlserver
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    Icono.Image.Save(ms, Icono.Image.RawFormat);

                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@Nombre_de_Icono", lblnumeroIcono.Text);
                    cmd.Parameters.AddWithValue("@Estado", "Activo");
                    //@nombres varchar(50),
                    //@login varchar(50),
                    //@password varchar(50),
                    //@Icono image,
                    //@Nombre_de_Icono varchar(MAX),
                    //@Correo varchar(Max),
                    //@Rol varchar(Max)
                    //ejecutar el proceso de almacenado
                    cmd.ExecuteNonQuery();
                    //cerrar la conexion
                    con.Close();
                    //LLama el proceso mostrar
                    mostrar();
                    //ocultar panel de registro
                    panel4.Visible = false;
                }
                catch (Exception ex)
                {
                    //anuncia si hay un error
                    //recuerda que se valida el login en el sql server
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //proceso independiente para mostrar
        private void mostrar()
        {
            try
            {
                //mostrar los datos en datagridview
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                con.Open();

                //declarar el proceso que vamos a llamar
                da = new SqlDataAdapter("mostrar_usuario", con);
                da.Fill(dt);
                //donde vamos a mostrar los datos
                dataListado.DataSource = dt;
                con.Close();

                //ocultar columnas
                dataListado.Columns[1].Visible = false;
                dataListado.Columns[5].Visible = false;
                dataListado.Columns[6].Visible = false;
                dataListado.Columns[7].Visible = false;
                dataListado.Columns[8].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox4.Image;
            lblnumeroIcono.Text = "1";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void LblAnuncioIcono_Click(object sender, EventArgs e)
        {
            panelIcono.Visible = true;
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox5.Image;
            lblnumeroIcono.Text = "2";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox6.Image;
            lblnumeroIcono.Text = "3";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox7.Image;
            lblnumeroIcono.Text = "4";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox8.Image;
            lblnumeroIcono.Text = "5";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox13_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox13.Image;
            lblnumeroIcono.Text = "6";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox14_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox14.Image;
            lblnumeroIcono.Text = "7";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox15_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox15.Image;
            lblnumeroIcono.Text = "8";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox9.Image;
            lblnumeroIcono.Text = "9";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void Usuariook_Load(object sender, EventArgs e)
        {

            panel4.Visible = false;
            panelIcono.Visible = false;
            mostrar();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            lblAnuncioIcono.Visible = true;
            txtNombre.Text = "";
            txtLogin.Text = "";
            txtPassword.Text = "";
            txtCorreo.Text = "";
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;

        }

        private void DataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblid_usuario.Text = dataListado.SelectedCells[1].Value.ToString();

            txtNombre.Text = dataListado.SelectedCells[2].Value.ToString();

            txtLogin.Text = dataListado.SelectedCells[3].Value.ToString();

            txtPassword.Text = dataListado.SelectedCells[4].Value.ToString();

            //para el icono
            Icono.BackgroundImage = null;
            byte[] b = (Byte[])dataListado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            lblAnuncioIcono.Visible = false;

            lblnumeroIcono.Text = dataListado.SelectedCells[6].Value.ToString();

            txtCorreo.Text = dataListado.SelectedCells[7].Value.ToString();

            txtRol.Text = dataListado.SelectedCells[8].Value.ToString();
            panel4.Visible = true;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void BtnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                con.Open();
                //procedimientos almacenados
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("editar_usuario", con);
                //insertar parametros
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", lblid_usuario.Text);
                cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Rol", txtRol.Text);
                //procesar imagenes para memorizarlas y transformarlas para sqlserver
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                Icono.Image.Save(ms, Icono.Image.RawFormat);

                cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                cmd.Parameters.AddWithValue("@Nombre_de_Icono", lblnumeroIcono.Text);
                //@nombres varchar(50),
                //@login varchar(50),
                //@password varchar(50),
                //@Icono image,
                //@Nombre_de_Icono varchar(MAX),
                //@Correo varchar(Max),
                //@Rol varchar(Max)
                //ejecutar el proceso de almacenado
                cmd.ExecuteNonQuery();
                //cerrar la conexion
                con.Close();
                //LLama el proceso mostrar
                mostrar();
                //ocultar panel de registro
                panel4.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            panelIcono.Visible = true;
        }

        private void DataListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == this.dataListado.Columns["Eli"].Index)
            {
                //me lanzaqra un mensaje
                DialogResult result;
                result = MessageBox.Show("Realmente quieres eliminar este usuario?","Eliminando Registros", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach(DataGridViewRow row in dataListado.SelectedRows)
                        {
                            int onekey = Convert.ToInt32(row.Cells["idUsuario"].Value);
                            string usuario = Convert.ToString(row.Cells["Login"].Value);
                            try
                            {
                                try
                                {
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                                    con.Open();
                                    cmd = new SqlCommand("eliminar_usuario",con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@idUsuario", onekey);
                                    cmd.Parameters.AddWithValue("@login", usuario);
                                    cmd.ExecuteNonQuery();
                
                                    con.Close();
                                }catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);   
                            }
                        }
                        mostrar();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
          
        }
    }
    }

