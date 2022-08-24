using System.Collections.Generic;

namespace SiltStrider.Records
{
    public abstract class SubRecord
    {
        protected SubRecord()
        {

        }

        //public Record Parent { get; set; }

        abstract public string Type { get; }



    }
}