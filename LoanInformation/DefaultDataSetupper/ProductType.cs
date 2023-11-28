namespace DefaultDataSetupper
{
    public class ProductType
    {
        public ProductType(
            int id,
            string type,
            string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }

        public int Id { get; }

        public string Type { get; }

        public string Name { get; }
    }
}