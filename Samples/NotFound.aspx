<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="Samples.NotFound" MasterPageFile="~/Main.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <br />
    <section class="content">
      <div class="error-page">
        <h2 class="headline text-danger">500</h2>

        <div class="error-content">
          <h3><i class="fas fa-exclamation-triangle text-danger"></i> Oops! Something went wrong.</h3>
          <p>
            We will work on fixing that right away.
            Meanwhile, you may 
              <%--<a href="../../index.html">return to dashboard</a> or try using the search form.--%>
          </p>
        </div>
      </div>
      <!-- /.error-page -->

    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>