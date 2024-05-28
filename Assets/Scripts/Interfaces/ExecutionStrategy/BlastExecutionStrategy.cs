using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces.Strategy
{
    public class BlastExecutionStrategy : IExecutionStrategy
    {
        private bool _isRunning = true;

        public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
        {
            Sequence blastItemSquishSequence = DOTween.Sequence();

            foreach (var cellView in cellsToExecute)
            {
                var item = cellView.ItemInside;
                blastItemSquishSequence.Join(item.transform.DOScale(Vector3.one * .2f, .25f).SetEase(Ease.OutSine));
            }

            blastItemSquishSequence.OnComplete(() =>
            {
                foreach (var cellView in cellsToExecute)
                {
                    cellView?.Execute(ExecuteTypeEnum.Blast);
                }
                _isRunning = false;
            });

            yield return new WaitWhile(() => _isRunning);
        }
    }
}
