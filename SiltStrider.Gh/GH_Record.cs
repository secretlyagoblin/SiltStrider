using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using SiltStrider.Records;
using SiltStrider.Conversion;
using Grasshopper.Kernel;

namespace SiltStrider.Gh
{
    public class GH_Record : GH_Goo<Record>, Grasshopper.Kernel.IGH_PreviewMeshData, Grasshopper.Kernel.IGH_PreviewObject
    {
        public override bool IsValid => true;

        public override string TypeName => "Record";

        public override string TypeDescription => "Record for generating Morrowind files";

        public bool Hidden { get; set; }

        public bool IsPreviewCapable => true;

        public BoundingBox ClippingBox
        {
            get
            {
                var bb = BoundingBox.Empty;

                foreach (var mesh in _meshes)
                {
                    bb.Union(mesh.GetBoundingBox(false));
                }

                return bb;
            }
        } 

        public void DestroyPreviewMeshes()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Dispose();
            }

            _meshes = new Mesh[0];
        }

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            foreach (var mesh in GetPreviewMeshes())
            {
                args.Display.DrawMeshShaded(mesh, args.ShadeMaterial);
            }           
        }

        public void DrawViewportWires(IGH_PreviewArgs args)
        {

        }

        public override Record Value { get { return base.Value; } set { base.Value = value; _meshes = GetPreviewMeshes(); } }

        private Mesh[] _meshes = new Mesh[0];

        public override IGH_Goo Duplicate()
        {
            throw new System.NotImplementedException();
        }
        public Mesh[] GetPreviewMeshes()
        {
            var meshSize = SiltStrider.Globals.CellSize * SiltStrider.Globals.UnitToMeterRatio;

            if (Value is Landscape land)
            {     
                var mesh = Rhino.Geometry.Mesh.CreateFromPlane(
                    Plane.WorldXY,
                    new Interval(0, meshSize),
                    new Interval(0, meshSize),
                    64,
                    64);

                var count = 0;

                foreach (var zValue in land.Heightmap.ToBytes())
                {
                    var height = zValue * Globals.UnitToMeterRatio;

                    var vert = mesh.Vertices[count];
                    mesh.Vertices.SetVertex(count++, vert.X, vert.Y, height + land.VerticalDatum);
                }

                mesh.Translate(meshSize * land.Location.X, meshSize * land.Location.Y, 0);

                return new Mesh[] { mesh };

            }

            return new Mesh[0];
        }
        public override string ToString() => Value.Name;
    }
}