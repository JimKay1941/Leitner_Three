﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace Leitner_Three {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    //[System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class LeitnerBox {
        
        private object[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Setting", typeof(LeitnerBoxSetting), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("ToLearn", typeof(LeitnerBoxToLearn), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LeitnerBoxSetting {
        
        private string questionTextBoxField;
        
        private string answerTextBoxField;
        
        private string hintTextBoxField;
        
        private string dateField;
        
        private string startTimeField;
        
        private string a_B_PreviousAutoAgeField;
        
        private string a_C_PreviousAutoAgeField;
        
        private string b_A_PreviousAutoAgeField;
        
        private string b_C_PreviousAutoAgeField;
        
        private string c_A_PreviousAutoAgeField;
        
        private string c_B_PreviousAutoAgeField;
        
        private int a_B_AutoAgeIntervalField;
        
        private bool a_B_AutoAgeIntervalFieldSpecified;
        
        private int a_C_AutoAgeIntervalField;
        
        private bool a_C_AutoAgeIntervalFieldSpecified;
        
        private int b_A_AutoAgeIntervalField;
        
        private bool b_A_AutoAgeIntervalFieldSpecified;
        
        private int b_C_AutoAgeIntervalField;
        
        private bool b_C_AutoAgeIntervalFieldSpecified;
        
        private int c_A_AutoAgeIntervalField;
        
        private bool c_A_AutoAgeIntervalFieldSpecified;
        
        private int c_B_AutoAgeIntervalField;
        
        private bool c_B_AutoAgeIntervalFieldSpecified;
        
        private string side1NameField;
        
        private string side2NameField;
        
        private string side3NameField;
        
        private string studySequenceField;
        
        private int studyModeField;
        
        private bool studyModeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string QuestionTextBox {
            get {
                return this.questionTextBoxField;
            }
            set {
                this.questionTextBoxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AnswerTextBox {
            get {
                return this.answerTextBoxField;
            }
            set {
                this.answerTextBoxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string HintTextBox {
            get {
                return this.hintTextBoxField;
            }
            set {
                this.hintTextBoxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StartTime {
            get {
                return this.startTimeField;
            }
            set {
                this.startTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string A_B_PreviousAutoAge {
            get {
                return this.a_B_PreviousAutoAgeField;
            }
            set {
                this.a_B_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string A_C_PreviousAutoAge {
            get {
                return this.a_C_PreviousAutoAgeField;
            }
            set {
                this.a_C_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string B_A_PreviousAutoAge {
            get {
                return this.b_A_PreviousAutoAgeField;
            }
            set {
                this.b_A_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string B_C_PreviousAutoAge {
            get {
                return this.b_C_PreviousAutoAgeField;
            }
            set {
                this.b_C_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string C_A_PreviousAutoAge {
            get {
                return this.c_A_PreviousAutoAgeField;
            }
            set {
                this.c_A_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string C_B_PreviousAutoAge {
            get {
                return this.c_B_PreviousAutoAgeField;
            }
            set {
                this.c_B_PreviousAutoAgeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_B_AutoAgeInterval {
            get {
                return this.a_B_AutoAgeIntervalField;
            }
            set {
                this.a_B_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_B_AutoAgeIntervalSpecified {
            get {
                return this.a_B_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.a_B_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_C_AutoAgeInterval {
            get {
                return this.a_C_AutoAgeIntervalField;
            }
            set {
                this.a_C_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_C_AutoAgeIntervalSpecified {
            get {
                return this.a_C_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.a_C_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_A_AutoAgeInterval {
            get {
                return this.b_A_AutoAgeIntervalField;
            }
            set {
                this.b_A_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_A_AutoAgeIntervalSpecified {
            get {
                return this.b_A_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.b_A_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_C_AutoAgeInterval {
            get {
                return this.b_C_AutoAgeIntervalField;
            }
            set {
                this.b_C_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_C_AutoAgeIntervalSpecified {
            get {
                return this.b_C_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.b_C_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_A_AutoAgeInterval {
            get {
                return this.c_A_AutoAgeIntervalField;
            }
            set {
                this.c_A_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_A_AutoAgeIntervalSpecified {
            get {
                return this.c_A_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.c_A_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_B_AutoAgeInterval {
            get {
                return this.c_B_AutoAgeIntervalField;
            }
            set {
                this.c_B_AutoAgeIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_B_AutoAgeIntervalSpecified {
            get {
                return this.c_B_AutoAgeIntervalFieldSpecified;
            }
            set {
                this.c_B_AutoAgeIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side1Name {
            get {
                return this.side1NameField;
            }
            set {
                this.side1NameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side2Name {
            get {
                return this.side2NameField;
            }
            set {
                this.side2NameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side3Name {
            get {
                return this.side3NameField;
            }
            set {
                this.side3NameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StudySequence {
            get {
                return this.studySequenceField;
            }
            set {
                this.studySequenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int StudyMode {
            get {
                return this.studyModeField;
            }
            set {
                this.studyModeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StudyModeSpecified {
            get {
                return this.studyModeFieldSpecified;
            }
            set {
                this.studyModeFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LeitnerBoxToLearn {
        
        private int idField;
        
        private bool idFieldSpecified;
        
        private string side1Field;
        
        private string side2Field;
        
        private string side3Field;
        
        private int a_B_GoodCountField;
        
        private bool a_B_GoodCountFieldSpecified;
        
        private int a_C_GoodCountField;
        
        private bool a_C_GoodCountFieldSpecified;
        
        private int b_A_GoodCountField;
        
        private bool b_A_GoodCountFieldSpecified;
        
        private int b_C_GoodCountField;
        
        private bool b_C_GoodCountFieldSpecified;
        
        private int c_A_GoodCountField;
        
        private bool c_A_GoodCountFieldSpecified;
        
        private int c_B_GoodCountField;
        
        private bool c_B_GoodCountFieldSpecified;
        
        private int a_B_BadCountField;
        
        private bool a_B_BadCountFieldSpecified;
        
        private int a_C_BadCountField;
        
        private bool a_C_BadCountFieldSpecified;
        
        private int b_A_BadCountField;
        
        private bool b_A_BadCountFieldSpecified;
        
        private int b_C_BadCountField;
        
        private bool b_C_BadCountFieldSpecified;
        
        private int c_A_BadCountField;
        
        private bool c_A_BadCountFieldSpecified;
        
        private int c_B_BadCountField;
        
        private bool c_B_BadCountFieldSpecified;
        
        private int a_B_DisplayLocationField;
        
        private bool a_B_DisplayLocationFieldSpecified;
        
        private int a_C_DisplayLocationField;
        
        private bool a_C_DisplayLocationFieldSpecified;
        
        private int b_A_DisplayLocationField;
        
        private bool b_A_DisplayLocationFieldSpecified;
        
        private int b_C_DisplayLocationField;
        
        private bool b_C_DisplayLocationFieldSpecified;
        
        private int c_A_DisplayLocationField;
        
        private bool c_A_DisplayLocationFieldSpecified;
        
        private int c_B_DisplayLocationField;
        
        private bool c_B_DisplayLocationFieldSpecified;
        
        private string a_B_TestDateField;
        
        private string a_C_TestDateField;
        
        private string b_A_TestDateField;
        
        private string b_C_TestDateField;
        
        private string c_A_TestDateField;
        
        private string c_B_TestDateField;
        
        private float randomizeField;
        
        private bool randomizeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdSpecified {
            get {
                return this.idFieldSpecified;
            }
            set {
                this.idFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side1 {
            get {
                return this.side1Field;
            }
            set {
                this.side1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side2 {
            get {
                return this.side2Field;
            }
            set {
                this.side2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Side3 {
            get {
                return this.side3Field;
            }
            set {
                this.side3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_B_GoodCount {
            get {
                return this.a_B_GoodCountField;
            }
            set {
                this.a_B_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_B_GoodCountSpecified {
            get {
                return this.a_B_GoodCountFieldSpecified;
            }
            set {
                this.a_B_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_C_GoodCount {
            get {
                return this.a_C_GoodCountField;
            }
            set {
                this.a_C_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_C_GoodCountSpecified {
            get {
                return this.a_C_GoodCountFieldSpecified;
            }
            set {
                this.a_C_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_A_GoodCount {
            get {
                return this.b_A_GoodCountField;
            }
            set {
                this.b_A_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_A_GoodCountSpecified {
            get {
                return this.b_A_GoodCountFieldSpecified;
            }
            set {
                this.b_A_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_C_GoodCount {
            get {
                return this.b_C_GoodCountField;
            }
            set {
                this.b_C_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_C_GoodCountSpecified {
            get {
                return this.b_C_GoodCountFieldSpecified;
            }
            set {
                this.b_C_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_A_GoodCount {
            get {
                return this.c_A_GoodCountField;
            }
            set {
                this.c_A_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_A_GoodCountSpecified {
            get {
                return this.c_A_GoodCountFieldSpecified;
            }
            set {
                this.c_A_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_B_GoodCount {
            get {
                return this.c_B_GoodCountField;
            }
            set {
                this.c_B_GoodCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_B_GoodCountSpecified {
            get {
                return this.c_B_GoodCountFieldSpecified;
            }
            set {
                this.c_B_GoodCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_B_BadCount {
            get {
                return this.a_B_BadCountField;
            }
            set {
                this.a_B_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_B_BadCountSpecified {
            get {
                return this.a_B_BadCountFieldSpecified;
            }
            set {
                this.a_B_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_C_BadCount {
            get {
                return this.a_C_BadCountField;
            }
            set {
                this.a_C_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_C_BadCountSpecified {
            get {
                return this.a_C_BadCountFieldSpecified;
            }
            set {
                this.a_C_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_A_BadCount {
            get {
                return this.b_A_BadCountField;
            }
            set {
                this.b_A_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_A_BadCountSpecified {
            get {
                return this.b_A_BadCountFieldSpecified;
            }
            set {
                this.b_A_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_C_BadCount {
            get {
                return this.b_C_BadCountField;
            }
            set {
                this.b_C_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_C_BadCountSpecified {
            get {
                return this.b_C_BadCountFieldSpecified;
            }
            set {
                this.b_C_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_A_BadCount {
            get {
                return this.c_A_BadCountField;
            }
            set {
                this.c_A_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_A_BadCountSpecified {
            get {
                return this.c_A_BadCountFieldSpecified;
            }
            set {
                this.c_A_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_B_BadCount {
            get {
                return this.c_B_BadCountField;
            }
            set {
                this.c_B_BadCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_B_BadCountSpecified {
            get {
                return this.c_B_BadCountFieldSpecified;
            }
            set {
                this.c_B_BadCountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_B_DisplayLocation {
            get {
                return this.a_B_DisplayLocationField;
            }
            set {
                this.a_B_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_B_DisplayLocationSpecified {
            get {
                return this.a_B_DisplayLocationFieldSpecified;
            }
            set {
                this.a_B_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int A_C_DisplayLocation {
            get {
                return this.a_C_DisplayLocationField;
            }
            set {
                this.a_C_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool A_C_DisplayLocationSpecified {
            get {
                return this.a_C_DisplayLocationFieldSpecified;
            }
            set {
                this.a_C_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_A_DisplayLocation {
            get {
                return this.b_A_DisplayLocationField;
            }
            set {
                this.b_A_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_A_DisplayLocationSpecified {
            get {
                return this.b_A_DisplayLocationFieldSpecified;
            }
            set {
                this.b_A_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int B_C_DisplayLocation {
            get {
                return this.b_C_DisplayLocationField;
            }
            set {
                this.b_C_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool B_C_DisplayLocationSpecified {
            get {
                return this.b_C_DisplayLocationFieldSpecified;
            }
            set {
                this.b_C_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_A_DisplayLocation {
            get {
                return this.c_A_DisplayLocationField;
            }
            set {
                this.c_A_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_A_DisplayLocationSpecified {
            get {
                return this.c_A_DisplayLocationFieldSpecified;
            }
            set {
                this.c_A_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int C_B_DisplayLocation {
            get {
                return this.c_B_DisplayLocationField;
            }
            set {
                this.c_B_DisplayLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool C_B_DisplayLocationSpecified {
            get {
                return this.c_B_DisplayLocationFieldSpecified;
            }
            set {
                this.c_B_DisplayLocationFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string A_B_TestDate {
            get {
                return this.a_B_TestDateField;
            }
            set {
                this.a_B_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string A_C_TestDate {
            get {
                return this.a_C_TestDateField;
            }
            set {
                this.a_C_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string B_A_TestDate {
            get {
                return this.b_A_TestDateField;
            }
            set {
                this.b_A_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string B_C_TestDate {
            get {
                return this.b_C_TestDateField;
            }
            set {
                this.b_C_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string C_A_TestDate {
            get {
                return this.c_A_TestDateField;
            }
            set {
                this.c_A_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string C_B_TestDate {
            get {
                return this.c_B_TestDateField;
            }
            set {
                this.c_B_TestDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public float Randomize {
            get {
                return this.randomizeField;
            }
            set {
                this.randomizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RandomizeSpecified {
            get {
                return this.randomizeFieldSpecified;
            }
            set {
                this.randomizeFieldSpecified = value;
            }
        }
    }
}