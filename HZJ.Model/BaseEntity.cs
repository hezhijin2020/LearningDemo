using System;

namespace HZJ.Model
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;
    }
}
