using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace SiltStrider.Gh.Nodes
{
    public class Landscape : GH_Component
    {
        public override Guid ComponentGuid => new Guid("5CC037ED-1D92-4FD6-8CAE-00E595EE81D5");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("X", "X", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Y", "Y", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Heights", "H", "", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new RecordParameter(), "Record", "R", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var x = 0;
            var y = 0;
            DA.GetData(0, ref x);
            DA.GetData(1, ref y);

            var heights = new List<int>();

            DA.GetDataList(2, heights);

            if(heights.Count != 65*65)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"A Landscape record requires exactly {65 * 65} heights (65*65)");
                DA.AbortComponentSolution();
            }

            var landscape = new LandscapeRecord(x,y,heights);

            DA.SetData(0, new GH_Record() { Value = landscape });


        }
    }
}
