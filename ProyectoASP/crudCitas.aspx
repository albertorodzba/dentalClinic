<%@ Page Language="C#" MasterPageFile="~/paginaMaestra.Master" AutoEventWireup="true" CodeBehind="crudCitas.aspx.cs" Inherits="ProyectoOlgaASP_actual_.crudCitas" %>

<asp:Content ID="CRUDCitas" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container container-fluid mt-3">
        <asp:Button OnClick="agregarCita" ID="error" runat="server" CssClass="btn btn-danger mb-3" Text="Agregar Cita" />
        <asp:Table ID="table_registrados" CssClass="table table-striped" runat="server">
            <asp:TableHeaderRow CssClass="headerRow">

                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Médico</asp:TableHeaderCell>
                <asp:TableHeaderCell>Fecha</asp:TableHeaderCell>
                <asp:TableHeaderCell>Horario</asp:TableHeaderCell>
                <asp:TableHeaderCell>ID Paciente</asp:TableHeaderCell>
                <asp:TableHeaderCell>Editar</asp:TableHeaderCell>
                <asp:TableHeaderCell>Eliminar</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
