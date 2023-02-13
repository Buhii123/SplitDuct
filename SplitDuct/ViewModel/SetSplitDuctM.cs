using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitDuct.ViewModel
{
    public static class SetSplitDuctM
    {
        public static void SetAllDuct(Document doc, Duct elduct, double semgemt)
        {

            ElementId DuctMainId = elduct.Id;

            Parameter paraE = elduct.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
            double lenghtDuct = paraE.AsDouble() * 304.8;

            double b = semgemt / 304.8;
            Duct ductM = doc.GetElement(DuctMainId) as Duct;

            List<Duct> listNewDuct = new List<Duct>();
            #region code
            if (lenghtDuct > semgemt)
            {
                using (TransactionGroup tranG = new TransactionGroup(doc))
                {
                    tranG.Start();
                    for (int i = 0; i <= (lenghtDuct % semgemt); i++)
                    {
                        try
                        {

                            if (((ductM.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble()) * 304.8) > semgemt)
                            {
                                using (Transaction transaction = new Transaction(doc, "Break Duct"))
                                {
                                    transaction.Start();

                                    Curve curve = (ductM.Location as LocationCurve).Curve;

                                    XYZ pt0 = curve.GetEndPoint(0);
                                    XYZ pt1 = curve.GetEndPoint(1);

                                    XYZ vector = pt1.Subtract(pt0).Normalize();
                                    XYZ breakPt = pt0.Add(vector.Multiply(b));

                                    ElementId newDuctId = MechanicalUtils.BreakCurve(doc, ductM.Id, breakPt);
                                    listNewDuct.Add(doc.GetElement(newDuctId) as Duct);
                                    transaction.Commit();
                                }
                            }
                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }


                    tranG.Assimilate();
                }
            }
            #endregion
            //code class main here.
            for (int i = 0; i < listNewDuct.Count; i++)
            {
                if (listNewDuct[i] != null)
                {
                    try
                    {
                        TrimDuctWithOni(doc, listNewDuct[i], listNewDuct[i + 1]);
                    }
                    catch (Exception)
                    {
                        TrimDuctWithOni(doc, listNewDuct[listNewDuct.Count - 1], (Duct)doc.GetElement(DuctMainId));
                    }
                }

            }
        }
        private static void TrimDuctWithOni(Document doc, Duct d1, Duct d2)
        {
            ConnectorSet conecDuct1 = d1.ConnectorManager.Connectors;
            ConnectorSet conecDuct2 = d2.ConnectorManager.Connectors;

            Connector con1 = null;
            Connector con2 = null;
            using (TransactionGroup transGroup = new TransactionGroup(doc))
            {
                transGroup.Start();

                    foreach (Connector c in conecDuct1)
                    {
                        foreach (Connector a in conecDuct2)
                        {
                            if (c.Origin.IsAlmostEqualTo(a.Origin))
                            {
                                con1 = c;
                                con2 = a;
                            }
                        }
                    }

                    using (Transaction t = new Transaction(doc, "Connect two ducts"))
                    {
                        t.Start();
                        doc.Create.NewUnionFitting(con1, con2);
                        t.Commit();
                    }
                transGroup.Assimilate();
            }

        }
    }
}
