using System.Collections;

public class ExecutionNode
{
    public IEnumerator ExecutionEnumerator;
    public int ExecutionIndex;

    public ExecutionNode(IEnumerator executionEnumerator, int executionIndex)
    {
        ExecutionEnumerator = executionEnumerator;
        ExecutionIndex = executionIndex;
    }
}
