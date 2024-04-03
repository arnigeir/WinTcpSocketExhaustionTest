# Simple platform to test out TCP connection pooling for WS (SOAP) client connections

## CoreWCFService
This is a simple WS server built with  <a href="https://github.com/corewcf/corewcf">CoreWCF</a>.  Build this and run from terminal


## CLI test clients using worker

### StandardSoapClient
Client, proxy and contract build with VS ConnectedServices tools and called without modifications in a while loop to simulate load. 
Build and run from terminal or VS debug session

### PooledSoapClient
The proxy built with VS ConnectedServices but a named client modified with HttpMessageHandlerBehavior to use tcp connection pooling 
Build and run from terminal or VS debug session


### Monitoring TCP connections in Win10/11

It takes some time for the clients to eat upp the TCP connections so the process can easily be monitored by polling the total 
number of connections from a command line.

Use  <i> Get-NetTCPConnection | Measure-Object</i> in PowerShell terminal to get total number of connections.  
Note that this can be bit slow when the system gets loaded.

### Tests
The tests were not conclusive as I failed to recall the original socket exception "System.ServiceModel.CommunicationException: Only one usage of each socket address (protocol/network address/port) is normally permitted."
Instead the tests failed with a "async" exception - probably because of the loop calling the same async method in the worker

The conclusion from those tests that it would be more realistic to call the soap service from a full blown NServiceBus endpoint and 
compare modified endpoint to a modified endpoint with connection pool.

## Tests using  NServiceBus endpoints



### StandardEndpoint
This endpoint is analogous to our current implementation of NSB endpoints that connect to SOAP APIs

### PooledEndpoint
This endpoint uses a client with pooled tcp connections so that connections are reused from the pool - otherwise it is 
identical to the StandardEndpoint.<br/>
The goal is to be able to modify current endpoints without too much work to make them more resilient against connection exhaustion
when calling SOAP api endpoints when the endpoint gets loaded.


## Testresults<
The SOAP server was installed  in folder  

> ami-bus-d-5\c$\Utils\CoreWCFService

and started from command line.
<br/>
The Standard and Pooled endpoints were installed in folders

> ami-bus-d-6\c$\Utils\StandardEndpointTest

and 

> \\ami-bus-d-6\c$\Utils\PooledEndpointTest

respectively

Both endpoints were run 3 times by sending 2000 message bursts every 15,10 and 5 seconds that equals roughly 
to 133,200 and 400 messages per second

The number of TCP connections were monitored on the client machine in a PowerShell terminal using the command 

> Get-NetTCPConnection | Measure-Object

repeatedly.
<br/><br/>

The maximum number of TCP connections observed while running the test endpoints

| Messages per second | Standard | Pooled |
|---------------------|----------|--------| 
| 133 | 16752 (sockets exhausted) | 600 |
| 200 | 16752 (sockets exhausted) | 600 |
| 400 | Not tested     | 625 |


It seems that the pooled version is much more stable for the loads tested.  When testing with 400 Msg/sec the RabbitMQ on the 
ami-bus-d-5 machine started to complain about "low disk" so tests with more load were not run.

