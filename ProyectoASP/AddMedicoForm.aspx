<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMedicoForm.aspx.cs" Inherits="ProyectoOlgaASP_actual_.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Añadir Medico</title>
    <!--bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <link href="Css/Registro.css" rel="stylesheet" />
</head>
<body>
    <div class="container container-fluid mt-5">
        <form id="addMedico" class="form-registro" runat="server">

            <div class="row">
                <div class="col">
                    <asp:Label runat="server">Nombres</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_Nombres" placeholder="Nombres"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Apellidos</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_apellidos" placeholder="Apellidos"></asp:TextBox>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Cédula</asp:Label>
                    <asp:TextBox runat="server" type="number" class="form-control" ID="txtBox_cedula" placeholder="Cédula"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Especialidad</asp:Label>

                <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="especialidades" CssClass="form-control" width="100%">
                </asp:DropDownList>

                </div>
            </div>
            <div class="row mt-3">
                <div class=" col">
                    <asp:Label runat="server">Télefono</asp:Label>
                    <asp:TextBox runat="server" type="number" CssClass="form-control" ID="txtBox_telefono" placeholder="Télefono"></asp:TextBox>
                </div>
            </div>
           
            <div class="row mt-3">
                <div class=" col">
                    <asp:Button OnClick="agregarMedico" runat="server" CssClass="btn btn-primary" Text="Guardar" />
                </div>

            </div>
            <asp:Label ID="message" runat="server"></asp:Label>
        </form>
    </div>
</body>
</html>
