# SocketChat
Client-Server application using Socket

# Starting server:
`int port = 8005;

string host = Dns.GetHostName();

IPAddress IP = Dns.GetHostAddresses(host)[1];

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP.ToString()), port);

var listenSocket = new CListeningSocket();

CPool cPool = new CPool(listenSocket);

listenSocket.Bind(ipPoint);

listenSocket.Listen(10);`

![image](https://user-images.githubusercontent.com/50167116/99145710-d248d000-2692-11eb-884d-e20298c9c495.png)


# Connecting to the server from the client:
`var ipPoint = new IPEndPoint(IPAddress.Parse("YOUR IP ADDRESS"), port);

CClientSocket socket = new CClientSocket();

CPool cPool = new CPool(socket);

cPool.Init(ipPoint);`


![image](https://user-images.githubusercontent.com/50167116/99145749-0fad5d80-2693-11eb-8917-752c90fef891.png)


# Receiving message in server:
`var res = cPool.ProcessAccept();

Log($"Received message: {res.StringResult}");`

![image](https://user-images.githubusercontent.com/50167116/99145758-25228780-2693-11eb-8f6a-bbb3c613a460.png)



# Sending a message from the server to the client:
` cPool.Send($"We received message {res.StringResult} from you. You are very COOOOl", res.handler);`
                 
![image](https://user-images.githubusercontent.com/50167116/99145789-60bd5180-2693-11eb-8856-54f990afd946.png)

# Receiving message of server in the client:
`cPool.ProcessRead()`

![image](https://user-images.githubusercontent.com/50167116/99145899-8b5bda00-2694-11eb-9877-03ae069f4785.png)


