using System.ComponentModel.DataAnnotations;

namespace ImpListScheduler
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly EndDate { get; set; }
    }
}