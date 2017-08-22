This sample code will demonstrate how to create a simple Pay Now button for Checkout using C#. 

Change the Secure Transaction Key and API Login ID located in the the pay.aspx.cs file.
The pay.aspx file will generate buttons that will launch Checkout.

After testing is complete. 
Change the Secure Transaction Key and API Login ID in pay.aspx.cs to their production values.
Change this line <script type="text/javascript" src="https://sandbox.forte.net/checkout/v1/js"> in pay.aspx to point to the production enviroment. <script type="text/javascript" src="https://checkout.forte.net/v1/js">