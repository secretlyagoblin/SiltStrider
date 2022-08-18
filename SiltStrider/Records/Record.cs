using System.Collections.Generic;

namespace SiltStrider.Records
{
    public abstract class Record
    {
        protected Record()
        {

        }

        protected Record(string name)
        {
            Name = name;
        }

        abstract public string Type { get; }
        public string Name { get; set; }



    }
}