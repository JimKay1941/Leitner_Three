using System;
using System.Windows.Forms;
using Leitner_Three.Properties;

namespace Leitner_Three
{
    public partial class SetUserCredentials : Form
    {

        public SetUserCredentials()
        {
            InitializeComponent();
        }

        private void SetUserCredentials_Load(object sender, EventArgs e)
        {
            textChineseStudyServer.Text = Properties.Settings.Default.ChineseStudyServer;
			textChineseStudyPassword1.Text = Properties.Settings.Default.ChineseStudyPassword;
			textChineseStudyPassword2.Text = Properties.Settings.Default.ChineseStudyPassword;
            textChineseStudyUserID.Text = Properties.Settings.Default.ChineseStudyUserID;
            textChineseStudyDatabase.Text = Properties.Settings.Default.ChineseStudyDatabase;

            textLessonDatabaseServer.Text = Properties.Settings.Default.LessonDatabaseServer;
            textLessonDatabasePassword1.Text = Properties.Settings.Default.LessonPassword;
            textLessonDatabasePassword2.Text = Properties.Settings.Default.LessonPassword;
            textLessonDatabaseUserID.Text = Properties.Settings.Default.LessonDatabaseUserID;
            textLessonDatabase.Text = Properties.Settings.Default.LessonDatabase;

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "";

            if (textChineseStudyPassword1.Text != textChineseStudyPassword2.Text)
            {
                txtStatus.Text = @"ChineseStudy Passwords do not match. Please retype them.";
                textChineseStudyPassword1.Text = "";
                textChineseStudyPassword2.Text = "";
                return;
            }

            if (textLessonDatabasePassword1.Text != textLessonDatabasePassword2.Text)
            {
                txtStatus.Text = @"Lesson Passwords do not match. Please retype them.";
                textLessonDatabasePassword1.Text = "";
                textLessonDatabasePassword2.Text = "";
                return;
            }

            if (textChineseStudyPassword1.Text != "")
            {
                Properties.Settings.Default.ChineseStudyPassword = textChineseStudyPassword1.Text;    
            }

            if (textLessonDatabasePassword1.Text != "")
            {
                Properties.Settings.Default.LessonPassword = textLessonDatabasePassword1.Text;
            }
            
            
            Properties.Settings.Default.ChineseStudyServer = textChineseStudyServer.Text;
            Properties.Settings.Default.ChineseStudyPassword = textChineseStudyPassword2.Text;
            Properties.Settings.Default.ChineseStudyUserID = textChineseStudyUserID.Text;
            Properties.Settings.Default.ChineseStudyDatabase = textChineseStudyDatabase.Text;

            Properties.Settings.Default.LessonDatabaseServer = textLessonDatabaseServer.Text;
            Properties.Settings.Default.LessonPassword = textLessonDatabasePassword1.Text;
            Properties.Settings.Default.LessonDatabaseUserID = textLessonDatabaseUserID.Text;
            Properties.Settings.Default.LessonDatabase = textLessonDatabase.Text;

            Properties.Settings.Default.Save();

            if (!checkUseLessonIntegratedSecurity.Checked)
            {
                //JKAYWINDOWS\SQLEXPRESS;Initial Catalog=JimKay;Integrated Security=False
                Settings.Default.LessonConnectionString =
                    "Data Source=" + Settings.Default.LessonDatabaseServer +
                    ";Initial Catalog=" + Settings.Default.LessonDatabase +
                    ";Persist Security Info=True;User ID=" + Settings.Default.LessonDatabaseUserID +
                    ";Password=" + Settings.Default.LessonPassword;
                string looker1 = Settings.Default.LessonConnectionString;
                string looker2 = Settings.Default.LessonConnectionString;

            }
            else
            {
                //JKAYWINDOWS\SQLEXPRESS;Initial Catalog=JimKay;Integrated Security=True
                Settings.Default.LessonConnectionString =
                    "Data Source=" + Properties.Settings.Default.LessonDatabaseServer +
                    ";Initial Catalog=" + Properties.Settings.Default.LessonDatabase +
                    ";Integrated Security=True";
                string looker3 = Settings.Default.LessonConnectionString;
                string looker4 = Settings.Default.LessonConnectionString;
            }

            if (!checkUseChineseStudyIntegratedSecurity.Checked)
            {
                Properties.Settings.Default.ChineseStudyConnection =
                    Properties.Settings.Default.ChineseStudyConnectionString;
            }
			else
	{
                Properties.Settings.Default.ChineseStudyConnection =
                     "Data Source=" + Properties.Settings.Default.ChineseStudyServer +
                    ";Initial Catalog=" + Properties.Settings.Default.ChineseStudyDatabase +
                    ";Persist Security Info=True;User ID=" + Properties.Settings.Default.ChineseStudyUserID +
                    ";Password=" + Properties.Settings.Default.ChineseStudyPassword;
                string looker5 = Settings.Default.ChineseStudyConnection;
                string looker6 = Settings.Default.ChineseStudyConnection; 
            }

            Properties.Settings.Default.Save();

			Close();
        }

        private void textUserPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}