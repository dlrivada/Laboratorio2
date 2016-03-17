using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace Laboratorio2.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public class VisualWebPart1 : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string AscxPath = @"~/_CONTROLTEMPLATES/15/Laboratorio2/VisualWebPart1/VisualWebPart1UserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(AscxPath);
            Controls.Add(control);
        }
    }
}
