using Autodesk.Revit.UI;
using SplitDuct.Model;
using SplitDuct.Resources.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct.Resources
{
    public static class RevitPushButton
    {
        public static PushButton Create(RevitPushButtonDataModel data)
        {

            string btnDataName = Guid.NewGuid().ToString();

            var btnData = new PushButtonData(btnDataName, data.Label, CoreAssembly.GetAssemblyLocation(), data.CommandNamespacePath)
            {
                ToolTip = data.Tooltip,
                LargeImage = ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = ResourceImage.GetIcon(data.TooltipImageName),
            };

            return data.Panel.AddItem(btnData) as PushButton;
        }
    }
}
