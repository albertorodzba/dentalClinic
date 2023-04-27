using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class Pagina_principal : System.Web.UI.Page
    {
        int contadorFilas = 0;
        string id_BotonPresionado;
        Button botonPresionado;
        string[] subString;
        readonly SqlConnection conexion = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");

        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow fila;
            TableCell celda;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LeerMedicos";
                conexion.Open();
                cmd.Connection = conexion;
                SqlDataReader registros = cmd.ExecuteReader();
                contadorFilas = 0;
                while (registros.Read())
                {
                    contadorFilas++;
                    fila = new TableRow();
                    for (int i = 1; i < 8; i++)
                    {
                        celda = new TableCell();
                        if (i >= 6)
                        {
                            Button button = new Button();
                            if (i == 6)
                            {
                                button.Text = "Editar";
                                button.ID = "editar-"+contadorFilas.ToString();
                                button.Click += new EventHandler(editarPersona);
                            }
                            else
                            {
                                button.Text = "Eliminar";
                                button.ID = "eliminar-" + contadorFilas.ToString();
                                button.Click += new EventHandler(borrarPersona);
                            }
                            
                            celda.Controls.Add(button);
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

        protected void agregarPersona(object sender, EventArgs e)
        {
            Response.Redirect("AddMedicoForm.aspx",false);
        }

        protected void editarPersona(object sender, EventArgs e)
        {
            botonPresionado = sender as Button;
            id_BotonPresionado = botonPresionado.ID.ToString();
            subString = id_BotonPresionado.Split('-');
            id_BotonPresionado = table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text;
            Response.Redirect("AddMedicoForm.aspx?parameter=" + id_BotonPresionado, false);
        }

        protected void borrarPersona(object sender, EventArgs e)
        {
            botonPresionado = sender as Button;
            id_BotonPresionado = botonPresionado.ID.ToString();
            subString = id_BotonPresionado.Split('-');
            SqlCommand cmd = new SqlCommand();            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarMedico";
            cmd.Parameters.AddWithValue("@cedula", Int32.Parse(table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text));//se obtiene la cedula en la columna 0 de la tabla
            cmd.Connection = conexion;
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            Response.Redirect("crudMedicos.aspx");
        }
    }
}