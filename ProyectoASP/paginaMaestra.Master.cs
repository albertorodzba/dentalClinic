using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class paginaMaestra : System.Web.UI.MasterPage
    {
        string usuario_nombre = ConfigurationManager.AppSettings["usuario_nombre"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                usuario.InnerText = ConfigurationManager.AppSettings["usuario"];
                imagen_usuario.ImageUrl = "data:Image/png;base64," + ConfigurationManager.AppSettings["foto"];

                //Modal
                nombre_Usuario.Text = ConfigurationManager.AppSettings["usuario"];
                usuario_real.Text = usuario_nombre;
                img_perfil.Src = "data:Image/png;base64," + ConfigurationManager.AppSettings["foto"];
            }
        }

        protected void guardarInfo(object sender, EventArgs e)
        {
            if (nombre_Usuario.Text.Length != 0 && usuario_real.Text.Length != 0 && contrasena.Text.Length != 0)
            {
                string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                SqlConnection sql_conectar = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand("SELECT Login, Password FROM Usuarios WHERE Login = '" + usuario_nombre + "'", sql_conectar);
                sql_conectar.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();

                if(sdr["Login"].ToString() == usuario_nombre && sdr["Password"].ToString() == contrasena.Text)
                {
                    // Actualizamos
                    sdr.Close();
                    cmd = new SqlCommand("UPDATE Usuarios SET Login = '" + usuario_real.Text + "', Nombre = '" + nombre_Usuario.Text + "'" +
                        " WHERE Login = '" + ConfigurationManager.AppSettings["usuario_nombre"] + "'", sql_conectar);
                    SqlDataReader data = cmd.ExecuteReader();
                    data.Read();
                    sql_conectar.Close();
                    ConfigurationManager.AppSettings["usuario"] = nombre_Usuario.Text;
                    ConfigurationManager.AppSettings["usuario_nombre"] = usuario_real.Text;
                    // string usuario_nombre = ConfigurationManager.AppSettings["usuario_nombre"];
                    contrasena.Text = "";
                    aviso("alert alert-success rounded", "Datos actualizados correctamente");

                } else
                {
                    contrasena.Text = "";
                    aviso("alert alert-danger rounded", "La contraseña es incorrecta");
                    sql_conectar.Close();
                }
            }
            else
            {
                aviso("alert alert-danger rounded", "No se permiten espacios en blanco");
            }
        }

        protected void cerrarSesion(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["usuario"] = "";
            ConfigurationManager.AppSettings["usuario_nombre"] = "";
            ConfigurationManager.AppSettings["foto"] = "";
            Response.Redirect("default.aspx", false);
        }
    
        private void aviso(string alerta, string t)
        {
            HtmlGenericControl crearDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            crearDiv.ID = "crearDiv";
            crearDiv.Attributes.Add("class", alerta);
            crearDiv.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            crearDiv.InnerHtml = t;
            notificacion_modificacion.Controls.Add(crearDiv);
        }
    }
}