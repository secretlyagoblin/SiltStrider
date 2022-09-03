using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using SiltStrider.Records;

namespace SiltStrider.Gh.Nodes
{
    public class CreatePlugin : GH_Component
    {
        public CreatePlugin() : base(
                name: "Create Plugin",
                nickname: "ESP",
                description: "Create a plugin from a set of records",
                category: "Silt Strider",
                subCategory: "Records"

            )
        {

        }

        public override bool IsPreviewCapable => true;

        public override Guid ComponentGuid => new Guid("3C6C2940-689E-4036-9146-335468B83F68");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddParameter(new RecordParameter(), "Records", "R", "", GH_ParamAccess.list);
            pManager.AddTextParameter("Name", "N", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Plugin", "ESP", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var records = new List<GH_Record>();
            string name = "";

            DA.GetDataList(0, records);
            DA.GetData(1,ref name);

            var plugin = new ES3Document();

            foreach (var r in records)
            {
                plugin.Records.Add(r.Value);
            }

            plugin.Name = name;

            DA.SetData(0, plugin.ToYaml());
        }
    }
}
