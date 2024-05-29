using Gameplay;
using System.Collections;
using System.Collections.Generic;

namespace Interfaces.Strategy
{
    public interface IExecutionStrategy
    {
        IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute, int executionIndex);
    }
}
