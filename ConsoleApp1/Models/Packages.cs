namespace ConsoleApp1.Models
{
    public class Packages
    {
        public List<Card> package = [];
        public Packages(List<Card> package_cards)
        {
            Random rdm = new Random();
            int random = 0;
            if (package_cards.Count == 5)
            {

                package = package_cards;

            }
            else if (package_cards.Count > 5)
            {
                for (int i = 0; i < (package_cards.Count - 5); i++)
                {
                    package_cards.RemoveAt(random);
                }
                package = package_cards;
            }
            else
                Console.WriteLine("Not enough Cards!");
        }
    }
}
