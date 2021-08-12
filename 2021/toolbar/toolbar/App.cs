#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

using Autodesk.Revit.UI;
#endregion

namespace toolbar
{
    class App : IExternalApplication
    {
        static void AddRibbonPanel(UIControlledApplication application)
        {
            //call method to load toolbar
            //create custom ribbon tab
            String TabName = "DBTools";
            application.CreateRibbonTab(TabName);

            /////old file location user appdata roaming path
            ///
            //var appFolderLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            //var specialDirectory = Path.GetDirectoryName(appFolderLocation);
            //var revitAppPath = specialDirectory + @"\Roaming\Autodesk\Revit\Addins\2019";

            //new file location program data - this works for all users
            var revitAppPath = @"C:\ProgramData\Autodesk\Revit\Addins\2021";


            //button data
            PushButtonData nwcButton = new PushButtonData("Export NWCs", "3D to" + "\n" + "NWC",
                revitAppPath + @"\DBToolBar\Export 3d views.dll", "export3dviews.export3d");

            PushButtonData dwgButton = new PushButtonData("Export DWGs", "Sheets" + "\n" + "to DWG",
                revitAppPath + @"\DBToolBar\export_dwg_files_revit.dll", "export_sheets_dwg.exportSheets");

            PushButtonData elevButton = new PushButtonData("Elevations", "Elevations" + "\n" + "To Sheet",
                revitAppPath + @"\DBToolBar\elevations.dll", "elevationsToSheets.elevationAdd");

            //images
            BitmapImage nwcImage = new BitmapImage(new Uri(revitAppPath + @"\DBToolBar\NWC.png"));
            nwcButton.LargeImage = nwcImage;

            BitmapImage dwgImage = new BitmapImage(new Uri(revitAppPath + @"\DBToolBar\DWG.png"));
            dwgButton.LargeImage = dwgImage;

            BitmapImage elevImage = new BitmapImage(new Uri(revitAppPath + @"\DBToolBar\ELEV.png"));
            elevButton.LargeImage = elevImage;

            //add new ribbon panels
            RibbonPanel ribbonpanel_export = application.CreateRibbonPanel(TabName, "Export");
            RibbonPanel ribbonpanel_elev = application.CreateRibbonPanel(TabName, "Views");

            //create buttons
            PushButton nwc = ribbonpanel_export.AddItem(nwcButton) as PushButton;
            nwc.ToolTip = "Select one or more 3D views to export to a NWC file.";

            PushButton dwg = ribbonpanel_export.AddItem(dwgButton) as PushButton;
            dwg.ToolTip = "Select one or more sheets to export to a dwg file.";

            PushButton elev = ribbonpanel_elev.AddItem(elevButton) as PushButton;
            elev.ToolTip = "Select one or more section or elevations to to place on a sheet.";

        }

        public Result OnStartup(UIControlledApplication application)
        {
            //call method to create ribbon
            AddRibbonPanel(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
           //do nothing
            return Result.Succeeded;
        }
    }
}
