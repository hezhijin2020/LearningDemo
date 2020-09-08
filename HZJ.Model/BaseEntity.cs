using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.Model
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;
    }
}
