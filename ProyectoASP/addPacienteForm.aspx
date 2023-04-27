<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addPacienteForm.aspx.cs" Inherits="ProyectoOlgaASP_actual_.addPacienteForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Añadir Paciente</title>
    <!--bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <link href="Css/Registro.css" rel="stylesheet" />
</head>
<body>
    <div class="container container-fluid mt-4">
        <form id="addMedico" class="form-registro" runat="server">

            <div class="row">
                <div class="col">
                    <asp:Label runat="server">Nombres</asp:Label>
                    <asp:TextBox  runat="server" type="text" class="form-control" ID="txtBox_Nombres" placeholder="Nombres"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Apellidos</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_apellidos" placeholder="Apellidos"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Fecha de nacimiento</asp:Label>
                    <asp:TextBox runat="server" type="date" class="form-control" ID="txtBox_fecha" placeholder="Número"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Sexo</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_sexo" placeholder="Sexo"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Calle</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_calle" placeholder="Calle"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Número</asp:Label>
                    <asp:TextBox runat="server" type="number" class="form-control" ID="txtBox_numero" placeholder="Número"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Código Postal</asp:Label>
                    <asp:TextBox runat="server" type="number" class="form-control" ID="txtBox_cp" placeholder="Código Postal"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Colonia</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_colonia" placeholder="Colonia"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Ciudad</asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="ciudades" CssClass="form-control" width="100%">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mt-3">
                <div class=" col">
                    <asp:Label runat="server">Télefono</asp:Label>
                    <asp:TextBox runat="server" type="phone" CssClass="form-control" ID="txtBox_telefono" placeholder="Télefono"></asp:TextBox>
                </div>
            </div>

             <div class="row mt-3" >
                <div class="col">
                    <asp:Label runat="server" for="formFile" ID="lbl_file" class="form-label">Imagen de perfil (Opcional)</asp:Label>
                    <asp:FileUpload runat="server" class="form-control" type="file" id="formFile" />
                </div>
            </div>

            <div class="row mt-3">
                <div class=" col">
                    <asp:Button OnClick="guardar" runat="server" CssClass="btn btn-primary" Text="Guardar" />
                </div>

            </div>
            <asp:Label Visible="false" ID="message" runat="server"></asp:Label>
            <asp:Label Visible="false"  ID="lbl_auxiliarID" runat="server"></asp:Label>
        </form>
    </div>
</body>
</html>
