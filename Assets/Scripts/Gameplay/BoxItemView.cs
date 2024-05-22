using Enums;

namespace Gameplay
{
    public class BoxItemView : ObstacleItemView
    {
        public override void Init(MatchTypeEnum matchType)
        {
            ItemType = ItemTypeEnum.BoxItem;
            MatchType = matchType;
        }

        public override void Execute(ExecuteTypeEnum executeType)
        {
            if (executeType == ExecuteTypeEnum.Special)
            {
                OnItemExecute?.Invoke();
                Destroy(gameObject);
            }
        }

        public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {
            if (executeType == ExecuteTypeEnum.Blast)
            {
                OnItemExecute?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
