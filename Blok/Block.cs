using System;
using System.Security.Cryptography;
using System.Text;

namespace Blok
{
    public class Block
    {
        private readonly int Index;
        private DateTime TimeStamp;
        private string Hash;
        private readonly string PreviousHash;
        private readonly string Data;
        private int MiningTests;

        public Block(int index, DateTime date, string previousHash, string data)
        {
            Index = index;
            TimeStamp = date;
            PreviousHash = previousHash;
            Data = data;
            MiningTests = 0;
            Hash = calculateHash(this);
        }

        public int getIndex()
        {
            return Index;
        }

        public DateTime getTimestamp()
        {
            return TimeStamp;
        }

        public string getHash()
        {
            return Hash;
        }

        public string getPreviousHash()
        {
            return PreviousHash;
        }

        public string getData()
        {
            return Data;
        }

        public string str()
        {
            string s = Index + TimeStamp.ToString() + PreviousHash + Data + MiningTests;
            return s;
        }

        public string BlocktoString()
        {
            string ReturnStr = "Block #" + Index +
                            " [previousHash : " + PreviousHash +
                            ", timestamp : " + TimeStamp +
                            ", data : " + Data +
                            ", hash : " + Hash + "]";
            return ReturnStr;
        }

        public static string calculateHash(Block block)
        {
            HashAlgorithm digest;

            digest = HashAlgorithm.Create("SHA-256");

            if (block != null)
            {
                string Text = block.str();
                byte[] bytes = Encoding.ASCII.GetBytes(Text);

                var lol = digest.ComputeHash(bytes);

                StringBuilder StrBuilder = new StringBuilder();

                foreach (var b in lol)
                {
                    string hex = b.ToString("x8");

                    if (hex.Length == 1)
                    {
                        StrBuilder.Append('0');
                    }

                    StrBuilder.Append(hex);
                }

                return StrBuilder.ToString();
            }

            return null;
        }

        public void mineBlock(int difficulty)
        {
            int MineTests = 0;

            while (!getHash().Substring(0, difficulty).Equals(Utils.zeros(difficulty)))
            {
                MineTests++;
                Hash = Block.calculateHash(this);
            }
        }
    }
}