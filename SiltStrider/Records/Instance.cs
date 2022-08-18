using SiltStrider.Primitives;

namespace SiltStrider.Records
{
    public class Instance : Record
    {
        public float Scale { get; set; } = 1f;
        public Float3 Position { get;set;}
        public Float3 Rotation { get;set;}
        public string Block { get; set; } = "flora_tree_gl_02";

        public override string Type => "ReferencedObject";

        public Instance(string modName, int index) : base($"::{modName}::cellref::{index}")
        {
            throw new System.NotImplementedException();
        }
    }
}