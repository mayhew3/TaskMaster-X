using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TaskMaster
{
    public class TodoItem
    {
        public UInt32 id { get; set; }
        public string name { get; set; }
        public UInt32 person_id { get; set; }
        public DateTime date_added { get; set; }
        public DateTime? date_completed { get; set; }

    }
}