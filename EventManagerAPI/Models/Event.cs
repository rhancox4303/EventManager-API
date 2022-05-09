using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EventDate { get; set; }
   
        public string EventLocation { get; set; }
        public string Summary { get; set; }

    }
}
