 <%@ Page Language="C#" MasterPageFile="~/paginaMaestra.Master" AutoEventWireup="true" CodeBehind="crudPacientes.aspx.cs" Inherits="ProyectoOlgaASP_actual_.FormularioAddMedicos" %>

<asp:Content runat="server" ID="CRUDPacientes" ContentPlaceHolderID="MainContent">
    <script>
        //Obtiene los datos del usuario a traves de sus ID's (puro frontend)
        function infoModal(Img) {
            let id = Img.id;
            let src_pic = Img.src;
            let array_patient_info = new Array();
            for (let i = 0; i <= 10; i++) {
                let element = document.getElementById(id + "-" + i);
                array_patient_info.push(element.textContent);
            }
            document.getElementById("nombre").textContent = array_patient_info[1] + " " + array_patient_info[2] + " - " + array_patient_info[9];
            document.getElementById("calle").textContent = array_patient_info[3];
            document.getElementById("num").textContent = array_patient_info[4];
            document.getElementById("col").textContent = array_patient_info[5];
            document.getElementById("ciu").textContent = array_patient_info[6];
            document.getElementById("cp").textContent = array_patient_info[7];
            document.getElementById("fn").textContent = array_patient_info[8];
            document.getElementById("t").textContent = array_patient_info[10];
            document.getElementById("img_perfil").src = src_pic;
        }
    </script>
    <div class="container container-fluid mt-3">
        <!--<input type="submit"  onclick ="addPerson()" class="btn btn-danger mb-3" value="Agregar Nuevo"/>-->
        <asp:Button OnClick="addPaciente" ID="error" runat="server" CssClass="btn btn-danger mb-3" Text="Agregar paciente" />
        <asp:Table ID="table_registrados" CssClass="table table-striped" runat="server">
            <asp:TableHeaderRow CssClass="headerRow">
                
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nombres</asp:TableHeaderCell>
                <asp:TableHeaderCell>Apellidos</asp:TableHeaderCell>
                <asp:TableHeaderCell>Calle</asp:TableHeaderCell>
                <asp:TableHeaderCell>Numero</asp:TableHeaderCell>
                <asp:TableHeaderCell>Colonia</asp:TableHeaderCell>
                <asp:TableHeaderCell>Ciudad</asp:TableHeaderCell>
                <asp:TableHeaderCell>CP</asp:TableHeaderCell>
                <asp:TableHeaderCell>Fecha de nacimiento</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sexo</asp:TableHeaderCell>
                <asp:TableHeaderCell>Teléfono</asp:TableHeaderCell>
                <asp:TableHeaderCell>Foto</asp:TableHeaderCell>
                <asp:TableHeaderCell>Editar</asp:TableHeaderCell>
                <asp:TableHeaderCell>Eliminar</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

</div>

<!-- Modal -->
<div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header text-center">
        <h4><b id ="nombre">Nombre</b></h4>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="container col text-center">
            <img id="img_perfil" style="border-radius:50%; width:150px; height:150px"/>
        </div>
          <div class="container text-center mx-auto">
              <table class="table">
                    <tr>
                      <td><b>Calle: </b></td>
                      <td><span id="calle"></span></td>
                    </tr>
                    <tr>
                      <td><b>Número: </b></td>
                      <td><span id="num"></span></td>
                    </tr>
                    <tr>
                      <td><b>Colonia: </b></td>
                      <td><span id="col"></span></td>
                    </tr>
                    <tr>
                       <td><b>Ciudad: </b></td>
                       <td><span id="ciu"></span></td>
                    </tr>
                    <tr>
                       <td><b>Código postal: </b></td>
                       <td><span id="cp"></span></td>
                    </tr>
                    <tr>
                       <td><b>Fecha de nacimiento: </b></td>
                       <td><span id="fn"></span></td>
                    </tr>
                    <tr>
                       <td><b>Télefono: </b></td>
                       <td><span id="t"></span></td>
                    </tr>
                </table>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div>
</div>

</asp:Content>
