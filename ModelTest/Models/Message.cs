using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTest.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public Theme Theme { get; set; }
    }
}
