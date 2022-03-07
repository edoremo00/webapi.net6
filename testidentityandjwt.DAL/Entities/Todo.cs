using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.DAL.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get;set; }=string.Empty;
        public string Description { get;set; } = string.Empty;
        public DateTime CreationDate { get;set; }
        public DateTime LastModifiedDate { get;set;}

        public bool isDone { get;set; }=false;
        public bool istodoDeleted { get; set; } = false;

        public MyUser? User { get; set; }

        public string? UserId { get; set; }

    }
}
