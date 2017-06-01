﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPP.ToolsMgmt.JobV31.MailManager {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailEncoding", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    public enum MailEncoding : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UTF8 = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ASCII = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UTF32 = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unicode = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailContract", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    [System.SerializableAttribute()]
    public partial class MailContract : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailAlternateView[] AlternateViewsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailAttachment[] AttachmentsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BCCField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailEncoding BodyEncodingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CCField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsBodyHtmlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailPriority PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReplyToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SubjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailEncoding SubjectEncodingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TrackingInfoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailAlternateView[] AlternateViews {
            get {
                return this.AlternateViewsField;
            }
            set {
                if ((object.ReferenceEquals(this.AlternateViewsField, value) != true)) {
                    this.AlternateViewsField = value;
                    this.RaisePropertyChanged("AlternateViews");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailAttachment[] Attachments {
            get {
                return this.AttachmentsField;
            }
            set {
                if ((object.ReferenceEquals(this.AttachmentsField, value) != true)) {
                    this.AttachmentsField = value;
                    this.RaisePropertyChanged("Attachments");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BCC {
            get {
                return this.BCCField;
            }
            set {
                if ((object.ReferenceEquals(this.BCCField, value) != true)) {
                    this.BCCField = value;
                    this.RaisePropertyChanged("BCC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Body {
            get {
                return this.BodyField;
            }
            set {
                if ((object.ReferenceEquals(this.BodyField, value) != true)) {
                    this.BodyField = value;
                    this.RaisePropertyChanged("Body");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailEncoding BodyEncoding {
            get {
                return this.BodyEncodingField;
            }
            set {
                if ((this.BodyEncodingField.Equals(value) != true)) {
                    this.BodyEncodingField = value;
                    this.RaisePropertyChanged("BodyEncoding");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CC {
            get {
                return this.CCField;
            }
            set {
                if ((object.ReferenceEquals(this.CCField, value) != true)) {
                    this.CCField = value;
                    this.RaisePropertyChanged("CC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From {
            get {
                return this.FromField;
            }
            set {
                if ((object.ReferenceEquals(this.FromField, value) != true)) {
                    this.FromField = value;
                    this.RaisePropertyChanged("From");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsBodyHtml {
            get {
                return this.IsBodyHtmlField;
            }
            set {
                if ((this.IsBodyHtmlField.Equals(value) != true)) {
                    this.IsBodyHtmlField = value;
                    this.RaisePropertyChanged("IsBodyHtml");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailPriority Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReplyTo {
            get {
                return this.ReplyToField;
            }
            set {
                if ((object.ReferenceEquals(this.ReplyToField, value) != true)) {
                    this.ReplyToField = value;
                    this.RaisePropertyChanged("ReplyTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Subject {
            get {
                return this.SubjectField;
            }
            set {
                if ((object.ReferenceEquals(this.SubjectField, value) != true)) {
                    this.SubjectField = value;
                    this.RaisePropertyChanged("Subject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailEncoding SubjectEncoding {
            get {
                return this.SubjectEncodingField;
            }
            set {
                if ((this.SubjectEncodingField.Equals(value) != true)) {
                    this.SubjectEncodingField = value;
                    this.RaisePropertyChanged("SubjectEncoding");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string To {
            get {
                return this.ToField;
            }
            set {
                if ((object.ReferenceEquals(this.ToField, value) != true)) {
                    this.ToField = value;
                    this.RaisePropertyChanged("To");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TrackingInfo {
            get {
                return this.TrackingInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.TrackingInfoField, value) != true)) {
                    this.TrackingInfoField = value;
                    this.RaisePropertyChanged("TrackingInfo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailAlternateView", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    [System.SerializableAttribute()]
    public partial class MailAlternateView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailEncoding EncodingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MailLinkedResource[] LinkedResourcesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MediaTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailEncoding Encoding {
            get {
                return this.EncodingField;
            }
            set {
                if ((this.EncodingField.Equals(value) != true)) {
                    this.EncodingField = value;
                    this.RaisePropertyChanged("Encoding");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MailLinkedResource[] LinkedResources {
            get {
                return this.LinkedResourcesField;
            }
            set {
                if ((object.ReferenceEquals(this.LinkedResourcesField, value) != true)) {
                    this.LinkedResourcesField = value;
                    this.RaisePropertyChanged("LinkedResources");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MediaType {
            get {
                return this.MediaTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.MediaTypeField, value) != true)) {
                    this.MediaTypeField = value;
                    this.RaisePropertyChanged("MediaType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailAttachment", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    [System.SerializableAttribute()]
    public partial class MailAttachment : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FileContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private IPP.ToolsMgmt.JobV31.MailManager.MediaType MediaTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] FileContent {
            get {
                return this.FileContentField;
            }
            set {
                if ((object.ReferenceEquals(this.FileContentField, value) != true)) {
                    this.FileContentField = value;
                    this.RaisePropertyChanged("FileContent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPP.ToolsMgmt.JobV31.MailManager.MediaType MediaType {
            get {
                return this.MediaTypeField;
            }
            set {
                if ((this.MediaTypeField.Equals(value) != true)) {
                    this.MediaTypeField = value;
                    this.RaisePropertyChanged("MediaType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailPriority", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    public enum MailPriority : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Normal = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Low = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailLinkedResource", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    [System.SerializableAttribute()]
    public partial class MailLinkedResource : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ContentStreamField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentId {
            get {
                return this.ContentIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentIdField, value) != true)) {
                    this.ContentIdField = value;
                    this.RaisePropertyChanged("ContentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] ContentStream {
            get {
                return this.ContentStreamField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentStreamField, value) != true)) {
                    this.ContentStreamField = value;
                    this.RaisePropertyChanged("ContentStream");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediaType", Namespace="http://oversea.newegg.com/Framework/EMail/DataContract")]
    public enum MediaType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GIF = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        JPEG = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TIFF = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PDF = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RTF = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SOAP = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ZIP = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Other = 7,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oversea.newegg.com/Framework/EMail/ServiceContract", ConfigurationName="MailManager.ISendEmail")]
    public interface ISendEmail {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Sim" +
            "ply", ReplyAction="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Sim" +
            "plyResponse")]
        bool SendMail_Simply(string from, string to, string subject, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Adv" +
            "anced", ReplyAction="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Adv" +
            "ancedResponse")]
        bool SendMail_Advanced(string from, string to, string cc, string bcc, string subject, string body, bool isHtmlBody, bool needLog);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Adv" +
            "ancedWithEncoding", ReplyAction="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Adv" +
            "ancedWithEncodingResponse")]
        bool SendMail_AdvancedWithEncoding(string from, string to, string cc, string bcc, string subject, string body, IPP.ToolsMgmt.JobV31.MailManager.MailEncoding subjectEncoding, IPP.ToolsMgmt.JobV31.MailManager.MailEncoding bodyEncoding, bool isHtmlBody, bool needLog);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Ful" +
            "ly", ReplyAction="http://oversea.newegg.com/Framework/EMail/ServiceContract/ISendEmail/SendMail_Ful" +
            "lyResponse")]
        bool SendMail_Fully(IPP.ToolsMgmt.JobV31.MailManager.MailContract mail, bool needLog);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISendEmailChannel : IPP.ToolsMgmt.JobV31.MailManager.ISendEmail, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SendEmailClient : System.ServiceModel.ClientBase<IPP.ToolsMgmt.JobV31.MailManager.ISendEmail>, IPP.ToolsMgmt.JobV31.MailManager.ISendEmail {
        
        public SendEmailClient() {
        }
        
        public SendEmailClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SendEmailClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendEmailClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendEmailClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SendMail_Simply(string from, string to, string subject, string body) {
            return base.Channel.SendMail_Simply(from, to, subject, body);
        }
        
        public bool SendMail_Advanced(string from, string to, string cc, string bcc, string subject, string body, bool isHtmlBody, bool needLog) {
            return base.Channel.SendMail_Advanced(from, to, cc, bcc, subject, body, isHtmlBody, needLog);
        }
        
        public bool SendMail_AdvancedWithEncoding(string from, string to, string cc, string bcc, string subject, string body, IPP.ToolsMgmt.JobV31.MailManager.MailEncoding subjectEncoding, IPP.ToolsMgmt.JobV31.MailManager.MailEncoding bodyEncoding, bool isHtmlBody, bool needLog) {
            return base.Channel.SendMail_AdvancedWithEncoding(from, to, cc, bcc, subject, body, subjectEncoding, bodyEncoding, isHtmlBody, needLog);
        }
        
        public bool SendMail_Fully(IPP.ToolsMgmt.JobV31.MailManager.MailContract mail, bool needLog) {
            return base.Channel.SendMail_Fully(mail, needLog);
        }
    }
}