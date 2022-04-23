namespace DevInSales.Core.Entities
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public decimal SuggestedPrice { get; private set; }

        public Product(string name, decimal suggestedPrice)
        {
            Name = name;
            SuggestedPrice = suggestedPrice;
        }
    }
}