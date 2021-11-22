using System;

namespace Blok
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain(5);
            blockchain.addBlock(blockchain.newBlock("Tout sur le Bitcoin"));
            blockchain.addBlock(blockchain.newBlock("Sylvain Saurel"));
            blockchain.addBlock(blockchain.newBlock("https://www.toutsurlebitcoin.fr"));

            Console.WriteLine("Blockchain valid ? " + blockchain.isBlockChainValid());

            blockchain.showInfo();

            Console.ReadLine();
        }
    }
}