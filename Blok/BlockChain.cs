using System;
using System.Collections.Generic;
using System.Linq;

namespace Blok
{
    internal class Blockchain
    {
        private int difficulty;
        private List<Block> blocks;

        public Blockchain(int difficulty)
        {
            this.difficulty = difficulty;
            blocks = new List<Block>();
            // create the first block
            Block b = new Block(0, DateTime.Now, null, "First Block");
            b.mineBlock(difficulty);
            blocks.Add(b);
        }

        public int getDifficulty()
        {
            return difficulty;
        }

        public Block latestBlock()
        {
            return blocks.Last();
        }

        public Block newBlock(string data)
        {
            Block LastBlock = latestBlock();
            Block NewBlock = new Block(LastBlock.getIndex() + 1,
                DateTime.Now, LastBlock.getHash(), data);
            return NewBlock;
        }

        public void showInfo()
        {
            foreach (var b in blocks)
            {
                Console.WriteLine(b.BlocktoString());
            }
        }

        public void addBlock(Block b)
        {
            if (b != null)
            {
                b.mineBlock(difficulty);
                blocks.Add(b);
            }
        }

        public bool isFirstBlockValid()
        {
            Block firstBlock = blocks[0];

            if (firstBlock.getIndex() != 0)
            {
                return false;
            }

            if (firstBlock.getPreviousHash() != null)
            {
                return false;
            }

            if (firstBlock.getHash() == null ||
                !Block.calculateHash(firstBlock).Equals(firstBlock.getHash()))
            {
                return false;
            }

            return true;
        }

        public bool isValidNewBlock(Block newBlock, Block previousBlock)
        {
            if (newBlock != null && previousBlock != null)
            {
                if (previousBlock.getIndex() + 1 != newBlock.getIndex())
                {
                    return false;
                }

                if (newBlock.getPreviousHash() == null ||
                    !newBlock.getPreviousHash().Equals(previousBlock.getHash()))
                {
                    return false;
                }

                if (newBlock.getHash() == null ||
                    !Block.calculateHash(newBlock).Equals(newBlock.getHash()))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool isBlockChainValid()
        {
            if (!isFirstBlockValid())
            {
                return false;
            }

            for (int i = 1; i < blocks.Count; i++)
            {
                Block currentBlock = blocks[i];
                Block previousBlock = blocks[i - 1];
                if (!isValidNewBlock(currentBlock, previousBlock))
                {
                    return false;
                }
            }
            return true;
        }
    }
}