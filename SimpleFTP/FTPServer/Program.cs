using SimpleFTP;

Server server = new SimpleFTP.Server("C:/", 8888);
        
await server.Start();