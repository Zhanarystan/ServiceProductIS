namespace API.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KzName { get; set; }
        public string RuName { get; set; }
        public bool IsCustom { get; set; }
        public string Description { get; set; }
    }
}