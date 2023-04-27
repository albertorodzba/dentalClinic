<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/paginaMaestra.Master" CodeBehind="crudMedicos.aspx.cs" Inherits="ProyectoOlgaASP_actual_.Pagina_principal" %>


<asp:Content ID="CRUDMedicos" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container container-fluid mt-3">
        <asp:Button OnClick="agregarPersona" ID="error" runat="server" CssClass="btn btn-danger mb-3" Text="Agregar Médico" />
        <asp:Table ID="table_registrados" CssClass="table table-striped" runat="server">
            <asp:TableHeaderRow CssClass="headerRow">
                <asp:TableHeaderCell>Cedula</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nombres</asp:TableHeaderCell>
                <asp:TableHeaderCell>Apellidos</asp:TableHeaderCell>
                <asp:TableHeaderCell>Especialidad</asp:TableHeaderCell>
                <asp:TableHeaderCell>Telefono</asp:TableHeaderCell>
                <asp:TableHeaderCell>Editar</asp:TableHeaderCell>
                <asp:TableHeaderCell>Eliminar</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
