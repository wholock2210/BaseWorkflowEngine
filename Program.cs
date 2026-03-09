using Core.Abstractions;
using Core.Engine;
using Modules;
using Core.Models;
using Core.Context;
using Core.ValueProviders;
using Pack;
using Core.Conditions;

class Program
{
    static void Main()
    {
        var workflow = new WorkflowDefinition
        {
            StartNodeId = "init",
            Connections = new List<ConnectionDefinition>
            {
                new() { From = "init", To = "connect" },
                new() { From = "connect", To = "if"},
                new() { From = "if", To = "connect-success", Branch = "true"},
                new() { From = "if", To = "connect-failed", Branch = "false"},
                new() { From = "connect-success", To = "end-success"},
                new() { From = "connect-failed", To = "end-failed"},
            }
        };

        var nodes = new Dictionary<string, IWorkflowNode>
        {
            ["init"] = new InitNode("init"),
            ["connect"] = new Pack.Database.Modules.ConnectionNode("init", "Server=127.0.0.1;User ID=wholock;Password=2210042;Database=OtoParking;"),
        };

        var context = new WorkflowContext();
        var executor = new WorkflowExecutor();

        executor.Run(workflow, nodes, context);
    }
}