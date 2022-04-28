using System.Text.Json.Serialization;
using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Entities
{
    public class City : Entity
    {
        public int StateId { get; private set; }
        public string Name { get; private set; }
        public State State { get; set; }
        [JsonIgnore]
        public List<Address> Addresses { get; private set; }

        public City(int stateId, string name)
        {
            StateId = stateId;
            Name = name;
        }

        
        public ReadCity ToReadCity(City city)
        {
            return new ReadCity
            {
                Id = city.Id,
                Name = city.Name,
                State = new ReadCityState {Id = city.State.Id, Name = city.State.Name, Initials = city.State.Initials} 
            };
        }
    }
}