using System.ComponentModel;
using ModelContextProtocol.Server;

namespace McpStarter.Tools;

[McpServerToolType]
public class HelloWorldTool
{
    [McpServerTool]
    [Description("Tells the secret message")]
    public string GetSecretMessage()
    {
        return "Hello, World with code 8751!";
    } 
}