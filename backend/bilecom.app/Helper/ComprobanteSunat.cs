using bilecom.be;
using bilecom.enums;
using bilecom.sunat.comprobante.creditnote;
using bilecom.sunat.comprobante.debitnote;
using bilecom.sunat.comprobante.invoice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static bilecom.enums.Enums;

namespace bilecom.app.Helper
{
    public class ComprobanteSunat
    {
        public enum VersionUBL
        {
            [DefaultValue("2.1")]
            [Category("2.0")]
            _2_1
        };
        

        public static InvoiceType ObtenerComprobante(FacturaBe item, VersionUBL version)
        {
            InvoiceType invoiceType = null;

            switch (version)
            {
                case VersionUBL._2_1: invoiceType = ObtenerComprobante_2_1(item, version);
                    break;
            }

            return invoiceType;
        }

        public static InvoiceType ObtenerComprobante_2_1(FacturaBe item, VersionUBL version)
        {
            InvoiceType invoiceType = new InvoiceType();

            string versionValue = version.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            string versionCustom = version.GetAttributeOfType<CategoryAttribute>().Category;
            int cantidadDecimales = 2;
            string formatoDecimales = "0." + (new String('0', cantidadDecimales));
            decimal totalImpuestos = Math.Round(item.TotalIgv + item.TotalIsc + item.TotalOtrosTributos, cantidadDecimales);
            int cantidadImpuestos = 0;
            if (item.TotalExportacion > 0) cantidadImpuestos++;
            if (item.TotalInafecto > 0) cantidadImpuestos++;
            if (item.TotalExonerado > 0) cantidadImpuestos++;
            if (item.TotalGratuito > 0) cantidadImpuestos++;
            if (item.TotalIgv > 0) cantidadImpuestos++;
            if (item.TotalIsc > 0) cantidadImpuestos++;
            if (item.TotalOtrosTributos > 0) cantidadImpuestos++;
            //if (item.TotalVentaArrozPilado > 0) cantidadImpuestos++;

            int cantidadCargos = 0;
            if (item.TotalOtrosCargosGlobal> 0) cantidadCargos++;
            if (item.TotalDescuentosGlobal > 0) cantidadCargos++;

            #region Datos de la Factura electrónica
            // 01. UBL
            invoiceType.UBLVersionID = new sunat.comprobante.invoice.UBLVersionIDType();
            invoiceType.UBLVersionID.Value = versionValue;

            // 02. CUSTOMIZATIONID
            invoiceType.CustomizationID = new sunat.comprobante.invoice.CustomizationIDType();
            invoiceType.CustomizationID.Value = versionCustom;
            invoiceType.CustomizationID.schemeAgencyName = "PE:SUNAT";

            // 03. FECHA DE EMISION
            invoiceType.IssueDate = new sunat.comprobante.invoice.IssueDateType();
            invoiceType.IssueDate.Value = item.FechaHoraEmision;

            // 04. HORA DE EMISION
            invoiceType.IssueTime = new sunat.comprobante.invoice.IssueTimeType();
            invoiceType.IssueTime.Value = item.FechaHoraEmision;

            // 05. TIPO COMPROBANTE
            invoiceType.InvoiceTypeCode = new sunat.comprobante.invoice.InvoiceTypeCodeType();
            invoiceType.InvoiceTypeCode.Value = TipoComprobante.Factura.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            invoiceType.InvoiceTypeCode.listID = item.TipoOperacionVenta.CodigoSunat;
            invoiceType.InvoiceTypeCode.listAgencyName = "PE:SUNAT";
            invoiceType.InvoiceTypeCode.listName = "Tipo de Documento";
            invoiceType.InvoiceTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";

            invoiceType.ID = new sunat.comprobante.invoice.IDType();
            invoiceType.ID.Value = $"{item.Serie.Serial}-{item.NroComprobante:00000000}";

            // 06. TIPO MONEDA
            invoiceType.DocumentCurrencyCode = new sunat.comprobante.invoice.DocumentCurrencyCodeType();
            invoiceType.DocumentCurrencyCode.Value = item.Moneda.Codigo;
            invoiceType.DocumentCurrencyCode.listID = "ISO 4217 Alpha";
            invoiceType.DocumentCurrencyCode.listName = "Currency";
            invoiceType.DocumentCurrencyCode.listAgencyName = "United Nations Economic Commission for Europe";

            // 07. FECHA DE VENCIMIENTO
            if (item.FechaVencimiento.HasValue)
            {
                invoiceType.DueDate = new sunat.comprobante.invoice.DueDateType();
                invoiceType.DueDate.Value = item.FechaVencimiento.Value;
            }

            invoiceType.ProfileID = new sunat.comprobante.invoice.ProfileIDType();
            invoiceType.ProfileID.Value = item.TipoOperacionVenta.CodigoSunat;
            //invoiceType.ProfileID.schemeName = "SUNAT:Identificador de Tipo de Operación";
            //invoiceType.ProfileID.schemeAgencyName = "PE:SUNAT";
            //invoiceType.ProfileID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo17";
            #endregion

            #region Firma Digital
            // 08. FIRMA DIGITAL
            invoiceType.UBLExtensions = new sunat.comprobante.invoice.UBLExtensionType[1];
            invoiceType.UBLExtensions[0] = new sunat.comprobante.invoice.UBLExtensionType();
            invoiceType.UBLExtensions[0].ExtensionContent = new System.Xml.XmlDocument().CreateElement("ext:firma", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            invoiceType.Signature = new sunat.comprobante.invoice.SignatureType[1];
            invoiceType.Signature[0] = new sunat.comprobante.invoice.SignatureType();
            invoiceType.Signature[0].ID = new sunat.comprobante.invoice.IDType();
            invoiceType.Signature[0].ID.Value = "IDSignJAREV";

            invoiceType.Signature[0].SignatoryParty = new sunat.comprobante.invoice.PartyType();
            invoiceType.Signature[0].SignatoryParty.PartyIdentification = new sunat.comprobante.invoice.PartyIdentificationType[1];
            invoiceType.Signature[0].SignatoryParty.PartyIdentification[0] = new sunat.comprobante.invoice.PartyIdentificationType();
            invoiceType.Signature[0].SignatoryParty.PartyIdentification[0].ID = new sunat.comprobante.invoice.IDType();
            invoiceType.Signature[0].SignatoryParty.PartyIdentification[0].ID.Value = item.Empresa.Ruc;
            invoiceType.Signature[0].SignatoryParty.PartyName = new sunat.comprobante.invoice.PartyNameType[1];
            invoiceType.Signature[0].SignatoryParty.PartyName[0] = new sunat.comprobante.invoice.PartyNameType();
            invoiceType.Signature[0].SignatoryParty.PartyName[0].Name = new sunat.comprobante.invoice.NameType1();
            invoiceType.Signature[0].SignatoryParty.PartyName[0].Name.Value = item.Empresa.RazonSocial;

            invoiceType.Signature[0].DigitalSignatureAttachment = new sunat.comprobante.invoice.AttachmentType();
            invoiceType.Signature[0].DigitalSignatureAttachment.ExternalReference = new sunat.comprobante.invoice.ExternalReferenceType();
            invoiceType.Signature[0].DigitalSignatureAttachment.ExternalReference.URI = new sunat.comprobante.invoice.URIType();
            invoiceType.Signature[0].DigitalSignatureAttachment.ExternalReference.URI.Value = "#SignatureJAREV";
            #endregion

            #region Datos del Emisor
            // 09. NÚMERO DE RUC
            invoiceType.AccountingSupplierParty = new sunat.comprobante.invoice.SupplierPartyType();
            invoiceType.AccountingSupplierParty.Party = new sunat.comprobante.invoice.PartyType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification = new sunat.comprobante.invoice.PartyIdentificationType[1];
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0] = new sunat.comprobante.invoice.PartyIdentificationType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID = new sunat.comprobante.invoice.IDType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.Value = item.Empresa.Ruc;
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeID = TipoDocumentoIdentidad.RUC.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeName = "Documento de Identidad";
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeAgencyName = "PE:SUNAT";
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";

            // 10. NOMBRE COMERCIAL
            invoiceType.AccountingSupplierParty.Party.PartyName = new sunat.comprobante.invoice.PartyNameType[1];
            invoiceType.AccountingSupplierParty.Party.PartyName[0] = new sunat.comprobante.invoice.PartyNameType();
            invoiceType.AccountingSupplierParty.Party.PartyName[0].Name = new sunat.comprobante.invoice.NameType1();
            invoiceType.AccountingSupplierParty.Party.PartyName[0].Name.Value = item.Empresa.NombreComercial;

            // 11. RAZON SOCIAL O NOMBRES
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity = new sunat.comprobante.invoice.PartyLegalEntityType[1];
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0] = new sunat.comprobante.invoice.PartyLegalEntityType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationName = new sunat.comprobante.invoice.RegistrationNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationName.Value = item.Empresa.RazonSocial;

            // 12. DOMICILIO FISCAL
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress = new sunat.comprobante.invoice.AddressType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine = new sunat.comprobante.invoice.AddressLineType[1];
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0] = new sunat.comprobante.invoice.AddressLineType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0].Line = new sunat.comprobante.invoice.LineType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0].Line.Value = item.Empresa.Direccion;

            // 12. URBANIZACIÓN
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CitySubdivisionName = new sunat.comprobante.invoice.CitySubdivisionNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CitySubdivisionName.Value = "";

            // 12. PROVINCIA
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CityName = new sunat.comprobante.invoice.CityNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CityName.Value = item.Empresa.Distrito.Provincia.Nombre;

            // 12. UBIGEO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID = new sunat.comprobante.invoice.IDType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.Value = item.Empresa.Distrito.CodigoUbigeo;
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.schemeAgencyName = "PE:INEI";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.schemeName = "Ubigeos";

            // 12. DEPARTAMENTO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CountrySubentity = new sunat.comprobante.invoice.CountrySubentityType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CountrySubentity.Value = item.Empresa.Distrito.Provincia.Departamento.Nombre;

            // 12. DISTRITO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.District = new sunat.comprobante.invoice.DistrictType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.District.Value = item.Empresa.Distrito.Nombre;

            // 12. PAIS
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country = new sunat.comprobante.invoice.CountryType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode = new sunat.comprobante.invoice.IdentificationCodeType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.Value = item.Empresa.Distrito.Provincia.Departamento.Pais.CodigoSunat;
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listID = "ISO 3166-1";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listAgencyName = "United Nations Economic Commission for Europe";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listName = "Country";

            // 13. DIRECCIÓN DE DELIVERY POR IMPLEMENTAR

            // 14. LOCAL DE DESPACHO POR DELIVERY POR IMPLEMENTAR
            #endregion

            #region Datos del Cliente
            // 15. TIPO Y NÚMERO DOCUMENTO IDENTIDAD
            invoiceType.AccountingCustomerParty = new sunat.comprobante.invoice.CustomerPartyType();
            invoiceType.AccountingCustomerParty.Party = new sunat.comprobante.invoice.PartyType();
            invoiceType.AccountingCustomerParty.Party.PartyIdentification = new sunat.comprobante.invoice.PartyIdentificationType[1];
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0] = new sunat.comprobante.invoice.PartyIdentificationType();
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID = new sunat.comprobante.invoice.IDType();
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID.Value = item.Cliente.NroDocumentoIdentidad;
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeID = item.Cliente.TipoDocumentoIdentidad.Codigo;
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeName = "Documento de Identidad";
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeAgencyName = "PE:SUNAT";
            invoiceType.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";

            // 16. RAZÓN SOCIAL O NOMBRES
            invoiceType.AccountingCustomerParty.Party.PartyLegalEntity = new sunat.comprobante.invoice.PartyLegalEntityType[1];
            invoiceType.AccountingCustomerParty.Party.PartyLegalEntity[0] = new sunat.comprobante.invoice.PartyLegalEntityType();
            invoiceType.AccountingCustomerParty.Party.PartyLegalEntity[0].RegistrationName = new sunat.comprobante.invoice.RegistrationNameType();
            invoiceType.AccountingCustomerParty.Party.PartyLegalEntity[0].RegistrationName.Value = item.Cliente.RazonSocial;
            #endregion

            #region Documentos de Referencia
            // 17. DOCUMENTO DE REFERENCIA - GUIA DE REMISIÓN
            if (item.ListaFacturaGuiaRemision != null)
            {
                int tamañoListaGuiaRemision = item.ListaFacturaGuiaRemision.Count;
                invoiceType.DespatchDocumentReference = new sunat.comprobante.invoice.DocumentReferenceType[tamañoListaGuiaRemision];
                for (int i = 0; i < tamañoListaGuiaRemision; i++)
                {
                    FacturaGuiaRemisionBe itemFacturaGuiaRemision = item.ListaFacturaGuiaRemision[i];
                    invoiceType.DespatchDocumentReference[i] = new sunat.comprobante.invoice.DocumentReferenceType();
                    invoiceType.DespatchDocumentReference[i].ID = new sunat.comprobante.invoice.IDType();
                    invoiceType.DespatchDocumentReference[i].ID.Value = itemFacturaGuiaRemision.SerieNroComprobante;

                    invoiceType.DespatchDocumentReference[i].DocumentTypeCode = new sunat.comprobante.invoice.DocumentTypeCodeType();
                    invoiceType.DespatchDocumentReference[i].DocumentTypeCode.Value = itemFacturaGuiaRemision.TipoComprobante.Codigo;
                    invoiceType.DespatchDocumentReference[i].DocumentTypeCode.listAgencyName = "PE:SUNAT";
                    invoiceType.DespatchDocumentReference[i].DocumentTypeCode.listName = "Tipo de Documento";
                    invoiceType.DespatchDocumentReference[i].DocumentTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";
                }
            }

            // 18. DOCUMENTO DE REFERENCIA - DOCUMENTO RELACIONADO
            if (item.ListaFacturaDocumento != null)
            {
                int tamañoListaDocumento = item.ListaFacturaDocumento.Count;
                invoiceType.AdditionalDocumentReference = new sunat.comprobante.invoice.DocumentReferenceType[tamañoListaDocumento];
                for (int i = 0; i < tamañoListaDocumento; i++)
                {
                    FacturaDocumentoBe itemFacturaDocumento = item.ListaFacturaDocumento[i];
                    invoiceType.AdditionalDocumentReference[i] = new sunat.comprobante.invoice.DocumentReferenceType();
                    invoiceType.AdditionalDocumentReference[i].ID = new sunat.comprobante.invoice.IDType();
                    invoiceType.AdditionalDocumentReference[i].ID.Value = itemFacturaDocumento.SerieNroComprobante;
                    invoiceType.AdditionalDocumentReference[i].DocumentTypeCode = new sunat.comprobante.invoice.DocumentTypeCodeType();
                    invoiceType.AdditionalDocumentReference[i].DocumentTypeCode.Value = itemFacturaDocumento.TipoComprobante.Codigo;
                    invoiceType.AdditionalDocumentReference[i].DocumentTypeCode.listAgencyName = "PE:SUNAT";
                    invoiceType.AdditionalDocumentReference[i].DocumentTypeCode.listName = "Documento relacionado";
                    invoiceType.AdditionalDocumentReference[i].DocumentTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo12";
                }
            }
            #endregion

            #region Datos del Detalle
            if (item.ListaFacturaDetalle != null)
            {
                int tamañoListaDetalle = item.ListaFacturaDetalle.Count;
                invoiceType.InvoiceLine = new sunat.comprobante.invoice.InvoiceLineType[tamañoListaDetalle];
                for (int i = 0; i < tamañoListaDetalle; i++)
                {
                    FacturaDetalleBe itemFacturaDetalle = item.ListaFacturaDetalle[i];

                    decimal totalImpuestosDetalle = Math.Round(itemFacturaDetalle.IGV + itemFacturaDetalle.ISC + itemFacturaDetalle.OTH, cantidadDecimales);
                    int cantidadImpuestosDetalle = 1;
                    //if (itemFacturaDetalle.IGV > 0) cantidadImpuestos++;
                    if (itemFacturaDetalle.ISC > 0) cantidadImpuestosDetalle++;
                    if (itemFacturaDetalle.OTH > 0) cantidadImpuestosDetalle++;
                    int cantidadCargosDetalle = 0;
                    if (itemFacturaDetalle.Descuento > 0) cantidadCargosDetalle++;
                    if (itemFacturaDetalle.OtrosCargos > 0) cantidadCargosDetalle++;

                    // 19. NÚMERO DE ORDEN
                    invoiceType.InvoiceLine[i] = new sunat.comprobante.invoice.InvoiceLineType();
                    invoiceType.InvoiceLine[i].ID = new sunat.comprobante.invoice.IDType();
                    invoiceType.InvoiceLine[i].ID.Value = (i + 1).ToString();

                    // 20. UNIDAD DE MEDIDA
                    invoiceType.InvoiceLine[i].InvoicedQuantity = new sunat.comprobante.invoice.InvoicedQuantityType();
                    invoiceType.InvoiceLine[i].InvoicedQuantity.unitCode = itemFacturaDetalle.UnidadMedida.Id;
                    invoiceType.InvoiceLine[i].InvoicedQuantity.unitCodeListID = "UN/ECE rec 20";
                    invoiceType.InvoiceLine[i].InvoicedQuantity.unitCodeListAgencyName = "United Nations Economic Commission for Europe";

                    // 21. CANTIDAD
                    invoiceType.InvoiceLine[i].InvoicedQuantity.Value = Math.Round(itemFacturaDetalle.Cantidad, cantidadDecimales);

                    // 22. CÓDIGO
                    invoiceType.InvoiceLine[i].Item = new sunat.comprobante.invoice.ItemType();
                    invoiceType.InvoiceLine[i].Item.SellersItemIdentification = new sunat.comprobante.invoice.ItemIdentificationType();
                    invoiceType.InvoiceLine[i].Item.SellersItemIdentification.ID = new sunat.comprobante.invoice.IDType();
                    invoiceType.InvoiceLine[i].Item.SellersItemIdentification.ID.Value = itemFacturaDetalle.Codigo;

                    // 23. CÓDIGO PRODUCTO
                    invoiceType.InvoiceLine[i].Item.CommodityClassification = new sunat.comprobante.invoice.CommodityClassificationType[1];
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0] = new sunat.comprobante.invoice.CommodityClassificationType();
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0].ItemClassificationCode = new sunat.comprobante.invoice.ItemClassificationCodeType();
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0].ItemClassificationCode.Value = itemFacturaDetalle.CodigoSunat;
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0].ItemClassificationCode.listID = "UNSPSC";
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0].ItemClassificationCode.listAgencyName = "GS1 US";
                    invoiceType.InvoiceLine[i].Item.CommodityClassification[0].ItemClassificationCode.listName = "Item Classification";

                    // 24. CÓDIGO PRODUCTO GS1 POR IMPLEMENTAR

                    // 25. NÚMERO DE PLACA DE VEHÍCULO POR IMPLEMENTAR

                    // 26. DESCRIPCIÓN
                    invoiceType.InvoiceLine[i].Item.Description = new sunat.comprobante.invoice.DescriptionType[1];
                    invoiceType.InvoiceLine[i].Item.Description[0] = new sunat.comprobante.invoice.DescriptionType();
                    invoiceType.InvoiceLine[i].Item.Description[0].Value = itemFacturaDetalle.Descripcion;

                    // 27. VALOR UNITARIO
                    invoiceType.InvoiceLine[i].Price = new sunat.comprobante.invoice.PriceType();
                    invoiceType.InvoiceLine[i].Price.PriceAmount = new sunat.comprobante.invoice.PriceAmountType();
                    invoiceType.InvoiceLine[i].Price.PriceAmount.Value = Math.Round(itemFacturaDetalle.ValorUnitario, cantidadDecimales);
                    invoiceType.InvoiceLine[i].Price.PriceAmount.currencyID = item.Moneda.Codigo;

                    // 28. PRECIO UNITARIO - 29 VALOR REFERENCIAL
                    invoiceType.InvoiceLine[i].PricingReference = new sunat.comprobante.invoice.PricingReferenceType();
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice = new sunat.comprobante.invoice.PriceType[1];
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0] = new sunat.comprobante.invoice.PriceType();
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceAmount = new sunat.comprobante.invoice.PriceAmountType();
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceAmount.Value = Math.Round(itemFacturaDetalle.PrecioUnitario, 2);
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceAmount.currencyID = item.Moneda.Codigo;

                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceTypeCode = new sunat.comprobante.invoice.PriceTypeCodeType();
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceTypeCode.Value = !itemFacturaDetalle.TipoAfectacionIgv.FlagGratuito ? "01" : "02";
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceTypeCode.listName = "Tipo de Precio";
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceTypeCode.listAgencyName = "PE:SUNAT";
                    invoiceType.InvoiceLine[i].PricingReference.AlternativeConditionPrice[0].PriceTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";

                    // 30. TOTAL IMPUESTOS
                    invoiceType.InvoiceLine[i].TaxTotal = new sunat.comprobante.invoice.TaxTotalType[1];
                    invoiceType.InvoiceLine[i].TaxTotal[0] = new sunat.comprobante.invoice.TaxTotalType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxAmount.Value = totalImpuestosDetalle;
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxAmount.currencyID = item.Moneda.Codigo;

                    // 31. IMPUESTOS
                    int iImpuestoDetalle = 0;
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal = new sunat.comprobante.invoice.TaxSubtotalType[cantidadImpuestosDetalle];

                    // 31. IMPUESTOS - IGV
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle] = new sunat.comprobante.invoice.TaxSubtotalType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.currencyID = item.Moneda.Codigo;

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.Value = Math.Round(itemFacturaDetalle.IGV, cantidadDecimales);
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.currencyID = item.Moneda.Codigo;

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent = new sunat.comprobante.invoice.PercentType1();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent.Value = Math.Round(itemFacturaDetalle.PorcentajeIGV, cantidadDecimales);

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxExemptionReasonCode = new sunat.comprobante.invoice.TaxExemptionReasonCodeType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxExemptionReasonCode.Value = itemFacturaDetalle.TipoAfectacionIgv.Id;
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxExemptionReasonCode.listAgencyName = "PE:SUNAT";
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxExemptionReasonCode.listName = "Afectacion del IGV";
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxExemptionReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.Value = itemFacturaDetalle.TipoTributoIGV.Codigo;
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name.Value = itemFacturaDetalle.TipoTributoIGV.Nombre;

                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                    invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode.Value = itemFacturaDetalle.TipoTributoIGV.CodigoNombre;

                    iImpuestoDetalle++;

                    // 31. IMPUESTOS - OTH
                    if (itemFacturaDetalle.OTH > 0)
                    {
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle] = new sunat.comprobante.invoice.TaxSubtotalType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.currencyID = item.Moneda.Codigo;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.Value = Math.Round(itemFacturaDetalle.OTH, cantidadDecimales);
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.currencyID = item.Moneda.Codigo;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent = new sunat.comprobante.invoice.PercentType1();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent.Value = Math.Round(itemFacturaDetalle.PorcentajeOTH, cantidadDecimales);

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.Value = itemFacturaDetalle.TipoTributoOTH.Codigo;
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name.Value = itemFacturaDetalle.TipoTributoOTH.Nombre;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode.Value = itemFacturaDetalle.TipoTributoOTH.CodigoNombre;

                        iImpuestoDetalle++;
                    }

                    // 32. IMPUESTOS - ISC
                    if (itemFacturaDetalle.ISC > 0)
                    {
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle] = new sunat.comprobante.invoice.TaxSubtotalType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxableAmount.currencyID = item.Moneda.Codigo;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.Value = Math.Round(itemFacturaDetalle.ISC, cantidadDecimales);
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxAmount.currencyID = item.Moneda.Codigo;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent = new sunat.comprobante.invoice.PercentType1();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.Percent.Value = Math.Round(itemFacturaDetalle.PorcentajeISC, cantidadDecimales);

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.Value = itemFacturaDetalle.TipoTributoISC.Codigo;
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.Name.Value = itemFacturaDetalle.TipoTributoISC.Nombre;

                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                        invoiceType.InvoiceLine[i].TaxTotal[0].TaxSubtotal[iImpuestoDetalle].TaxCategory.TaxScheme.TaxTypeCode.Value = itemFacturaDetalle.TipoTributoISC.CodigoNombre;
                    }

                    // 33. VALOR VENTA
                    invoiceType.InvoiceLine[i].LineExtensionAmount = new sunat.comprobante.invoice.LineExtensionAmountType();
                    invoiceType.InvoiceLine[i].LineExtensionAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                    invoiceType.InvoiceLine[i].LineExtensionAmount.currencyID = item.Moneda.Codigo;

                    // 34. CARGO/DESCUENTO
                    int iCargoDetalle = 0;
                    invoiceType.InvoiceLine[i].AllowanceCharge = new sunat.comprobante.invoice.AllowanceChargeType[cantidadCargosDetalle];

                    // 34. CARGO
                    if (itemFacturaDetalle.OtrosCargos > 0)
                    {
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle] = new sunat.comprobante.invoice.AllowanceChargeType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].ChargeIndicator = new sunat.comprobante.invoice.ChargeIndicatorType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].ChargeIndicator.Value = true;
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode = new sunat.comprobante.invoice.AllowanceChargeReasonCodeType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.Value = "48";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listAgencyName = "PE:SUNAT";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listName = "Cargo";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo53";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].MultiplierFactorNumeric = new sunat.comprobante.invoice.MultiplierFactorNumericType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].MultiplierFactorNumeric.Value = Math.Round(itemFacturaDetalle.PorcentajeOtrosCargos, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount = new sunat.comprobante.invoice.AmountType2();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount.Value = Math.Round(itemFacturaDetalle.OtrosCargos, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount.currencyID = item.Moneda.Codigo;
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount = new sunat.comprobante.invoice.BaseAmountType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount.currencyID = item.Moneda.Codigo;

                        iCargoDetalle++;
                    }

                    // 34. DESCUENTO
                    if (itemFacturaDetalle.Descuento > 0)
                    {
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle] = new sunat.comprobante.invoice.AllowanceChargeType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].ChargeIndicator = new sunat.comprobante.invoice.ChargeIndicatorType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].ChargeIndicator.Value = false;
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode = new sunat.comprobante.invoice.AllowanceChargeReasonCodeType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.Value = "01";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listAgencyName = "PE:SUNAT";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listName = "Descuento";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].AllowanceChargeReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo53";
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].MultiplierFactorNumeric = new sunat.comprobante.invoice.MultiplierFactorNumericType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].MultiplierFactorNumeric.Value = Math.Round(itemFacturaDetalle.PorcentajeDescuento, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount = new sunat.comprobante.invoice.AmountType2();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount.Value = Math.Round(itemFacturaDetalle.Descuento, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].Amount.currencyID = item.Moneda.Codigo;
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount = new sunat.comprobante.invoice.BaseAmountType();
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount.Value = Math.Round(itemFacturaDetalle.ValorVenta, cantidadDecimales);
                        invoiceType.InvoiceLine[i].AllowanceCharge[iCargoDetalle].BaseAmount.currencyID = item.Moneda.Codigo;
                    }
                }
            }
            #endregion

            #region Totales de la Factura
            // 35. TOTAL IMPUESTOS
            invoiceType.TaxTotal = new sunat.comprobante.invoice.TaxTotalType[1];
            invoiceType.TaxTotal[0] = new sunat.comprobante.invoice.TaxTotalType();
            invoiceType.TaxTotal[0].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
            invoiceType.TaxTotal[0].TaxAmount.Value = totalImpuestos;
            invoiceType.TaxTotal[0].TaxAmount.currencyID = item.Moneda.Codigo;

            // 36. TOTAL EXPORTACION
            int iImpuesto = 0;
            invoiceType.TaxTotal[0].TaxSubtotal = new sunat.comprobante.invoice.TaxSubtotalType[cantidadImpuestos];
            if (item.TotalExportacion > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalExportacion, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(0M, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoExportacion.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoExportacion.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoExportacion.CodigoNombre;

                iImpuesto++;
            }

            // 37. TOTAL INAFECTO
            if (item.TotalInafecto > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalInafecto, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(0M, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoInafecto.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoInafecto.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoInafecto.CodigoNombre;

                iImpuesto++;
            }

            // 38. TOTAL EXONERADO
            if (item.TotalExonerado > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalExonerado, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(0M, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoExonerado.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoExonerado.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoExonerado.CodigoNombre;

                iImpuesto++;
            }

            // 39. TOTAL GRATUITO
            if (item.TotalGratuito > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalGratuito, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(0M, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoGratuito.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoGratuito.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoGratuito.CodigoNombre;

                iImpuesto++;
            }

            // 40-41. TOTAL IGV
            if (item.TotalIgv > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalGravado, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(item.TotalIgv, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoIgv.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoIgv.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoIgv.CodigoNombre;

                iImpuesto++;
            }

            // 42. TOTAL ISC
            if (item.TotalIsc > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalBaseImponible, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(item.TotalIsc, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoIsc.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoIsc.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoIsc.CodigoNombre;

                iImpuesto++;
            }

            // 43. TOTAL OTROS TRIBUTOS
            if (item.TotalOtrosTributos > 0)
            {
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto] = new sunat.comprobante.invoice.TaxSubtotalType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount = new sunat.comprobante.invoice.TaxableAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.Value = Math.Round(item.TotalBaseImponible, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxableAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount = new sunat.comprobante.invoice.TaxAmountType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.Value = Math.Round(item.TotalOtrosTributos, cantidadDecimales);
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxAmount.currencyID = item.Moneda.Codigo;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory = new sunat.comprobante.invoice.TaxCategoryType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme = new sunat.comprobante.invoice.TaxSchemeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID = new sunat.comprobante.invoice.IDType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.Value = item.TipoTributoOtrosTributos.Codigo;
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeName = "Codigo de tributos";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeAgencyName = "PE:SUNAT";
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name = new sunat.comprobante.invoice.NameType1();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.Name.Value = item.TipoTributoOtrosTributos.Nombre;

                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode = new sunat.comprobante.invoice.TaxTypeCodeType();
                invoiceType.TaxTotal[0].TaxSubtotal[iImpuesto].TaxCategory.TaxScheme.TaxTypeCode.Value = item.TipoTributoOtrosTributos.CodigoNombre;

                iImpuesto++;
            }

            // 44. TOTAL CARGOS Y DESCUENTOS GLOBALES
            int iCargo = 0;
            invoiceType.AllowanceCharge = new sunat.comprobante.invoice.AllowanceChargeType[cantidadCargos];

            // 44. TOTAL CARGOS GLOBALES
            if (item.TotalOtrosCargosGlobal > 0)
            {
                invoiceType.AllowanceCharge[iCargo] = new sunat.comprobante.invoice.AllowanceChargeType();
                invoiceType.AllowanceCharge[iCargo].ChargeIndicator = new sunat.comprobante.invoice.ChargeIndicatorType();
                invoiceType.AllowanceCharge[iCargo].ChargeIndicator.Value = true;
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode = new sunat.comprobante.invoice.AllowanceChargeReasonCodeType();
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.Value = "50";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listAgencyName = "PE:SUNAT";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listName = "Cargo";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo53";
                invoiceType.AllowanceCharge[iCargo].MultiplierFactorNumeric = new sunat.comprobante.invoice.MultiplierFactorNumericType();
                invoiceType.AllowanceCharge[iCargo].MultiplierFactorNumeric.Value = Math.Round(item.PorcentajeOtrosCargosGlobal, cantidadDecimales);
                invoiceType.AllowanceCharge[iCargo].Amount = new sunat.comprobante.invoice.AmountType2();
                invoiceType.AllowanceCharge[iCargo].Amount.Value = Math.Round(item.TotalOtrosCargosGlobal, cantidadDecimales);
                invoiceType.AllowanceCharge[iCargo].Amount.currencyID = item.Moneda.Codigo;
                invoiceType.AllowanceCharge[iCargo].BaseAmount = new sunat.comprobante.invoice.BaseAmountType();
                invoiceType.AllowanceCharge[iCargo].BaseAmount.Value = Math.Round(item.TotalBaseImponible);
                invoiceType.AllowanceCharge[iCargo].BaseAmount.currencyID = item.Moneda.Codigo;

                iCargo++;
            }

            // 44. TOTAL DESCUENTOS GLOBALES
            if (item.TotalDescuentosGlobal > 0)
            {
                invoiceType.AllowanceCharge[iCargo] = new sunat.comprobante.invoice.AllowanceChargeType();
                invoiceType.AllowanceCharge[iCargo].ChargeIndicator = new sunat.comprobante.invoice.ChargeIndicatorType();
                invoiceType.AllowanceCharge[iCargo].ChargeIndicator.Value = false;
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode = new sunat.comprobante.invoice.AllowanceChargeReasonCodeType();
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.Value = "03";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listAgencyName = "PE:SUNAT";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listName = "Cargo";
                invoiceType.AllowanceCharge[iCargo].AllowanceChargeReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo53";
                invoiceType.AllowanceCharge[iCargo].MultiplierFactorNumeric = new sunat.comprobante.invoice.MultiplierFactorNumericType();
                invoiceType.AllowanceCharge[iCargo].MultiplierFactorNumeric.Value = Math.Round(item.PorcentajeDescuentosGlobal, cantidadDecimales);
                invoiceType.AllowanceCharge[iCargo].Amount = new sunat.comprobante.invoice.AmountType2();
                invoiceType.AllowanceCharge[iCargo].Amount.Value = Math.Round(item.TotalDescuentosGlobal, cantidadDecimales);
                invoiceType.AllowanceCharge[iCargo].Amount.currencyID = item.Moneda.Codigo;
                invoiceType.AllowanceCharge[iCargo].BaseAmount = new sunat.comprobante.invoice.BaseAmountType();
                invoiceType.AllowanceCharge[iCargo].BaseAmount.Value = Math.Round(item.TotalBaseImponible);
                invoiceType.AllowanceCharge[iCargo].BaseAmount.currencyID = item.Moneda.Codigo;
            }

            // 45. TOTAL DESCUENTOS
            invoiceType.LegalMonetaryTotal = new sunat.comprobante.invoice.MonetaryTotalType();
            invoiceType.LegalMonetaryTotal.AllowanceTotalAmount = new sunat.comprobante.invoice.AllowanceTotalAmountType();
            invoiceType.LegalMonetaryTotal.AllowanceTotalAmount.Value = Math.Round(item.TotalDescuentos, cantidadDecimales);
            invoiceType.LegalMonetaryTotal.AllowanceTotalAmount.currencyID = item.Moneda.Codigo;

            // 46. TOTAL CARGOS
            invoiceType.LegalMonetaryTotal.ChargeTotalAmount = new sunat.comprobante.invoice.ChargeTotalAmountType();
            invoiceType.LegalMonetaryTotal.ChargeTotalAmount.Value = Math.Round(item.TotalOtrosCargos, cantidadDecimales);
            invoiceType.LegalMonetaryTotal.ChargeTotalAmount.currencyID = item.Moneda.Codigo;

            // 47. TOTAL IMPORTE
            invoiceType.LegalMonetaryTotal.PayableAmount = new sunat.comprobante.invoice.PayableAmountType();
            invoiceType.LegalMonetaryTotal.PayableAmount.Value = Math.Round(item.ImporteTotal, cantidadDecimales);
            invoiceType.LegalMonetaryTotal.PayableAmount.currencyID = item.Moneda.Codigo;

            // 48. TOTAL VALOR VENTA
            invoiceType.LegalMonetaryTotal.LineExtensionAmount = new sunat.comprobante.invoice.LineExtensionAmountType();
            invoiceType.LegalMonetaryTotal.LineExtensionAmount.Value = Math.Round(item.TotalBaseImponible, cantidadDecimales);
            invoiceType.LegalMonetaryTotal.LineExtensionAmount.currencyID = item.Moneda.Codigo;

            // 49. TOTAL PRECIO VENTA
            invoiceType.LegalMonetaryTotal.TaxInclusiveAmount = new sunat.comprobante.invoice.TaxInclusiveAmountType();
            invoiceType.LegalMonetaryTotal.TaxInclusiveAmount.Value = Math.Round(item.ImporteTotal, cantidadDecimales);
            invoiceType.LegalMonetaryTotal.TaxInclusiveAmount.currencyID = item.Moneda.Codigo;
            #endregion

            return invoiceType;
        }


        public static DebitNoteType ObtenerComprobante(NotaDebitoBe item)
        {
            DebitNoteType debitNoteType = new DebitNoteType();

            return debitNoteType;
        }

        public static CreditNoteType ObtenerComprobante(NotaCreditoBe item)
        {
            CreditNoteType creditNoteType = new CreditNoteType();

            return creditNoteType;
        }
    }
}