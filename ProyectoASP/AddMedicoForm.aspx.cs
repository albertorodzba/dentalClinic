using System;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoOlgaASP_actual_
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string accion = "añadir";
        string nombre = "";
        string apellidos = "";
        string especialidad = null;
        string telefono = "";
        int cedula = -1;
        SqlConnection conection = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LeerEspecialidades";
            conection.Open();
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                especialidades.Items.Add(registro["Descripcion"].ToString());
            }
            conection.Close();

            //leer si se pulso un boton editar con envio de parametro
            if (Request.QueryString["parameter"] != null)
            {
                accion = "editar";
                //message.Text = "se envio un dato";
                if (IsPostBack == false)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "leerRegistroMedico";
                    cmd.Parameters.Add("@cedula", SqlDbType.Int).Value = Int32.Parse(Request.QueryString["parameter"]);

                    conection.Open();
                    registro = cmd.ExecuteReader();
                    registro.Read();
                    cedula = Int32.Parse(registro[1].ToString());
                    nombre = registro[2].ToString();
                    apellidos = registro[3].ToString();
                    especialidad = registro[4].ToString();
                    telefono = registro[5].ToString();

                    txtBox_cedula.Text = cedula.ToString();
                    txtBox_cedula.Enabled = false;
                    txtBox_Nombres.Text = nombre;
                    txtBox_apellidos.Text = apellidos;
                    especialidades.Text = especialidad;
                    txtBox_telefono.Text = telefono;
                    conection.Close();
                }
            }
            else
            {
                accion = "añadir";
            }
        }


        protected void agregarMedico(object sender, EventArgs e)
        {
            if (txtBox_Nombres.Text.Length != 0 && txtBox_apellidos.Text.Length != 0 && txtBox_cedula.Text.Length != 0
                && txtBox_telefono.Text.Length != 0)
            {
                cedula = Int32.Parse(txtBox_cedula.Text);
                nombre = txtBox_Nombres.Text;
                apellidos = txtBox_apellidos.Text;
                telefono = txtBox_telefono.Text;
                especialidad = especialidades.SelectedValue;

                if (accion.Equals("añadir")) // añadir
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conection;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "AgregarMedico";
                        cmd.Parameters.Add("@cedula", SqlDbType.Int).Value = cedula;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                        cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = apellidos;
                        cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefono;
                        cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;
                        Console.WriteLine(cedula + especialidad);

                        conection.Open();
                        cmd.ExecuteNonQuery();

                        conection.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else // editar
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conection;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ActualizarMedico";
                        cmd.Parameters.Add("@cedula", SqlDbType.Int).Value = cedula;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                        cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = apellidos;
                        cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefono;
                        cmd.Parameters.Add("@especialidad", SqlDbType.VarChar).Value = especialidad;

                        conection.Open();
                        cmd.ExecuteNonQuery();
                        conection.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
                Response.Redirect("crudMedicos.aspx", false);
            }
        }
    }
}