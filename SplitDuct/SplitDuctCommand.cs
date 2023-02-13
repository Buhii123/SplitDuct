using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SplitDuct.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class SplitDuctCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication UIAPP = commandData.Application;
            UIDocument UIDOC = UIAPP.ActiveUIDocument;
            Document DOC = UIDOC.Document;

            if (DOC.IsFamilyDocument)
            {
                TaskDialog.Show("Warrning", "Không thể Chạy Tool trên Family!");
                return Result.Cancelled;
            }
            try
            {
                #region Code here!

                ViewModelMain Mainwindow = new ViewModelMain(UIAPP);
                Mainwindow.MainView.ShowDialog();


                #endregion
            }
            catch (Exception ex) 
            {
                TaskDialog.Show("Erro","Lỗi: "+ex.Message);
                return Result.Cancelled;    
            }


            return Result.Succeeded;
        }
        public static string GetPath()
        {
            return typeof(SplitDuctCommand).Namespace + "." + nameof(SplitDuctCommand);
        }
    }
   
}
