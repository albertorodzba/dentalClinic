using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class FormularioAddMedicos : System.Web.UI.Page
    {
        int contadorFilas = 0;
       

        //SqlConnection conection = new SqlConnection("SERVER=DESKTOP-2HN9NDK\\SQLEXPRESS; DATABASE=Odontología; Integrated security=true");
        SqlConnection conection = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow row;
            TableCell Cell;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LeerPacientes";
                conection.Open();
                cmd.Connection = conection;

                SqlDataReader registros = cmd.ExecuteReader();

                while (registros.Read())
                {
                    contadorFilas++;
                    row = new TableRow();
                    for (int i = 0; i < 14; i++)
                    {
                        Cell = new TableCell();
                        if (i >= 11)
                        {
                            Button button = new Button();
                            if(i == 11)
                            {
                                Image foto = new Image();
                                byte[] bytes = (byte[])registros["Foto"];
                                string strbase64 = Convert.ToBase64String(bytes);
                                foto.ImageUrl = "data:Image/png;base64," + strbase64;
                                foto.Height = 60;
                                foto.Width = 60;
                                foto.ID = contadorFilas.ToString();
                                foto.Attributes.Add("data-bs-target","#staticBackdrop");
                                foto.Attributes.Add("data-bs-toggle", "modal");
                                foto.Attributes.Add("style", "border-radius:50%;cursor:pointer;");
                                foto.Attributes.Add("onclick", "infoModal(this)");
                                Cell.Controls.Add(foto);
                            }
                            else if (i == 12)
                            {
                                button.Text = "Editar";
                                button.ID = "editar-" + contadorFilas.ToString();
                                button.Click += new EventHandler(editPaciente);
                                Cell.Controls.Add(button);
                            }
                            else
                            {
                                button.Text = "Eliminar";
                                button.ID = "eliminar-" + contadorFilas.ToString();
                                button.Click += new EventHandler(deletePaciente);
                                Cell.Controls.Add(button);
                            }
                            row.Cells.Add(Cell);
                        }
                        else
                        {
                            Cell.Text = registros[i].ToString();
                            Cell.ID = contadorFilas.ToString() + "-" + i;
                            row.Cells.Add(Cell);
                            Cell = null;
                        }

                    }
                    table_registrados.Rows.Add(row);
                    row = null;
                }
                conection.Close();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
            }


        }
        string id_ButtonPressed;
        Button clickedButton;
        string[] subString;

        protected void addPaciente(object sender, EventArgs e)
        {
            Response.Redirect("AddPacienteForm.aspx", false);
        }
        protected void editPaciente(object sender, EventArgs e)
        {
            clickedButton = sender as Button;
            id_ButtonPressed = clickedButton.ID.ToString();
            subString = id_ButtonPressed.Split('-');
            id_ButtonPressed = table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text;
            Response.Redirect("AddPacienteForm.aspx?parameter=" + id_ButtonPressed, false);
        }
        protected void deletePaciente(object sender, EventArgs e)
        {
            clickedButton = sender as Button;
            id_ButtonPressed = clickedButton.ID.ToString();
            subString = id_ButtonPressed.Split('-');

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarPaciente";
            cmd.Parameters.AddWithValue("@id", Int32.Parse(table_registrados.Rows[Int32.Parse(subString[1])].Cells[0].Text));//toma el valor de la columna id

            cmd.Connection = conection;
            conection.Open();
            cmd.ExecuteNonQuery();
            conection.Close();
            Response.Redirect("crudPacientes.aspx", false);
        }
    }
}