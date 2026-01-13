namespace SheepSheep
{
    public class BlockClickEvent
    {
        public BlockController block;

        public BlockClickEvent(BlockController block)
        {
            this.block = block;
        }
    }
}