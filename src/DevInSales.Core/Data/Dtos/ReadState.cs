using DevInSales.Core.Entities;

namespace DevInSales.Core.Data.Dtos
{
    public class ReadState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public List<ReadStateCities> Cities { get; set; }  

    }
}