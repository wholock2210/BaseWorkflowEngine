using Core.Abstractions;
using Core.Engine;
using Modules;
using Core.Models;
using Core.Context;
using Core.ValueProviders;
using Pack;
using Core.Conditions;
using Pack.Database.Modules;

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
        var context = new WorkflowContext();
        var executor = new WorkflowExecutor();

        var nodes = new Dictionary<string, IWorkflowNode>
        {
            ["init"] = new InitNode("init"),
            ["connect"] = new Pack.Database.Modules.ConnectionNode("connect", "Server=127.0.0.1;User ID=wholock;Password=221004;Database=OtoParking;"),
            ["if"] = new IfNode<Boolean>("if", new CompareCondition<bool>(
                                                    new BooleanValueProvider("connect"),
                                                    new BooleanValueProvider(true),
                                                    CompareOperator.Equal)),
            ["connect-success"] = new ReadSchemaNode("connect-success"),
            ["connect-failed"] = new NoOperationNode("connect-failed"),
            ["end-success"] = new EndNode("end-success", "Kết nối thành công"),
            ["end-failed"] = new EndNode("end-failed", "Kết nối thất bại"),

        };

        
        executor.Run(workflow, nodes, context);
    }
}