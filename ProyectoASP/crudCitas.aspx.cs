using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class crudCitas : System.Web.UI.Page
    {
        IDictionary cedulasDoctores = new Dictionary<string, string>();
        IDictionary pacientes = new Dictionary<string, string>();
        int contadorFilas = 0;
        string id_BotonPresionado;
        Button botonPresionado;
        string[] subString;
        SqlConnection conexion = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");
        SqlConnection conexionParaDatos = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");


        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow fila;
            TableCell celda;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LeerCitas";
                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader registros = cmd.ExecuteReader();
                contadorFilas = 0;
                while (registros.Read())
                {
                    contadorFilas++;
                    fila = new TableRow();
                    for (int i = 0; i < 7; i++)
                    {
                        celda = new TableCell();
                        
                        if (i  == 1)
                        {
                            string cedulaActual = registros[i].ToString();
                            //Si la cedula actual ya se leyo antes y esta en el diccionario de cedulas...
                            if (cedulasDoctores.Contains(cedulaActual))
                            {

                            } else
                            {
                                // Consultar al medico con la cedula actual
                                conexionParaDatos.Open();
                                SqlCommand consultaDoctor = new SqlCommand("Select Nombres, Apellidos from Medico where Cedula = " + cedulaActual, conexionParaDatos);
                                using (SqlDataReader nombres_apellidos = consultaDoctor.ExecuteReader())
                                {
                                    nombres_apellidos.Read();
                                    celda.Text = registros[i].ToString() + ". " + nombres_apellidos[0].ToString() + " " + nombres_apellidos[1].ToString();
                                    fila.Cells.Add(celda);
                                    celda = null;
                                }
                                conexionParaDatos.Close();
                            }
                        } else if (i == 4)
                        {
                            string pacienteActual = registros[i].ToString();
                            //Si el paciente ya se leyo antes y esta en el diccionario de pacientes...
                            if (pacientes.Contains(pacienteActual))
                            {
                                celda.Text = pacienteActual + ". " + pacientes[pacienteActual];
                                fila.Cells.Add(celda);
                                celda = null;
                            }
                            else
                            {
                                // Consultar al paciente con el noasig actual
                                conexionParaDatos.Open();
                                SqlCommand consultaPaciente = new SqlCommand("Select Nombres, Apellidos from Paciente where NoAsig = " + pacienteActual, conexionParaDatos);
                                using (SqlDataReader nombres_apellidos = consultaPaciente.ExecuteReader())
                                {
                                    nombres_apellidos.Read();
                                    celda.Text = registros[i].ToString() + ". " + nombres_apellidos[0].ToString() + " " + nombres_apellidos[1].ToString();
                                    fila.Cells.Add(celda);
                                    celda = null;
                                    pacientes.Add(pacienteActual, nombres_apellidos[0].ToString() + " " + nombres_apellidos[1].ToString());
                                }
                                conexionParaDatos.Close();
                            }
                        }
                          else if (i >= 5)
                        {
                            Button button = new Button();
                       
                            if (i == 5)
                            {
                                button.Text = "Editar";
                                button.ID = "editar-" + contadorFilas.ToString();
                                button.Click += new EventHandler(editarCita);
                                celda.Controls.Add(button);
                            }
                            else
                            {
                                button.Text = "Eliminar";
                                button.ID = "eliminar-" + contadorFilas.ToString();
                                button.Click += new EventHandler(borrarCita);
                                celda.Controls.Add(button);
                            }


                            fila.Cells.Add(celda);
                        }
                        else
                        {

                            celda.Text = registros[i].ToString();
                            fila.Cells.Add(celda);
                            celda = null;
                        }

                    }
                    table_registrados.Rows.Add(fila);
                    fila = null;
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
            }


        }


        protected void agregarCita(object sender, EventArgs e)
        {
            Response.Redirect("AddCitaForm.aspx", false);
        }
        protected void editarCita(object sender, EventArgs e)
        {
            botonPresionado = sender as Button;
            id_BotonPresionado = botonPresionado.ID.ToString();
            subString = id_BotonPresionado.Split('-');
            id_BotonPresionado = table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text;
            Response.Redirect("AddCitaForm.aspx?parameter=" + id_BotonPresionado + "&m=" + table_registrados.Rows[Int32.Parse(subString[1])].Cells[1].Text
                + "&p=" + table_registrados.Rows[Int32.Parse(subString[1])].Cells[4].Text, false);
        }


        protected void borrarCita(object sender, EventArgs e)
        {
            botonPresionado = sender as Button;
            id_BotonPresionado = botonPresionado.ID.ToString();
            subString = id_BotonPresionado.Split('-');

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarCita";
            cmd.Parameters.AddWithValue("@id", Int32.Parse(table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text));//toma el valor de la columna id

            cmd.Connection = conexion;
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            Response.Redirect("crudCitas.aspx", false);
        }


    }
}