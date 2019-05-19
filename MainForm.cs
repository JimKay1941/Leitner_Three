#define OWNER
#if OWNER
#define CHINESE
#endif
//#define CHINESE
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class MainForm : Form
    {
        //#region Fields

        private LeitnerBox.ToLearnRow _selectedItem;

        //#endregion Fields

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public MainForm()
        {
            InitializeComponent();
#if CHINESE
            btnbutton1.Visible = false;
#endif
#if OWNER
#else
            updateFEIToolStripMenuItem.Visible = false;
#endif
            // my file dialogs
            Variables.ChooseInputFileDialog.FileOk += OnInputFileDialogOk;
            Variables.Delim2[0] = '\t';
            Variables.Delim1[0] = '\"';
            
            InputFile.Text = Properties.Settings.Default.BulkLoadPath;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Variables.Reading != null)
            {
                Variables.Reading.Close();
            }
        }

        private void OnInputFileDialogOk(object sender, CancelEventArgs e)
        {
            InputFile.Text = Variables.ChooseInputFileDialog.FileName;
            Properties.Settings.Default.BulkLoadPath = Variables.ChooseInputFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        private void ChooseInput_Click(object sender, EventArgs e)
        {
            newtyping.Text = @""; 
            Variables.ChooseInputFileDialog.ShowDialog();
        }

        //#region Load a Lesson from a flat file

        private void ReadInput_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
            {
                labelAddQuestionMessage.Text = @"You must select a lesson before trying to load new data";
                return;
            }
            labelAddQuestionMessage.Text = "";
            Status.Text = "";
            treeView1.BeginUpdate();
            Variables.InputCounter = 0;
            const string boxId = @"DataBase";
            const string partId = "";

            var maxId = 0;

            try
            {
                var questions = from q in Variables.Leitner.ToLearn
                                orderby q.Id
                                select q;

                maxId = Enumerable.Select(questions, question => question.Id).Concat(new[] {0}).Max();
            }
            catch (Exception)
            { }
            maxId++;

            try
            {
                InputCount.Text = "";
                var txtReading = new FileStream(InputFile.Text, FileMode.Open, FileAccess.Read, FileShare.None);
                var inputLine = new StreamReader(txtReading);

                Variables.FirstRead = inputLine.ReadLine();

                if (Variables.FirstRead != null)
                {
                    do
                    {
                        Variables.TranslatorLine = null;
                        string[] lineParts1 = null;
                        string[] lineParts2 = null;
                        Variables.InputCounter++;

                        lineParts1 = Variables.FirstRead.Split(Variables.Delim1);

                        foreach (var t in lineParts1)
                        {
                            Variables.TranslatorLine += t;
                        }

                        if (Variables.TranslatorLine != null)
                            lineParts2 = Variables.TranslatorLine.Split(Variables.Delim2);

                        // Excel exports empty line padding
                        if ((lineParts2[0] == "") || (lineParts2[1] == ""))
                        {
                            Variables.InputCounter--;
                        }
                        else
                        {
                            var destinationRow = Variables.Leitner.ToLearn.NewToLearnRow();
                            InitNewRow(destinationRow);

                            destinationRow.Id = maxId;
                            destinationRow.Side1 = lineParts2[01];
                            destinationRow.Side2 = lineParts2[02];
                            destinationRow.Side3 = lineParts2[03];
                            destinationRow.A_B_GoodCount = (lineParts2.Length > 04) ? GetValidInt(lineParts2[04]) : 0;
                            destinationRow.A_C_GoodCount = (lineParts2.Length > 05) ? GetValidInt(lineParts2[05]) : 0;
                            destinationRow.B_A_GoodCount = (lineParts2.Length > 06) ? GetValidInt(lineParts2[06]) : 0;
                            destinationRow.B_C_GoodCount = (lineParts2.Length > 07) ? GetValidInt(lineParts2[07]) : 0;
                            destinationRow.C_A_GoodCount = (lineParts2.Length > 08) ? GetValidInt(lineParts2[08]) : 0;
                            destinationRow.C_B_GoodCount = (lineParts2.Length > 09) ? GetValidInt(lineParts2[09]) : 0;
                            destinationRow.A_B_BadCount = (lineParts2.Length > 10) ? GetValidInt(lineParts2[10]) : 0;
                            destinationRow.A_C_BadCount = (lineParts2.Length > 11) ? GetValidInt(lineParts2[11]) : 0;
                            destinationRow.B_A_BadCount = (lineParts2.Length > 12) ? GetValidInt(lineParts2[12]) : 0;
                            destinationRow.B_C_BadCount = (lineParts2.Length > 13) ? GetValidInt(lineParts2[13]) : 0;
                            destinationRow.C_A_BadCount = (lineParts2.Length > 14) ? GetValidInt(lineParts2[14]) : 0;
                            destinationRow.C_B_BadCount = (lineParts2.Length > 15) ? GetValidInt(lineParts2[15]) : 0;
                            destinationRow.A_B_DisplayLocation = (lineParts2.Length > 16) ? GetValidDisplayLocation(lineParts2[16]) : 30;
                            destinationRow.A_C_DisplayLocation = (lineParts2.Length > 17) ? GetValidDisplayLocation(lineParts2[17]) : 30;
                            destinationRow.B_A_DisplayLocation = (lineParts2.Length > 18) ? GetValidDisplayLocation(lineParts2[18]) : 30;
                            destinationRow.B_C_DisplayLocation = (lineParts2.Length > 19) ? GetValidDisplayLocation(lineParts2[19]) : 30;
                            destinationRow.C_A_DisplayLocation = (lineParts2.Length > 20) ? GetValidDisplayLocation(lineParts2[20]) : 30;
                            destinationRow.C_B_DisplayLocation = (lineParts2.Length > 21) ? GetValidDisplayLocation(lineParts2[21]) : 30;
                            destinationRow.A_B_TestDate = (lineParts2.Length > 22) ? GetValidDate(lineParts2[22]) : DateTime.Now.ToString();
                            destinationRow.A_C_TestDate = (lineParts2.Length > 23) ? GetValidDate(lineParts2[23]) : DateTime.Now.ToString();
                            destinationRow.B_A_TestDate = (lineParts2.Length > 24) ? GetValidDate(lineParts2[24]) : DateTime.Now.ToString();
                            destinationRow.B_C_TestDate = (lineParts2.Length > 25) ? GetValidDate(lineParts2[25]) : DateTime.Now.ToString();
                            destinationRow.C_A_TestDate = (lineParts2.Length > 26) ? GetValidDate(lineParts2[26]) : DateTime.Now.ToString();
                            destinationRow.C_B_TestDate = (lineParts2.Length > 27) ? GetValidDate(lineParts2[27]) : DateTime.Now.ToString();

                            Variables.Leitner.ToLearn.AddToLearnRow(destinationRow);

                            OneRecordAdded(destinationRow);

                            AddToTreeView(boxId, partId, maxId);
                            maxId++;
                        }
                        Variables.FirstRead = inputLine.ReadLine();
                    } while (Variables.FirstRead != null);
                    treeView1.EndUpdate();
                }

                inputLine.Close();

                //SaveXml();

                Status.Text = @"Input Processing completed";
                InputCount.Text = Convert.ToString(Variables.InputCounter);
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        private static int GetValidInt(string linePart)
        {
            return Int32.TryParse(linePart, out var validInt) ? validInt : 0;
        }

        private static int GetValidDisplayLocation(string LinePart)
        {
            if (Int32.TryParse(LinePart, out var ValidInt))
            {
                return ValidInt;
            }
            else
            {
                return 30;
            }
        }

        private static string GetValidDate(string LinePart)
        {
            if (DateTime.TryParse(LinePart, out var ValidDate))
            {
                return ValidDate.ToString();
            }
            else
            {
                return DateTime.Now.ToString();
            }
        }

        //#endregion Load a Lesson from a flat file

        //#region shortcut buttons

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F2) && (Variables.LessonTableName != ""))
            {
                int intStudyMode = Variables.Leitner.Setting[0].StudyMode + 1;
                String NowStudyMode = intStudyMode.ToString();
                String NowStudySequence = Variables.Leitner.Setting[0].StudySequence;

                if (NowStudyMode == NowStudySequence.Substring(0, 1))
                {
                    NowStudyMode = NowStudySequence.Substring(1, 1);
                }
                else if (NowStudyMode == NowStudySequence.Substring(1, 1))
                {
                    NowStudyMode = NowStudySequence.Substring(2, 1);
                }
                else if (NowStudyMode == NowStudySequence.Substring(2, 1))
                {
                    NowStudyMode = NowStudySequence.Substring(3, 1);
                }
                else if (NowStudyMode == NowStudySequence.Substring(3, 1))
                {
                    NowStudyMode = NowStudySequence.Substring(4, 1);
                }
                else if (NowStudyMode == NowStudySequence.Substring(4, 1))
                {
                    NowStudyMode = NowStudySequence.Substring(5, 1);
                }
                else //if ( NowStudyMode == NowStudySequence.Substring( 0, 1 ) )
                {
                    NowStudyMode = NowStudySequence.Substring(0, 1);
                }

                Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(NowStudyMode) - 1;

                UpdateSQLSettings();
                //SaveXml();
                LoadXML();
                TabPageStatistics_Enter();
            }
            if ((e.KeyCode == Keys.F3) && (Variables.LessonTableName != ""))
            {
                PrimeBox1ToolStripMenuItem_Click(sender, e);
            }
            else if ((e.KeyCode == Keys.F4) && (Variables.LessonTableName != ""))
            {
                try
                {
                    // If AutoAging Interval has not been set, set it here.
                    try
                    {
                        int aginginterval = 0;
                        string prevAutoAgeing = null;

                        GetAgingIntervalAndPrevDate(ref aginginterval, ref prevAutoAgeing);

                        if (aginginterval != 0)
                        {
                            //string prevAutoAgeing = (from q in Variables.xDocument.Descendants("AutoAge") select q.Value).First();
                            DateTime agedThen = DateTime.Parse(prevAutoAgeing);
                            TimeSpan tillNextAging = TimeSpan.Parse(aginginterval.ToString());
                            DateTime dueToBeAged = agedThen.Add(tillNextAging);
                            int decideOnAutoAging = dueToBeAged.CompareTo(DateTime.Now);

                            if (decideOnAutoAging <= 0)
                            {
                                SetPrevousAutoAge();

                                //SaveXml();

                                AutoPush("2");
                                AutoPush("3");
                                AutoPush("4");
                                AutoPush("5");
                                //successful( "Automatic Push", "Leitner Two has been updated" );
                                TabPageStatistics_Enter();
                                //SaveXml();
                                LoadXML();
                            }
                        }
                        else
                        {
                            SetPrevousAutoAge();

                            //SaveXml();

                            AutoPush("2");
                            AutoPush("3");
                            AutoPush("4");
                            AutoPush("5");
                            //successful( "Automatic Push", "Leitner Two has been updated" );
                            TabPageStatistics_Enter();
                            //SaveXml();
                            LoadXML();
                        }
                    }
                    catch (Exception ex)
                    {
                        StackFrame file_info = new StackFrame(true);
                        Error(ref file_info, ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    StackFrame file_info = new StackFrame(true);
                    Error(ref file_info, ex.Message);
                }
            }
            // NOTE: F5 should be reserved for debugging
            else if ((e.KeyCode == Keys.F6) && (textBoxQuestion.Text == ""))
            {
                labelAnswerToQuestionMessage.Text = @"There is no Question image to display!";
                return;
            }
            else if ((e.KeyCode == Keys.F6) && (textBoxQuestion.Text != ""))
            {
                labelAnswerToQuestionMessage.Text = @"";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "H:\\OneDrive\\Visual Studio\\Composite Program Collection\\quickview\\QuickView\\bin\\Release\\QuickView.exe",
                    WorkingDirectory = Environment.CurrentDirectory,
                    Arguments = textBoxQuestion.Text
                };
                Process proc = Process.Start(psi);
            }
            else if ((e.KeyCode == Keys.F7) && (textBoxAnswer.Text == ""))
            {
                labelAnswerToQuestionMessage.Text = @"There is no Answer image to display!";
                return;
            }
            else if ((e.KeyCode == Keys.F7) && (textBoxAnswer.Text != ""))
            {
                labelAnswerToQuestionMessage.Text = @"";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "H:\\OneDrive\\Visual Studio\\Composite Program Collection\\quickview\\QuickView\\bin\\Release\\QuickView.exe",
                    WorkingDirectory = Environment.CurrentDirectory,
                    Arguments = textBoxAnswer.Text
                };
                Process proc = Process.Start(psi);
            }
            else if ((e.KeyCode == Keys.F12) && (Variables.LessonTableName != ""))
            {
                // Archive Everything under the current Study Mode
                var questions = from q in Variables.Leitner.ToLearn
                                orderby q.Id
                                select q;

                foreach (var question in questions)
                {
                    SetDisplayLocation(30, question);
                    SetGoodCount(0, question);
                    SetBadCount(0, question);
                }

                DisplayLocationChanged();
                //SaveXml();
                LoadXML();
                TabPageStatistics_Enter();
            }
            else if (e.KeyCode == Keys.S && e.Control && buttonShowAnswer.Enabled)
            {
                ButtonShowAnswer_Click(null, null);
            }
            else if (e.KeyCode == Keys.T && e.Control && buttonTrue.Enabled)
            {
                ButtonTrue_Click(null, null);
            }
            else if (e.KeyCode == Keys.F && e.Control && buttonFalse.Enabled)
            {
                ButtonFalse_Click(null, null);
            }
        }

        //#endregion shortcut buttons

        private string ComputePersianDate(DateTime dateTime)
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                var str = persianCalendar.GetYear(dateTime).ToString() + " / " +
                                            persianCalendar.GetMonth(dateTime).ToString() + " / " +
                                            persianCalendar.GetDayOfMonth(dateTime).ToString() + "   " +
                                            persianCalendar.GetHour(dateTime).ToString() + ":" +
                                            persianCalendar.GetMinute(dateTime).ToString() + ":" +
                                            persianCalendar.GetSecond(dateTime).ToString();
                return str;
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
                return "";
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region AutoComplete Events

        bool questionIsActive = false;
        bool answerIsActive = false;
        bool hintIsActive = false;

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Variables.EnableAutoComplete = true;
            TextBox textBox = sender as TextBox;

            if (textBox.Name == "textBoxNewQuestion")
            {
                questionIsActive = true;
                answerIsActive = false;
                hintIsActive = false;
            }
            else if (textBox.Name == "textBoxNewAnswer")
            {
                questionIsActive = false;
                answerIsActive = true;
                hintIsActive = false;
            }
            else if (textBox.Name == "textBoxNewHint")
            {
                questionIsActive = false;
                answerIsActive = false;
                hintIsActive = true;
            }

            if ((int)e.KeyChar == 13 && listBoxAutoComplete.Visible)
            {
                try
                {
                    e.Handled = true;
                    textBox.Text = listBoxAutoComplete.Items[listBoxAutoComplete.SelectedIndex] as String;
                    listBoxAutoComplete.Visible = false;
                }
                catch { }
            }
        }

        private void ListBoxAutoComplete_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (questionIsActive)
                {
                    textBoxNewQuestion.Text = listBoxAutoComplete.Items[listBoxAutoComplete.SelectedIndex] as String;
                    textBoxNewQuestion.Focus();
                    newtyping.Text = "";
                    textCh.Text = "";
                    textCangjie.Text = "";
                    textFarEast.Text = "";
                    InputCount.Text = "";
                }
                else if (answerIsActive)
                {
                    textBoxNewAnswer.Text = listBoxAutoComplete.Items[listBoxAutoComplete.SelectedIndex] as String;
                    textBoxNewAnswer.Focus();
                }
                else if (hintIsActive)
                {
                    textBoxNewHint.Text = listBoxAutoComplete.Items[listBoxAutoComplete.SelectedIndex] as String;
                    textBoxNewHint.Focus();
                }
            }
            catch
            {
            }
            finally
            {
                listBoxAutoComplete.Visible = false;
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                listBoxAutoComplete.Visible = false;
            }
            else if (e.KeyCode == Keys.Down && listBoxAutoComplete.Visible)
            {
                try { listBoxAutoComplete.SelectedIndex++; }
                catch { }
            }
            else if (e.KeyCode == Keys.Up && listBoxAutoComplete.Visible)
            {
                try { listBoxAutoComplete.SelectedIndex--; }
                catch { }
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            labelAddQuestionMessage.Text = "";
            labelAnswerToQuestionMessage.Text = "";
            labelSearchMessage.Text = "";
            (sender as TextBox).SelectAll();
            listBoxAutoComplete.Visible = false;
        }

        //#endregion AutoComplete Events

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region Messages

        private void Error(ref StackFrame file_info, string errorMassage)
        {
            try
            {
                if (file_info.GetFileName() == null)
                    MessageBox.Show(this, "Exception : " + errorMassage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, "File : " + file_info.GetFileName() + "\nLine : " + file_info.GetFileLineNumber().ToString() + "\nException : " + errorMassage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }

        private void Successful(string title, string message)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        //#endregion Messages

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region Load XML file and apply Setting

        private void AddNodesToTreeView()
        {
            treeView1.Nodes[0].Nodes.Clear();

            for (int i = 0; i < 2; i++)
            {
                treeView1.Nodes[1].Nodes[i].Nodes.Clear();
            }

            for (int i = 0; i < 5; i++)
            {
                treeView1.Nodes[2].Nodes[i].Nodes.Clear();
            }

            for (int i = 0; i < 8; i++)
            {
                treeView1.Nodes[3].Nodes[i].Nodes.Clear();
            }

            for (int i = 0; i < 14; i++)
            {
                treeView1.Nodes[4].Nodes[i].Nodes.Clear();
            }

            treeView1.Nodes[5].Nodes.Clear();

            var questions = from q in Variables.Leitner.ToLearn
                            orderby q.Id
                            select q;

            foreach (var question in questions)
            {
                int intLocation = GetDisplayLocation(question);

                if (intLocation == 0)
                {
                    treeView1.Nodes[0].Nodes.Add("Word" + question.Id.ToString(), "Item " +
                        question.Id.ToString() + ":" +
                        GetGoodCount(question).ToString() + "/" +
                        GetBadCount(question).ToString(), 3, 3);
                }
                else
                    if (intLocation == 30)
                    {
                        treeView1.Nodes[5].Nodes.Add("Word" + question.Id.ToString(), "Item " +
                            question.Id.ToString() + ":" +
                            GetGoodCount(question).ToString() + "/" +
                            GetBadCount(question).ToString(), 3, 3);
                    }
                    else
                    {
                        int intBox = 0;
                        int intPart = 0;
                        CalculateBoxPartFromDisplayLocation(ref intBox, ref intPart, intLocation);

                        treeView1.Nodes[intBox - 1].Nodes[intPart - 1].Nodes.Add("Word" + question.Id.ToString(),
                            "Item " + question.Id.ToString() + ":" +
                            GetGoodCount(question).ToString() + "/" +
                            GetBadCount(question).ToString(), 3, 3);
                    }
            }
        }

        private void LoadXML()
        {
            if ((Variables.LessonTableName == null) || (Variables.LessonTableName == ""))
            {
                return;
            }
            Variables.Leitner = new LeitnerBox();
            try
            {
                string strBlanks = "               ";


                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {

                        string strSide1Name = n01set.Side1Name.Trim();
                        if (strSide1Name.Length > 16)
                            strSide1Name = strSide1Name.Substring(0, 16);
                        if (strSide1Name.Length < 16)
                            strSide1Name = strSide1Name + strBlanks.Substring(0, 16 - strSide1Name.Length);

                        string strSide2Name = n01set.Side2Name.Trim();
                        if (strSide2Name.Length > 16)
                            strSide2Name = strSide2Name.Substring(0, 16);
                        if (strSide2Name.Length < 16)
                            strSide2Name = strSide2Name + strBlanks.Substring(0, 16 - strSide2Name.Length);

                        string strSide3Name = n01set.Side3Name.Trim();
                        if (strSide3Name.Length > 16)
                            strSide3Name = strSide3Name.Substring(0, 16);
                        if (strSide3Name.Length < 16)
                            strSide3Name = strSide3Name + strBlanks.Substring(0, 16 - strSide3Name.Length);

                        Variables.RadioButtonText[1] = strSide1Name +
                                 strSide2Name +
                                 strSide3Name;
                        Variables.RadioButtonText[2] = strSide1Name +
                                                         strSide3Name +
                                                         strSide2Name;
                        Variables.RadioButtonText[3] = strSide2Name +
                                                         strSide1Name +
                                                         strSide3Name;
                        Variables.RadioButtonText[4] = strSide2Name +
                                                         strSide3Name +
                                                         strSide1Name;
                        Variables.RadioButtonText[5] = strSide3Name +
                                                         strSide1Name +
                                                         strSide2Name;
                        Variables.RadioButtonText[6] = strSide3Name +
                                                         strSide2Name +
                                                         strSide1Name;

                        // Make sure the Lesson file Variables.Date field matches the user default Date selection from USER Variables
                        string strDate = n01set.Date.ToString();

                        if ((Properties.Settings.Default.user_date != "Christian") && (Properties.Settings.Default.user_date != "Persian"))
                        {
                            Properties.Settings.Default.user_date = strDate;
                            Properties.Settings.Default.Save();
                        }

                        // If StartTime has not been set, set it here.
                        try
                        {
                            Variables.LastStartTime = n01set.StartTime;

                            n01set.StartTime = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
                        }
                        catch (Exception ex)
                        {
                            StackFrame file_info = new StackFrame(true);
                            Error(ref file_info, ex.Message);
                        }

                        var n01Localsets = from S01 in Variables.SettingDataContext.Setting01s
                                           select S01;

                        foreach (var n01Localsetting in n01Localsets)
                        {
                            LeitnerBox.SettingRow newLocal = Variables.Leitner.Setting.NewSettingRow();

                            newLocal.QuestionTextBox = n01Localsetting.QuestionTextBox;
                            newLocal.AnswerTextBox = n01Localsetting.AnswerTextBox;
                            newLocal.HintTextBox = n01Localsetting.HintTextBox;
                            newLocal.Date = n01Localsetting.Date;
                            newLocal.StartTime = n01Localsetting.StartTime;
                            newLocal.A_B_PreviousAutoAge = GetValidDate(n01Localsetting.A_B_PreviousAutoAge);
                            newLocal.A_C_PreviousAutoAge = GetValidDate(n01Localsetting.A_C_PreviousAutoAge);
                            newLocal.B_A_PreviousAutoAge = GetValidDate(n01Localsetting.B_A_PreviousAutoAge);
                            newLocal.B_C_PreviousAutoAge = GetValidDate(n01Localsetting.B_C_PreviousAutoAge);
                            newLocal.C_A_PreviousAutoAge = GetValidDate(n01Localsetting.C_A_PreviousAutoAge);
                            newLocal.C_B_PreviousAutoAge = GetValidDate(n01Localsetting.C_B_PreviousAutoAge);
                            newLocal.A_B_AutoAgeInterval = n01Localsetting.A_B_AutoAgeInterval;
                            newLocal.A_C_AutoAgeInterval = n01Localsetting.A_C_AutoAgeInterval;
                            newLocal.B_A_AutoAgeInterval = n01Localsetting.B_A_AutoAgeInterval;
                            newLocal.B_C_AutoAgeInterval = n01Localsetting.B_C_AutoAgeInterval;
                            newLocal.C_A_AutoAgeInterval = n01Localsetting.C_A_AutoAgeInterval;
                            newLocal.C_B_AutoAgeInterval = n01Localsetting.C_A_AutoAgeInterval;
                            newLocal.Side1Name = n01Localsetting.Side1Name;
                            newLocal.Side2Name = n01Localsetting.Side2Name;
                            newLocal.Side3Name = n01Localsetting.Side3Name;
                            newLocal.StudySequence = n01Localsetting.StudySequence;
                            newLocal.StudyMode = n01Localsetting.StudyMode;

                            Variables.Leitner.Setting.AddSettingRow(newLocal);
                        }
                    }
                }

                using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
                {
                    var n01less = from L01 in Variables.LessonDataContext.Lesson01s
                                  orderby L01.Id
                                  select L01;

                    foreach (Lesson01 n01les in n01less)
                    {
                        LeitnerBox.ToLearnRow DestinationRow = Variables.Leitner.ToLearn.NewToLearnRow();

                        DestinationRow.Id = n01les.Id;
                        DestinationRow.Side1 = n01les.Side1;
                        DestinationRow.Side2 = n01les.Side2;
                        DestinationRow.Side3 = n01les.Side3;
                        DestinationRow.A_B_GoodCount = n01les.A_B_GoodCount;
                        DestinationRow.A_C_GoodCount = n01les.A_C_GoodCount;
                        DestinationRow.B_A_GoodCount = n01les.B_A_GoodCount;
                        DestinationRow.B_C_GoodCount = n01les.B_C_GoodCount;
                        DestinationRow.C_A_GoodCount = n01les.C_A_GoodCount;
                        DestinationRow.C_B_GoodCount = n01les.C_B_GoodCount;
                        DestinationRow.A_B_BadCount = n01les.A_B_BadCount;
                        DestinationRow.A_C_BadCount = n01les.A_C_BadCount;
                        DestinationRow.B_A_BadCount = n01les.B_A_BadCount;
                        DestinationRow.B_C_BadCount = n01les.B_C_BadCount;
                        DestinationRow.C_A_BadCount = n01les.C_A_BadCount;
                        DestinationRow.C_B_BadCount = n01les.C_B_BadCount;
                        DestinationRow.A_B_DisplayLocation = n01les.A_B_DisplayLocation;
                        DestinationRow.A_C_DisplayLocation = n01les.A_C_DisplayLocation;
                        DestinationRow.B_A_DisplayLocation = n01les.B_A_DisplayLocation;
                        DestinationRow.B_C_DisplayLocation = n01les.B_C_DisplayLocation;
                        DestinationRow.C_A_DisplayLocation = n01les.C_A_DisplayLocation;
                        DestinationRow.C_B_DisplayLocation = n01les.C_B_DisplayLocation;
                        DestinationRow.A_B_TestDate = n01les.A_B_TestDate;
                        DestinationRow.A_C_TestDate = n01les.A_C_TestDate;
                        DestinationRow.B_A_TestDate = n01les.B_A_TestDate;
                        DestinationRow.B_C_TestDate = n01les.B_C_TestDate;
                        DestinationRow.C_A_TestDate = n01les.C_A_TestDate;
                        DestinationRow.C_B_TestDate = n01les.C_B_TestDate;

                        Variables.Leitner.ToLearn.AddToLearnRow(DestinationRow);

                    }
                }

                // Clear out the old display data
                textBoxAnswer.Text = "";
                textIsThisRight.Text = "";
                textBoxQuestion.Text = "";
                typing.Text = "";
                newtyping.Text = "";
                textBoxHint.Text = "";
                textBoxNewAnswer.Text = "";
                textBoxNewQuestion.Text = "";
                textCh.Text = "";
                textCangjie.Text = "";
                textFarEast.Text = "";
                textBoxNewHint.Text = "";

                string userName = Properties.Settings.Default.LessonDatabase;

                this.Text = Variables.Title + userName + @", (" + Variables.LessonName + @": " + Variables.LastStartTime + @") " + @"   Mode: " + Variables.RadioButtonText[Variables.Leitner.Setting[0].StudyMode + 1] + BuildStudyModeString();

                AddNodesToTreeView();
                ApplySetting();

                treeView1.Enabled = tabControl1.Enabled = true;
#if DEBUG
#else
                timer1.Start();
//              timer2.Start();
#endif
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
                treeView1.Enabled = tabControl1.Enabled = false;
#if DEBUG
#else
                timer1.Stop();
//              timer2.Stop();
#endif
            }
        }

        private void SaveXml()
        {
            try
            {
                //if (Variables.SavingLesson) return;
                //Variables.SavingLesson = true;

                //if (Variables.Reading == null)
                //{
                //    Variables.SavingLesson = false;
                //    return;
                //}
                //if (!Variables.Leitner.HasChanges())
                //{
                //    this.labelAnswerToQuestionMessage.Text = "No pending changes: autosave bypassed";
                //    //Variables.SavingLesson = false;
                //    return;
                //}

                Variables.Leitner.AcceptChanges();
                Variables.Reading = new FileStream(Variables.XmlFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                Variables.Leitner.WriteXml(Variables.Reading);
                Variables.Reading.Close();

                //Variables.SavingLesson = false;

                Successful("Backup", "Your current Lessson has been backed up as an XML file");
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private static void SetTestDate(string newDate, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.A_B_TestDate = newDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.A_C_TestDate = newDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.B_A_TestDate = newDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.B_C_TestDate = newDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.C_A_TestDate = newDate;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.C_B_TestDate = newDate;
            }
        }

        private static void SetGoodCount(int newInt, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.A_B_GoodCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.A_C_GoodCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.B_A_GoodCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.B_C_GoodCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.C_A_GoodCount = newInt;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.C_B_GoodCount = newInt;
            }
        }

        private static void SetBadCount(int newInt, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.A_B_BadCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.A_C_BadCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.B_A_BadCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.B_C_BadCount = newInt;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.C_A_BadCount = newInt;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.C_B_BadCount = newInt;
            }
        }

        private static void SetDisplayLocation(int newInt, LeitnerBox.ToLearnRow tolearnitem)
        {
            try
            {
                if (newInt < 0 || newInt > 30)
                    throw new IndexOutOfRangeException();

                if (Variables.Leitner.Setting[0].StudyMode == 0)
                {
                    tolearnitem.A_B_DisplayLocation = newInt;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 1)
                {
                    tolearnitem.A_C_DisplayLocation = newInt;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 2)
                {
                    tolearnitem.B_A_DisplayLocation = newInt;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 3)
                {
                    tolearnitem.B_C_DisplayLocation = newInt;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 4)
                {
                    tolearnitem.C_A_DisplayLocation = newInt;
                }

                else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
                {
                    tolearnitem.C_B_DisplayLocation = newInt;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        private static void SetQuestion(string newQuestion, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.Side1 = newQuestion;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.Side1 = newQuestion;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.Side2 = newQuestion;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.Side2 = newQuestion;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.Side3 = newQuestion;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.Side3 = newQuestion;
            }
        }

        private static void SetAnswer(string newAnswer, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.Side2 = newAnswer;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.Side3 = newAnswer;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.Side1 = newAnswer;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.Side3 = newAnswer;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.Side1 = newAnswer;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.Side2 = newAnswer;
            }
        }

        private static void SetHint(string newHint, LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                tolearnitem.Side3 = newHint;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                tolearnitem.Side2 = newHint;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                tolearnitem.Side3 = newHint;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                tolearnitem.Side1 = newHint;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                tolearnitem.Side2 = newHint;
            }

            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                tolearnitem.Side1 = newHint;
            }
        }

        private static string GetHint(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.Side3;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.Side2;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.Side3;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.Side1;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.Side2;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.Side1;
            }
            return "Invalid Hint requested";
        }

        private static string GetAnswer(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.Side2;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.Side3;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.Side1;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.Side3;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.Side1;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.Side2;
            }
            return "Invalid Answer requested";
        }

        private static string GetQuestion(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.Side1;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.Side1;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.Side2;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.Side2;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.Side3;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.Side3;
            }
            return "Invalid Question requested";
        }

        private static string GetTestDate(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.A_B_TestDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.A_C_TestDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.B_A_TestDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.B_C_TestDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.C_A_TestDate;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.C_B_TestDate;
            }
            return DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
        }

        private static int GetDisplayLocation(LeitnerBox.ToLearnRow tolearnitem)
        {
            try
            {
                if (Variables.Leitner.Setting[0].StudyMode == 0)
                {
                    int GetChecker = tolearnitem.A_B_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 1)
                {
                    int GetChecker = tolearnitem.A_C_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 2)
                {
                    int GetChecker = tolearnitem.B_A_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 3)
                {
                    int GetChecker = tolearnitem.B_C_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 4)
                {
                    int GetChecker = tolearnitem.C_A_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }

                else if (Variables.Leitner.Setting[0].StudyMode == 5)
                {
                    int GetChecker = tolearnitem.C_B_DisplayLocation;
                    if (GetChecker < 0 || GetChecker > 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return GetChecker;
                }
                return 30;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
        }

        private static int GetGoodCount(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.A_B_GoodCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.A_C_GoodCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.B_A_GoodCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.B_C_GoodCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.C_A_GoodCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.C_B_GoodCount;
            }
            return 0;
        }

        private static int GetBadCount(LeitnerBox.ToLearnRow tolearnitem)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return tolearnitem.A_B_BadCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return tolearnitem.A_C_BadCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return tolearnitem.B_A_BadCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return tolearnitem.B_C_BadCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return tolearnitem.C_A_BadCount;
            }

            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return tolearnitem.C_B_BadCount;
            }
            return 0;
        }

        private static void GetAgingIntervalAndPrevDate(ref int aginginterval, ref string prevAutoAgeing)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                aginginterval = Variables.Leitner.Setting[0].A_B_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].A_B_PreviousAutoAge);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                aginginterval = Variables.Leitner.Setting[0].A_C_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].A_C_PreviousAutoAge);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                aginginterval = Variables.Leitner.Setting[0].B_A_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].B_A_PreviousAutoAge);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                aginginterval = Variables.Leitner.Setting[0].B_C_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].B_C_PreviousAutoAge);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                aginginterval = Variables.Leitner.Setting[0].C_A_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].C_A_PreviousAutoAge);
            }
            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                aginginterval = Variables.Leitner.Setting[0].C_B_AutoAgeInterval;
                prevAutoAgeing = GetValidDate(Variables.Leitner.Setting[0].C_B_PreviousAutoAge);
            }
        }

        private static void SetPrevousAutoAge()
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                Variables.Leitner.Setting[0].A_B_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                Variables.Leitner.Setting[0].A_C_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                Variables.Leitner.Setting[0].B_A_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                Variables.Leitner.Setting[0].B_C_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                Variables.Leitner.Setting[0].C_A_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }
            else //if ( Variables.Leitner.Setting[0].StudyMode == 5 )
            {
                Variables.Leitner.Setting[0].C_B_PreviousAutoAge = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            }

            UpdateSQLSettings();
        }

        private static void UpdateSQLSettings()
        {
            using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
            {

                var settings = from s in Variables.SettingDataContext.Setting01s
                               select s;

                foreach (var n01set in settings)
                {
                    n01set.QuestionTextBox = Variables.Leitner.Setting[0].QuestionTextBox;
                    n01set.AnswerTextBox = Variables.Leitner.Setting[0].AnswerTextBox;
                    n01set.HintTextBox = Variables.Leitner.Setting[0].HintTextBox;
                    n01set.Date = Variables.Leitner.Setting[0].Date;
                    n01set.StartTime = Variables.Leitner.Setting[0].StartTime;
                    n01set.A_B_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].A_B_PreviousAutoAge);
                    n01set.A_C_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].A_C_PreviousAutoAge);
                    n01set.B_A_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].B_A_PreviousAutoAge);
                    n01set.B_C_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].B_C_PreviousAutoAge);
                    n01set.C_A_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].C_A_PreviousAutoAge);
                    n01set.C_B_PreviousAutoAge = GetValidDate(Variables.Leitner.Setting[0].C_B_PreviousAutoAge);
                    n01set.A_B_AutoAgeInterval = Variables.Leitner.Setting[0].A_B_AutoAgeInterval;
                    n01set.A_C_AutoAgeInterval = Variables.Leitner.Setting[0].A_C_AutoAgeInterval;
                    n01set.B_A_AutoAgeInterval = Variables.Leitner.Setting[0].B_A_AutoAgeInterval;
                    n01set.B_C_AutoAgeInterval = Variables.Leitner.Setting[0].B_C_AutoAgeInterval;
                    n01set.C_A_AutoAgeInterval = Variables.Leitner.Setting[0].C_A_AutoAgeInterval;
                    n01set.C_B_AutoAgeInterval = Variables.Leitner.Setting[0].C_A_AutoAgeInterval;
                    n01set.Side1Name = Variables.Leitner.Setting[0].Side1Name;
                    n01set.Side2Name = Variables.Leitner.Setting[0].Side2Name;
                    n01set.Side3Name = Variables.Leitner.Setting[0].Side3Name;
                    n01set.StudySequence = Variables.Leitner.Setting[0].StudySequence;
                    n01set.StudyMode = Variables.Leitner.Setting[0].StudyMode;
                }

                Variables.SettingDataContext.SubmitChanges();
            }
            //SaveXML();
        }

        private void ApplySetting()
        {
            try
            {
                string Date = Variables.Leitner.Setting[0].Date.ToString();

                if (Date == "Christian")
                {
                    ToolStripMenuItemChristianDate.Checked = true;
                    ToolStripMenuItemPersianDate.Checked = false;
                }
                else
                {
                    ToolStripMenuItemChristianDate.Checked = false;
                    ToolStripMenuItemPersianDate.Checked = true;
                }

                string QuestionTextBoxes = Variables.Leitner.Setting[0].QuestionTextBox.ToString();
                if (QuestionTextBoxes == "RightToLeft")
                    RightToLeftQuestion();
                else
                    LeftToRightQuestion();

                string AnswerTextBoxes = Variables.Leitner.Setting[0].AnswerTextBox.ToString();

                if (AnswerTextBoxes == "RightToLeft")
                    RightToLeftAnswer();
                else
                    LeftToRightAnswer();

                string HintTextBoxes = Variables.Leitner.Setting[0].HintTextBox.ToString();
                if (HintTextBoxes == "RightToLeft")
                    RightToLeftHint();
                else
                    LeftToRightHint();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void RightToLeftQuestion()
        {
            ToolStripMenuItemLeftToRightQuestion.Checked = false;
            ToolStripMenuItemRightToLeftQuestion.Checked = true;

            textBoxNewQuestion.RightToLeft = RightToLeft.Yes;
            textBoxQuestion.RightToLeft = RightToLeft.Yes;
            //typing.Text.RightToLeft = RightToLeft.Yes;
            textBoxSearchQuestion.RightToLeft = RightToLeft.Yes;
        }

        private void LeftToRightQuestion()
        {
            ToolStripMenuItemLeftToRightQuestion.Checked = true;
            ToolStripMenuItemRightToLeftQuestion.Checked = false;

            textBoxNewQuestion.RightToLeft = RightToLeft.No;
            textBoxQuestion.RightToLeft = RightToLeft.No;
            //typing.Text.RightToLeft = RightToLeft.No;
            textBoxSearchQuestion.RightToLeft = RightToLeft.No;
        }

        private void RightToLeftAnswer()
        {
            ToolStripMenuItemRightToLeftAnswer.Checked = true;
            ToolStripMenuItemLeftToRightAnswer.Checked = false;

            textBoxNewAnswer.RightToLeft = RightToLeft.Yes;
            textBoxAnswer.RightToLeft = RightToLeft.Yes;
            textBoxSearchAnswer.RightToLeft = RightToLeft.Yes;
        }

        private void LeftToRightAnswer()
        {
            ToolStripMenuItemRightToLeftAnswer.Checked = false;
            ToolStripMenuItemLeftToRightAnswer.Checked = true;

            textBoxNewAnswer.RightToLeft = RightToLeft.No;
            textBoxAnswer.RightToLeft = RightToLeft.No;
            textBoxSearchAnswer.RightToLeft = RightToLeft.No;
        }

        private void RightToLeftHint()
        {
            ToolStripMenuItemLeftToRightHint.Checked = false;
            ToolStripMenuItemRightToLeftHint.Checked = true;

            textBoxNewHint.RightToLeft = RightToLeft.Yes;
            textBoxHint.RightToLeft = RightToLeft.Yes;
            textBoxSearchHint.RightToLeft = RightToLeft.Yes;
        }

        private void LeftToRightHint()
        {
            ToolStripMenuItemLeftToRightHint.Checked = true;
            ToolStripMenuItemRightToLeftHint.Checked = false;

            textBoxNewHint.RightToLeft = RightToLeft.No;
            textBoxHint.RightToLeft = RightToLeft.No;
            textBoxSearchHint.RightToLeft = RightToLeft.No;
        }

        //#endregion Load XML file and apply Setting

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region XML Queries -> Updating XML file

        /// <summary>
        /// Adds an element to the xml file
        /// This method is obsolete in the six-mode version and is currently unused
        /// </summary>

        private bool AddToXMLAndTreeView(ref int oldID, string boxID, string partID, string newQuestion, string newAnswer, string newGood, string newBad, string newHint, string date, bool selectDestinationTreeNode)
        {
            try
            {
                LeitnerBox.ToLearnRow WorkingSelectedItem = Variables.Leitner.ToLearn.NewToLearnRow();
                InitNewRow(WorkingSelectedItem);

                SetQuestion(newQuestion, WorkingSelectedItem);
                SetAnswer(newAnswer, WorkingSelectedItem);
                SetHint(newHint, WorkingSelectedItem);
                SetGoodCount(GetValidInt(newGood), WorkingSelectedItem);
                SetBadCount(Convert.ToInt32(newBad), WorkingSelectedItem);
                SetTestDate(date, WorkingSelectedItem);

                int maxID = 0;
                if (oldID != 0)
                {
                    maxID = oldID;
                }
                else
                {
                    try
                    {
                        maxID = 0;
                        var questions = from q in Variables.Leitner.ToLearn
                                        orderby q.Id
                                        select q;

                        foreach (var question in questions)
                        {
                            if (question.Id > maxID)
                                maxID = question.Id;
                        }
                    }
                    catch { }
                    maxID++;
                }

                WorkingSelectedItem.Id = maxID;
                Variables.Leitner.ToLearn.AddToLearnRow(WorkingSelectedItem);

                OneRecordAdded(WorkingSelectedItem);
                //SaveXml();

                oldID = maxID; // tell caller the new item number for possible display to user

                AddToTreeView(boxID, partID, maxID);

                this._selectedItem = WorkingSelectedItem;

                return true;
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
                return false;
            }
        }

        private bool AddToTreeView(string boxID, string partID, int maxID)
        {
            //Adds a new TreeNode to treeView
            TreeNode destinationTreeNode = new TreeNode();
            if (boxID == "1")
            {
                destinationTreeNode = treeView1.Nodes.Find("Box1", false).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + "0" + "/" + "0", 3, 3);
                return true;
            }
            else if ((boxID == "DataBase") || (boxID == ""))
            {
                destinationTreeNode = treeView1.Nodes.Find("DataBase", true).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + "0" + "/" + "0", 3, 3);
                return true;
            }

            if (boxID != "DataBase")
            {
                destinationTreeNode = treeView1.Nodes.Find("Box" + boxID + "Part" + partID, true).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + "0" + "/" + "0", 3, 3);
                return true;
            }

            return false;
        }

        private bool AddToTreeView(string boxID, string partID, int maxID, int goodCount, int badCount)
        {
            //Adds a new TreeNode to treeView
            TreeNode destinationTreeNode = new TreeNode();
            if (boxID == "1")
            {
                destinationTreeNode = treeView1.Nodes.Find("Box1", false).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + goodCount.ToString() + "/" + badCount.ToString(), 3, 3);
                return true;
            }
            else if ((boxID == "DataBase") || (boxID == ""))
            {
                destinationTreeNode = treeView1.Nodes.Find("DataBase", true).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + goodCount.ToString() + "/" + badCount.ToString(), 3, 3);
                return true;
            }

            if (boxID != "DataBase")
            {
                destinationTreeNode = treeView1.Nodes.Find("Box" + boxID + "Part" + partID, true).First();
                destinationTreeNode.Nodes.Add("Word" + Convert.ToString(maxID), "Item" + " " + Convert.ToString(maxID) + ":" + goodCount.ToString() + "/" + badCount.ToString(), 3, 3);
                return true;
            }

            return false;
        }

        private static void InitNewRow(LeitnerBox.ToLearnRow DestinationRow)
        {
            DestinationRow.Side1 = "";
            DestinationRow.Side2 = "";
            DestinationRow.Side3 = "";

            DestinationRow.A_B_BadCount = 0;
            DestinationRow.A_C_BadCount = 0;
            DestinationRow.B_A_BadCount = 0;
            DestinationRow.B_C_BadCount = 0;
            DestinationRow.C_A_BadCount = 0;
            DestinationRow.C_B_BadCount = 0;

            DestinationRow.A_B_DisplayLocation = 0;
            DestinationRow.A_C_DisplayLocation = 0;
            DestinationRow.B_A_DisplayLocation = 0;
            DestinationRow.B_C_DisplayLocation = 0;
            DestinationRow.C_A_DisplayLocation = 0;
            DestinationRow.C_B_DisplayLocation = 0;

            DestinationRow.A_B_GoodCount = 0;
            DestinationRow.A_C_GoodCount = 0;
            DestinationRow.B_A_GoodCount = 0;
            DestinationRow.B_C_GoodCount = 0;
            DestinationRow.C_A_GoodCount = 0;
            DestinationRow.C_B_GoodCount = 0;

            DestinationRow.A_B_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            DestinationRow.A_C_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            DestinationRow.B_A_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            DestinationRow.B_C_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            DestinationRow.C_A_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            DestinationRow.C_B_TestDate = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
        }

        //#endregion XML Queries -> Updating XML file

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region tabControl1

        //#region TabPages Enter Events

        private void TabPageInsertWord_Enter(object sender, EventArgs e)
        {
            listBoxAutoComplete.Visible = false;
        }

        private void TabPageExplorer_Enter(object sender, EventArgs e)
        {
            labelAnswerToQuestionMessage.Text = "";
            Variables.EnableAutoComplete = false;
            listBoxAutoComplete.Visible = false;
        }

        private void TabPageStatistics_Enter(object sender, EventArgs e)
        {
            TabPageStatistics_Enter();
        }

        private void TabPageStatistics_Enter()
        {
            var sTotalP1 = 0;
            var sTotalP2 = 0;
            var sTotalP3 = 0;
            var sTotalP4 = 0;
            var sTotalP5 = 0;
            var sTotalP6 = 0;
            var sTotalP7 = 0;
            var sTotalP8 = 0;
            var sTotalP9 = 0;
            var sTotalP10 = 0;
            var sTotalP11 = 0;
            var sTotalP12 = 0;
            var sTotalP13 = 0;
            var sTotalP14 = 0;
            var intStatisticsBox11 = 0;
            var intStatisticsBox21 = 0;
            var intStatisticsBox22 = 0;
            var intStatisticsBox31 = 0;
            var intStatisticsBox32 = 0;
            var intStatisticsBox33 = 0;
            var intStatisticsBox34 = 0;
            var intStatisticsBox35 = 0;
            var intStatisticsBox41 = 0;
            var intStatisticsBox42 = 0;
            var intStatisticsBox43 = 0;
            var intStatisticsBox44 = 0;
            var intStatisticsBox45 = 0;
            var intStatisticsBox46 = 0;
            var intStatisticsBox47 = 0;
            var intStatisticsBox48 = 0;
            var intStatisticsBox51 = 0;
            var intStatisticsBox52 = 0;
            var intStatisticsBox53 = 0;
            var intStatisticsBox54 = 0;
            var intStatisticsBox55 = 0;
            var intStatisticsBox56 = 0;
            var intStatisticsBox57 = 0;
            var intStatisticsBox58 = 0;
            var intStatisticsBox59 = 0;
            var intStatisticsBox510 = 0;
            var intStatisticsBox511 = 0;
            var intStatisticsBox512 = 0;
            var intStatisticsBox513 = 0;
            var intStatisticsBox514 = 0;
            var intStatisticsArchive1 = 0;

            if (Variables.LessonTableName == "")
                return;
            var ItemsFound = 0;
            try
            {
                Variables.EnableAutoComplete = false;
                listBoxAutoComplete.Visible = false;

                var tolearnrow = from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 select q;

                foreach (var tolearnitem in tolearnrow)
                {
                    ItemsFound++;

                    var thisDisplayLocation = GetDisplayLocation(tolearnitem);

                    switch (thisDisplayLocation)
                    {
                        case 0:
                            intStatisticsBox11++;
                            sTotalP1++;
                            break;
                        case 1:
                            intStatisticsBox21++;
                            sTotalP1++;
                            break;
                        case 2:
                            intStatisticsBox22++;
                            sTotalP2++;
                            break;
                        case 3:
                            intStatisticsBox31++;
                            sTotalP1++;
                            break;
                        case 4:
                            intStatisticsBox32++;
                            sTotalP2++;
                            break;
                        case 5:
                            intStatisticsBox33++;
                            sTotalP3++;
                            break;
                        case 6:
                            intStatisticsBox34++;
                            sTotalP4++;
                            break;
                        case 7:
                            intStatisticsBox35++;
                            sTotalP5++;
                            break;
                        case 8:
                            intStatisticsBox41++;
                            sTotalP1++;
                            break;
                        case 9:
                            intStatisticsBox42++;
                            sTotalP2++;
                            break;
                        case 10:
                            intStatisticsBox43++;
                            sTotalP3++;
                            break;
                        case 11:
                            intStatisticsBox44++;
                            sTotalP4++;
                            break;
                        case 12:
                            intStatisticsBox45++;
                            sTotalP5++;
                            break;
                        case 13:
                            intStatisticsBox46++;
                            sTotalP6++;
                            break;
                        case 14:
                            intStatisticsBox47++;
                            sTotalP7++;
                            break;
                        case 15:
                            intStatisticsBox48++;
                            sTotalP8++;
                            break;
                        case 16:
                            intStatisticsBox51++;
                            sTotalP1++;
                            break;
                        case 17:
                            intStatisticsBox52++;
                            sTotalP2++;
                            break;
                        case 18:
                            intStatisticsBox53++;
                            sTotalP3++;
                            break;
                        case 19:
                            intStatisticsBox54++;
                            sTotalP4++;
                            break;
                        case 20:
                            intStatisticsBox55++;
                            sTotalP5++;
                            break;
                        case 21:
                            intStatisticsBox56++;
                            sTotalP6++;
                            break;
                        case 22:
                            intStatisticsBox57++;
                            sTotalP7++;
                            break;
                        case 23:
                            intStatisticsBox58++;
                            sTotalP8++;
                            break;
                        case 24:
                            intStatisticsBox59++;
                            sTotalP9++;
                            break;
                        case 25:
                            intStatisticsBox510++;
                            sTotalP10++;
                            break;
                        case 26:
                            intStatisticsBox511++;
                            sTotalP11++;
                            break;
                        case 27:
                            intStatisticsBox512++;
                            sTotalP12++;
                            break;
                        case 28:
                            intStatisticsBox513++;
                            sTotalP13++;
                            break;
                        case 29:
                            intStatisticsBox514++;
                            sTotalP14++;
                            break;
                        default:
                            intStatisticsArchive1++;
                            break;
                    }
                }

                labelSTotalP_1.Text = Convert.ToString(sTotalP1);
                labelSTotalP_2.Text = Convert.ToString(sTotalP2);
                labelSTotalP_3.Text = Convert.ToString(sTotalP3);
                labelSTotalP_4.Text = Convert.ToString(sTotalP4);
                labelSTotalP_5.Text = Convert.ToString(sTotalP5);
                labelSTotalP_6.Text = Convert.ToString(sTotalP6);
                labelSTotalP_7.Text = Convert.ToString(sTotalP7);
                labelSTotalP_8.Text = Convert.ToString(sTotalP8);
                labelSTotalP_9.Text = Convert.ToString(sTotalP9);
                labelSTotalP_10.Text = Convert.ToString(sTotalP10);
                labelSTotalP_11.Text = Convert.ToString(sTotalP11);
                labelSTotalP_12.Text = Convert.ToString(sTotalP12);
                labelSTotalP_13.Text = Convert.ToString(sTotalP13);
                labelSTotalP_14.Text = Convert.ToString(sTotalP14);

                labelStatisticsBox1_1.Text = intStatisticsBox11.ToString();
                labelStatisticsBox2_1.Text = intStatisticsBox21.ToString();
                labelStatisticsBox2_2.Text = intStatisticsBox22.ToString();
                labelStatisticsBox3_1.Text = intStatisticsBox31.ToString();
                labelStatisticsBox3_2.Text = intStatisticsBox32.ToString();
                labelStatisticsBox3_3.Text = intStatisticsBox33.ToString();
                labelStatisticsBox3_4.Text = intStatisticsBox34.ToString();
                labelStatisticsBox3_5.Text = intStatisticsBox35.ToString();
                labelStatisticsBox4_1.Text = intStatisticsBox41.ToString();
                labelStatisticsBox4_2.Text = intStatisticsBox42.ToString();
                labelStatisticsBox4_3.Text = intStatisticsBox43.ToString();
                labelStatisticsBox4_4.Text = intStatisticsBox44.ToString();
                labelStatisticsBox4_5.Text = intStatisticsBox45.ToString();
                labelStatisticsBox4_6.Text = intStatisticsBox46.ToString();
                labelStatisticsBox4_7.Text = intStatisticsBox47.ToString();
                labelStatisticsBox4_8.Text = intStatisticsBox48.ToString();
                labelStatisticsBox5_1.Text = intStatisticsBox51.ToString();
                labelStatisticsBox5_2.Text = intStatisticsBox52.ToString();
                labelStatisticsBox5_3.Text = intStatisticsBox53.ToString();
                labelStatisticsBox5_4.Text = intStatisticsBox54.ToString();
                labelStatisticsBox5_5.Text = intStatisticsBox55.ToString();
                labelStatisticsBox5_6.Text = intStatisticsBox56.ToString();
                labelStatisticsBox5_7.Text = intStatisticsBox57.ToString();
                labelStatisticsBox5_8.Text = intStatisticsBox58.ToString();
                labelStatisticsBox5_9.Text = intStatisticsBox59.ToString();
                labelStatisticsBox5_10.Text = intStatisticsBox510.ToString();
                labelStatisticsBox5_11.Text = intStatisticsBox511.ToString();
                labelStatisticsBox5_12.Text = intStatisticsBox512.ToString();
                labelStatisticsBox5_13.Text = intStatisticsBox513.ToString();
                labelStatisticsBox5_14.Text = intStatisticsBox514.ToString();
                labelStatisticsArchive_1.Text = intStatisticsArchive1.ToString();

                var gTotal = (sTotalP1 +
                                sTotalP2 +
                                sTotalP3 +
                                sTotalP4 +
                                sTotalP5 +
                                sTotalP6 +
                                sTotalP7 +
                                sTotalP8 +
                                sTotalP9 +
                                sTotalP10 +
                                sTotalP11 +
                                sTotalP12 +
                                sTotalP13 +
                                sTotalP14 +
                                intStatisticsArchive1);

                labelGTotal.Text = gTotal.ToString(CultureInfo.InvariantCulture);
                labelStatisticsArchive_3.Text = (gTotal - intStatisticsArchive1).ToString(CultureInfo.InvariantCulture);

                //string dateString = (from q in Variables.xDocument.Descendants("StartTime") select q.Value).First();
                var dateString = Variables.LastStartTime;

                //Start date
                labelStatisticsDate.Text = ToolStripMenuItemPersianDate.Checked ? ComputePersianDate(DateTime.Parse(dateString)) : dateString;
                //\\
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        private void TabPageSearch_Enter(object sender, EventArgs e)
        {
            Variables.EnableAutoComplete = false;
            listBoxAutoComplete.Visible = false;

            listViewSearch.Items.Clear();

            buttonSearchDelete.Enabled = false;
            buttonSearchMoveToBox1.Enabled = false;
            textBoxSearchAnswer.Enabled = false;
            textBoxSearchQuestion.Enabled = false;

            textBoxSearchAnswer.Text = "";
            textBoxSearch.Text = "";
            textBoxSearchHint.Text = "";
            textBoxSearchQuestion.Text = "";
            labelSearchMessage.Text = "";
        }

        //#endregion TabPages Enter Events

        //#region Buttons

        private void ButtonAddNewWord_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                labelAddQuestionMessage.Text = "";

                ////////////////////////////////////
                errorProvider1.Clear();

                labelAddQuestionMessage.Text = "";

                if (textBoxNewQuestion.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxNewQuestion, "Please fill in this textbox");
                    return;
                }
                else if (textBoxNewAnswer.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxNewAnswer, "Please fill in this textbox");
                    return;
                }

                SetSideStrings(out string strSide1, out var strSide2, out var strSide3);

                var duplicates = (from q in Variables.Leitner.ToLearn
                                  where q.Side1.ToLower() == strSide1.ToLower()
                                  where q.Side2.ToLower() == strSide2.ToLower()
                                  where q.Side3.ToLower() == strSide3.ToLower()
                                  orderby q.Id
                                  select q).Count();

                if (duplicates > 0)
                {
                    errorProvider1.SetError(this.textBoxNewQuestion, "There is a duplicate Item in the Lesson File");
                    return;
                }
                ////////////////////////////////////

                string boxID = labelBoxID.Text;
                if (boxID == "Archive") boxID = "DataBase";
                string partID = "";
                try
                {
                    partID = labelPartID.Text;
                }
                catch { }

                int currID = 0;

                //string hint = textBoxNewHint.Text;
                if (!CreateNewItem(strSide1, strSide2, strSide3, ref currID))
                {
                    labelAddQuestionMessage.Text = "Error in creating new database row";
                    return;
                }

                // It has been decreed that all ADD commands will add to Box 1. This is because
                // the default for 'display location' is set to '0' so the display tree must be Box 1
                // regardless of what the current node is. This makes new words immediately available for study.
                // If you don't want to study the new word immediately, move it now.
                if (!AddToTreeView("1", "", currID))
                {
                    labelAddQuestionMessage.Text = "Error in adding data to XML file";
                    return;
                }

                //if (partID != "")
                //    labelAddQuestionMessage.Text = "Item " + currID.ToString() + " was added to Box " + boxID + " -> " + "Part " + partID + " successfully";
                //else if ((boxID == "DataBase") || (boxID == ""))
                labelAddQuestionMessage.Text = "Item " + currID.ToString() + " was added to Box 1 successfully";
                //else
                //    labelAddQuestionMessage.Text = "Item " + currID.ToString() + " was added to Box " + boxID + " successfully";

                textBoxNewQuestion.Text = "";
                textCh.Text = "";
                textCangjie.Text = "";
                textFarEast.Text = "";
                newtyping.Text = "";
                textBoxNewHint.Text = "";
                textBoxNewAnswer.Text = "";
                textBoxQuestion.Text = "";
                typing.Text = "";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }

            // Update the stats page for the new entry
            TabPageStatistics_Enter();

            //SaveXml();
        }

        private void SetSideStrings(out string strSide1, out string strSide2, out string strSide3)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                strSide1 = textBoxNewQuestion.Text.Trim();
                strSide2 = textBoxNewAnswer.Text.Trim();
                strSide3 = textBoxNewHint.Text.Trim();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                strSide1 = textBoxNewQuestion.Text.Trim();
                strSide3 = textBoxNewAnswer.Text.Trim();
                strSide2 = textBoxNewHint.Text.Trim();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                strSide2 = textBoxNewQuestion.Text.Trim();
                strSide1 = textBoxNewAnswer.Text.Trim();
                strSide3 = textBoxNewHint.Text.Trim();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                strSide2 = textBoxNewQuestion.Text.Trim();
                strSide3 = textBoxNewAnswer.Text.Trim();
                strSide1 = textBoxNewHint.Text.Trim();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                strSide3 = textBoxNewQuestion.Text.Trim();
                strSide1 = textBoxNewAnswer.Text.Trim();
                strSide2 = textBoxNewHint.Text.Trim();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                strSide3 = textBoxNewQuestion.Text.Trim();
                strSide2 = textBoxNewAnswer.Text.Trim();
                strSide1 = textBoxNewHint.Text.Trim();
            }
            else
            {
                strSide1 = "";
                strSide2 = "";
                strSide3 = "";
            }
        }

        private void ButtonDelete1_Click(object sender, EventArgs e)
        {
            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            try
            {
                if (MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                string question = GetQuestion(WorkingSelectedItem);
                string id = WorkingSelectedItem.Id.ToString();
                int ID = WorkingSelectedItem.Id;

                WorkingSelectedItem.Delete();

                OnestudyItemDeleted(ID);
                //SaveXml();
                LoadXML();

                //treeView1.Nodes.Find("Word" + id, true).First().Remove();

                labelAnswerToQuestionMessage.Text = "The Item was deleted.";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ButtonSave1_Click(object sender, EventArgs e)
        {
            LeitnerBox.ToLearnRow workingSelectedItem = this._selectedItem;
            try
            {
                labelAnswerToQuestionMessage.Text = "";

                ////////////////////////////////////
                errorProvider1.Clear();

                if (textBoxQuestion.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxQuestion, "Please fill this textbox");
                    return;
                }

                if (textBoxAnswer.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxAnswer, "Please fill this textbox");
                    return;
                }
                ////////////////////////////////////

                SetQuestion(textBoxQuestion.Text.Trim(), workingSelectedItem);

                SetAnswer(textBoxAnswer.Text.Trim(), workingSelectedItem);

                if (textBoxHint.Enabled == true)
                {
                    SetHint(textBoxHint.Text.Trim(), workingSelectedItem);
                }
                OneRecordChanged(workingSelectedItem);
                //SaveXml();

                labelAnswerToQuestionMessage.Text = @"The changes saved successfully";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ButtonFalse_Click(object sender, EventArgs e)
        {
            var workingSelectedItem = this._selectedItem;
            buttonTrue.Enabled = false;
            buttonFalse.Enabled = false;
            buttonDelete1.Enabled = false;
            buttonSave1.Enabled = false;

            try
            {
                labelAnswerToQuestionMessage.Text = "";

				if (!(string.IsNullOrEmpty(labelPartID.Text) || labelPartID.Text == @"1"))
                {
                    buttonTrue.Enabled = true;
                    buttonFalse.Enabled = true;
                    buttonDelete1.Enabled = true;
                    buttonSave1.Enabled = true;
                    labelAnswerToQuestionMessage.Text = @"Function valid only in Box 1 or any Part 1";
                    return;
                }

                textBoxQuestion.Text = "";
                typing.Text = "";
                textBoxAnswer.Text = "";
                textIsThisRight.Text = "";
                textBoxHint.Text = "";

                var date = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(@"?.?", @"PM").Replace(@"?.?", @"AM");
                SetTestDate(date, workingSelectedItem);
                var goodcount = GetGoodCount(workingSelectedItem);

                var badcount = GetBadCount(workingSelectedItem);
                badcount++;
                SetBadCount(badcount, workingSelectedItem);
                var wordId = treeView1.SelectedNode.Name.Replace(@"Word", @"").Trim();
                var currId = workingSelectedItem.Id;

                //Moves the word to Box 1

                treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                AddToTreeView(@"1", @"", GetValidInt(wordId), goodcount, badcount);
                SetDisplayLocation(0, workingSelectedItem);
                OneRecordChanged(workingSelectedItem);
                labelAnswerToQuestionMessage.Text = @"Moved to Box 1";
                //SendKeys.Send(@"\t");

            }
            catch (Exception ex)
            {
                labelAnswerToQuestionMessage.Text = @"Error";
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
            OneRecordChanged(workingSelectedItem);
            //SaveXml();
        }

        private void ButtonTrue_Click(object sender, EventArgs e)
        {
            buttonTrue.Enabled = false;
            buttonFalse.Enabled = false;
            buttonDelete1.Enabled = false;
            buttonSave1.Enabled = false;
            var workingSelectedItem = _selectedItem;

            try
            {
                labelAnswerToQuestionMessage.Text = "";
                if (labelPartID != null && ((labelPartID.Text != "" && labelPartID.Text != @"1") || (labelBoxID.Text == @"Archive")))
                {
                    buttonTrue.Enabled = true;
                    buttonFalse.Enabled = true;
                    buttonDelete1.Enabled = true;
                    buttonSave1.Enabled = true;
                    labelAnswerToQuestionMessage.Text = @"Function valid only in Box 1 or any Part 1";
                    return;
                }

                textBoxQuestion.Text = "";
                typing.Text = "";
                textBoxAnswer.Text = "";
                textIsThisRight.Text = "";
                textBoxHint.Text = "";

                var date = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
                SetTestDate(date, workingSelectedItem);
                var goodcount = GetGoodCount(workingSelectedItem);
                goodcount++;
                SetGoodCount(goodcount, workingSelectedItem);

                var badcount = GetBadCount(workingSelectedItem);
                var boxId = labelBoxID.Text;
                var partId = "";
                var wordId = treeView1.SelectedNode.Name.Replace("Word", "").Trim();

                // Prepare to identify the destination PartId
                switch (labelBoxID.Text)
                {
                    ////////////////////////////////
                    //Moves the word to Box2 Part2
                    case "1":
                        CalculateBoxPartFromDisplayLocation(ref boxId, ref partId, 2);
                        treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                        AddToTreeView(@"2", @"2", GetValidInt(wordId), goodcount, badcount);
                        SetDisplayLocation(2, workingSelectedItem);
                        DisplayLocationChanged(workingSelectedItem);
                        TabPageStatistics_Enter();
                        labelAnswerToQuestionMessage.Text = @"Moved to Box2 Part2";

                        break;
                    ////////////////////////////////
                    //Moves the word to Box3 Part5
                    case "2":
                        CalculateBoxPartFromDisplayLocation(ref boxId, ref partId, 7);
                        treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                        SetDisplayLocation(7, workingSelectedItem);
                        DisplayLocationChanged(workingSelectedItem);
                        TabPageStatistics_Enter();
                        AddToTreeView(@"3", @"5", GetValidInt(wordId), goodcount, badcount);
                        labelAnswerToQuestionMessage.Text = @"Moved to Box3 Part5";

                        break;
                    ////////////////////////////////
                    case "3":
                        CalculateBoxPartFromDisplayLocation(ref boxId, ref partId, 15);
                        treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                        SetDisplayLocation(15, workingSelectedItem);
                        DisplayLocationChanged(workingSelectedItem);
                        TabPageStatistics_Enter();
                        AddToTreeView(@"4", @"8", GetValidInt(wordId), goodcount, badcount);
                        labelAnswerToQuestionMessage.Text = @"Moved to Box4 Part8";

                        break;
                    ////////////////////////////////
                    case "4":
                        CalculateBoxPartFromDisplayLocation(ref boxId, ref partId, 29);
                        treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                        SetDisplayLocation(29, workingSelectedItem);
                        DisplayLocationChanged(workingSelectedItem);
                        TabPageStatistics_Enter();
                        AddToTreeView(@"5", @"14", GetValidInt(wordId), goodcount, badcount);

                        break;
                    ////////////////////////////////
                    case "5":
                        CalculateBoxPartFromDisplayLocation(ref boxId, ref partId, 30);
                        treeView1.Nodes.Find(@"Word" + wordId, true).First().Remove();
                        SetDisplayLocation(30, workingSelectedItem);
                        SetGoodCount(0, workingSelectedItem);
                        SetBadCount(0, workingSelectedItem);
                        OneRecordChanged(workingSelectedItem);
                        AddToTreeView(@"DataBase", @"", GetValidInt(wordId), 0, 0);
                        labelAnswerToQuestionMessage.Text = @"Moved to Archive";

                        break;
                    ////////////////////////////////
                    default:
                        throw new Exception("wrong box id");
                }
            }
            catch (Exception ex)
            {
                labelAnswerToQuestionMessage.Text = "Error";
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
            OneRecordChanged(workingSelectedItem);
            //SendKeys.Send(@"\t");
        }

        private void ButtonSearchSave_Click(object sender, EventArgs e)
        {
            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            try
            {
                labelSearchMessage.Text = "";

                string strid = listViewSearch.SelectedItems[0].Name.Replace("Item", "");
                int intid = GetValidInt(strid);

                SetQuestion(textBoxSearchQuestion.Text.Trim(), WorkingSelectedItem);
                SetAnswer(textBoxSearchAnswer.Text.Trim(), WorkingSelectedItem);
                SetHint(textBoxSearchHint.Text.Trim(), WorkingSelectedItem);

                textBoxSearch.Text = "";
                textBoxSearchQuestion.Text = "";
                textBoxSearchAnswer.Text = "";
                textBoxSearchHint.Text = "";

                //SaveXml();

                labelSearchMessage.Text = "The changes saved successfully .";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ButtonSearchMoveToBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure ?", "Move", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                string id = listViewSearch.SelectedItems[0].Name.Replace("Item", "");
                int intid = GetValidInt(id);

                var movers = from q in Variables.Leitner.ToLearn
                             where q.Id == intid
                             orderby q.Id
                             select q;

                foreach (var mover in movers)
                {
                    int disploc = GetDisplayLocation(mover);
                    var Items = ListItemsInDisplayLocation(disploc);
                    foreach (var Item in Items)
                    {
                        if (Item.Id == intid)
                        {
                            SetDisplayLocation(0, Item);
                            treeView1.Nodes.Find("Word" + id, true).First().Remove();
                            AddToTreeView("1", "", intid);

                            OneRecordChanged(Item);
                            break;
                        }
                    }
                    break;
                }

                textBoxSearch.Text = "";
                textBoxSearchQuestion.Text = "";
                textBoxSearchAnswer.Text = "";
                textBoxSearchHint.Text = "";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }

            //SaveXml();
            //try
            //{
            //    addNodesToTreeView( );
            //}
            //catch (Exception ex)
            //{
            //    StackFrame file_info = new StackFrame(true);
            //    error(ref file_info, ex.Message);
            //}
        }

        private void ButtonSearchDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                string id = listViewSearch.SelectedItems[0].Name.Replace("Item", "");

                var questions = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Id == GetValidInt(id)
                                 select q);

                foreach (var question in questions)
                {
                    OnestudyItemDeleted(question.Id);

                    question.Delete();
                    break;
                }

                try
                {
                    listViewSearch.Items.Find("Item" + id, true).First().Remove();
                }
                catch { }
                try
                {
                    treeView1.Nodes.Find("Word" + id, true).First().Remove();
                }
                catch { }

                //SaveXml();

                textBoxSearch.Text = "";
                textBoxSearchQuestion.Text = "";
                textBoxSearchAnswer.Text = "";
                textBoxSearchHint.Text = "";

                labelSearchMessage.Text = "The question has been deleted successfully .";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ButtonShowAnswer_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;

            var workingSelectedItem = this._selectedItem;

            buttonTrue.Enabled = !buttonTrue.Enabled;
            buttonFalse.Enabled = !buttonFalse.Enabled;
            buttonDelete1.Enabled = !buttonDelete1.Enabled;
            buttonSave1.Enabled = !buttonSave1.Enabled;

            if (buttonTrue.Enabled)
            {
                try
                {
                    textBoxAnswer.Enabled = true;
                    textBoxAnswer.Text = GetAnswer(workingSelectedItem).Replace("\n", Environment.NewLine);
#if CHINESE
                    // This piece of code is a short-cut for students of Chinese
                    if ((Variables.Leitner.Setting[0].StudyMode == 0 || Variables.Leitner.Setting[0].StudyMode == 5) && (textIsThisRight.Text == textBoxAnswer.Text))
                    {
                        ButtonTrue_Click(sender, e);
                        //SendKeys.Send(@"\t");
                        return;    
                    }
#endif
                    textBoxHint.Enabled = false;
                    textBoxHint.Text = GetHint(workingSelectedItem);
                    ButtonShowHint_Click(sender, e);
                    typing.Text = ExpandCangJei(workingSelectedItem.Side2);
                }
                catch (Exception ex)
                {
                    var fileInfo = new StackFrame(true);
                    Error(ref fileInfo, ex.Message);
                }
            }
            else
            {
                try
                {
                    textBoxAnswer.Enabled = false;
                    textBoxAnswer.Text = @"";
                    textBoxHint.Text = @"";
                    textBoxHint.Enabled = false;
                    typing.Text = @"";
                }
                catch (Exception ex)
                {
                    var fileInfo = new StackFrame(true);
                    Error(ref fileInfo, ex.Message);
                }
            }
        }

        private void ButtonShowHint_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "") return;

            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            if (!textBoxHint.Enabled)
            {
                textBoxHint.Enabled = true;
                textBoxHint.Text = GetHint(WorkingSelectedItem);
                //typing.Text = expandCangJei(WorkingSelectedItem.Side2);
            }
            else
            {
                try
                {
                    textBoxHint.Enabled = false;
                    if (textBoxHint.Text.Trim() != "")
                    {
                        SetHint(textBoxHint.Text.Trim(), WorkingSelectedItem);
                    }
                    textBoxHint.Text = "";
                    //typing.Text = "";
                }
                catch (Exception ex)
                {
                    StackFrame file_info = new StackFrame(true);
                    Error(ref file_info, ex.Message);
                }
            }
        }

        //#endregion Buttons

        //#region Search Methods

        private string ExpandCangJei(string Side2)
        {
            StringBuilder WorkingWork = new StringBuilder();

            try
            {
                char[] ChinChars = Side2.ToCharArray();

                using (FullFarEastDataContext FullFarEast = new FullFarEastDataContext(Properties.Settings.Default.ChineseStudyConnection))
                {
                    for (int index = 0; index < Side2.Length; index++)
                    {
                        char[] workChin = new char[1];
                        workChin[0] = ChinChars[index];
                        string UniHex = ExpandUnihex(workChin);

                        try
                        {
                            var characters = from q in FullFarEast.UniHans
                                             where q.cp == UniHex
                                             //where q.Char == ChinChars[index].ToString()
                                             select q;

                            try
                            {
                                foreach (var character in characters)
                                {
                                    WorkingWork.Append(character.kCangjie);
                                    WorkingWork.Append(" ");
                                    break;
                                }
                            }
                            catch (System.Data.StrongTypingException ex)
                            {
                                WorkingWork.Append(ex.Message);
                                WorkingWork.Append(" ");
                                return WorkingWork.ToString();
                            }
                        }
                        catch (System.Data.StrongTypingException ex)
                        {
                            WorkingWork.Append(ex.Message);
                            WorkingWork.Append(" ");
                            return WorkingWork.ToString();
                        }
                    }
                    return WorkingWork.ToString();
                }
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
                return "";
            }
        }


        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                listViewSearch.Items.Clear();
                if (textBoxSearch.Text.Trim().Equals("") || textBoxSearch.Text.Trim().Length < 1) return;

                IEnumerable<Leitner_Three.LeitnerBox.ToLearnRow> items = null;
                if (checkBoxSearchInQuestion.Checked && checkBoxSearchInAnswer.Checked && checkBoxSearchInHint.Checked)
                {
                    items = (from q in Variables.Leitner.ToLearn
                             where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                             q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                             q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                             orderby q.Id
                             select q);
                }
                else if (checkBoxSearchInQuestion.Checked && checkBoxSearchInAnswer.Checked && !checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        if (Variables.Leitner.Setting[0].StudyMode == 0)
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                        else if (Variables.Leitner.Setting[0].StudyMode == 1)
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                        else if (Variables.Leitner.Setting[0].StudyMode == 2)
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                        else if (Variables.Leitner.Setting[0].StudyMode == 3)
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                        else if (Variables.Leitner.Setting[0].StudyMode == 4)
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                        else
                        {
                            items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                     q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                     select q);
                        }
                    }
                }
                else if (checkBoxSearchInQuestion.Checked && !checkBoxSearchInAnswer.Checked && checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 1)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 2)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 3)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 4)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                }
                else if (checkBoxSearchInQuestion.Checked && !checkBoxSearchInAnswer.Checked && !checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 1)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 2)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 3)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 4)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                }
                else if (!checkBoxSearchInQuestion.Checked && checkBoxSearchInAnswer.Checked && checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 1)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 2)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 3)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 4)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower()) ||
                                 q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                }
                else if (!checkBoxSearchInQuestion.Checked && checkBoxSearchInAnswer.Checked && !checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 1)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 2)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 3)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 4)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                }
                else if (!checkBoxSearchInQuestion.Checked && !checkBoxSearchInAnswer.Checked && checkBoxSearchInHint.Checked)
                {
                    if (Variables.Leitner.Setting[0].StudyMode == 0)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 1)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 2)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side3.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 3)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else if (Variables.Leitner.Setting[0].StudyMode == 4)
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side2.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                    else
                    {
                        items = (from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Side1.ToLower().Contains(textBoxSearch.Text.Trim().ToLower())
                                 select q);
                    }
                }
                else
                {
                    buttonSearchDelete.Enabled = false;
                    buttonSearchMoveToBox1.Enabled = false;
                    textBoxSearchAnswer.Enabled = false;
                    textBoxSearchQuestion.Enabled = false;

                    textBoxSearchAnswer.Text = "";
                    textBoxSearchQuestion.Text = "";
                    textBoxSearchHint.Text = "";
                    return;
                }
                if (items.Count() <= 0)
                {
                    buttonSearchDelete.Enabled = false;
                    buttonSearchMoveToBox1.Enabled = false;
                    textBoxSearchAnswer.Enabled = false;
                    textBoxSearchQuestion.Enabled = false;

                    textBoxSearchAnswer.Text = "";
                    textBoxSearchQuestion.Text = "";
                    textBoxSearchHint.Text = "";
                    return;
                }

                foreach (var item in items)
                {
                    ListViewItem LVitem = new ListViewItem(new string[]
                        { GetQuestion(item),
                          GetAnswer(item),
                          GetHint(item)})
                    {
                        Name = "Item" + item.Id
                    };
                    listViewSearch.Items.Add(LVitem);
                }
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ListViewSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                treeView1.CollapseAll();

                string id = ((ListView)sender).SelectedItems[0].Name.Replace("Item", "");

                try
                {
                    this.treeView1.SelectedNode =
                        this.treeView1.Nodes.Find("Word" + id, true).First();
                }
                catch
                {
                    this.treeView1.SelectedNode = this.treeView1.Nodes.Find("DataBase", true).First();
                }

                buttonSearchDelete.Enabled = true;
                buttonSearchMoveToBox1.Enabled = true;
                textBoxSearchAnswer.Enabled = true;
                textBoxSearchQuestion.Enabled = true;
                textBoxSearchHint.Enabled = true;

                var questions = from q in Variables.Leitner.ToLearn
                                orderby q.Id
                                where q.Id == GetValidInt(id)
                                select q;

                foreach (var question in questions)
                {
                    textBoxSearchQuestion.Text = GetQuestion(question);
                    textBoxSearchAnswer.Text = GetAnswer(question);
                    textBoxSearchHint.Text = GetHint(question);
                }
            }
            catch
            {
                buttonSearchDelete.Enabled = false;
                buttonSearchMoveToBox1.Enabled = false;
                textBoxSearchAnswer.Enabled = false;
                textBoxSearchQuestion.Enabled = false;
                textBoxSearchHint.Enabled = false;
            }
        }

        private void TextBoxNewQuestion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Variables.EnableAutoComplete)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                if (textBoxNewQuestion.Text.Trim() == "") // || textBoxNewQuestion.Text.Trim( ).Length <= 1 )
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                SetSideStrings(out string strSide1, out string strSide2, out var strSide3);

                EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions = null; //= ( from q in Variables.Leitner.ToLearn

                questions = SearchOneSide("A", textBoxNewQuestion.Text.Trim().ToLower(), questions);

                if (questions.Count() == 0)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                listBoxAutoComplete.Items.Clear();
                listBoxAutoComplete.Location = new Point(100, 251);//96, 204
                listBoxAutoComplete.Visible = true;
                listBoxAutoComplete.RightToLeft = textBoxNewQuestion.RightToLeft;

                foreach (var question in questions)
                {
                    listBoxAutoComplete.Items.Add(GetQuestion(question));
                }
            }
            catch
            {
            }
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchOneSide(string column, string lookingfor, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchQuestions(string column, string lookingfor, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchAnswers(string column, string lookingfor, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchHints(string column, string lookingfor, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                if (column == "A")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                if (column == "A")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                if (column == "A")
                {
                    questions = SearchSide3(lookingfor, questions);
                }
                else if (column == "B")
                {
                    questions = SearchSide2(lookingfor, questions);
                }
                else if (column == "C")
                {
                    questions = SearchSide1(lookingfor, questions);
                }
            }
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchSide3(string strSide3, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            questions = (from q in Variables.Leitner.ToLearn
                         orderby q.Id
                         where q.Side3.ToLower().Contains(strSide3)
                         select q);
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchSide2(string strSide2, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            questions = (from q in Variables.Leitner.ToLearn
                         orderby q.Id
                         where q.Side2.ToLower().Contains(strSide2)
                         select q);
            return questions;
        }

        private static EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> SearchSide1(string strSide1, EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions)
        {
            questions = (from q in Variables.Leitner.ToLearn
                         orderby q.Id
                         where q.Side1.ToLower().Contains(strSide1)
                         select q);
            return questions;
        }

        private void TextBoxNewAnswer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Variables.EnableAutoComplete)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                if (textBoxNewAnswer.Text.Trim() == "") // || textBoxNewAnswer.Text.Trim().Length <= 1)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                SetSideStrings(out string strSide1, out string strSide2, out var strSide3);

                EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions = null;

                questions = SearchOneSide("B", textBoxNewAnswer.Text.Trim().ToLower(), questions);

                if (questions.Count() == 0)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                listBoxAutoComplete.Items.Clear();
                listBoxAutoComplete.Location = new Point(100, 89);//96, 204
                listBoxAutoComplete.Visible = true;
                listBoxAutoComplete.RightToLeft = textBoxNewAnswer.RightToLeft;

                foreach (var question in questions)
                {
                    listBoxAutoComplete.Items.Add(GetAnswer(question));
                }
            }
            catch { }
        }

        //#endregion Search Methods

        //#endregion tabControl1

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region TreeView

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // This event is useless at startup; there is no XML file loaded yet.
            if (Variables.LessonTableName == "") return;

            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            try
            {
                string boxID = "DataBase";
                string partID = "";
                int intDisplayLocationLow = 0;
                int intDisplayLocationHigh = 0;
                int intDisplayLocation = 0;
                int intNodeTotal = 0;

                labelAddQuestionMessage.Text = labelAnswerToQuestionMessage.Text = "";
                listBoxAutoComplete.Visible = false;
                Variables.EnableAutoComplete = false;
                buttonTrue.Enabled = false;
                buttonFalse.Enabled = false;
                buttonDelete1.Enabled = false;
                buttonSave1.Enabled = false;
                //buttonSave2.Enabled = false;

                //بدست آوردن تعداد سوال در گره انتخاب شده

                //#region Obtaining the numbers of selected node's descendants

                if (e.Node.Name.Contains("Word"))
                {
                    try
                    {
                        string strWordID = Regex.Replace(e.Node.Name, @"Word", "", RegexOptions.IgnoreCase);
                        int intWordID = GetValidInt(strWordID);
                        var WordRows = from q in Variables.Leitner.ToLearn
                                       orderby q.Id
                                       select q;

                        foreach (var WordRow in WordRows)
                        {
                            if (WordRow.Id == intWordID)
                            {
                                WorkingSelectedItem = WordRow;
                                break;
                            }
                        }

                        label2WordsCount.Text = "1";
                        if (e.Node.Parent.Name.Contains("Part"))
                        {
                            partID = Regex.Replace(e.Node.Parent.Name, @"Box.Part", "", RegexOptions.IgnoreCase);
                            boxID = Regex.Replace(e.Node.Parent.Parent.Name, @"Box", "", RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            partID = "";
                            boxID = "1";
                            if (e.Node.Parent.Name.Contains("DataBase")) boxID = "Archive";
                        }
                    }
                    catch
                    {
                        label2WordsCount.Text = "";
                    }
                }
                else if (e.Node.Name.Contains("Part"))
                {
                    try
                    {
                        boxID = Regex.Replace(e.Node.Parent.Name, @"Box", "", RegexOptions.IgnoreCase);
                        partID = Regex.Replace(e.Node.Name, @"Box.Part", "", RegexOptions.IgnoreCase);
                        intNodeTotal = 0;
                        intDisplayLocation = CalcDisplayLocation(boxID, partID);

                        var questions = from q in Variables.Leitner.ToLearn
                                        orderby q.Id
                                        select q;

                        foreach (var question in questions)
                        {
                            if (GetDisplayLocation(question) == intDisplayLocation)
                            {
                                intNodeTotal++;
                            }
                        }

                        label2WordsCount.Text = intNodeTotal.ToString();
                    }
                    catch
                    {
                        label2WordsCount.Text = "";
                    }
                }
                else if (e.Node.Name.Contains("Box"))
                {
                    try
                    {
                        boxID = Regex.Replace(e.Node.Name, @"Box", "", RegexOptions.IgnoreCase);

                        if (boxID == "1")
                        {
                            intDisplayLocationLow = 0;
                            intDisplayLocationHigh = 0;
                        }
                        else if (boxID == "2")
                        {
                            intDisplayLocationLow = 1;
                            intDisplayLocationHigh = 2;
                        }
                        else if (boxID == "3")
                        {
                            intDisplayLocationLow = 3;
                            intDisplayLocationHigh = 7;
                        }
                        else if (boxID == "4")
                        {
                            intDisplayLocationLow = 8;
                            intDisplayLocationHigh = 15;
                        }
                        else if (boxID == "5")
                        {
                            intDisplayLocationLow = 16;
                            intDisplayLocationHigh = 29;
                        }
                        else
                        {
                            intDisplayLocationLow = 30;
                            intDisplayLocationHigh = 30;
                        }

                        var questions = from q in Variables.Leitner.ToLearn
                                        orderby q.Id
                                        select q;

                        foreach (var question in questions)
                        {
                            if ((GetDisplayLocation(question) >= intDisplayLocationLow) && (GetDisplayLocation(question) <= intDisplayLocationHigh))
                            {
                                intNodeTotal++;
                            }
                        }

                        label2WordsCount.Text = intNodeTotal.ToString();
                    }
                    catch
                    {
                        label2WordsCount.Text = "";
                    }
                }
                else if (e.Node.Name.Contains("DataBase"))
                {
                    try
                    {
                        intDisplayLocationLow = 30;
                        intDisplayLocationHigh = 30;
                        intNodeTotal = 0;

                        boxID = e.Node.Name;
                        partID = "";

                        var questions = from q in Variables.Leitner.ToLearn
                                        orderby q.Id
                                        select q;

                        foreach (var question in questions)
                        {
                            if ((GetDisplayLocation(question) >= intDisplayLocationLow) && (GetDisplayLocation(question) <= intDisplayLocationHigh))
                            {
                                intNodeTotal++;
                            }
                        }

                        label2WordsCount.Text = intNodeTotal.ToString();
                    }
                    catch
                    {
                        label2WordsCount.Text = "";
                    }
                }

                labelBoxID.Text = boxID;
                if (labelBoxID.Text == "DataBase") labelBoxID.Text = "Archive";
                labelPartID.Text = partID;

                //#endregion Obtaining the numbers of selected node's descendants

                //#region Getting the question and answer of selected node and showing it in the textboxes

                labelRegDate.Text = "00 / 00 / 00";
                textBoxQuestion.Enabled = false;
                textBoxHint.Text = "";
                textBoxHint.Enabled = false;
                buttonSave1.Enabled = false;
                buttonShowAnswer.Enabled = false;
                buttonShowHint.Enabled = false;
                buttonDelete1.Enabled = false;
                textBoxNewQuestion.Text = "";
                textCh.Text = "";
                textCangjie.Text = "";
                textFarEast.Text = "";
                newtyping.Text = "";
                typing.Text = "";
                textBoxNewAnswer.Text = "";
                textBoxNewHint.Text = "";

                try
                {
                    if (e.Node.Name.Contains("Word"))
                    {
                        string id = e.Node.Name.Replace("Word", "");

                        var questions = from q in Variables.Leitner.ToLearn
                                        orderby q.Id
                                        where q.Id == GetValidInt(id)
                                        select q;

                        foreach (var question in questions)
                        {
                            WorkingSelectedItem = question;
                            textBoxQuestion.Text = GetQuestion(question);
                            typing.Text = "";
                            textBoxNewQuestion.Text = textBoxQuestion.Text;
                            textCh.Text = "";
                            textCangjie.Text = "";
                            textFarEast.Text = "";
                            newtyping.Text = "";

                            textBoxNewAnswer.Text = GetAnswer(question);
                            string good = GetGoodCount(question).ToString();
                            string bad = GetBadCount(question).ToString();
                            string hint = GetHint(question);
                            textBoxNewHint.Text = hint;
                            newtyping.Text = ExpandCangJei(question.Side2);
                            //Register date
                            if (ToolStripMenuItemPersianDate.Checked)
                                labelRegDate.Text = ComputePersianDate(DateTime.Parse(GetTestDate(question)));
                            else
                                labelRegDate.Text = GetTestDate(question);
                            break;
                        }

                        textBoxQuestion.Enabled = true;
                        textBoxAnswer.Enabled = false;
                        textBoxAnswer.Text = "";
                        textIsThisRight.Text = "";
                        textBoxHint.Enabled = false;
                        textBoxHint.Text = "";
                        buttonSave1.Enabled = false;
                        buttonShowAnswer.Enabled = true;
                        buttonShowHint.Enabled = true;
                        buttonDelete1.Enabled = false;
                        //\\
                    }
                }
                catch (Exception ex)
                {
                    StackFrame file_info = new StackFrame(true);
                    Error(ref file_info, ex.Message);
                    textBoxQuestion.Text = "";
                    typing.Text = "";
                }

                //#endregion Getting the question and answer of selected node and showing it in the textboxes
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
            this._selectedItem = WorkingSelectedItem;
        }

        private static int CalcDisplayLocation(string boxID, string partID)
        {
            if (!Int32.TryParse(boxID, out var intBoxNumber))
            {
                intBoxNumber = 6;
            }

            if (!Int32.TryParse(partID, out var intPartNumber))
            {
                intPartNumber = 0;
            }

            if (intBoxNumber == 1)
            {
                return 0;
            }

            if (intBoxNumber == 2)
            {
                return intPartNumber;
            }

            if (intBoxNumber == 3)
            {
                return intPartNumber + 2;
            }

            if (intBoxNumber == 4)
            {
                return intPartNumber + 7;
            }

            if (intBoxNumber == 5)
            {
                return intPartNumber + 15;
            }

            if (intBoxNumber == 6)
            {
                return 30;
            }
            return 30;
        }

        private void TreeView1_MouseClick(object sender, MouseEventArgs e)
        {
            // This event is useless at startup; there is no XML file loaded
            if (Variables.LessonTableName == "") return;
            try
            {
                Variables.EnableAutoComplete = false;
                listBoxAutoComplete.Visible = false;

                treeView1.SelectedNode = treeView1.GetNodeAt(new Point(e.X, e.Y));

                ToolStripMenuItemShiftLeft.Enabled = true;
                ToolStripMenuItemExport.Enabled = false;
                ToolStripMenuItemDelete.Enabled = false;
                ToolStripMenuItemMoveTo.Enabled = false;

                if (treeView1.SelectedNode.Name.Contains(@"Word"))
                {
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemExport.Enabled = true;
                    ToolStripMenuItemShiftLeft.Enabled = false;
                    ToolStripMenuItemMoveTo.Enabled = true;
                }
                else if (treeView1.SelectedNode.Name.Contains(@"Part"))
                {
                    ToolStripMenuItemDelete.Enabled = false;
                    ToolStripMenuItemExport.Enabled = false;
                    ToolStripMenuItemShiftLeft.Enabled = false;
                }

                if (treeView1.SelectedNode.Name.Contains(@"Box1") || treeView1.SelectedNode.Name.Contains(@"DataBase"))
                {
                    ToolStripMenuItemShiftLeft.Enabled = false;
                }

                if (e.Button == MouseButtons.Right)
                        contextMenuStrip1.Show(this.treeView1, new Point(e.X, e.Y));   
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        //#endregion TreeView

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region MenuStrip

        private void RenumberXmlFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var elements = from q in Variables.Leitner.ToLearn
                               orderby q.Id
                               select q;
                int counter = 0;
                foreach (var element in elements)
                {
                    counter++;

                    if (element.Id != counter)
                    {
                        RecordIdChanged(element, counter);
                        element.Id = counter;
                    }

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            if (element.A_B_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                        case 1:
                            if (element.A_C_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                        case 2:
                            if (element.B_A_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                        case 3:
                            if (element.B_C_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                        case 4:
                            if (element.C_A_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                        case 5:
                            if (element.C_B_GoodCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                GoodCountChanged(element);
                            }
                            break;
                    }

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            if (element.A_B_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                        case 1:
                            if (element.A_C_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                        case 2:
                            if (element.B_A_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                        case 3:
                            if (element.B_C_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                        case 4:
                            if (element.C_A_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                        case 5:
                            if (element.C_B_BadCount != 0 && Properties.Settings.Default.Reset_GB_Counts)
                            {
                                BadCountChanged(element);
                            }
                            break;
                    }
                }

                //MemToSQL();

                //addNodesToTreeView();
                LoadXML();

                Successful("Renumbering", "The elements have been renumbered successfully");
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.Reading != null)
            {
                //SaveXml();
                Variables.Reading.Close();
            }
            this.Close();
        }

        private void ToolStripMenuItemRightToLeftQuestion_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                RightToLeftQuestion();
                Variables.Leitner.Setting[0].QuestionTextBox = "RightToLeft";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.QuestionTextBox = "RightToLeft";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXml();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemLeftToRightQuestion_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                LeftToRightQuestion();

                string QuestionTextBoxes = Variables.Leitner.Setting[0].QuestionTextBox.ToString();

                QuestionTextBoxes = "";
                QuestionTextBoxes = "LeftToRight";
                Variables.Leitner.Setting[0].QuestionTextBox = "LeftToRight";
                //SaveXml();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemRightToLeftAnswer_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                RightToLeftAnswer();

                string AnswerTextBoxes = Variables.Leitner.Setting[0].AnswerTextBox.ToString();
                AnswerTextBoxes = "RightToLeft";
                Variables.Leitner.Setting[0].AnswerTextBox = "RightToLeft";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.AnswerTextBox = "RightToLeft";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXml();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemLeftToRightAnswer_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                LeftToRightAnswer();

                string AnswerTextBoxes = Variables.Leitner.Setting[0].AnswerTextBox.ToString();
                AnswerTextBoxes = "LeftToRight";
                Variables.Leitner.Setting[0].AnswerTextBox = "LeftToRight";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.AnswerTextBox = "LeftToRight";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXml();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemRightToLeftHint_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                RightToLeftHint();
                Variables.Leitner.Setting[0].HintTextBox = "RightToLeft";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.HintTextBox = "RightToLeft";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXML();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemLeftToRightHint_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                LeftToRightHint();

                string HintTextBoxes = Variables.Leitner.Setting[0].HintTextBox.ToString();

                HintTextBoxes = "";
                HintTextBoxes = "LeftToRight";
                Variables.Leitner.Setting[0].HintTextBox = "LeftToRight";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.HintTextBox = "LeftToRight";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXML();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemChristianDate_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                ToolStripMenuItemChristianDate.Checked = true;
                ToolStripMenuItemPersianDate.Checked = false;

                string Date = Variables.Leitner.Setting[0].Date.ToString();

                if (Date.Length == 0)
                    Date = "Christian";

                //SaveXml();

                // Save the default for the next run
                Properties.Settings.Default.user_date = Date;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemPersianDate_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                ToolStripMenuItemChristianDate.Checked = false;
                ToolStripMenuItemPersianDate.Checked = true;

                Variables.Leitner.Setting[0].Date = "Persian";

                using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {

                    var settings = from s in Variables.SettingDataContext.Setting01s
                                   select s;

                    foreach (var n01set in settings)
                    {
                        n01set.Date = "Persian";
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }
                //SaveXml();

                // Save the default for the next run
                Properties.Settings.Default.user_date = "Persian";
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ExportAllWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                this.Cursor = Cursors.WaitCursor;

                ExportWords(saveFileDialog1.FileName, "Replace");

                this.Cursor = Cursors.Default;
                Successful("Item Saving", "The Items have been saved successfully");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemAboutMe_Click(object sender, EventArgs e)
        {
            AboutMe aboutMeForm = new AboutMe();
            aboutMeForm.ShowDialog();
        }

        private void ToolStripMenuItemCreateNewLesson_Click(object sender, EventArgs e)
        {
            //SaveXml();
            NewLesson newLesson = new NewLesson();
            newLesson.ShowDialog();
            LoadXML();
            TabPageStatistics_Enter(sender, e);
        }

        private void ToolStripMenuItemSelectLesson_Click(object sender, EventArgs e)
        {
            //SaveXml();
            Variables.LastStartTime = null;
            try
            {
                SelectLesson selectLessonForm = new SelectLesson();
                selectLessonForm.ShowDialog();
                if (Variables.LessonTableName == @"")
                {
                    return;
                }
                labelBoxID.Text = null;
                labelPartID.Text = null;
                LoadXML();
                TabPageStatistics_Enter(sender, e);
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemMoveTo_Click(object sender, EventArgs e)
        {
            var workingSelectedItem = _selectedItem;
            var toolStripMenuItem = sender as ToolStripMenuItem;

            var question = GetQuestion(workingSelectedItem);
            var answer = GetAnswer(workingSelectedItem);
            var date = DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM");
            var id = workingSelectedItem.Id.ToString();

            var currId = GetValidInt(id);

            var good = GetGoodCount(workingSelectedItem).ToString();
            var bad = GetBadCount(workingSelectedItem).ToString();
            var hint = GetHint(workingSelectedItem);

            //ToolStripMenuItemBox2Part1
            var boxId = Regex.Replace(toolStripMenuItem.Name, @"(ToolStripMenuItem)|(Box)|(Part..*)", "", RegexOptions.IgnoreCase);
            var partId = Regex.Replace(toolStripMenuItem.Name, @"(ToolStripMenuItem)|(Box.)|(Part)", "", RegexOptions.IgnoreCase);

            treeView1.Nodes.Find("Word" + id, true).First().Remove();
            var intGood = GetValidInt(good);
            var intBad = GetValidInt(bad);
            
            switch (boxId)
            {
                case "1":
                    labelAddQuestionMessage.Text = @"The Item was moved to Box 1";
                    labelAnswerToQuestionMessage.Text = @"The Item was moved to Box 1";
                    AddToTreeView(boxId, partId, currId, intGood, intBad);
                    break;
                case "DataBase":
                    labelAddQuestionMessage.Text = @"The Item was moved to Archive";
                    labelAnswerToQuestionMessage.Text = @"The Item was moved to Archive";
                    AddToTreeView(boxId, partId, currId, 0, 0);
                    break;
                default:
                    labelAddQuestionMessage.Text = @"The Item was moved to Box " + boxId + @" Part " + partId;
                    labelAnswerToQuestionMessage.Text = @"The Item was moved to Box " + boxId + @" Part " + partId;
                    AddToTreeView(boxId, partId, currId, intGood, intBad);
                    break;
            }

            SetDisplayLocation(CalcDisplayLocation(boxId, partId), workingSelectedItem);

            var n01Sets = from s01 in Variables.Leitner.ToLearn
                          orderby s01.Id
                          where s01.Id == currId
                          select s01;

            foreach (var n01Set in n01Sets)
            {
                if (boxId == "DataBase")
                {
					SetTestDate(date, n01Set);
                    SetGoodCount(0, n01Set);
                    SetBadCount(0, n01Set);
                }
                OneRecordChanged(n01Set);
                break;
            }
            //SaveXml();
            try
            {
                TabPageStatistics_Enter(sender, e);
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        //#endregion MenuStrip

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region ContextMenuStrip

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            try
            {
                if (MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                OnestudyItemDeleted(WorkingSelectedItem.Id);
                WorkingSelectedItem.Delete();
                treeView1.SelectedNode.Remove();

                //SaveXml();
                TabPageStatistics_Enter(sender, e);
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ToolStripMenuItemShiftLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(@"Are you sure ?", @"Shift left", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                switch (labelBoxID.Text)
                {
                    case @"2":
                        AutoPush(@"2");
                        break;
                    case @"3":
                        AutoPush(@"3");
                        break;
                    case @"4":
                        AutoPush(@"4");
                        break;
                    default:
                        AutoPush(@"5");
                        break;
                }

                TabPageStatistics_Enter(sender, e);

                LoadXML();
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        private void ToolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
            {
                return;
            }
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

                ExportWords(saveFileDialog1.FileName, "Append");

                Successful("File Saving", "The file has been saved successfully");
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void ExportWords(string filename, string method)
        {
            FileStream Writing;
            if (method == "Append")
            {
                Writing = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.None);
            }
            else
            {
                Writing = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            }

            using (StreamWriter sw = new StreamWriter(Writing, UTF8Encoding.UTF8))
            {
                FileInfo fileInfo = new FileInfo(Variables.LessonTableName);

                EnumerableRowCollection<LeitnerBox.ToLearnRow> tolearnrow;
                if (method == "Append")
                {
                    tolearnrow = from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 where q.Id == this._selectedItem.Id
                                 select q;
                }
                else
                {
                    tolearnrow = from q in Variables.Leitner.ToLearn
                                 orderby q.Id
                                 select q;
                }

                foreach (var word in tolearnrow)
                {
                    sw.WriteLine(
                        word.Id.ToString() + "\t" +
                            word.Side1.ToString() + "\t" +
                            word.Side2.ToString() + "\t" +
                            word.Side3.ToString() + "\t" +
                            word.A_B_GoodCount.ToString() + "\t" +
                            word.A_C_GoodCount.ToString() + "\t" +
                            word.B_A_GoodCount.ToString() + "\t" +
                            word.B_C_GoodCount.ToString() + "\t" +
                            word.C_A_GoodCount.ToString() + "\t" +
                            word.C_B_GoodCount.ToString() + "\t" +
                            word.A_B_BadCount.ToString() + "\t" +
                            word.A_C_BadCount.ToString() + "\t" +
                            word.B_A_BadCount.ToString() + "\t" +
                            word.B_C_BadCount.ToString() + "\t" +
                            word.C_A_BadCount.ToString() + "\t" +
                            word.C_B_BadCount.ToString() + "\t" +
                            word.A_B_DisplayLocation.ToString() + "\t" +
                            word.A_C_DisplayLocation.ToString() + "\t" +
                            word.B_A_DisplayLocation.ToString() + "\t" +
                            word.B_C_DisplayLocation.ToString() + "\t" +
                            word.C_A_DisplayLocation.ToString() + "\t" +
                            word.C_B_DisplayLocation.ToString() + "\t" +
                            word.A_B_TestDate.ToString() + "\t" +
                            word.A_C_TestDate.ToString() + "\t" +
                            word.B_A_TestDate.ToString() + "\t" +
                            word.B_C_TestDate.ToString() + "\t" +
                            word.C_A_TestDate.ToString() + "\t" +
                            word.C_B_TestDate.ToString()

                        );
                }

                sw.Close();
            }
        }

        //#endregion ContextMenuStrip

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //#region Timer

        private void Timer1_Tick(object sender, EventArgs e)
        {
            labelNewWordDate.Text = ToolStripMenuItemPersianDate.Checked ? ComputePersianDate(DateTime.Now) : DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        //#endregion Timer

        private void SectionToolStripMenuItemSection_Click(object sender, EventArgs e)
        {
            try
            {
                var setLessionSectionForm = new SetLessonSection();
                setLessionSectionForm.ShowDialog();
            }
            catch (Exception ex)
            {
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
        }

        private void AutoPush(string whichBox)
        {
            if (Variables.LessonTableName == "") return;

            var sortable = new string[Variables.Maxwords, 2];
            var firstBoxLocation = 30;
            var lastBoxLocation = 30;

            switch (whichBox)
            {
                case "2":
                    firstBoxLocation = 1;
                    lastBoxLocation = 2;
                    break;
                case "3":
                    firstBoxLocation = 3;
                    lastBoxLocation = 7;
                    break;
                case "4":
                    firstBoxLocation = 8;
                    lastBoxLocation = 15;
                    break;
                case "5":
                    firstBoxLocation = 16;
                    lastBoxLocation = 29;
                    break;
                default:
                    return;
            }

            var firstBoxContains = CountBoxHoldings(firstBoxLocation);
            var secondBoxContains = CountBoxHoldings(firstBoxLocation + 1);

            if (secondBoxContains > 0)
            {
                if (firstBoxContains < Variables.MaxStudy)
                {
                    var firstBoxFreeCapacity = Variables.MaxStudy - firstBoxContains;
                    var toMove = firstBoxFreeCapacity;
                    if (firstBoxFreeCapacity > secondBoxContains)
                    {
                        toMove = secondBoxContains;
                    }

                    // Here we need a list of the items in the Source Box
                    var questions = ListItemsInDisplayLocation(firstBoxLocation + 1);

                    var sortedIndex = 0;
                    var randy = new Random();

                    foreach (var question in questions)
                    {
                        sortable[sortedIndex, 0] = question.Id.ToString();
                        sortable[sortedIndex, 1] = randy.Next().ToString(CultureInfo.InvariantCulture);

                        sortedIndex++;
                        if (sortedIndex > Variables.Maxwords - 1) { throw new IndexOutOfRangeException(); }
                    }

                    QuickSort(ref sortable, 0, sortedIndex - 1);

                    var indexOldest = 0;

                    for (var moveThem = toMove; moveThem >= 1; moveThem--)
                    {
                        var items = (from q in Variables.Leitner.ToLearn
                                     orderby q.Id
                                     select q);

                        foreach (var item in Enumerable.Where(items, item => sortable[indexOldest, 0] == item.Id.ToString(CultureInfo.InvariantCulture)))
                        {
                            SetDisplayLocation(GetDisplayLocation(item) - 1, item);

                            DisplayLocationChanged(item);
                            TabPageStatistics_Enter();
                            break;
                        }
                        indexOldest++;
                    }

                }
            }

            switch (whichBox)
            {
                case "3":
                    firstBoxLocation = 4;
                    lastBoxLocation = 7;
                    PushThirdPartPlusRestOfParts(firstBoxLocation, lastBoxLocation);
                    break;
                case "4":
                    firstBoxLocation = 9;
                    lastBoxLocation = 15;
                    PushThirdPartPlusRestOfParts(firstBoxLocation, lastBoxLocation);
                    break;
                case "5":
                    firstBoxLocation = 17;
                    lastBoxLocation = 29;
                    PushThirdPartPlusRestOfParts(firstBoxLocation, lastBoxLocation);
                    break;
            }
        }

        private static void CalculateBoxPartFromDisplayLocation(ref string boxName, ref string partName, int locationId)
        {
            if (partName == null) throw new ArgumentNullException("partName");
            switch (locationId)
            {
                case 0:
                    boxName = "Box 1";
                    partName = "";
                    break;
                case 1:
                    boxName = "Box 2";
                    partName = "Part 1";
                    break;
                case 2:
                    boxName = "Box 2";
                    partName = "Part 2";
                    break;
                case 3:
                    boxName = "Box 3";
                    partName = "Part 1";
                    break;
                case 4:
                    boxName = "Box 3";
                    partName = "Part 2";
                    break;
                case 5:
                    boxName = "Box 3";
                    partName = "Part 3";
                    break;
                case 6:
                    boxName = "Box 3";
                    partName = "Part 4";
                    break;
                case 7:
                    boxName = "Box 3";
                    partName = "Part 5";
                    break;
                case 8:
                    boxName = "Box 4";
                    partName = "Part 1";
                    break;
                case 9:
                    boxName = "Box 4";
                    partName = "Part 2";
                    break;
                case 10:
                    boxName = "Box 4";
                    partName = "Part 3";
                    break;
                case 11:
                    boxName = "Box 4";
                    partName = "Part 4";
                    break;
                case 12:
                    boxName = "Box 4";
                    partName = "Part 5";
                    break;
                case 13:
                    boxName = "Box 4";
                    partName = "Part 6";
                    break;
                case 14:
                    boxName = "Box 4";
                    partName = "Part 7";
                    break;
                case 15:
                    boxName = "Box 4";
                    partName = "Part 8";
                    break;
                case 16:
                    boxName = "Box 5";
                    partName = "Part 1";
                    break;
                case 17:
                    boxName = "Box 5";
                    partName = "Part 2";
                    break;
                case 18:
                    boxName = "Box 5";
                    partName = "Part 3";
                    break;
                case 19:
                    boxName = "Box 5";
                    partName = "Part 4";
                    break;
                case 20:
                    boxName = "Box 5";
                    partName = "Part 5";
                    break;
                case 21:
                    boxName = "Box 5";
                    partName = "Part 6";
                    break;
                case 22:
                    boxName = "Box 5";
                    partName = "Part 7";
                    break;
                case 23:
                    boxName = "Box 5";
                    partName = "Part 8";
                    break;
                case 24:
                    boxName = "Box 5";
                    partName = "Part 9";
                    break;
                case 25:
                    boxName = "Box 5";
                    partName = "Part 10";
                    break;
                case 26:
                    boxName = "Box 5";
                    partName = "Part 11";
                    break;
                case 27:
                    boxName = "Box 5";
                    partName = "Part 12";
                    break;
                case 28:
                    boxName = "Box 5";
                    partName = "Part 13";
                    break;
                case 29:
                    boxName = "Box 5";
                    partName = "Part 14";
                    break;
                default:
                    boxName = "DataBase";
                    partName = "";
                    break;
            }
        }

        private static void CalculateBoxPartFromDisplayLocation(ref int boxNumber, ref int partNumber, int locationId)
        {
            switch (locationId)
            {
                case 0:
                    boxNumber = 1;
                    partNumber = 0;
                    break;
                case 1:
                    boxNumber = 2;
                    partNumber = 1;
                    break;
                case 2:
                    boxNumber = 2;
                    partNumber = 2;
                    break;
                case 3:
                    boxNumber = 3;
                    partNumber = 1;
                    break;
                case 4:
                    boxNumber = 3;
                    partNumber = 2;
                    break;
                case 5:
                    boxNumber = 3;
                    partNumber = 3;
                    break;
                case 6:
                    boxNumber = 3;
                    partNumber = 4;
                    break;
                case 7:
                    boxNumber = 3;
                    partNumber = 5;
                    break;
                case 8:
                    boxNumber = 4;
                    partNumber = 1;
                    break;
                case 9:
                    boxNumber = 4;
                    partNumber = 2;
                    break;
                case 10:
                    boxNumber = 4;
                    partNumber = 3;
                    break;
                case 11:
                    boxNumber = 4;
                    partNumber = 4;
                    break;
                case 12:
                    boxNumber = 4;
                    partNumber = 5;
                    break;
                case 13:
                    boxNumber = 4;
                    partNumber = 6;
                    break;
                case 14:
                    boxNumber = 4;
                    partNumber = 7;
                    break;
                case 15:
                    boxNumber = 4;
                    partNumber = 8;
                    break;
                case 16:
                    boxNumber = 5;
                    partNumber = 1;
                    break;
                case 17:
                    boxNumber = 5;
                    partNumber = 2;
                    break;
                case 18:
                    boxNumber = 5;
                    partNumber = 3;
                    break;
                case 19:
                    boxNumber = 5;
                    partNumber = 4;
                    break;
                case 20:
                    boxNumber = 5;
                    partNumber = 5;
                    break;
                case 21:
                    boxNumber = 5;
                    partNumber = 6;
                    break;
                case 22:
                    boxNumber = 5;
                    partNumber = 7;
                    break;
                case 23:
                    boxNumber = 5;
                    partNumber = 8;
                    break;
                case 24:
                    boxNumber = 5;
                    partNumber = 9;
                    break;
                case 25:
                    boxNumber = 5;
                    partNumber = 10;
                    break;
                case 26:
                    boxNumber = 5;
                    partNumber = 11;
                    break;
                case 27:
                    boxNumber = 5;
                    partNumber = 12;
                    break;
                case 28:
                    boxNumber = 5;
                    partNumber = 13;
                    break;
                default:
                    boxNumber = 5;
                    partNumber = 14;
                    break;
            }
        }

        private static void PushThirdPartPlusRestOfParts(int firstBoxLocation, int lastBoxLocation)
        {
            for (var dest = firstBoxLocation; dest < lastBoxLocation; dest++)
            {
                var moveItems = from q in Variables.Leitner.ToLearn
                                orderby q.Id
                                select q;

                foreach (var moveItem in moveItems)
                {
                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            if (moveItem.A_B_DisplayLocation == dest + 1)
                            {
                                moveItem.A_B_DisplayLocation = dest;
                            }
                            break;
                        case 1:
                            if (moveItem.A_C_DisplayLocation == dest + 1)
                            {
                                moveItem.A_C_DisplayLocation = dest;
                            }
                            break;
                        case 2:
                            if (moveItem.B_A_DisplayLocation == dest + 1)
                            {
                                moveItem.B_A_DisplayLocation = dest;
                            }
                            break;
                        case 3:
                            if (moveItem.B_C_DisplayLocation == dest + 1)
                            {
                                moveItem.B_C_DisplayLocation = dest;
                            }
                            break;
                        case 4:
                            if (moveItem.C_A_DisplayLocation == dest + 1)
                            {
                                moveItem.C_A_DisplayLocation = dest;
                            }
                            break;
                        default:
                            if (moveItem.C_B_DisplayLocation == dest + 1)
                            {
                                moveItem.C_B_DisplayLocation = dest;
                            }
                            break;
                    }
                }
                DisplayLocationChanged(dest + 1, dest);
            }
        }

        private void PrimeBox1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;

            int intDisplayLocation = 0;
            var sortable = new string[Variables.Maxwords, 2];

            intDisplayLocation = 30;
            var dataBasecontains = CountBoxHoldings(intDisplayLocation);

            if (dataBasecontains <= 0) return;
            intDisplayLocation = 0;

            var box1Contains = CountBoxHoldings(intDisplayLocation);

            if (box1Contains >= Variables.MaxStudy) return;
            var box1FreeCapacity = Variables.MaxStudy - box1Contains;
            if (box1FreeCapacity < 1) return;
            try
            {
                var toMove = box1FreeCapacity;
                if (box1FreeCapacity > dataBasecontains)
                {
                    toMove = dataBasecontains;
                }

                // Here we need a list of the items in the Archive
                intDisplayLocation = 30;
                var questions = ListItemsInDisplayLocation(intDisplayLocation);

                var sortedIndex = 0;
                var randy = new Random();

                foreach (var question in questions)
                {
                    sortable[sortedIndex, 0] = question.Id.ToString(CultureInfo.InvariantCulture);
                    sortable[sortedIndex, 1] = sortable[sortedIndex, 1] = randy.Next().ToString(CultureInfo.InvariantCulture);

                    sortedIndex++;
                    if (sortedIndex > Variables.Maxwords - 1) { throw new IndexOutOfRangeException(); }
                }

                QuickSort(ref sortable, 0, sortedIndex - 1);

                var indexOldest = 0;
                for (var moveThem = toMove; moveThem >= 1; moveThem--)
                {
                    var sortedMoveQuestions = from q in Variables.Leitner.ToLearn
                                              orderby q.Id
                                              where q.Id == GetValidInt(sortable[indexOldest, 0])
                                              select q;
                    var wordId = "";
                    foreach (var sortedMoveQuestion in sortedMoveQuestions)
                    {
                        wordId = sortedMoveQuestion.Id.ToString(CultureInfo.InvariantCulture);
                    }

                    indexOldest++;

                    const string boxId = "1";
                    const string partId = "";

                    treeView1.Nodes.Find("Word" + wordId, true).First().Remove();

                    var currId = GetValidInt(wordId);
                    AddToTreeView(boxId, partId, currId);
                    // Update DisplayLocation in XML
                    var newDisplays = from q in Variables.Leitner.ToLearn
                                      orderby q.Id
                                      where q.Id == currId
                                      select q;

                    foreach (var newDisplay in newDisplays)
                    {
                        SetDisplayLocation(0, newDisplay);

                        DisplayLocationChanged(newDisplay);
                        TabPageStatistics_Enter();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                var fileInfo = new StackFrame(true);
                Error(ref fileInfo, ex.Message);
            }
            TabPageStatistics_Enter(sender, e);
        }

        private static void RecordIdChanged(LeitnerBox.ToLearnRow changedRecord, int newId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET Id = " + newId
                         + " WHERE Id = " + changedRecord.Id;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void DisplayLocationChanged(LeitnerBox.ToLearnRow changedRecord)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_B_DisplayLocation = " + changedRecord.A_B_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 1:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_C_DisplayLocation = " + changedRecord.A_C_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 2:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_A_DisplayLocation = " + changedRecord.B_A_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 3:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_C_DisplayLocation = " + changedRecord.B_C_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 4:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_A_DisplayLocation = " + changedRecord.C_A_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 5:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_B_DisplayLocation = " + changedRecord.C_B_DisplayLocation
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void DisplayLocationChanged(int OldLoc, int NewLoc)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_B_DisplayLocation = " + NewLoc
                                 + " WHERE A_B_DisplayLocation = " + OldLoc;
                            break;
                        case 1:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_C_DisplayLocation = " + NewLoc
                                 + " WHERE A_C_DisplayLocation = " + OldLoc;
                            break;
                        case 2:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_A_DisplayLocation = " + NewLoc
                                 + " WHERE B_A_DisplayLocation = " + OldLoc;
                            break;
                        case 3:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_C_DisplayLocation = " + NewLoc
                                 + " WHERE B_C_DisplayLocation = " + OldLoc;
                            break;
                        case 4:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_A_DisplayLocation = " + NewLoc
                                 + " WHERE C_A_DisplayLocation = " + OldLoc;
                            break;
                        case 5:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_B_DisplayLocation = " + NewLoc
                                 + " WHERE C_B_DisplayLocation = " + OldLoc;
                            break;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void DisplayLocationChanged()
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_B_DisplayLocation = " + 30;
                            break;
                        case 1:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_C_DisplayLocation = " + 30;
                            break;
                        case 2:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_A_DisplayLocation = " + 30;
                            break;
                        case 3:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_C_DisplayLocation = " + 30;
                            break;
                        case 4:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_A_DisplayLocation = " + 30;
                            break;
                        case 5:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_B_DisplayLocation = " + 30;
                            break;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void OneRecordChanged(LeitnerBox.ToLearnRow SomethingChanged)
        {
            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                var n01sets = from S01 in Variables.LessonDataContext.Lesson01s
                              where S01.Id == SomethingChanged.Id
                              select S01;

                foreach (var n01set in n01sets)
                {
                    n01set.Id = SomethingChanged.Id;
                    n01set.Side1 = SomethingChanged.Side1;
                    n01set.Side2 = SomethingChanged.Side2;
                    n01set.Side3 = SomethingChanged.Side3;
                    n01set.A_B_GoodCount = SomethingChanged.A_B_GoodCount;
                    n01set.A_C_GoodCount = SomethingChanged.A_C_GoodCount;
                    n01set.B_A_GoodCount = SomethingChanged.B_A_GoodCount;
                    n01set.B_C_GoodCount = SomethingChanged.B_C_GoodCount;
                    n01set.C_A_GoodCount = SomethingChanged.C_A_GoodCount;
                    n01set.C_B_GoodCount = SomethingChanged.C_B_GoodCount;
                    n01set.A_B_BadCount = SomethingChanged.A_B_BadCount;
                    n01set.A_C_BadCount = SomethingChanged.A_C_BadCount;
                    n01set.B_A_BadCount = SomethingChanged.B_A_BadCount;
                    n01set.B_C_BadCount = SomethingChanged.B_C_BadCount;
                    n01set.C_A_BadCount = SomethingChanged.C_A_BadCount;
                    n01set.C_B_BadCount = SomethingChanged.C_B_BadCount;
                    n01set.A_B_DisplayLocation = SomethingChanged.A_B_DisplayLocation;
                    n01set.A_C_DisplayLocation = SomethingChanged.A_C_DisplayLocation;
                    n01set.B_A_DisplayLocation = SomethingChanged.B_A_DisplayLocation;
                    n01set.B_C_DisplayLocation = SomethingChanged.B_C_DisplayLocation;
                    n01set.C_A_DisplayLocation = SomethingChanged.C_A_DisplayLocation;
                    n01set.C_B_DisplayLocation = SomethingChanged.C_B_DisplayLocation;
                    n01set.A_B_TestDate = SomethingChanged.A_B_TestDate;
                    n01set.A_C_TestDate = SomethingChanged.A_C_TestDate;
                    n01set.B_A_TestDate = SomethingChanged.B_A_TestDate;
                    n01set.B_C_TestDate = SomethingChanged.B_C_TestDate;
                    n01set.C_A_TestDate = SomethingChanged.C_A_TestDate;
                    n01set.C_B_TestDate = SomethingChanged.C_B_TestDate;
                }
                Variables.LessonDataContext.SubmitChanges();
            }
        }

        private static void OneRecordAdded(LeitnerBox.ToLearnRow SomethingChanged)
        {
            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                Lesson01 n01set = new Lesson01
                {
                    Id = SomethingChanged.Id,
                    Side1 = SomethingChanged.Side1,
                    Side2 = SomethingChanged.Side2,
                    Side3 = SomethingChanged.Side3,
                    A_B_GoodCount = SomethingChanged.A_B_GoodCount,
                    A_C_GoodCount = SomethingChanged.A_C_GoodCount,
                    B_A_GoodCount = SomethingChanged.B_A_GoodCount,
                    B_C_GoodCount = SomethingChanged.B_C_GoodCount,
                    C_A_GoodCount = SomethingChanged.C_A_GoodCount,
                    C_B_GoodCount = SomethingChanged.C_B_GoodCount,
                    A_B_BadCount = SomethingChanged.A_B_BadCount,
                    A_C_BadCount = SomethingChanged.A_C_BadCount,
                    B_A_BadCount = SomethingChanged.B_A_BadCount,
                    B_C_BadCount = SomethingChanged.B_C_BadCount,
                    C_A_BadCount = SomethingChanged.C_A_BadCount,
                    C_B_BadCount = SomethingChanged.C_B_BadCount,
                    A_B_DisplayLocation = SomethingChanged.A_B_DisplayLocation,
                    A_C_DisplayLocation = SomethingChanged.A_C_DisplayLocation,
                    B_A_DisplayLocation = SomethingChanged.B_A_DisplayLocation,
                    B_C_DisplayLocation = SomethingChanged.B_C_DisplayLocation,
                    C_A_DisplayLocation = SomethingChanged.C_A_DisplayLocation,
                    C_B_DisplayLocation = SomethingChanged.C_B_DisplayLocation,
                    A_B_TestDate = SomethingChanged.A_B_TestDate,
                    A_C_TestDate = SomethingChanged.A_C_TestDate,
                    B_A_TestDate = SomethingChanged.B_A_TestDate,
                    B_C_TestDate = SomethingChanged.B_C_TestDate,
                    C_A_TestDate = SomethingChanged.C_A_TestDate,
                    C_B_TestDate = SomethingChanged.C_B_TestDate
                };

                Variables.LessonDataContext.Lesson01s.InsertOnSubmit(n01set);
                Variables.LessonDataContext.SubmitChanges();
            }
        }

        private static void OnestudyItemDeleted(int id)
        {
            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                var n01sets = from S01 in Variables.LessonDataContext.Lesson01s
                              where S01.Id == id
                              select S01;

                foreach (var n01set in n01sets)
                {
                    Variables.LessonDataContext.Lesson01s.DeleteOnSubmit(n01set);
                }

                Variables.LessonDataContext.SubmitChanges();
            }
        }

        private static IEnumerable<LeitnerBox.ToLearnRow> ListItemsInDisplayLocation(int intDisplayLocation)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.A_B_DisplayLocation == intDisplayLocation
                        select q);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.A_C_DisplayLocation == intDisplayLocation
                        select q);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.B_A_DisplayLocation == intDisplayLocation
                        select q);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.B_C_DisplayLocation == intDisplayLocation
                        select q);
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.C_A_DisplayLocation == intDisplayLocation
                        select q);
            }
            else //if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.C_B_DisplayLocation == intDisplayLocation
                        select q);
            }
        }

        private static int CountBoxHoldings(int intDisplayLocation)
        {
            if (Variables.Leitner.Setting[0].StudyMode == 0)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.A_B_DisplayLocation == intDisplayLocation
                        select q).Count();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 1)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.A_C_DisplayLocation == intDisplayLocation
                        select q).Count();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 2)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.B_A_DisplayLocation == intDisplayLocation
                        select q).Count();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 3)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.B_C_DisplayLocation == intDisplayLocation
                        select q).Count();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 4)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.C_A_DisplayLocation == intDisplayLocation
                        select q).Count();
            }
            else if (Variables.Leitner.Setting[0].StudyMode == 5)
            {
                return (from q in Variables.Leitner.ToLearn
                        orderby q.Id
                        where q.C_B_DisplayLocation == intDisplayLocation
                        select q).Count();
            }

            return 0;
        }

        private void ButtonSave2_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            LeitnerBox.ToLearnRow WorkingSelectedItem = this._selectedItem;
            try
            {
                labelAddQuestionMessage.Text = "";

                ////////////////////////////////////

                if (textBoxNewQuestion.Text.Trim() == "")
                {
                    labelAddQuestionMessage.Text = "Please fill new Question box";
                    return;
                }

                if (textBoxNewAnswer.Text == "")
                {
                    labelAddQuestionMessage.Text = "Please fill new Answer box";
                    return;
                }
                ////////////////////////////////////

                SetQuestion(textBoxNewQuestion.Text.Trim(), WorkingSelectedItem);
                SetAnswer(textBoxNewAnswer.Text.Trim(), WorkingSelectedItem);

                SetHint(textBoxNewHint.Text.Trim(), WorkingSelectedItem);

                OneRecordChanged(WorkingSelectedItem);
                //SaveXml();

                labelAddQuestionMessage.Text = "The changes saved successfully";
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void QuickSort(ref string[,] subArray, int first, int last)
        {
            if (first >= last) return;
            var q = Partition(ref subArray, first, last);
            QuickSort(ref subArray, first, q - 1);
            QuickSort(ref subArray, q + 1, last);
        }

        private int Partition(ref string[,] subArray, int start, int last)
        {
            string holdId;
            string holdRandom;

            var oFx = subArray[last, 1];

            var i = start - 1;
            for (var j = start; j <= last - 1; j++)
            {
                var oFj = subArray[j, 1];
                var doubleoFj = Convert.ToDouble(oFj);
                var doubleoFx = Convert.ToDouble(oFx);

                if (!(doubleoFj <= doubleoFx)) continue;
                i++;
                holdId = subArray[i, 0];
                holdRandom = subArray[i, 1];

                subArray[i, 0] = subArray[j, 0];
                subArray[i, 1] = subArray[j, 1];

                subArray[j, 0] = holdId;
                subArray[j, 1] = holdRandom;
            }
            holdId = subArray[i + 1, 0];
            holdRandom = subArray[i + 1, 1];

            subArray[i + 1, 0] = subArray[last, 0];
            subArray[i + 1, 1] = subArray[last, 1];

            subArray[last, 0] = holdId;
            subArray[last, 1] = holdRandom;

            return (i + 1);
        }

        private bool SortByDate(ref string[,] sortable, int size)
        {
            try
            {
                for (int top = 0; top < size; top++)
                {
                    for (int shuttle = top + 1; shuttle <= size; shuttle++)
                    {
                        // working DateTime variables

                        // Convert string date to DateTime (or crash if it cannot be convertd)
                        if (!DateTime.TryParse(sortable[top, 1], out var upper)) { throw new InvalidDataException(); }
                        if (!DateTime.TryParse(sortable[shuttle, 1], out var lower)) { throw new InvalidDataException(); }

                        if (lower < upper)
                        {
                            string holdID;
                            string holdDateTime;

                            holdID = sortable[top, 0];
                            holdDateTime = sortable[top, 1];

                            sortable[top, 0] = sortable[shuttle, 0];
                            sortable[top, 1] = sortable[shuttle, 1];

                            sortable[shuttle, 0] = holdID;
                            sortable[shuttle, 1] = holdDateTime;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Side1Side2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side1Side2AutoAgeingInterval Side1Side2AutoAgingIntervalForm = new Side1Side2AutoAgeingInterval();
                Side1Side2AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void Side1Side3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side1Side3AutoAgeingInterval Side1Side3AutoAgingIntervalForm = new Side1Side3AutoAgeingInterval();
                Side1Side3AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void Side2Side1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side2Side1AutoAgeingInterval Side2Side1AutoAgingIntervalForm = new Side2Side1AutoAgeingInterval();
                Side2Side1AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void Side2Side3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side2Side3AutoAgeingInterval Side2Side3AutoAgingIntervalForm = new Side2Side3AutoAgeingInterval();
                Side2Side3AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void Side3Side1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side3Side1AutoAgeingInterval Side3Side1AutoAgingIntervalForm = new Side3Side1AutoAgeingInterval();
                Side3Side1AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void Side3Side2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                Side3Side2AutoAgeingInterval Side3Side2AutoAgingIntervalForm = new Side3Side2AutoAgeingInterval();
                Side3Side2AutoAgingIntervalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void SelectStudyModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                int PreviousStudyMode = Variables.Leitner.Setting[0].StudyMode;

                SelectStudyMode SetLessionSectionForm = new SelectStudyMode();
                SetLessionSectionForm.ShowDialog();

                if (Variables.LessonTableName != "")
                {
                    //addNodesToTreeView();
                    TabPageStatistics_Enter();

                    int CurrentStudyMode = Variables.Leitner.Setting[0].StudyMode;
                    if (PreviousStudyMode != CurrentStudyMode)

                        UpdateSQLSettings();
                    //SaveXml();

                    LoadXML();
                }
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void TextBoxNewHint_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Variables.EnableAutoComplete)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                if (textBoxNewHint.Text.Trim() == "") // || textBoxNewHint.Text.Trim().Length <= 1)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                SetSideStrings(out string strSide1, out string strSide2, out var strSide3);

                EnumerableRowCollection<Leitner_Three.LeitnerBox.ToLearnRow> questions = null;

                questions = SearchOneSide("C", textBoxNewHint.Text.Trim().ToLower(), questions);

                if (questions.Count() == 0)
                {
                    listBoxAutoComplete.Visible = false;
                    return;
                }

                listBoxAutoComplete.Items.Clear();
                listBoxAutoComplete.Location = new Point(100, 307);//107, 294
                listBoxAutoComplete.Visible = true;
                listBoxAutoComplete.RightToLeft = textBoxNewHint.RightToLeft;

                foreach (var question in questions)
                {
                    listBoxAutoComplete.Items.Add(GetHint(question));
                }
            }
            catch { listBoxAutoComplete.Visible = false; }
        }

        private bool CreateNewItem(string newSide1, string newSide2, string newSide3, ref int newItemId)
        {
            try
            {
                LeitnerBox.ToLearnRow DestinationRow = Variables.Leitner.ToLearn.NewToLearnRow();
                InitNewRow(DestinationRow);

                try
                {
                    newItemId = 0;
                    var questions = from q in Variables.Leitner.ToLearn
                                    orderby q.Id
                                    select q;

                    foreach (var question in questions)
                    {
                        if (question.Id > newItemId)
                            newItemId = question.Id;
                    }
                }
                catch
                {
                }
                newItemId++;

                DestinationRow.Id = newItemId;
                DestinationRow.Side1 = newSide1;
                DestinationRow.Side2 = newSide2;
                DestinationRow.Side3 = newSide3;

                Variables.Leitner.ToLearn.AddToLearnRow(DestinationRow);

                OneRecordAdded(DestinationRow);
                //SaveXml();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //SaveXml();
        }

        private void SideNamesAndStudySequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variables.LessonTableName == "")
                return;
            try
            {
                SideNamesAndStudySequence SideNamesAndStudySequenceForm = new SideNamesAndStudySequence();
                SideNamesAndStudySequenceForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private String BuildStudyModeString()
        {
            String StudyString = "";
            String[] StringsStudyMode = new String[6];
            String strStudySequence = Variables.Leitner.Setting[0].StudySequence;
            for (int index = 0; index < 6; index++)
            {
                StringsStudyMode[index] = strStudySequence.Substring(index, 1);
            }
            String strStudyMode = (Variables.Leitner.Setting[0].StudyMode + 1).ToString();

            for (int index = 0; index < 6; index++)
            {
                if (StringsStudyMode[index] == strStudyMode)
                {
                    StringsStudyMode[index] = "X";
                }
                StudyString += StringsStudyMode[index];
            }
            return StudyString;
        }

        private void F2NextModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F2}");
        }

        private void F3PrimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F3}");
        }

        private void F4AutoAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void F12ArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F12}");
        }

        private void TextCh_TextChanged(object sender, EventArgs e)
        {
            if (textCh.Text.Length != 1)
            {
                textCh.Text = "";
                textCangjie.Text = "";
                textFarEast.Text = "";
                return;
            }
            string looker1 = Properties.Settings.Default.ChineseStudyConnection;
            using (FullFarEastDataContext FullFarEast = new FullFarEastDataContext(Properties.Settings.Default.ChineseStudyConnection))
            {
                var Indexes = from q in FullFarEast.FullFarEasts
                              where q.Char == textCh.Text
                              select q;
                try
                {
                    foreach (var Index in Indexes)
                    {
                        textFarEast.Text = Index.FeNumber.ToString();
                    }
                }
                catch (System.Data.StrongTypingException ex)
                {
                    textCangjie.Text = "OuterFeIndex";
                    newtyping.Text = ex.Message;
                }
            }

            using (FullFarEastDataContext UniHan = new FullFarEastDataContext(Properties.Settings.Default.ChineseStudyConnection))
            {
                char[] workChin = new char[1];
                workChin[0] = textCh.Text[0];
                string UniHex = ExpandUnihex(workChin);

                var Indexes = from q in UniHan.UniHans
                              where q.cp == UniHex
                              select q;
                try
                {
                    foreach (var Index in Indexes)
                    {
                        textCangjie.Text = Index.kCangjie;
                    }
                }
                catch (System.Data.StrongTypingException ex)
                {
                    textCangjie.Text = "OuterUniHan";
                    newtyping.Text = ex.Message;
                }
            }
        }

        private string ExpandUnihex(char[] chinchars)
        {
            char[] Nibble = new char[4];
            string UniHex;

            uint CharNo = (uint)chinchars[0];
            uint UintNibble;
            for (int x = 0; x <= 3; x++)
            {
                UintNibble = CharNo % 16;
                if (UintNibble == 0) Nibble[x] = '0';
                if (UintNibble == 1) Nibble[x] = '1';
                if (UintNibble == 2) Nibble[x] = '2';
                if (UintNibble == 3) Nibble[x] = '3';
                if (UintNibble == 4) Nibble[x] = '4';
                if (UintNibble == 5) Nibble[x] = '5';
                if (UintNibble == 6) Nibble[x] = '6';
                if (UintNibble == 7) Nibble[x] = '7';
                if (UintNibble == 8) Nibble[x] = '8';
                if (UintNibble == 9) Nibble[x] = '9';
                if (UintNibble == 10) Nibble[x] = 'A';
                if (UintNibble == 11) Nibble[x] = 'B';
                if (UintNibble == 12) Nibble[x] = 'C';
                if (UintNibble == 13) Nibble[x] = 'D';
                if (UintNibble == 14) Nibble[x] = 'E';
                if (UintNibble == 15) Nibble[x] = 'F';
                CharNo = CharNo / 16;
            }

            UniHex = Nibble[3].ToString() + Nibble[2].ToString() + Nibble[1].ToString() + Nibble[0].ToString();

            return UniHex;
        }

        private void SetUserCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetUserCredentials SetUserCredentialsForm = new SetUserCredentials();
                SetUserCredentialsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void LabelFourScreens(string Message)
        {
            labelAddQuestionMessage.Text = Message;
            labelAnswerToQuestionMessage.Text = Message;
            labelSearchMessage.Text = Message;
            labelStatisticsMessage.Text = Message;
        }

        private void DropSettings()
        {
            using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
            {
                var n01setting = from S01 in Variables.SettingDataContext.Setting01s
                                 select S01;
                Variables.SettingDataContext.Setting01s.DeleteAllOnSubmit(n01setting);

                Variables.SettingDataContext.SubmitChanges();
            }
        }

        private void DropLesson()
        {
            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                var n01lesson = from S01 in Variables.LessonDataContext.Lesson01s
                                select S01;
                Variables.LessonDataContext.Lesson01s.DeleteAllOnSubmit(n01lesson);

                Variables.LessonDataContext.SubmitChanges();
            }
        }

        private void MemToSQL()
        {
            DropLesson();

            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                var n01Items = from I01 in Variables.Leitner.ToLearn
                               orderby I01.Id
                               select I01;

                foreach (var n01Item in n01Items)
                {
                    Lesson01 working = new Lesson01
                    {
                        Id = n01Item.Id,
                        Side1 = n01Item.Side1,
                        Side2 = n01Item.Side2,
                        Side3 = n01Item.Side3,
                        A_B_GoodCount = n01Item.A_B_GoodCount,
                        A_C_GoodCount = n01Item.A_C_GoodCount,
                        B_A_GoodCount = n01Item.B_A_GoodCount,
                        B_C_GoodCount = n01Item.B_C_GoodCount,
                        C_A_GoodCount = n01Item.C_A_GoodCount,
                        C_B_GoodCount = n01Item.C_B_GoodCount,
                        A_B_BadCount = n01Item.A_B_BadCount,
                        A_C_BadCount = n01Item.A_C_BadCount,
                        B_A_BadCount = n01Item.B_A_BadCount,
                        B_C_BadCount = n01Item.B_C_BadCount,
                        C_A_BadCount = n01Item.C_A_BadCount,
                        C_B_BadCount = n01Item.C_B_BadCount,
                        A_B_DisplayLocation = n01Item.A_B_DisplayLocation,
                        A_C_DisplayLocation = n01Item.A_C_DisplayLocation,
                        B_A_DisplayLocation = n01Item.B_A_DisplayLocation,
                        B_C_DisplayLocation = n01Item.B_C_DisplayLocation,
                        C_A_DisplayLocation = n01Item.C_A_DisplayLocation,
                        C_B_DisplayLocation = n01Item.C_B_DisplayLocation,
                        A_B_TestDate = n01Item.A_B_TestDate,
                        A_C_TestDate = n01Item.A_C_TestDate,
                        B_A_TestDate = n01Item.B_A_TestDate,
                        B_C_TestDate = n01Item.B_C_TestDate,
                        C_A_TestDate = n01Item.C_A_TestDate,
                        C_B_TestDate = n01Item.C_B_TestDate
                    };

                    Variables.LessonDataContext.Lesson01s.InsertOnSubmit(working);
                }

                Variables.LessonDataContext.SubmitChanges();
            }
        }

        private void BackupLessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveXml();
        }

        private void BackupFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetBackupPath SetBackupPathForm = new SetBackupPath();
                SetBackupPathForm.ShowDialog();
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }
        }

        private void RestoreLessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variables.Reading != null)
                {
                    Variables.Reading.Close();
                    Variables.Leitner.Clear();
                }

                Variables.Leitner = new LeitnerBox();
                Variables.Reading = new FileStream(Variables.XmlFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Variables.Leitner.ReadXml(Variables.Reading);
                Variables.Reading.Close();

                DropLesson();

                using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
                {
                    var n01Items = from I01 in Variables.Leitner.ToLearn
                                   orderby I01.Id
                                   select I01;

                    foreach (var n01Item in n01Items)
                    {
                        Lesson01 working = new Lesson01();;

                        working.Id = n01Item.Id;

                        if (n01Item.Side1.Length > 255)
                        {
                            working.Side1 = n01Item.Side1.Substring(0, 255);
                        }
                        else
                        {
                            working.Side1 = n01Item.Side1;
                        }

                        if (n01Item.Side2.Length > 255)
                        {
                            working.Side2 = n01Item.Side2.Substring(0, 255);
                        }
                        else
                        {
                            working.Side2 = n01Item.Side2;
                        }

                        if (n01Item.Side3.Length > 255)
                        {
                            working.Side3 = n01Item.Side3.Substring(0, 255);
                        }
                        else
                        {
                            working.Side3 = n01Item.Side3;
                        }
                        working.A_B_GoodCount = n01Item.A_B_GoodCount;
                        working.A_C_GoodCount = n01Item.A_C_GoodCount;
                        working.B_A_GoodCount = n01Item.B_A_GoodCount;
                        working.B_C_GoodCount = n01Item.B_C_GoodCount;
                        working.C_A_GoodCount = n01Item.C_A_GoodCount;
                        working.C_B_GoodCount = n01Item.C_B_GoodCount;
                        working.A_B_BadCount = n01Item.A_B_BadCount;
                        working.A_C_BadCount = n01Item.A_C_BadCount;
                        working.B_A_BadCount = n01Item.B_A_BadCount;
                        working.B_C_BadCount = n01Item.B_C_BadCount;
                        working.C_A_BadCount = n01Item.C_A_BadCount;
                        working.C_B_BadCount = n01Item.C_B_BadCount;
                        working.A_B_DisplayLocation = n01Item.A_B_DisplayLocation;
                        working.A_C_DisplayLocation = n01Item.A_C_DisplayLocation;
                        working.B_A_DisplayLocation = n01Item.B_A_DisplayLocation;
                        working.B_C_DisplayLocation = n01Item.B_C_DisplayLocation;
                        working.C_A_DisplayLocation = n01Item.C_A_DisplayLocation;
                        working.C_B_DisplayLocation = n01Item.C_B_DisplayLocation;
                        working.A_B_TestDate = n01Item.A_B_TestDate;
                        working.A_C_TestDate = n01Item.A_C_TestDate;
                        working.B_A_TestDate = n01Item.B_A_TestDate;
                        working.B_C_TestDate = n01Item.B_C_TestDate;
                        working.C_A_TestDate = n01Item.C_A_TestDate;
                        working.C_B_TestDate = n01Item.C_B_TestDate;

                        Variables.LessonDataContext.Lesson01s.InsertOnSubmit(working);
                    }

                    Variables.LessonDataContext.SubmitChanges();
                }

                UpdateSQLSettings();
                TabPageStatistics_Enter();
                LoadXML();

                Successful("Restore", "Your lesson has been restored from: " + Variables.XmlFileName);
            }
            catch (Exception ex)
            {
                StackFrame file_info = new StackFrame(true);
                Error(ref file_info, ex.Message);
            }

        }

        private void RandomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Variables.LessonName))
            {
                return;
            }
            var randy = new Random();

            var n01Sets = from s01 in Variables.Leitner.ToLearn
                          orderby s01.Id
                          select s01;

            foreach (var n01Set in n01Sets)
            {
                n01Set.Randomize = (float)randy.NextDouble();
            }
            Variables.Leitner.ToLearn.AcceptChanges();

            var n02Sets = from s02 in Variables.Leitner.ToLearn
                          orderby s02.Randomize
                          select s02;

            int newId = 0;
            foreach (var n02Set in n02Sets)
            {
                n02Set.Id = ++newId;
            }
            Variables.Leitner.ToLearn.AcceptChanges();

            DropLesson();

            using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
            {
                var n01Items = from i01 in Variables.Leitner.ToLearn
                               orderby i01.Id
                               select i01;

                foreach (var n01Item in n01Items)
                {
                    var working = new Lesson01
                        {
                            Id = n01Item.Id,
                            Side1 = n01Item.Side1,
                            Side2 = n01Item.Side2,
                            Side3 = n01Item.Side3,
                            A_B_GoodCount = n01Item.A_B_GoodCount,
                            A_C_GoodCount = n01Item.A_C_GoodCount,
                            B_A_GoodCount = n01Item.B_A_GoodCount,
                            B_C_GoodCount = n01Item.B_C_GoodCount,
                            C_A_GoodCount = n01Item.C_A_GoodCount,
                            C_B_GoodCount = n01Item.C_B_GoodCount,
                            A_B_BadCount = n01Item.A_B_BadCount,
                            A_C_BadCount = n01Item.A_C_BadCount,
                            B_A_BadCount = n01Item.B_A_BadCount,
                            B_C_BadCount = n01Item.B_C_BadCount,
                            C_A_BadCount = n01Item.C_A_BadCount,
                            C_B_BadCount = n01Item.C_B_BadCount,
                            A_B_DisplayLocation = n01Item.A_B_DisplayLocation,
                            A_C_DisplayLocation = n01Item.A_C_DisplayLocation,
                            B_A_DisplayLocation = n01Item.B_A_DisplayLocation,
                            B_C_DisplayLocation = n01Item.B_C_DisplayLocation,
                            C_A_DisplayLocation = n01Item.C_A_DisplayLocation,
                            C_B_DisplayLocation = n01Item.C_B_DisplayLocation,
                            A_B_TestDate = n01Item.A_B_TestDate,
                            A_C_TestDate = n01Item.A_C_TestDate,
                            B_A_TestDate = n01Item.B_A_TestDate,
                            B_C_TestDate = n01Item.B_C_TestDate,
                            C_A_TestDate = n01Item.C_A_TestDate,
                            C_B_TestDate = n01Item.C_B_TestDate
                        };

                    Variables.LessonDataContext.Lesson01s.InsertOnSubmit(working);
                }

                Variables.LessonDataContext.SubmitChanges();
            }

            AddNodesToTreeView();
        }

        private void UpdateFEIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FullFarEastDataContext FullFarEast = new FullFarEastDataContext(Properties.Settings.Default.ChineseStudyConnection))
            {
                double newFEI;

                try
                {
                    newFEI = Convert.ToDouble(textFarEast.Text);
                }
                catch (FormatException)
                {
                    newtyping.Text = "FEI field does not contain a valid decimal number!";
                    return;
                }
                var indexes = from q in FullFarEast.FullFarEasts
                              where q.Char == textCh.Text
                              select q;

                foreach (var index in indexes)
                {
                    if (index.FeNumber != newFEI.ToString(CultureInfo.InvariantCulture))
                    {
                        index.FeNumber = newFEI.ToString(CultureInfo.InvariantCulture);
                    }
                }
                FullFarEast.SubmitChanges();
            }
        }

        private void ArchiveBacklogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrimOnePart(0);
            TrimOnePart(2);
            TrimOnePart(4);
            TrimOnePart(9);
            TrimOnePart(17);

            TabPageStatistics_Enter();
        }

        private void TrimOnePart(int partNo)
        {

            if (Variables.LessonTableName == "") return;

            var sortable = new string[Variables.Maxwords, 2];
            var partContains = CountBoxHoldings(partNo);


            if (partContains <= Variables.MaxStudy) return;

            var toArchive = partContains - Variables.MaxStudy;

            // Here we need a list of the items in the Source Box
            var questions = ListItemsInDisplayLocation(partNo);

            var sortedIndex = 0;
            var randy = new Random();

            foreach (var question in questions)
            {
                sortable[sortedIndex, 0] = question.Id.ToString(CultureInfo.InvariantCulture);
                sortable[sortedIndex, 1] = randy.Next().ToString(CultureInfo.InvariantCulture);

                sortedIndex++;
                if (sortedIndex > Variables.Maxwords - 1) { throw new IndexOutOfRangeException(); }
            }

            //if (!SortByDate(ref Sortable, SortedIndex - 1)) { throw new InvalidDataException(); }
            QuickSort(ref sortable, 0, sortedIndex - 1);

            var indexOldest = 0;

            for (var moveThem = toArchive; moveThem >= 1; moveThem--)
            {
                var items = (from q in Variables.Leitner.ToLearn
                             orderby q.Id
                             select q);

                foreach (var item in Enumerable.Where(items, item => sortable[indexOldest, 0] == item.Id.ToString(CultureInfo.InvariantCulture)))
                {
                    SetDisplayLocation(30, item);
                    SetGoodCount(0, item);
                    SetBadCount(0, item);
                    OneRecordChanged(item);
                    break;
                }
                indexOldest++;
            }
        }

        private void ResetGoodBadCountsOnRenumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset_GB_Counts = true;
        }

        private void PreserveGoodBadCountsOnRenumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset_GB_Counts = false;
        }

        private static void GoodCountChanged(LeitnerBox.ToLearnRow changedRecord)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_B_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 1:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_C_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 2:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_A_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 3:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_C_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 4:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_A_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 5:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_B_GoodCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void BadCountChanged(LeitnerBox.ToLearnRow changedRecord)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.LessonConnectionString;
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    switch (Variables.Leitner.Setting[0].StudyMode)
                    {
                        case 0:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_B_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 1:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET A_C_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 2:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_A_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 3:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET B_C_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 4:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_A_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                        case 5:
                            cmd.CommandText = "UPDATE " + Variables.LessonTableName + " SET C_B_BadCount = " + 0
                                 + " WHERE Id = " + changedRecord.Id;
                            break;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}