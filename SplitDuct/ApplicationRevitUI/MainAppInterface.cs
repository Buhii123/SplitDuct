using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct.ApplicationRevitUI
{
    internal class MainAppInterface : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            var ui = new DataAppSetupInterface(application);
            ui.InitializeSetupButton();
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
