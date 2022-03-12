using System.Text.Json.Serialization;

namespace testidentityandjwt.BL.DTO
{
    public class TodoDTO//DTO BASE TODO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime Created { get; set; }

        public bool isTodoDone { get; set; } = false;

        public string Description { get; set; } = string.Empty;

        public DateTime Lastmodified { get; set; }

        public string UserId { get; set; }=Guid.NewGuid().ToString();

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

    }

}
