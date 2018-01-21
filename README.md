# RMQ
WPF, Windows Presentation Foundation application developed for testing RabbitMQ

## Prerequisites
Erlan, RabbitMQ and .NET 4.5

### Code choices
A hybrid between MVVM and MVC.  
<br>
Model->MessageQueue->Stores the queue object  
private string name;  
//Durable (the queue will survive a broker restart)  
private bool durable;  
//Exclusive(used by only one connection and the queue will be deleted when that connection closes)  
private bool privateCon;  
//Auto-delete(queue that has had at least one consumer is deleted when last consumer unsubscribes)  
private bool delete; 
<br>
Send->Producer->Uses MessageQueue, connects to RMQ and sends data  
Send 1 packet or send many / a file, splitt by ;  
<br><br>
Recieve->Cosumer->  
