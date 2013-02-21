<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pp.aspx.vb" Inherits="BetterclassifiedsWeb.pp" %>
<%@ Import Namespace="BetterclassifiedsCore"%>
<%@ Import Namespace="BetterclassifiedsCore.BundleBooking"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="payForm" method="post" runat="server">
        <input type="hidden" name="cmd"  id="cmd" value="_xclick"/>
        <input type="hidden" name="business"  value="<% Response.Write (Settings.BusinessEmail) %>"/>
        <input type="hidden" name="item_name" value="<% Response.Write(ItemName)%>"/>
        <input type="hidden" name="amount"  value="<% Response.Write(Cost)%>"/>
        <input type="hidden" name="no_shipping"  value="1" />
        <input type="hidden" name="return"  value="<% Response.Write(SuccessUrl)%>"/>
        <input type="hidden" name="rm"  value="2"/>
        <input type="hidden" name="notify_url" value="<% Response.Write(NotifyUrl)%>"/>
        <input type="hidden" name="cancel_return"  value="<% Response.Write(Settings.CancelPurchaseUrl)%>"/>
        <input type="hidden" name="currency_code"  value="<% Response.Write (Settings.CurrencyCode)%>"/>
        
        <% If BookingController.BookingType = Booking.BookingAction.NormalBooking Then%>
        
        <input type="hidden" name="custom" value="<%Response.Write ( BookingController.AdBookCart.BookReference) %>"/>
        
        <% ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then%>
        
        <input type="hidden" name="custom" value="<%Response.Write ( BookingController.SpecialBookCart.BookReference) %>"/>
        
        <% ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then%>
        
        <input type="hidden" name="custom" value="<%Response.Write ( BundleController.BundleCart.BookReference) %>"/>
        
        
        <% End If%>
    </form>
    
       <script language="javascript" type="text/javascript">
           document.payForm.submit();

        </script>
</body>
</html>
