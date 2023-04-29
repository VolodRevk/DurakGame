using ConsoleApp2;
using System;

namespace ConsoleApp1
{
    public class Player 
    {
        public List<string> cards = new List<string>();
        private List<string> _convertedCardstoTypes = new List<string>();
        public List<string> possibleBeats = new List<string>();
        public List<string> possibleThrowUps = new List<string>();
        public char trumpP = ' ';
        public bool bot = false;
        public bool beaten = false;
        public bool thrown = false;
        public Player() { }
        public void TakeCards(List<string> Cards)
        {
            if (Cards.Count() != 0)
            {
                while (cards.Count() < 6)
                {
                    var rnd = new Random();
                    var randomCard = rnd.Next(0, Cards.Count);
                    for (var i = 0; i < Cards.Count(); i++)
                    {
                        if (i == randomCard)
                        {
                            cards.Add(Cards[i]);
                            Cards.Remove(Cards[i]);
                        }
                    }
                }
            }
        }
        private void ConvertCardsToTypes()
        {
            _convertedCardstoTypes.Clear();
            for (var i = 0; i < cards.Count(); i++)
            {
                if (cards[i].Remove(cards[i].Length - 1) == "11")
                {
                    _convertedCardstoTypes.Add("J" + cards[i].Last());
                }
                else if (cards[i].Remove(cards[i].Length - 1) == "12")
                {
                    _convertedCardstoTypes.Add("Q" + cards[i].Last());
                }
                else if (cards[i].Remove(cards[i].Length - 1) == "13")
                {
                    _convertedCardstoTypes.Add("K" + cards[i].Last());
                }
                else if (cards[i].Remove(cards[i].Length - 1) == "14")
                {
                    _convertedCardstoTypes.Add("A" + cards[i].Last());
                }
                else
                {
                    _convertedCardstoTypes.Add(cards[i]);
                }
            }
        }
        public void PrintCards()
        {
            ConvertCardsToTypes();
            Console.Write("Cards: ");
            for (var i = 0; i < _convertedCardstoTypes.Count(); i++)
            {
                Console.Write($"{_convertedCardstoTypes[i]} ");
            }
            Console.WriteLine();
            Console.Write("       ");
            for (var i = 0; i < _convertedCardstoTypes.Count(); i++)
            {
                Console.Write($"[{i}]");
            }
            Console.WriteLine();
        }
        private int ConvertCardToInt(string card)
        {
            int value = 0;
            return value = Int32.Parse(card.Remove(card.Length - 1));
        }
        public int CheckCardsOnMinTrump()
        {
            var min = 15;
            List<int> trumpCards = new List<int>();
            for (var i = 0; i < cards.Count(); i++)
            {
                if (cards[i].Last() == trumpP)
                {
                    trumpCards.Add(ConvertCardToInt(cards[i]));
                }
            }
            for (var i = 0; i < trumpCards.Count(); i++)
            {
                if (trumpCards[i] < min)
                {
                    min = trumpCards[i];
                }
            }
            if (min == 15)
            {
                min = 0;
            }
            return min;
        }
        public int AskCard()
        {
            var card = 36;
            while (card > cards.Count() - 1)
            {
                Console.Write($"Choose the card(0-{cards.Count() - 1}): ");
                card = Convert.ToInt32(Console.ReadLine());
            }
            return card;    
        }
        public void Attack(List<string> battlefieldP)
        {
            beaten = false;
            var attackCard = AskCard();  
            for (var i = 0; i < cards.Count(); i++)
            {
                if (i == attackCard)
                {
                    battlefieldP.Add(cards[i]);
                    cards.Remove(cards[i]);
                }
            }
        }
        public void CheckBeatCases(List<string> battlefieldP)
        {
            possibleBeats.Clear();
            var beatCard = battlefieldP.Last();
            for (var i = 0; i < cards.Count(); i++)
            {
                if (cards[i].Last() == beatCard.Last() && ConvertCardToInt(cards[i]) > ConvertCardToInt(beatCard))
                {
                    possibleBeats.Add(cards[i]);
                }
                else if (beatCard.Last() != trumpP && cards[i].Last() == trumpP)
                {
                    possibleBeats.Add(cards[i]);
                }
            }
        }
        public void Beat(List<string> battlefieldP)
        {
            beaten = false;
            CheckBeatCases(battlefieldP);
            var beatCard = AskCard();
            for (var i = 0; i < cards.Count(); i++)
            {
                if (i == beatCard)
                {
                    for (var y = 0; y < possibleBeats.Count(); y++)
                    {
                        if (possibleBeats[y] == cards[i])
                        {
                            battlefieldP.Add(cards[i]);
                            cards.Remove(cards[i]);
                            beaten = true;
                            break;
                        }
                    }
                }
            }
            if (!beaten)
            {
                Console.WriteLine("Wrong card");
            }
        }
        public void Take(List<string> battlefieldP)
        {
            for (var i = 0; i < battlefieldP.Count(); i++)
            {
                cards.Add(battlefieldP[i]);
            }
        }
        public void CheckThrowUpCases(List<string> battlefieldP)
        {
            possibleThrowUps.Clear();
            for (var i = 0; i < cards.Count(); i++)
            {
                for (var y = 0; y < battlefieldP.Count(); y++)
                {
                    if (cards[i].Remove(cards[i].Length - 1) == battlefieldP[y].Remove(battlefieldP[y].Length - 1))
                    {
                        possibleThrowUps.Add(cards[i]);
                    }
                }
            }
        }
        public void ThrowUp(List<string> battlefieldP)
        {
            thrown = false;
            var attackCard = AskCard();
            CheckThrowUpCases(battlefieldP);
            for (var i = 0; i < cards.Count(); i++)
            {
                if (i == attackCard)
                {
                    for (var y = 0; y < possibleThrowUps.Count(); y++)
                    {
                        if (cards[i] == possibleThrowUps[y])
                        {
                            battlefieldP.Add(cards[i]);
                            cards.Remove(cards[i]);
                            thrown = true;
                            break;
                        }
                    }
                }
            }
            if (!thrown)
            {
                Console.WriteLine("Wrong card");
                thrown = false;
            }
        }
    }
}
