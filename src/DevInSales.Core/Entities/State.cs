namespace DevInSales.Core.Entities
{
    public class State
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Initials { get; private set; }

        public State(string name, string initials)
        {
            Name = name;
            Initials = initials;
        }
    }
}