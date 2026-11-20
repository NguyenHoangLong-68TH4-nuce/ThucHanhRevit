using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace ThucHanhRevit
{
    [Autodesk. Revit. Attributes. Transaction(Autodesk. Revit. Attributes. TransactionMode.Manual) ]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            TaskDialog.Show("Revit", "Số lượng đoi tượng được chon: " + selectedIds.Count.ToString());
            ICollection<ElementId> selectedWallIds = new List<ElementId>();
            foreach (ElementId id in selectedIds)
            {
                Element elements1 = uidoc.Document.GetElement(id);
                if (elements1 is Wall)
                {
                    selectedWallIds.Add(id);
                }
            }

            // Set the created element set as current select element set.
            uidoc.Selection.SetElementIds(selectedWallIds);
            if (0 != selectedWallIds.Count)
            {
                TaskDialog.Show("Revit", selectedWallIds.Count.ToString() + " Đoi tượng Tưong đa đợc chon!");
            }
            else
            {
                TaskDialog.Show("Revit", "Khong co đoi tượng tưong nao được chon!");
            }

            return Result.Succeeded;
        }
    }
}
