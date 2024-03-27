<h1>Simple platform to test out TCP connection pooling for WS (SOAP) client connections</h2>


<h2>CoreWCFService</h2>
This is a simple WS server built with  <a href="https://github.com/corewcf/corewcf">CoreWCF</a>.  Build this and run from terminal

<h2>StandardSoapClient</h2>
Client, proxy and contract build with VS ConnectedServices tools and called without modifications in a while loop to simulate load. 
Build and run from terminal or VS debug session

<h2>PooledSoapClient</h2>
The proxy built with VS ConnectedServices but a named client modified with HttpMessageHandlerBehavior to use tcp connection pooling 
Build and run from terminal or VS debug session

<h2>Monitoring TCP connections in Win10/11</h2>

It takes some time for the clients to eat upp the TCP connections so the process can easily be monitored by polling the total 
number of connections from a command line.

Use  <i> Get-NetTCPConnection | Measure-Object</i> in PowerShell terminal to get total number of connections.  
Note that this can be bit slow when the system gets loaded.

