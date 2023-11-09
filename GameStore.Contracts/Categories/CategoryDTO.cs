namespace GameStore.Contracts.Categories
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {

        }
        public CategoryDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
