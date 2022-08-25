using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SiltStrider.Primitives;
using SiltStrider.Records;

namespace SiltStrider.Gh.Nodes
{
    public class CreateCells : GH_Component
    {
        public CreateCells() : base(
                name: "Cells from Instances",
                nickname: "Cell",
                description: "Create a list of cells from an array of instances",
                category: "Silt Strider",
                subCategory: "Records"
            
            )
        {

        }

        public override bool IsPreviewCapable => true;

        public override Guid ComponentGuid => new Guid("FE40D277-233E-4639-A897-C3E74E11F392");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddParameter(new SubRecordParameter(),"Instances", "I", "", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new RecordParameter(), "Cells", "C", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<GH_SubRecord> subrecrods = new List<GH_SubRecord>();
            DA.GetDataList(0, subrecrods);

            var cells = new Dictionary<Long2,Cell>();

            foreach (var subRecord in subrecrods)
            {
                if (subRecord.Value is Instance instance)
                {
                    var x = (long)Math.Floor(instance.Position.X / SiltStrider.Globals.CellSize);
                    var y = (long)Math.Floor(instance.Position.Y / SiltStrider.Globals.CellSize);

                    var i = new Long2(x, y);

                    if (cells.TryGetValue(i, out var cell))
                    {
                        cells[i].References.Add(instance);
                    }
                    else
                    {
                        cells.Add(i, new Cell(x, y, Region.BitterCoastRegion, new List<Instance>() { instance }));
                    }

                }
                else throw new Exception("Subrecords must be instances");
            }

            DA.SetDataList(0, cells.Select(x=>new GH_Record() { Value = x.Value }));

        }
    }
}
