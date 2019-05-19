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
            textChineseStudyServer.Text = Settings.Default.ChineseStudyServer;
			textChineseStudyPassword1.Text = Settings.Default.ChineseStudyPassword;
			textChineseStudyPassword2.Text = Settings.Default.ChineseStudyPassword;
            textChineseStudyUserID.Text = Settings.Default.ChineseStudyUserID;
            textChineseStudyDatabase.Text = Settings.Default.ChineseStudyDatabase;

            textLessonDatabaseServer.Text = Settings.Default.LessonDatabaseServer;
            textLessonDatabasePassword1.Text = Settings.Default.LessonPassword;
            textLessonDatabasePassword2.Text = Settings.Default.LessonPassword;
            textLessonDatabaseUserID.Text = Settings.Default.LessonDatabaseUserID;
            textLessonDatabase.Text = Settings.Default.LessonDatabase;

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
                Settings.Default.ChineseStudyPassword = textChineseStudyPassword1.Text;    
            }

            if (textLessonDatabasePassword1.Text != "")
            {
                Settings.Default.LessonPassword = textLessonDatabasePassword1.Text;
            }
            
            
            Settings.Default.ChineseStudyServer = textChineseStudyServer.Text;
            Settings.Default.ChineseStudyPassword = textChineseStudyPassword2.Text;
            Settings.Default.ChineseStudyUserID = textChineseStudyUserID.Text;
            Settings.Default.ChineseStudyDatabase = textChineseStudyDatabase.Text;

            Settings.Default.LessonDatabaseServer = textLessonDatabaseServer.Text;
            Settings.Default.LessonPassword = textLessonDatabasePassword1.Text;
            Settings.Default.LessonDatabaseUserID = textLessonDatabaseUserID.Text;
            Settings.Default.LessonDatabase = textLessonDatabase.Text;

            Settings.Default.Save();

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
                    "Data Source=" + Settings.Default.LessonDatabaseServer +
                    ";Initial Catalog=" + Settings.Default.LessonDatabase +
                    ";Integrated Security=True";
                string looker3 = Settings.Default.LessonConnectionString;
                string looker4 = Settings.Default.LessonConnectionString;
            }

            if (!checkUseChineseStudyIntegratedSecurity.Checked)
            {
                Settings.Default.ChineseStudyConnection =
                    Settings.Default.ChineseStudyConnectionString;
            }
			else
	{
                Settings.Default.ChineseStudyConnection =
                     "Data Source=" + Settings.Default.ChineseStudyServer +
                    ";Initial Catalog=" + Settings.Default.ChineseStudyDatabase +
                    ";Persist Security Info=True;User ID=" + Settings.Default.ChineseStudyUserID +
                    ";Password=" + Settings.Default.ChineseStudyPassword;
                string looker5 = Settings.Default.ChineseStudyConnection;
                string looker6 = Settings.Default.ChineseStudyConnection; 
            }

            Settings.Default.Save();

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