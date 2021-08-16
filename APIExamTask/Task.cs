using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace API
{
    class Task
    {

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TaskBoard board { get; set; }
        public string dateModified { get; set; }
        public string dateCreated { get; set; }
        public string actions { get; set; }

        public override string ToString()
        {
            return "Task: " + JsonConvert.SerializeObject(this, Formatting.Indented);

        }
    }
}
