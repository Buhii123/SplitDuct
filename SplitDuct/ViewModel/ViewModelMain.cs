using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using SplitDuct.Model;
using SplitDuct.ViewModel.Base;
using SplitDuct.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        UIDocument uidoc { get; }
        Document doc { get; }
        public ObservableCollection<DuctTypeModel> ListductTypes { get; set; }
        #region Main View
        private ViewMainUser mainview;
        public ViewMainUser MainView
        {
            get
            {
                if (mainview == null)
                {
                    mainview = new ViewMainUser() { DataContext = this };
                }
                return mainview;
            }
            set
            {
                mainview = value;
                OnPropertyChanged(nameof(MainView));
            }
        }
        #endregion
       
        public RelayCommand<object> ButtonRun { get; set; }
        public RelayCommand<object> ButtonClose{ get; set; }

        public ViewModelMain(UIApplication uiapp) 
        {
            uidoc = uiapp.ActiveUIDocument;
            doc = uidoc.Document;

            List<DuctType> clection = new FilteredElementCollector(doc)
                .OfClass(typeof(DuctType))
                .OfCategory(BuiltInCategory.OST_DuctCurves)
                .Cast<DuctType>()
                .ToList();
            List<Duct> duct = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfClass(typeof(Duct))
                .OfCategory(BuiltInCategory.OST_DuctCurves)
                .WhereElementIsNotElementType()
                .Cast<Duct>()
                .ToList();  
                       
            ListductTypes = new ObservableCollection<DuctTypeModel>();
            ListductTypes.Add(new DuctTypeModel(duct));
            foreach (DuctType c in clection) 
            {
                ListductTypes.Add(new DuctTypeModel(c, duct));   
            }
            ButtonRun = new RelayCommand<object>(p => ConditionRun(), p=> EventButtonRun());
            ButtonClose = new RelayCommand<object>(p => true, p => { this.mainview.Close();});

        }

        public void EventButtonRun() 
        {
            double result = double.Parse(mainview.tbSement.Text);

            DuctTypeModel ductype = mainview.cbbDuctType.SelectedItem as DuctTypeModel;
            foreach(Duct d in ductype.ListDuct) 
            {
                SetSplitDuctM.SetAllDuct(doc, d, result);
                
            }
            

        }
        public bool ConditionRun()
        {
            if (!string.IsNullOrEmpty(mainview.tbSement.Text))
            {
                return true;
            }
            return false;
        }
    }
}
