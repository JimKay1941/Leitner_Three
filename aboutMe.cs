using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class AboutMe : Form
    {
        public AboutMe()
        {
            InitializeComponent();
            linkLabelMD.Links[0].LinkData = "http://www.mds-soft.persianblog.ir/";
            linkLabelJK.Links[0].LinkData = "http://www.jimkay.info";
            linkLabelCP.Links[0].LinkData = "http://www.codeproject.com";
            linkLabelCPOL.Links[0].LinkData = "http://www.codeproject.com/info/cpol10.aspx";
            linkLabelSL.Links[0].LinkData = "http://en.wikipedia.org/wiki/Leitner_system";
        }

        private void linkLabelMD_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch
            {
            }
        }

        private void linkLabelCPOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch
            {
            }
        }

        private void linkLabelSL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch
            {
            }
        }

        private void linkLabelJK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch
            {
            }
        }

        private void linkLabelCP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch
            {
            }
        }

		private void label1_Click(object sender, System.EventArgs e)
		{

		}
    }
}