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

        public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {
            OnItemExecute?.Invoke();
            Destroy(gameObject);
        }
    }
}
