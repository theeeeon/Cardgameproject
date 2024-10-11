namespace ConsoleApp1.Models
{
    internal class Packages
    {
        private Card[] package = new Card[5];
        public Packages(List<Card> package_cards)
        {
            Random rdm = new Random();
            int random = 0;
            if (package_cards.Count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    package[i] = package_cards[i];
                }
            }
            else if (package_cards.Count > 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    random = rdm.Next(0, package_cards.Count);
                    package[i] = package_cards[random];
                    package_cards.RemoveAt(random);
                }
            }
            else
                Console.WriteLine("Not enough Cards!");
        }
    }
}
