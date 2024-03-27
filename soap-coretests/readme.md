<h1>Simple platform to test out TCP connection pooling for WS (SOAP) client connections</h2>


<h2>CoreWCFService</h2>
This is the WS server.  Build and run from terminal

<h2>StandardSoapClient</h2>
Client, proxy and contract build with VS ConnectedServices tools and called without modifications in a while loop to simulate load

<h2>PooledSoapClient</h2>
The proxy built with VS ConnectedServices but a named client modified with HttpMessageHandlerBehavior to use tcp connection pooling 
