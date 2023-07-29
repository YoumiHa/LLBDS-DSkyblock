//using Command Namesapce
using LiteLoader.DynamicCommand;
using LiteLoader.Logger;
using MC;
using System.Collections.Generic;

namespace ExamplePlugin.Examples;

public class ExampleDynamicCommand
{
    private static readonly Logger logger = new("ExampleCommand");
    public void Execute()
    {
        DynamicCommandInstance instance = DynamicCommand.CreateCommand("dytest", ".NET test command", CommandPermissionLevel.Any);
        instance.SetAlias("aliafortest");
        instance.AddOverload(new List<string>());
        instance.SetCallback((cmd, origin, output, results) =>
        {
          
            output.Success("successful");
        });
        DynamicCommand.Setup(instance);
    }
}
