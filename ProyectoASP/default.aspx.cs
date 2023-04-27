using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void inicio_Sesion(object sender, EventArgs e)
        {
            // Se obtiene la cadena de conexion del webconfig
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            SqlConnection sql_conectar = new SqlConnection(conectar);
            SqlCommand sql_cmd = new SqlCommand("LeerUsuarios", sql_conectar)
            {
                CommandType = CommandType.StoredProcedure
            };

            sql_cmd.Connection.Open();
            sql_cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = inputUsuario.Text;
            sql_cmd.Parameters.Add("@pw", SqlDbType.VarChar).Value = inputPassword.Text;

            SqlDataReader lectorDatos = sql_cmd.ExecuteReader();

            if (lectorDatos.Read())
            {
                ConfigurationManager.AppSettings["usuario"] = lectorDatos["Nombre"].ToString();
                byte[] bytes = (byte[])lectorDatos["foto"];
                string strbase64 = Convert.ToBase64String(bytes);
                ConfigurationManager.AppSettings["foto"] = strbase64;
                ConfigurationManager.AppSettings["usuario_nombre"] = lectorDatos["Login"].ToString();
                Response.Redirect("crudMedicos.aspx", false);
            }
            else
            {
                aviso("alert alert-secondary", "Escribió mal el usuario o contraseña");
            }
            sql_cmd.Connection.Close();
        }

        private void aviso(string estilo, string texto)
        {
            HtmlGenericControl crearDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            crearDiv.ID = "crearDiv";
            crearDiv.Attributes.Add("class", estilo);
            crearDiv.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            crearDiv.InnerHtml = texto;
            contenedorDivMensaje.Controls.Add(crearDiv);
        }

    }
}