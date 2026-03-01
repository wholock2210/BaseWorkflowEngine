using Core.Abstractions;
using Core.Engine;
using Modules;
using Core.Models;
using Core.Context;
using Core.ValueProviders;

class Program
{
    static void Main()
    {
        var workflow = new WorkflowDefinition
        {
            StartNodeId = "init",
            Connections = new List<ConnectionDefinition>
            {
                new() { From = "init", To = "val-a" },
                new() { From = "val-a", To = "val-b" },
                new() { From = "val-b", To = "loop"},
                new() { From = "loop", To = "into-loop", Branch = "loop" },
                new() { From = "loop", To = "exit-loop", Branch = "exit" },
                new() { From = "into-loop", To = "inc-a"},
                new() { From = "inc-a" , To = "loop"},
                new() { From = "exit-loop", To = "end"}
            }
        };

        var nodes = new Dictionary<string, IWorkflowNode>
        {
            ["init"] = new InitNode("init"),
            ["val-a"] = new CreateValNode("val-a","X","15"),
            ["val-b"] = new CreateValNode("val-b","Y","10"),
            ["loop"] = new LoopNode("loop", new IfNode(
                        "ifloop",
                        new Core.Conditions.CompareCondition(
                        new ContextValueProvider("X"),
                        new ConstantValueProvider(16),
                        Core.Conditions.CompareOperator.LessOfEqual
                    )
                )
            ),
            ["into-loop"] = new NothingNode("into-loop"),
            ["inc-a"] = new IncreaseNode("inc-a",new ContextValueProvider("X")),
            ["exit-loop"] = new PrintNode("exit-loop", new ContextValueProvider("X")),
            ["end"] = new EndNode("end", "ket thuc chuong trinh")
        };

        var context = new WorkflowContext();
        var executor = new WorkflowExecutor();

        executor.Run(workflow, nodes, context);
    }
}