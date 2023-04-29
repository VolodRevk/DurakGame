using ConsoleApp2;
using System;

namespace ConsoleApp1
{
    public class GameDurak
    {
        private List<string> Cards = new List<string>(36);
        private List<string> Battlefield = new List<string>();
        private char _trump;
        private bool _done = false;
        public GameDurak()
        {
            var CardBox = new CreateCardBox(Cards);
            Trump();
            var player1 = new Player();
            var player2 = new Player();
            player1.trumpP = _trump;
            player2.trumpP = _trump;
            player1.TakeCards(Cards);
            player2.TakeCards(Cards);
            if (CompareMinTrumps2P(player1, player2) == 1)
            {
                player2.beaten = true;
            }
            if (CompareMinTrumps2P(player1, player2) == 2)
            {
                player1.beaten = true;
            }
            Game2Players(player2, player1);
            PrintDurak(player1, player2);
                   
        }
        private void Trump()
        {
            var rnd = new Random();
            int randomCard = rnd.Next(0, Cards.Count());
            for (var i = 0; i < Cards.Count(); i++)
            {
                if (i == randomCard)
                {
                    _trump = Cards[i].Last();
                    var buffer = Cards[i];
                    Cards.RemoveAt(i);
                    Cards.Add(buffer);
                }
            }
        }
        private string ConvertToType(string card)
        {
            if (card.Remove(card.Length - 1) == "11")
            {
                card = "J" + card.Last();
            }
            else if (card.Remove(card.Length - 1) == "12")
            {
                card = "Q" + card.Last();
            }
            else if (card.Remove(card.Length - 1) == "13")
            {
                card = "K" + card.Last();
            }
            else if (card.Remove(card.Length - 1) == "14")
            {
                card = "A" + card.Last();
            }
            return card;
        }
        private static int CompareMinTrumps2P(Player player1, Player player2)
        {
            var value = 0;
            if (player1.CheckCardsOnMinTrump() > player2.CheckCardsOnMinTrump())
            {
                value = 1;
            }
            if (player1.CheckCardsOnMinTrump() < player2.CheckCardsOnMinTrump())
            {
                value = 2;
            }
            return value;
        }
        private void PrintInfo(int player)
        {
            Console.Clear();
            Console.WriteLine($"Trump: {_trump}");
            Console.WriteLine($"Player{player}");
            Console.WriteLine($"Cards:{Cards.Count()}");
            for (var i = 0; i < Battlefield.Count(); i++)
            {
                Console.Write($"{ConvertToType(Battlefield[i])}<--");
            }
            Console.WriteLine();
        }
        private char BeatOrTake()
        {
            var choice = 'T';
            while (true)
            {
                Console.Write("(B) - Beat, (T) - Take: ");
                choice = Convert.ToChar(Console.ReadLine());
                if (choice == 'B' || choice == 'T')
                {
                    break;
                }
            }
            return choice;
        }
        private char AttackOrPass()
        {
            var choice = ' ';
            while (true)
            {
                Console.Write("(A) - Attack, (P) - Pass: ");
                choice = Convert.ToChar(Console.ReadLine());
                if (choice == 'A' || choice == 'P')
                {
                    break;
                }
            }
            return choice;
        }
        private void Pass()
        {
            Battlefield.Clear();
            _done = true;
        }
        private void AttackCase(Player player)
        {
            player.PrintCards();
            player.Attack(Battlefield);
        }
        private void AttackAgainCase(Player player1, Player player2)
        {
            player1.PrintCards();
            var doneInside = false;
            while (!doneInside)
            {
                if (Battlefield.Count() < 11 && player2.cards.Count() > 0)
                {
                    if (AttackOrPass() == 'A')
                    {
                        player1.ThrowUp(Battlefield);
                        if (player1.thrown)
                        {
                            doneInside = true;
                        }
                    }
                    else
                    {
                        if (!player2.beaten)
                        {
                            player2.Take(Battlefield);
                            player1.beaten = true;
                        }
                        Pass();
                        doneInside = true;

                    }
                }
                else
                {
                    Console.WriteLine("Desk is overnumbered");
                    doneInside = true;
                }
            }
            if (player2.beaten)
            {
                player1.beaten = false;
            }
        }
        private void BeatCase(Player player)
        {
            var doneInside = false;
            var done = false;
            player.PrintCards();
            while (!doneInside)
            {
                if (player.cards.Count() == 0)
                {
                    doneInside = true;
                }
                if (BeatOrTake() == 'B')
                {
                    player.Beat(Battlefield);
                    if (player.beaten)
                    {
                        doneInside = true;
                    }
                }
                else
                {
                    player.beaten = false;
                    doneInside = true;
                }
            }
        }
        private void PrintDurak(Player player1, Player player2)
        {
            Console.Clear();
            if (player1.cards.Count() > 0)
            {
                Console.WriteLine("Player1-->Durak");
            }
            if (player2.cards.Count() > 0)
            {
                Console.WriteLine("Player2-->Durak");
            }
            if (player1.cards.Count() == 0 && player2.cards.Count() == 0)
            {
                Console.WriteLine("Drawn Game");
            }
        }
        private void Game2Players(Player player1, Player player2)
        {
            while (true)
            {
                _done = false;
                if (player1.cards.Count() == 0 || player2.cards.Count() == 0)
                {
                    break;
                }
                if (!player2.beaten)
                {
                    PrintInfo(1);
                    AttackCase(player1);
                    while (!_done)
                    {
                        PrintInfo(2);
                        BeatCase(player2);
                        PrintInfo(1);
                        AttackAgainCase(player1, player2);
                    }
                }
                else if (!player1.beaten)
                {
                    PrintInfo(2);
                    AttackCase(player2);
                    while (!_done)
                    {
                        PrintInfo(1);
                        BeatCase(player1);
                        PrintInfo(2);
                        AttackAgainCase(player2, player1);
                    }
                }
                player1.TakeCards(Cards);
                player2.TakeCards(Cards);
            }

        }
    }
}