# Distrubuted Messaging System
Example of a distributed system. Collection of apps/services 

Fake business for package delivery, designed to scale, add and remove features with minimal impact.
Ability to raise capacity.
Continous deployment - autonomus services
Modular components - micro services


## Messaging

We will use a message broker for message communtion and use this as our method of RPC - remote procedure call.

 `---------------------              `---------------------              .--------------------.   
   -ooooooooooooooooooooo              -ooooooooooooooooooooo              :oooooooooooooooooooo+   
   -ooooooooooooooooooooo              -ooooooooooooooooooooo              :oooooooooooooooooooo+   
   -oooooooSERVICEooooooo              -oooooooSERVICEooooooo              :oooooooSERVICEooooooo   
   -oooooo++++++++ooooooo              -oooooo+++o++++ooooooo              :oooooo+++o++++oooooo+   
   -ooooooooooooooooooooo              -ooooooooooooooooooooo              :oooooooooooooooooooo+   
   -/////////////////////              -/////////////////////              -/////////////////////   
             `.                                   .                                   .             
              `                                   `                                   `             
              `                                   `                                   `             
              `                                   `                                   `             
             `-                                   -                                   -             
   `.............................................................................................   
   -oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo+   
   -ooooooooooooooooooooooooooooooooooooppoMESSAGE BROKERppooooooooooooooooooooooooooooooooooooo+   
   -ooooooooooooooooooooooooooooooooooooooooooo+++oo+ooooooooooooooooooooooooooooooooooooooooooo+   
   -++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++/   
   
Services will communite via the message broker. (Mail service) - Queue. First in first out using RabbitMQ

Possible patterns: 
	1. Point to point: Sends a message directly from one service directly to another.
	2. Event broadcasting: Sent to multiple services at once. (Publish / Subscribe).
	
Messages are created as interfaces so code sharing can be minimised. 
	
The message broker will be used to make use of the benefits over MSMQ. 

## Message Broker: 
	1. Centralised
	2. Multi Platform
	3. Standardized - Compatable with others
	4. Scale with clustering
	5. Supported by MassTransit
	
## MSMQ: - Rejected
	1. Decentralized - each machine has its own que
	2. Windows only - Installed by default, only for .net
	3. No Standard - Propriety implemetation 
	4. Scales automatically
	5. Supported by NServiceBus

## MicroServices
	Messaging will keep them decoupled. 
	Each service will be autonomous. 

## RabbitMQ

Message broker
Supports AMPQ by default.
Plugins & Clustering

### Breakdown - RabbitMQ

The application that creates the message is the ```producer``` or publisher
It sends the message over the AMPQ protocol. 
This goes to the exchange and routes them to queues using a binding.
Exchnages can also route to other exchanges.
Consumers monitor the queues.

Consumer sends an acknolegement to confirm the message has been processed.

Messages are held in queues until they are ready to be processed.

### Exchanges

1. Direct: Sends message to a queues.
	Can also be bound to multiple queues.
	A routing key can be provided. Exchange can send message to a queue via the routing key.
2. Topic Type: Uses a more complex routing key and queues can be confgigured to work on the key or part of the key. 
	* and # are the wildcards
3. Fanout: Key is ignored. Goes to all bound queues






	
	