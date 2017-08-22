<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay.aspx.cs" Inherits="Forte.CheckOut"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Checkout</title>
    
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://sandbox.forte.net/checkout/v1/js"></script>
    <script>
       function oncallback(e) {
           $('#message').html(e.data);

           var response = JSON.parse(e.data);

           switch (response.event) {

               case 'begin':

                   //call to forte checkout is successful
                   $('#success').css('display', 'none');
                   break;

               case 'complete':

                   //transaction successful

                   //(optional) validate from response.signature that this message is coming from forte

                   //display a receipt

                   $('#success').css('display', '');
                   alert('thanks for your order. the trace number is ' + response.trace_number);

                   break;

               case 'fail':

                   //handle failed transaction

                   alert('sorry, transaction failed. failed reason is ' + response.response_description);

           }

         }
    </script>
</head>

<body style="background-color: white;width: 900px; border:groove; margin-top:100px;margin-left:20%">
    <form runat="server" >
        
        <div id="mainbody" >
             <label style="font:bold 20px Verdana">Check Out</label>       
            
               <div id="success" style="display:none; color: blue; margin-left: 0px;">Transaction Processed Successfully !!! </div>
            <hr />
                    <label style="font:Verdana">Call Back Message</label>
                    <div id="message" style="color: green; padding-top: 10px; width: 885px; overflow: auto"> </div>
            </div>
         </form>
        
        <hr />
             <button api_login_id="<%=api_loginID%>" method="sale" version_number="1.0" utc_time="<%=utc_time%>" signature="<%= pay_now_single_return_string%>" callback="oncallback" total_amount="10.00" order_number="A1234">Pay Now</button>

          <hr />
          <button api_login_id="<%=api_loginID%>" method="schedule" version_number="1.0" utc_time="<%=utc_time%>" signature="<%= pay_schedule_amount_return_string%>" callback="oncallback" total_amount="1-9.5;5" schedule_start_date="1/1/2015" schedule_frequency="monthly" schedule_quantity="12" schedule_continuous="false" order_number="A1234" save_token="true">Subscribe</button>
    
        <hr />
          <button api_login_id="<%=api_loginID%>" method="sale" version_number="1.0" utc_time="<%=utc_time%>" signature="<%= pay_range_select_amount_return_string%>" callback="oncallback" total_amount="{20,40,60,80,100,0};20-1000" >Select Amount</button>
    
        <hr />
          <button api_login_id="<%=api_loginID%>" method="sale" version_number="1.0" utc_time="<%=utc_time%>" signature="<%= pay_range_select_amount_labels%>" callback="oncallback" total_amount="{1375.23,1573.66,56.99,0|Total outstanding,Last statement balance,Minimum balance,Specify different amount}" >Select Amount with Labels</button>
</body>
    

</html>


