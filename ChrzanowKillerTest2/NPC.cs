using System;

namespace NPCProximity
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerX = 0, playerY = 0;
            int npcX = 3, npcY = 3;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Player Position: ({playerX}, {playerY})");
                Console.WriteLine($"NPC Position: ({npcX}, {npcY})");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (Math.Abs(playerX - npcX) + Math.Abs(playerY - npcY) <= 2)
                {
                    Console.WriteLine("Poprosze monety ");
                }
            }
        }
    }
}