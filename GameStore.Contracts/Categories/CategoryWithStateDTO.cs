namespace GameStore.Contracts.Categories
{
    public class CategoryWithStateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public CategoryWithStateDTO()
        { }

        public CategoryWithStateDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryWithStateDTO(Guid id, string name, bool isChecked)
        {
            Id = id;
            Name = name;
            IsChecked = isChecked;
        }
    }
}
