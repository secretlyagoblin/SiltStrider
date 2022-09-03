using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SiltStrider.Records;

namespace SiltStrider.Gh.Nodes
{
    public class CreateInstance : GH_Component
    {
        public CreateInstance() : base(
                name: "Create Instance",
                nickname: "Frmr",
                description: "Create an instance of an object",
                category: "Silt Strider",
                subCategory: "Records"
            
            )
        {

        }

        public override bool IsPreviewCapable => true;

        public override Guid ComponentGuid => new Guid("973512C4-5478-48EA-846E-DFDA05258D31");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Transform", "T", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Scale", "S", "", GH_ParamAccess.item,1.0);
            pManager.AddTextParameter("BlockName", "N", "", GH_ParamAccess.item, "");
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new SubRecordParameter(), "SubRecord", "SR", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Plane p = null;
            var scale = 1.0;
            var block = "";
            DA.GetData(0, ref p);
            DA.GetData(1, ref scale);
            DA.GetData(2, ref block);

            var transform = Rhino.Geometry.Transform.PlaneToPlane(Rhino.Geometry.Plane.WorldXY, p.Value);

            // As I understand it, we break the compound translation/rotation transformation into its two components
            transform.DecomposeRigid(out var position, out var rotation, Rhino.RhinoMath.ZeroTolerance);
            // Now that the rotation is 'pure' we can break it down into a euler rotation
            // I've made the out variables what I think that they coordinate with
            rotation.GetYawPitchRoll(out var z, out var y, out var x);

            var instance = new Instance()
            {
                Position = position.ToMorrow(),
                Rotation = new Primitives.Float3((float)x, (float)y, (float)z),
                Scale = (float)scale,
                Block = block
            };

            DA.SetData(0, new GH_SubRecord() { Value = instance });

        }
    }
}
