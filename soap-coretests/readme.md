Simple platform to test out TCP connection pooling for WS (SOAP) client connections


#CoreWCFService
This is the WS server.  Build and run from terminal

#StandardSoapClient
Client, proxy and contract build with VS ConnectedServices tools and called without modifications in a while loop to simulate load

#PooledSoapClient
The proxy built with VS ConnectedServices but a named client modified with HttpMessageHandlerBehavior to use tcp connection pooling 
