using Enums;

namespace Gameplay
{
    public abstract class SpecialItemView : ItemView
    {
        public override bool IsSpecial { get; set; } = true;
        public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {

        }
    }
}
