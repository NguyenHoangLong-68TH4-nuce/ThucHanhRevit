using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ThucHanhRevit
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            IList<Element> columnList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            //StringBuilder output = new StringBuilder("Tất cả các Structural Column trong BIM model :"+columnList.Count()+"\r\n");
            StringBuilder output = new StringBuilder("Tất cả các Walls trong BIM model :" + columnList.Count() + "\r\n");

            foreach (Element e1 in columnList)
            {
                string elemName = e1.Id.ToString() + "\n";
                ElementType type = doc.GetElement(e1.GetTypeId()) as ElementType;
                Parameter h1 = type.LookupParameter("Width");
                double hh1 = ChangeUnitFeetToMillimeter(h1.AsDouble());
                //Parameter b = type.LookupParameter("b");
                //double bb1 = ChangeUnitFeetToMillimeter(b.AsDouble());
                elemName = elemName + "Width = " + hh1.ToString() + "\n";
                //elemName = elemName + "b = " + bb1.ToString() + "\n";
                output.Append(elemName);
                output.AppendLine();
            }
            TaskDialog.Show("Popup",output.ToString());
            return Result.Succeeded;
        }
        static double ChangeUnitFeetToMillimeter(double Value)
        {
            double value = Value;
            double valueInMillimeters = UnitUtils.Convert(
            value,
            UnitTypeId.Feet,
            UnitTypeId.Millimeters);
            valueInMillimeters = Math.Round(valueInMillimeters, 2);

            return value;
        }
    }
}
