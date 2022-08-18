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

        public override Guid ComponentGuid => new Guid("973512C4 - 5478 - 48EA - 846E-DFDA05258D31");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Transform", "T", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Scale", "S", "", GH_ParamAccess.item,1.0);
            pManager.AddTextParameter("BlockName","N","", GH_ParamAccess.item,)
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new RecordParameter(), "Record", "R", "", GH_ParamAccess.item);
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
            var rotation = transform.GetEulerZYZ(out var u, out var v, out var w);


            var heights = new List<int>();

            DA.GetDataList(2, heights);

            //TODO - store as double until the last minute??? or something??

            //Also could just store in world space and then dynamically drop it into cells depending on bounds

            var instance = new Instance("a", 2)
            {
                Position = p.Value.Origin.ToMorrow(),
                Rotation = new Primitives.Float3((float)u, (float)v, (float)w),
                Scale = (float)scale,
                Block = block
            };

            DA.SetData(0, new GH_Record() { Value = instance });

        }
    }
}
