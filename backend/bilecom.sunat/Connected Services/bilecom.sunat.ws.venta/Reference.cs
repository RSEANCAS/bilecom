﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bilecom.sunat.bilecom.sunat.ws.venta {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.sunat.gob.pe", ConfigurationName="bilecom.sunat.ws.venta.billService")]
    public interface billService {
        
        // CODEGEN: El parámetro 'status' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:getStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse getStatus(bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:getStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse> getStatusAsync(bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest request);
        
        // CODEGEN: El parámetro 'applicationResponse' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendBill", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="applicationResponse")]
        bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse sendBill(bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendBill", ReplyAction="*")]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse> sendBillAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest request);
        
        // CODEGEN: El parámetro 'ticket' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendPack", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ticket")]
        bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse sendPack(bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendPack", ReplyAction="*")]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse> sendPackAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest request);
        
        // CODEGEN: El parámetro 'ticket' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendSummary", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ticket")]
        bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse sendSummary(bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendSummary", ReplyAction="*")]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse> sendSummaryAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.sunat.gob.pe")]
    public partial class statusResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] contentField;
        
        private string statusCodeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", Order=0)]
        public byte[] content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("content");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string statusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
                this.RaisePropertyChanged("statusCode");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatus", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public getStatusRequest() {
        }
        
        public getStatusRequest(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatusResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bilecom.sunat.bilecom.sunat.ws.venta.statusResponse status;
        
        public getStatusResponse() {
        }
        
        public getStatusResponse(bilecom.sunat.bilecom.sunat.ws.venta.statusResponse status) {
            this.status = status;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBill", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendBillRequest() {
        }
        
        public sendBillRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBillResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] applicationResponse;
        
        public sendBillResponse() {
        }
        
        public sendBillResponse(byte[] applicationResponse) {
            this.applicationResponse = applicationResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPack", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendPackRequest() {
        }
        
        public sendPackRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPackResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendPackResponse() {
        }
        
        public sendPackResponse(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummary", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendSummaryRequest() {
        }
        
        public sendSummaryRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummaryResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendSummaryResponse() {
        }
        
        public sendSummaryResponse(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface billServiceChannel : bilecom.sunat.bilecom.sunat.ws.venta.billService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class billServiceClient : System.ServiceModel.ClientBase<bilecom.sunat.bilecom.sunat.ws.venta.billService>, bilecom.sunat.bilecom.sunat.ws.venta.billService {
        
        public billServiceClient() {
        }
        
        public billServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public billServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public billServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public billServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse bilecom.sunat.bilecom.sunat.ws.venta.billService.getStatus(bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest request) {
            return base.Channel.getStatus(request);
        }
        
        public bilecom.sunat.bilecom.sunat.ws.venta.statusResponse getStatus(string ticket) {
            bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest();
            inValue.ticket = ticket;
            bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse retVal = ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).getStatus(inValue);
            return retVal.status;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse> bilecom.sunat.bilecom.sunat.ws.venta.billService.getStatusAsync(bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest request) {
            return base.Channel.getStatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.getStatusResponse> getStatusAsync(string ticket) {
            bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.getStatusRequest();
            inValue.ticket = ticket;
            return ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).getStatusAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse bilecom.sunat.bilecom.sunat.ws.venta.billService.sendBill(bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest request) {
            return base.Channel.sendBill(request);
        }
        
        public byte[] sendBill(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse retVal = ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendBill(inValue);
            return retVal.applicationResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse> bilecom.sunat.bilecom.sunat.ws.venta.billService.sendBillAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest request) {
            return base.Channel.sendBillAsync(request);
        }
        
        public System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendBillResponse> sendBillAsync(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendBillRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendBillAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse bilecom.sunat.bilecom.sunat.ws.venta.billService.sendPack(bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest request) {
            return base.Channel.sendPack(request);
        }
        
        public string sendPack(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse retVal = ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendPack(inValue);
            return retVal.ticket;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse> bilecom.sunat.bilecom.sunat.ws.venta.billService.sendPackAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest request) {
            return base.Channel.sendPackAsync(request);
        }
        
        public System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendPackResponse> sendPackAsync(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendPackRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendPackAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse bilecom.sunat.bilecom.sunat.ws.venta.billService.sendSummary(bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest request) {
            return base.Channel.sendSummary(request);
        }
        
        public string sendSummary(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse retVal = ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendSummary(inValue);
            return retVal.ticket;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse> bilecom.sunat.bilecom.sunat.ws.venta.billService.sendSummaryAsync(bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest request) {
            return base.Channel.sendSummaryAsync(request);
        }
        
        public System.Threading.Tasks.Task<bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryResponse> sendSummaryAsync(string fileName, byte[] contentFile, string partyType) {
            bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest inValue = new bilecom.sunat.bilecom.sunat.ws.venta.sendSummaryRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((bilecom.sunat.bilecom.sunat.ws.venta.billService)(this)).sendSummaryAsync(inValue);
        }
    }
}
