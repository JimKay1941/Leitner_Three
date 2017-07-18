using System;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class SetBackupPath : Form
    {
		private readonly FolderBrowserDialog _chooseInputFolderDialog = new FolderBrowserDialog();

        public SetBackupPath()
        {
            InitializeComponent();
        }

        private void SetUserPath_Load(object sender, EventArgs e)
        {
            textUserPath.Text = Properties.Settings.Default.user_path;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
			string addIt = "\\";

			if ((textUserPath.Text.Length >= 1) && (textUserPath.Text.Substring(textUserPath.Text.Length - 1, 1) == "\\"))
				addIt = "";

            Properties.Settings.Default.user_path = textUserPath.Text + addIt;
            Properties.Settings.Default.Save();
            Variables.UsersFolder = textUserPath.Text;
			Close();
        }

        private void textUserPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
			_chooseInputFolderDialog.ShowDialog();
			Variables.UsersFolder = _chooseInputFolderDialog.SelectedPath;
            Properties.Settings.Default.user_path = Variables.UsersFolder;
            Properties.Settings.Default.Save();

            textUserPath.Text = Variables.UsersFolder;
        }
    }
}