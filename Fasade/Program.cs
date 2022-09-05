using Fasade;

var subsystem1 = new Subsystem1();
var subsystem2 = new Subsystem2();
var facade = new Facade(subsystem1, subsystem2);
var client = new Client();
client.ClientCode(facade);
