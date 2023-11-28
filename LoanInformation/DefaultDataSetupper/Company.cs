namespace DefaultDataSetupper
{
    public class Company
    {
        public Company(int id, string name, string ui)
        {
            Id = id;
            Name = name;
            Ui = ui;
        }

        public int Id { get; }

        public string Name { get; }

        public string Ui { get; }
    }
}
