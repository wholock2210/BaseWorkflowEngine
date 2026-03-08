namespace Core.Models
{
    public class WorkflowDefinition
    {
        public string StartNodeId {get;init;} = default!;

        public List<ConnectionDefinition> Connections {get;init;} = default!;

        public string? ResolveNext(string currentNodeId, string? branch)
        {
            return Connections.FirstOrDefault(c =>  
                c.From == currentNodeId &&
                (c.Branch == branch || c.Branch == null) 
            )?.To;
        }
    }
}   