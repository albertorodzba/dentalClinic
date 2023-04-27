<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="ProyectoOlgaASP_actual_.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro</title>
    <!--bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <link href="Css/Registro.css" rel="stylesheet" />
</head>
<body>

    <div class="container container-fluid mt-5">
         <div id="contenedorAviso" class="container container-fluid"></div>
        <form id="registro" class="form-registro" runat="server" onsubmit="required()">
            <div class ="row">
                <div class ="col">
                <asp:Label runat="server" for="formFile" class="fs-2 text-center"><h1>¡Registrate!</h1></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <asp:Label runat="server">Nombre</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_Nombres" placeholder="Nombres"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Apellidos</asp:Label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtBox_apellidos" placeholder="Apellidos"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col">
                    <asp:Label runat="server">Contraseña</asp:Label>
                    <asp:TextBox runat="server" type="password" class="form-control" ID="txtBox_contrasena" placeholder="Contraseña"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label runat="server">Confirma tu contraseña</asp:Label>
                    <asp:TextBox runat="server" type="password" class="form-control" ID="txtBox_confirmarContrasena" placeholder="Confirma tu contraseña"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class=" col">
                    <asp:Label runat="server">Usuario</asp:Label>
                    <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtBox_usuario" placeholder="Usuario"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3" >
                <div class="col">
                    <asp:Label runat="server" for="formFile" class="form-label">Imagen de perfil (No se podra editar posteriormente)</asp:Label>
                    <asp:FileUpload runat="server" class="form-control" type="file" id="formFile" />
                </div>
            </div>

            <div class="row mt-3">
                <div class=" col">
                    <asp:Button OnClick="registrar" runat="server" CssClass="btn btn-success" Text="Registrarme" />
                </div>

            </div>
        </form>
    </div>
</body>
</html>
