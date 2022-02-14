using System.Text.Json.Serialization;

namespace testidentityandjwt.BL.DTO
{
    public class TodoDTO//DTO BASE TODO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime Created { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

    }

}
