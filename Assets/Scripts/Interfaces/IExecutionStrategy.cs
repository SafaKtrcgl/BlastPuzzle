using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;

public interface IExecutionStrategy
{
    IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute);
}
