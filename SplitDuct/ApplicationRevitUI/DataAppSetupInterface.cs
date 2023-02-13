using Autodesk.Revit.UI;
using SplitDuct.Model;
using SplitDuct.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct.ApplicationRevitUI
{
    public class DataAppSetupInterface
    {
        private UIControlledApplication App;
        public DataAppSetupInterface(UIControlledApplication app)
        {
            App = app;
        }
        public void InitializeSetupButton()
        {
            string TabName = "Duct Tool";
            App.CreateRibbonTab(TabName);
            var panel = App.CreateRibbonPanel(TabName, "Split Duct Command");
            var ExportCadDataButton = new RevitPushButtonDataModel()
            {
                Label = "Split Duct \n(Filter)",
                Panel = panel,
                Tooltip = "Split Duct With Fillter",
                IconImageName = "Duct (32x32).png",
                TooltipImageName = "SplitPic(320x320) .png",
                CommandNamespacePath = SplitDuctCommand.GetPath()
            };
            PushButton button = RevitPushButton.Create(ExportCadDataButton);

        }
    }
}
