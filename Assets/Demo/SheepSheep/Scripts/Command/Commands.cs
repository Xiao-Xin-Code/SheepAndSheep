using QFramework;

namespace SheepSheep
{
    public class BlockClickCommand : AbstractCommand
    {
        BlockController block;

        public BlockClickCommand(BlockController block)
        {
            this.block = block;
        }

        protected override void OnExecute()
        {

            this.SendEvent(new BlockClickEvent(block));
        }
    }
}
