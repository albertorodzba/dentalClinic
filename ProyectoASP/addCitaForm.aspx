<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addCitaForm.aspx.cs" Inherits="ProyectoOlgaASP_actual_.addCitaForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>Añadir Cita</title>
    <!--bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <link href="Css/Registro.css" rel="stylesheet" />
</head>
<body>
    <div class="container container-fluid mt-5">
        <form id="addMedico" class="form-registro" runat="server">
        <div class="container" id="contenedorDivMensaje" runat="server"> </div>
            <div class="row">
                <div class="col">
                    <asp:Label runat="server">ID Cita</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_ID" placeholder="ID"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Horario</asp:Label>
                    <asp:TextBox runat="server" type="time" class="form-control" ID="txtBox_horario" placeholder="Horario"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <asp:Label runat="server">Médico</asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="medicos" CssClass="form-control" width="100%">
                </asp:DropDownList></div>
                <div class="col">
                    <asp:Label runat="server">Paciente</asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="pacientes" CssClass="form-control" width="100%">
                </asp:DropDownList></div>
            </div>
            <div class ="row">
                <div class="col">
                    <asp:Label runat="server">Fecha</asp:Label>
                    <asp:TextBox runat="server" type="date" class="form-control" ID="txtBox_fecha" placeholder="Fecha"></asp:TextBox>
                </div>
            </div>
           
            <div class="row mt-3">
                <div class=" col">
                    <asp:Button OnClick="guardar" runat="server" CssClass="btn btn-primary" Text="Guardar" />
                </div>

            </div>
            <asp:Label ID="message" runat="server"></asp:Label>
        </form>
    </div>
</body>
</html>
