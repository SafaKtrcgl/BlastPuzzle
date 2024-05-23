using Enums;

namespace Gameplay
{
    public abstract class SpecialItemView : ItemView
    {
        public override void Init(MatchTypeEnum matchType)
        {
            IsSpecial = true;
            IsMatchable = true;

            MatchType = matchType;
        }

        public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {

        }
    }
}
