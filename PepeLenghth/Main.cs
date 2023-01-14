using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задание 3.2 Общая длина труб

namespace PepeLenghth
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedRef = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите трубы");
            var lengththList = new List<double>();


            foreach (var selectedElement in selectedRef)
            {

                var element = doc.GetElement(selectedElement);

                if (element is Pipe)
                {

                    Parameter lenghthParameter = element.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);

                    if (lenghthParameter.StorageType == StorageType.Double)
                    {
                        double lenghth = UnitUtils.ConvertFromInternalUnits(lenghthParameter.AsDouble(), UnitTypeId.Meters);
                        lengththList.Add(lenghth);
                    }

                }
            }

            TaskDialog.Show("Volume", $"Длина выбранных труб = {lengththList.Sum()} м");
            return Result.Succeeded;
        }
    }
}
