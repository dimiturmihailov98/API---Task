using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace API
{
    class TaskBoard
    {
        public int id {get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "TaskBoard: " + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
