﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcEshop.DocumentFinder {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestMessage", Namespace="http://schemas.datacontract.org/2004/07/RequestIOService")]
    [System.SerializableAttribute()]
    public partial class RequestMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerMailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DatabaseCodesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FileBodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextBodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
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
        public string CustomerMail {
            get {
                return this.CustomerMailField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerMailField, value) != true)) {
                    this.CustomerMailField = value;
                    this.RaisePropertyChanged("CustomerMail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DatabaseCodes {
            get {
                return this.DatabaseCodesField;
            }
            set {
                if ((object.ReferenceEquals(this.DatabaseCodesField, value) != true)) {
                    this.DatabaseCodesField = value;
                    this.RaisePropertyChanged("DatabaseCodes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] FileBody {
            get {
                return this.FileBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.FileBodyField, value) != true)) {
                    this.FileBodyField = value;
                    this.RaisePropertyChanged("FileBody");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TextBody {
            get {
                return this.TextBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.TextBodyField, value) != true)) {
                    this.TextBodyField = value;
                    this.RaisePropertyChanged("TextBody");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Results", Namespace="http://schemas.datacontract.org/2004/07/RequestIOService")]
    [System.SerializableAttribute()]
    public partial class Results : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MvcEshop.DocumentFinder.Result[] ResultListField;
        
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
        public int ResultCode {
            get {
                return this.ResultCodeField;
            }
            set {
                if ((this.ResultCodeField.Equals(value) != true)) {
                    this.ResultCodeField = value;
                    this.RaisePropertyChanged("ResultCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MvcEshop.DocumentFinder.Result[] ResultList {
            get {
                return this.ResultListField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultListField, value) != true)) {
                    this.ResultListField = value;
                    this.RaisePropertyChanged("ResultList");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/RequestIOService")]
    [System.SerializableAttribute()]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ItemNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SubjectSimilarityField;
        
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
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object ItemNumber {
            get {
                return this.ItemNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ItemNumberField, value) != true)) {
                    this.ItemNumberField = value;
                    this.RaisePropertyChanged("ItemNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SubjectSimilarity {
            get {
                return this.SubjectSimilarityField;
            }
            set {
                if ((this.SubjectSimilarityField.Equals(value) != true)) {
                    this.SubjectSimilarityField = value;
                    this.RaisePropertyChanged("SubjectSimilarity");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="OriginalityReport", Namespace="http://schemas.datacontract.org/2004/07/RequestIOService")]
    [System.SerializableAttribute()]
    public partial class OriginalityReport : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlReportField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SimilarityPercentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SimilarityReferenceField;
        
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
        public string HtmlReport {
            get {
                return this.HtmlReportField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlReportField, value) != true)) {
                    this.HtmlReportField = value;
                    this.RaisePropertyChanged("HtmlReport");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ResultCode {
            get {
                return this.ResultCodeField;
            }
            set {
                if ((this.ResultCodeField.Equals(value) != true)) {
                    this.ResultCodeField = value;
                    this.RaisePropertyChanged("ResultCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SimilarityPercent {
            get {
                return this.SimilarityPercentField;
            }
            set {
                if ((this.SimilarityPercentField.Equals(value) != true)) {
                    this.SimilarityPercentField = value;
                    this.RaisePropertyChanged("SimilarityPercent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SimilarityReference {
            get {
                return this.SimilarityReferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.SimilarityReferenceField, value) != true)) {
                    this.SimilarityReferenceField = value;
                    this.RaisePropertyChanged("SimilarityReference");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DocumentFinder.IRequestIOService")]
    public interface IRequestIOService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/PutMessage", ReplyAction="http://tempuri.org/IRequestIOService/PutMessageResponse")]
        int PutMessage(MvcEshop.DocumentFinder.RequestMessage value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/PutMessage", ReplyAction="http://tempuri.org/IRequestIOService/PutMessageResponse")]
        System.Threading.Tasks.Task<int> PutMessageAsync(MvcEshop.DocumentFinder.RequestMessage value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetResultsByRequestID", ReplyAction="http://tempuri.org/IRequestIOService/GetResultsByRequestIDResponse")]
        MvcEshop.DocumentFinder.Results GetResultsByRequestID(int RequestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetResultsByRequestID", ReplyAction="http://tempuri.org/IRequestIOService/GetResultsByRequestIDResponse")]
        System.Threading.Tasks.Task<MvcEshop.DocumentFinder.Results> GetResultsByRequestIDAsync(int RequestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetReportByResultID", ReplyAction="http://tempuri.org/IRequestIOService/GetReportByResultIDResponse")]
        MvcEshop.DocumentFinder.OriginalityReport GetReportByResultID(int ResultID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetReportByResultID", ReplyAction="http://tempuri.org/IRequestIOService/GetReportByResultIDResponse")]
        System.Threading.Tasks.Task<MvcEshop.DocumentFinder.OriginalityReport> GetReportByResultIDAsync(int ResultID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetFinalReportByRequestID", ReplyAction="http://tempuri.org/IRequestIOService/GetFinalReportByRequestIDResponse")]
        MvcEshop.DocumentFinder.OriginalityReport GetFinalReportByRequestID(int RequestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRequestIOService/GetFinalReportByRequestID", ReplyAction="http://tempuri.org/IRequestIOService/GetFinalReportByRequestIDResponse")]
        System.Threading.Tasks.Task<MvcEshop.DocumentFinder.OriginalityReport> GetFinalReportByRequestIDAsync(int RequestID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRequestIOServiceChannel : MvcEshop.DocumentFinder.IRequestIOService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RequestIOServiceClient : System.ServiceModel.ClientBase<MvcEshop.DocumentFinder.IRequestIOService>, MvcEshop.DocumentFinder.IRequestIOService {
        
        public RequestIOServiceClient() {
        }
        
        public RequestIOServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RequestIOServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequestIOServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequestIOServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int PutMessage(MvcEshop.DocumentFinder.RequestMessage value) {
            return base.Channel.PutMessage(value);
        }
        
        public System.Threading.Tasks.Task<int> PutMessageAsync(MvcEshop.DocumentFinder.RequestMessage value) {
            return base.Channel.PutMessageAsync(value);
        }
        
        public MvcEshop.DocumentFinder.Results GetResultsByRequestID(int RequestID) {
            return base.Channel.GetResultsByRequestID(RequestID);
        }
        
        public System.Threading.Tasks.Task<MvcEshop.DocumentFinder.Results> GetResultsByRequestIDAsync(int RequestID) {
            return base.Channel.GetResultsByRequestIDAsync(RequestID);
        }
        
        public MvcEshop.DocumentFinder.OriginalityReport GetReportByResultID(int ResultID) {
            return base.Channel.GetReportByResultID(ResultID);
        }
        
        public System.Threading.Tasks.Task<MvcEshop.DocumentFinder.OriginalityReport> GetReportByResultIDAsync(int ResultID) {
            return base.Channel.GetReportByResultIDAsync(ResultID);
        }
        
        public MvcEshop.DocumentFinder.OriginalityReport GetFinalReportByRequestID(int RequestID) {
            return base.Channel.GetFinalReportByRequestID(RequestID);
        }
        
        public System.Threading.Tasks.Task<MvcEshop.DocumentFinder.OriginalityReport> GetFinalReportByRequestIDAsync(int RequestID) {
            return base.Channel.GetFinalReportByRequestIDAsync(RequestID);
        }
    }
}