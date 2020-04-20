using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTest.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
