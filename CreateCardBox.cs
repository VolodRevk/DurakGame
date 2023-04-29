using System;

namespace ConsoleApp2
{
    public class CreateCardBox
    {
        public CreateCardBox(List<string> Cards)
        {
           Create(Cards);
        }
        private void Create(List<string> cards)
        {
            for (var i = 6; i <= 14; i++)
            {
               cards.Add(i + "♠");
               cards.Add(i + "♥");
               cards.Add(i + "♦");
               cards.Add(i + "♣");
            }
        }
    }
}
