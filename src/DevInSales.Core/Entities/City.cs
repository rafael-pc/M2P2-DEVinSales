namespace DevInSales.Core.Entities
{
    public class City
    {
        public int Id { get; private set; }
        public int StateId { get; private set; }
        public string Name { get; private set; }

        public City(int stateId, string name)
        {
            StateId = stateId;
            Name = name;
        }
    }
}