<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProyectoOlgaASP_actual_.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar sesión</title>
    <!--bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <link href="Css//Default.css" rel="stylesheet" />
</head>
<body class="Background">
    <div class="container container-fluid  ">
        <div class="container" id="contenedorDivMensaje" runat="server"> </div>
        <form class="form-login" id="login" runat="server">
            <asp:Image ID="logoOdontología" CssClass="img-fluid" runat="server" ImageUrl="~/Imagenes/logo.png" />
            <div class="form-group mt-3">
                <asp:Label runat="server" for="inputUsuario">Usuario</asp:Label>
                <asp:TextBox type="text" class="form-control" ID="inputUsuario" runat="server" placeholder="Usuario"></asp:TextBox>
            </div>
            <div class="form-group mt-3">
                <asp:Label runat="server" for="inputPassword">Contraseña</asp:Label>
                <asp:TextBox type="password" class="form-control" ID="inputPassword" runat="server" placeholder="Contraseña"></asp:TextBox>
            </div>
            <asp:Button OnClick="inicio_Sesion" type="submit" class="btn btn-primary mt-3" runat="server" Text="Iniciar Sesión"></asp:Button>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registrarse.aspx"><b>¿No tienes una cuenta? Crea una</b></asp:HyperLink>
        </form>
    </div>

</body>
</html>
