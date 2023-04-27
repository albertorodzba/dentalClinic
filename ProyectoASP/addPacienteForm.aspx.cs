using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoOlgaASP_actual_
{
    public partial class addPacienteForm : System.Web.UI.Page
    {
        string accion = "añadir";
        string nombre = "";
        string apellidos = "";
        string calle = "";
        string numero = "";
        string colonia = "";
        string ciudad = "";
        int cp = -1;
        string fechaNac = "";
        char sexo;
        string telefono = "";
        int id = -1;
        SqlConnection conection = new SqlConnection("SERVER=localhost; DATABASE=Odontología; Integrated security=true");
        SqlCommand cmd = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LeerCiudades";
            conection.Open();
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                ciudades.Items.Add(registro["Ciudad"].ToString() + ", " + registro["Estado"].ToString());
            }
            conection.Close();
            //leer si se pulso un boton editar con envio de parametro
            if (Request.QueryString["parameter"] != null)
            {
                formFile.Visible = false;
                lbl_file.Visible = false;
                accion = "editar";
                //message.Text = "se envio un dato";
                if (IsPostBack == false)
                {

                    cmd = new SqlCommand();
                    cmd.Connection = conection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "leerRegistroPaciente";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Int32.Parse(Request.QueryString["parameter"]);
                    conection.Open();
                    registro = cmd.ExecuteReader();
                    registro.Read();

                    id = Int32.Parse(registro[0].ToString());
                    nombre = registro[1].ToString();
                    apellidos = registro[2].ToString();
                    calle = registro[3].ToString();
                    numero = registro[4].ToString();
                    colonia = registro[5].ToString();
                    ciudad = registro[6].ToString();
                    cp = Int32.Parse(registro[7].ToString());
                    fechaNac = registro[8].ToString();
                    sexo = registro[9].ToString()[0];
                    telefono = registro[10].ToString();

                    lbl_auxiliarID.Text = id.ToString();//se usa esta etiqueta oculta como auxiliar para no perder el ID para posteriormente consultar
                    txtBox_Nombres.Text = nombre;
                    txtBox_apellidos.Text = apellidos;
                    txtBox_calle.Text = calle;
                    txtBox_numero.Text = numero;
                    txtBox_colonia.Text = colonia;
                    ciudades.Text = ciudad;
                    txtBox_cp.Text = cp.ToString();
                    txtBox_fecha.Text = Convert.ToDateTime(fechaNac).Date.ToString("yyyy-MM-dd"); //Enviar la fecha correctamente
                    txtBox_sexo.Text = sexo.ToString();
                    txtBox_telefono.Text = telefono;
                    
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
            if (txtBox_apellidos.Text.Length != 0 && txtBox_calle.Text.Length != 0 && txtBox_colonia.Text.Length != 0 
                && txtBox_cp.Text.Length != 0 && txtBox_fecha.Text.Length != 0 && txtBox_Nombres.Text.Length != 0 && 
                txtBox_numero.Text.Length != 0 && txtBox_sexo.Text.Length != 0 && txtBox_telefono.Text.Length != 0)
            {
                nombre = txtBox_Nombres.Text;
                apellidos = txtBox_apellidos.Text;
                calle = txtBox_calle.Text;
                numero = txtBox_numero.Text;
                colonia = txtBox_colonia.Text;
                ciudad = ciudades.SelectedValue;
                cp = Int32.Parse(txtBox_cp.Text);
                fechaNac = txtBox_fecha.Text;
                sexo = txtBox_sexo.Text[0];
                telefono = txtBox_telefono.Text;
                message.Text = fechaNac;

                if (accion.Equals("añadir"))
                {
                    try
                    {

                        cmd.Connection = conection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "AgregarPaciente";

                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidos", apellidos);
                        cmd.Parameters.AddWithValue("@calle", calle);
                        cmd.Parameters.AddWithValue("@numero", numero);
                        cmd.Parameters.AddWithValue("@colonia", colonia);
                        cmd.Parameters.AddWithValue("@ciudad", ciudad);
                        cmd.Parameters.AddWithValue("@cp", cp);
                        cmd.Parameters.AddWithValue("@fecha_nac", fechaNac);
                        cmd.Parameters.AddWithValue("@sexo", sexo);
                        cmd.Parameters.AddWithValue("telefono", telefono);

                        byte[] bytes = subirImagen();
                        if (bytes.Length > 1)
                        {
                            cmd.Parameters.AddWithValue("@foto", bytes) ;
                            conection.Open();
                            cmd.ExecuteNonQuery();
                            conection.Close();
                            Response.Redirect("default.aspx", false);
                        }
                        conection.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    id = Int32.Parse(lbl_auxiliarID.Text);//se recuperan los datos

                    try
                    {
                        cmd.Connection = conection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ActualizarPaciente";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidos", apellidos);
                        cmd.Parameters.AddWithValue("@calle", calle);
                        cmd.Parameters.AddWithValue("@numero", numero);
                        cmd.Parameters.AddWithValue("@colonia", colonia);
                        cmd.Parameters.AddWithValue("@ciudad", ciudad);
                        cmd.Parameters.AddWithValue("@cp", cp);
                        cmd.Parameters.AddWithValue("@fecha_nac", fechaNac);
                        cmd.Parameters.AddWithValue("@sexo", sexo);
                        cmd.Parameters.AddWithValue("telefono", telefono);
                        conection.Open();
                        cmd.ExecuteNonQuery();
                        conection.Close();
                        Response.Redirect("crudPacientes.aspx", false);
                    }
                    catch (SqlException ex)
                    {
                        message.Text = ex.ToString();
                    }
                }
                Response.Redirect("crudPacientes.aspx", false);
            }
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
                return new byte[0];
            }
        }
    }
}