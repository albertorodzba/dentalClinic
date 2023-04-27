using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void registrar(object sender, EventArgs e)
        {
            // Validacion de campos vacios...
            if (txtBox_contrasena.Text.Equals(txtBox_confirmarContrasena.Text) &&
                txtBox_contrasena.Text.Length != 0 && txtBox_Nombres.Text.Length != 0 && txtBox_usuario.Text.Length != 0 &&
                formFile.FileBytes.Length > 0)
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                SqlConnection sql_conectar = new SqlConnection(conectar);
                SqlCommand sql_cmd = new SqlCommand("LeerUsuarios", sql_conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sql_cmd.Connection.Open();
                sql_cmd.Parameters.Add("@login", SqlDbType.VarChar, 150).Value = txtBox_usuario.Text;
                sql_cmd.Parameters.Add("@pw", SqlDbType.VarChar, 15).Value = txtBox_contrasena.Text;
                SqlDataReader dr = sql_cmd.ExecuteReader();
                if (dr.Read())
                {
                    aviso("container container-fluid mt-5", "El usuario ya se encuentra registrado");
                    txtBox_apellidos.Text = "";
                    txtBox_confirmarContrasena.Text = "";
                    txtBox_contrasena.Text = "";
                    txtBox_Nombres.Text = "";
                    txtBox_usuario.Text = "";
                }
                else
                {
                    sql_cmd.Connection.Close();
                    sql_cmd = new SqlCommand("AgregarUsuario", sql_conectar)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sql_cmd.Connection.Open();
                    sql_cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = txtBox_Nombres.Text + " " + txtBox_apellidos.Text;
                    sql_cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = txtBox_usuario.Text;
                    sql_cmd.Parameters.Add("@pw", SqlDbType.VarChar).Value = txtBox_contrasena.Text;

                    byte[] bytes = subirImagen();
                    if (bytes.Length > 1)
                    {
                        sql_cmd.Parameters.Add("@foto", SqlDbType.Image).Value = bytes;
                        SqlDataReader lectorDatos = sql_cmd.ExecuteReader();
                        sql_cmd.Connection.Close();
                        Response.Redirect("default.aspx", false);
                    }
                    sql_cmd.Connection.Close();
                }
            }
            else
            {
                aviso("container container-fluid mt-5", "Las contraseñas no coinciden o campos vacíos ");
            }
        }

        private void aviso(string estilo, string texto)
        {
            HtmlGenericControl crearDiv =
            new HtmlGenericControl("DIV");
            crearDiv.ID = "crearDiv";
            crearDiv.Attributes.Add("class", estilo);
            crearDiv.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            crearDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
            crearDiv.InnerHtml = texto;
            this.Controls.Add(crearDiv);

        }

        private byte[] subirImagen()
        {
            HttpPostedFile archivo_publicado = formFile.PostedFile;
            string nombre = Path.GetFileName(archivo_publicado.FileName);
            string extension = Path.GetExtension(nombre);

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".gif"
                || extension.ToLower() == ".png" || extension.ToLower() == ".bmp")
            {
                Stream s = archivo_publicado.InputStream;
                BinaryReader lectorBinario = new BinaryReader(s);
                byte[] bytes = lectorBinario.ReadBytes((int)s.Length);
                return bytes;
            }
            else
            {
                aviso("container container-fluid mt-5", "Solo aceptamos imagenes con formato jpg, png, bmp o gif.");
                return new byte[0];
            }
        }
    }
}