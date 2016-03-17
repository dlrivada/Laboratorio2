using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace Laboratorio2.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        private Guid _selectedSiteGuid = Guid.Empty;
        private bool _siteUpdated;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void AddItems()
        {
            pnlUpdateControls.Visible = false;
            SPSite site = SPContext.Current.Site;
            lstWebs.Items.Clear();
            foreach (SPWeb web in site.AllWebs)
                try
                {
                    lstWebs.Items.Add(new ListItem(web.Title, web.ID.ToString()));
                }
                finally
                {
                    web.Dispose();
                }

            if (_siteUpdated)
            {
                lstWebs.SelectedIndex = -1;
                _selectedSiteGuid = Guid.Empty;
                pnlResult.Visible = true;
            }
            else
                pnlResult.Visible = false;

            if (_selectedSiteGuid.Equals(Guid.Empty)) return;
            lstWebs.Items.FindByValue(_selectedSiteGuid.ToString()).Selected = true;
            pnlUpdateControls.Visible = true;
        }

        protected void lstWebs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedSiteGuid = new Guid(lstWebs.SelectedValue);
            txtTitle.Text = lstWebs.SelectedItem.Text;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            _selectedSiteGuid = new Guid(lstWebs.SelectedValue);

            string newTitle = txtTitle.Text;
            if (string.IsNullOrEmpty(newTitle) || _selectedSiteGuid.Equals(Guid.Empty)) return;
            using (SPWeb web = SPContext.Current.Site.OpenWeb(_selectedSiteGuid))
            {
                web.Title = newTitle;
                web.Update();
                litResult.Text = $"El titulo del sitio <i>{web.Url}</i> se ha cambiado a <i>{newTitle}</i>.";
            }
            _siteUpdated = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            AddItems();
        }

    }
}
