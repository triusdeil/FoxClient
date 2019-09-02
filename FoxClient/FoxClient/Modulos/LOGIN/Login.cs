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
using System.Net.Mail;
using System.Management;
using System.Net;
namespace FoxClient.Modulos
{
    public partial class Login : Form
    {
        int contador;
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
            txtlogin.Text = ((PictureBox)sender).Tag.ToString();
            panel2.Visible = true;
            panel1.Visible = false;
        }
        private void mieventoLabel(System.Object sender, EventArgs e)
        {
            txtlogin.Text = ((Label)sender).Text;
            panel2.Visible = true;
            panel1.Visible = false;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            DIBUJARusuarios();
            panel2.Visible = false;
            timer1.Start();

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
                da.SelectCommand.Parameters.AddWithValue("@login", txtlogin.Text);
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
            Iniciar_Sesion_Correcto();
        }
     
        private void Iniciar_Sesion_Correcto()
        {
            cargar_usuario();
            Contar();
            if(contador > 0)
            {
                this.Hide();
                CAJA.APERTURA_DE_CAJA formulario_apertura_de_caja = new CAJA.APERTURA_DE_CAJA();
                formulario_apertura_de_caja.ShowDialog();
                
            }

        }
        private void Contar()
        {
            int x;
            x = dataListado.Rows.Count;
            contador = (x);
        }

        private void mostrar_correos()
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
                da = new SqlDataAdapter("select Correo from USUARIO2 where Estado = 'ACTIVO' ", con);
              
                da.Fill(dt);
                txtCorreo.DisplayMember = "Correo";
                txtCorreo.ValueMember = "Correo";
                txtCorreo.DataSource = dt;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void Button3_Click(object sender, EventArgs e)
        {
            panel_Restaurar_Cuenta.Visible = true;
            mostrar_correos();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel_Restaurar_Cuenta.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
        }
        private void mostrar_usuarios_por_correo()
        {
            try
            {
                string resultado;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", txtCorreo.Text);

                con.Open();
                lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        internal void enviarCorreocodigo(string emisor, string password, string mensaje, string asunto, string destinatario, string ruta )
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add((destinatario));
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, password);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);
                lblEstado_de_envio.Text = "ENVIADO";
                MessageBox.Show("Contraseña enviada, revisa tu correo electronico");
                panel_Restaurar_Cuenta.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Enviarcorreo_Click(object sender, EventArgs e)
        {
            mostrar_usuarios_por_correo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", lblResultadoContraseña.Text);
            enviarCorreocodigo("foxclientsystem@gmail.com", "Luis26179956", richTextBox1.Text, "Solicitud de contraseña", txtCorreo.Text, "");

        }
        private void LblResultadoContraseña_Click(object sender, EventArgs e)
        {

        }
        private void MOSTRAR_CAJA_POR_SERIAL()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.CONEXIONMAESTRA.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_cajas_por_serial_de_DiscoDuro", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", lblSerialPc.Text);
                da.Fill(dt);
                datalistado_caja.DataSource = dt;
                con.Close();

            }catch(Exception ex)
                {
                MessageBox.Show(ex.Message);
                }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                ManagementObjectSearcher MDS = new ManagementObjectSearcher("Select * From win32_BaseBoard");
                    foreach (ManagementObject getserial in MDS.Get())
                {
                    lblSerialPc.Text = getserial.Properties["SerialNumber"].Value.ToString();
                    MOSTRAR_CAJA_POR_SERIAL();
                    try
                    {
                        txtidcaja.Text = datalistado_caja.SelectedCells[1].Value.ToString();
                        lblcaja.Text = datalistado_caja.SelectedCells[2].Value.ToString();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "0";
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "1";
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "3";
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "4";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            txtpassword.Text = txtpassword.Text + "9";
        }

        private void BtnborrarTodo_Click(object sender, EventArgs e)
        {
            txtpassword.Clear();
        }
        public static string Mid(string param, int startIndex,int lenght)
        {
            string result = param.Substring(startIndex);
            return result;
        }
        private void BtnborrarDerecha_Click(object sender, EventArgs e)
        {
            try
            {
                int largo;
                if(txtpassword.Text != "")
                {
                    largo = txtpassword.Text.Length;
                    txtpassword.Text = Mid(txtpassword.Text, 1, largo - 1);
                }
            }catch(Exception ex)
            {

            }
        }

        private void Tver_Click(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = '\0';
            tocultar.Visible = true;
            tver.Visible = false;
        }

        private void Tocultar_Click(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = '*';
            tocultar.Visible = false;
            tver.Visible = true;
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario o Contraseña Incorrectos", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LblSerialpc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
