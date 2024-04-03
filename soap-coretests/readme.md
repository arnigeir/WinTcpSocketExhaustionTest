<h1>Simple platform to test out TCP connection pooling for WS (SOAP) client connections</h2>


<h2>CoreWCFService</h2>
This is a simple WS server built with  <a href="https://github.com/corewcf/corewcf">CoreWCF</a>.  Build this and run from terminal


<h2>CLI test clients using worker</h2>

<h3>StandardSoapClient</h3>
Client, proxy and contract build with VS ConnectedServices tools and called without modifications in a while loop to simulate load. 
Build and run from terminal or VS debug session

<h3>PooledSoapClient</h3>
The proxy built with VS ConnectedServices but a named client modified with HttpMessageHandlerBehavior to use tcp connection pooling 
Build and run from terminal or VS debug session


<h3>Monitoring TCP connections in Win10/11</h3>

It takes some time for the clients to eat upp the TCP connections so the process can easily be monitored by polling the total 
number of connections from a command line.

Use  <i> Get-NetTCPConnection | Measure-Object</i> in PowerShell terminal to get total number of connections.  
Note that this can be bit slow when the system gets loaded.


<h2>NServiceBus test endpoints using Quartz jobs to publish messages</h2>


<h3>StandardEndpoint</h3>
This endpoint is analogous to our current implementation of NSB endpoints that connect to SOAP APIs

<h3>PooledEndpoint</h3>
This endpoint uses a client with pooled tcp connections so that connections are reused from the pool

<h2>Testresults</h2>
The SOAP server was installed  in folder  \\ami-bus-d-5\c$\Utils\CoreWCFService  and started from command line.
The Standard and Pooled endpoints were installed in folders <br/><br/>
\\ami-bus-d-6\c$\Temp\StandardEndpointTest <br/>and <br/>
\\ami-bus-d-6\c$\Temp\PooledEndpointTest<br/><br/>

respectively

Both endpoints were run 3 times by sending 2000 message bursts every 15,10 and 5 seconds that equals roughly 
to 133,200 and 400 messages per second

The number of TCP connections were monitored on the client machine in a PowerShell terminal using the command 
<br/></br>
 Get-NetTCPConnection | Measure-Object
<br/></br>
repeatedly


<table>
<head>
<tr>
<th>Messages pre second</th>
<th>Standard</th>
<th>Pooled</th>
</tr>
</head>
<body>

<tr>
<td>133</td>
<td>16752 (failed)</td>
<td>600</td>
</tr>

<tr>
<td>200</td>
<td>16752 (failed)</td>
<td>600</td>
</tr>

<tr>
<td>400</td>
<td>Not tested</td>
<td>625</td>
</tr>

</body>

It seems that the pooled version is much more stable for the loads tested.  When testing with 400 Msg/sec the RabbitMQ on the 
ami-bus-d-5 machine started to complain about "low disk" so tests with more load were not run.

