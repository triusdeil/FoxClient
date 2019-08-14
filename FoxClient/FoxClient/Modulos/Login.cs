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
using FoxClient.Modulos;
namespace FoxClient.Modulos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public void DIBUJARusuarios()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
            con.Open();
            //procedimientos almacenados
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from USUARIO2 WHERE Estado='ACTIVO'", con);
            SqlDataReader rdc = cmd.ExecuteReader();

            while (rdc.Read())
            {
                Label b = new Label();
                Panel p1 = new Panel();
                PictureBox I1 = new PictureBox();
                //Propiedades
                //El texto va a tomar la columna Login
                b.Text = rdc["Login"].ToString();
                //En el nombre del label va a mostrar el id del usuario
                b.Name = rdc["idUsuario"].ToString();
                //tamaño
                b.Size = new System.Drawing.Size(175, 25);
                b.Font = new System.Drawing.Font("Montserrat Alternates", 13);
                b.FlatStyle = FlatStyle.Flat;
                //Color
                b.BackColor = Color.FromArgb(20, 20, 20);
                b.ForeColor = Color.White;
                //Propiedad del dock
                b.Dock = DockStyle.Bottom;
                //alineamiento del label
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Cursor = Cursors.Hand;

                //CONFIGURACION DEL PANEL
                p1.Size = new System.Drawing.Size(155, 167);
                p1.BorderStyle = BorderStyle.None;
                p1.BackColor = Color.FromArgb(20, 20, 20);

                //CONFIGURACION DEL PICTUREBOX
                I1.Size = new System.Drawing.Size(175, 132);
                I1.Dock = DockStyle.Top;
                I1.BackgroundImage = null;
                //tratamiento de conversion
                byte[] bi = (Byte[])rdc["Icono"];
                MemoryStream ms = new MemoryStream(bi);
                I1.Image = Image.FromStream(ms);
                //tamaño
                I1.SizeMode = PictureBoxSizeMode.Zoom;
                I1.Tag = rdc["Login"].ToString();
                I1.Cursor = Cursors.Hand;

                //Mostrar en el panel
                p1.Controls.Add(b);
                p1.Controls.Add(I1);
                b.BringToFront();
                flowLayoutPanel1.Controls.Add(p1);
                b.Click += new EventHandler(mieventoLabel);
                I1.Click += new EventHandler(mieventoImagen);
            }
            con.Close();
        }
        private void mieventoImagen(System.Object sender, EventArgs e)
        {

        }
        private void mieventoLabel(System.Object sender, EventArgs e)
        {

        }
        private void Login_Load(object sender, EventArgs e)
        {
            DIBUJARusuarios();

        }
        private void cargar_usuario()
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
                da = new SqlDataAdapter("validar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", txtpassword.Text);
                da.SelectCommand.Parameters.AddWithValue("@login", txtBuscar.Text);
                da.Fill(dt);
                //donde vamos a mostrar los datos
                dataListado.DataSource = dt;
                con.Close();

          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void Txtpassword_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
