using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class addCitaForm : System.Web.UI.Page
    {
        string accion = "añadir";
        int id = -1;
        int idMedico = -1;
        string fecha = "";
        string horario = "";
        int idPaciente = -1;
        IList cedulas = new List<string>();
        IList fechas = new List<string>();
        IList horarios = new List<string>();

        SqlConnection conection = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cargar medico en la cita a editar
            SqlCommand cmd = new SqlCommand();
            string datosCompletosMedicos;
            string medicoActual = Request.QueryString["m"];
            cmd.Connection = conection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LeerMedicos";
            conection.Open();
            medicos.Items.Add(medicoActual);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                datosCompletosMedicos = registro["cedula"].ToString() + ". " + registro["Nombres"].ToString() + " " + registro["Apellidos"].ToString();
                if (!datosCompletosMedicos.Equals(medicoActual))
                {
                    medicos.Items.Add(datosCompletosMedicos);
                }
            }
            conection.Close();

            // Cargar paciente de la cita a editar
            cmd = new SqlCommand();
            cmd.Connection = conection;
            string datosCompletosPacientes;
            string pacienteActual = Request.QueryString["p"];
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LeerPacientes";
            conection.Open();
            pacientes.Items.Add(pacienteActual);
            registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                datosCompletosPacientes = registro["NoAsig"].ToString() + ". " + registro["Nombres"].ToString() + " " + registro["Apellidos"].ToString();
                if (!datosCompletosPacientes.Equals(pacienteActual))
                {
                    pacientes.Items.Add(datosCompletosPacientes);
                }
            }
            conection.Close();

            //leer si se pulso un boton editar con envio de parametro
            if (Request.QueryString["parameter"] != null)
            {
                accion = "editar";
                if (IsPostBack == false)
                {

                    cmd = new SqlCommand();
                    cmd.Connection = conection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "leerRegistroCita";
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = Int32.Parse(Request.QueryString["parameter"]);

                    conection.Open();
                    registro = cmd.ExecuteReader();
                    registro.Read();
                    id = Int32.Parse(registro[0].ToString());
                    idMedico = Int32.Parse(registro[1].ToString());
                    fecha = Convert.ToDateTime(registro[2]).Date.ToString("yyyy-MM-dd");
                    horario = registro[3].ToString();
                    idPaciente = Int32.Parse(registro[4].ToString());

                    txtBox_ID.Text = id.ToString();
                    medicos.Text = idMedico.ToString();
                    txtBox_fecha.Text = fecha;
                    txtBox_horario.Text = horario;
                    pacientes.Text = idPaciente.ToString();

                    conection.Close();
                }
            }
            else
            {
                accion = "añadir";
            }
        }


        protected void guardar(object sender, EventArgs e)
        {
            if (txtBox_fecha.Text.Length != 0 && txtBox_horario.Text.Length != 0 &&
                txtBox_ID.Text.Length != 0)
            {
                //Utilizando REGEX para obtener la cedula y el NoAsig
                string regex_patron = @"(?:- *)?\d+(?:\.\d+)?";
                string medico = medicos.SelectedValue;
                string cedula = "";
                string paciente = pacientes.SelectedValue;
                string noasig = "";
                Regex r = new Regex(regex_patron);
                foreach (Match m in r.Matches(medico))
                {
                    cedula += m.Value;
                }
                foreach (Match m in r.Matches(paciente))
                {
                    noasig += m.Value;
                }

                idMedico = Int32.Parse(cedula);
                fecha = txtBox_fecha.Text;
                horario = txtBox_horario.Text;

                // SABER SI LAS FECHA Y HORARIO NO ESTAN REGISTRADAS YA
                if(validarFechaHorario(cedula, fecha, horario))
                {
                    idPaciente = Int32.Parse(noasig);

                    if (accion.Equals("añadir"))//añadir
                    {
                        try
                        {

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conection;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AgregarCita";
                            cmd.Parameters.AddWithValue("@idMedico", idMedico);
                            cmd.Parameters.AddWithValue("@fecha", fecha);
                            cmd.Parameters.AddWithValue("@horario", horario);
                            cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            conection.Open();
                            cmd.ExecuteNonQuery();

                            conection.Close();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else//editar
                    {
                        id = Int32.Parse(txtBox_ID.Text);
                        try
                        {

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conection;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "ActualizarCita";
                            cmd.Parameters.AddWithValue("@idMedico", idMedico);
                            cmd.Parameters.AddWithValue("@fecha", fecha);
                            cmd.Parameters.AddWithValue("@horario", horario);
                            cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            cmd.Parameters.AddWithValue("@id", id);
                            conection.Open();
                            cmd.ExecuteNonQuery();
                            conection.Close();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                    Response.Redirect("crudCitas.aspx", false);
                }
            }
        }

        private bool validarFechaHorario(string cedula, string fecha, string horario)
        {
            SqlConnection conexion = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");
            SqlCommand cmd = new SqlCommand("Select MedicoId, Fecha, Horario from Citas", conexion);
            conexion.Open();
            using (SqlDataReader datos = cmd.ExecuteReader())
            {
                while (datos.Read())
                {
                    cedulas.Add(datos[0].ToString());
                    fechas.Add(datos[1].ToString());
                    horarios.Add(datos[2].ToString());
                }
            }
            DateTime formatoFecha = DateTime.Parse(fecha);
            horario = Convert.ToDateTime(horario).ToString("HH:mm:ss");

            if (cedulas.Contains(cedula))
            {
                if (fechas.Contains(formatoFecha.ToString()) && horarios.Contains(horario))
                {
                    aviso("alert alert-secondary", "La fecha ya esta ocupada por el médico");
                    return false;
                }
                else
                {
                    return true;
                }
            } else
            {
                return true;
            }
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