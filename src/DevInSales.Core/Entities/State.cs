using System.Text.Json.Serialization;
using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Entities
{
    public class State : Entity
    {
        public string Name { get; private set; }
        public string Initials { get; private set; }
        public List<City> Cities { get; set; }

        public State(int id, string name, string initials)
        {
            Id = id;
            Name = name;
            Initials = initials;
        }

        public ReadState ToReadState(State state)
        {
            return new ReadState
            {
                Id = state.Id,
                Name = state.Name,
                Initials = state.Initials,
                Cities = state.Cities
                    .Select(c => new ReadStateCities { Id = c.Id, Name = c.Name })
                    .ToList()
            };
        }
    }
}
