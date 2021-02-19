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
        

        public InvoiceType ObtenerComprobante(FacturaBe item, VersionUBL version)
        {
            InvoiceType invoiceType = null;

            switch (version)
            {
                case VersionUBL._2_1: invoiceType = ObtenerComprobante_2_1(item, version);
                    break;
            }

            return invoiceType;
        }

        public InvoiceType ObtenerComprobante_2_1(FacturaBe item, EmpresaBe empresa, VersionUBL version)
        {
            InvoiceType invoiceType = new InvoiceType();

            string versionValue = version.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            string versionCustom = version.GetAttributeOfType<CategoryAttribute>().Category;

            #region Datos de la Factura electrónica
            // UBL
            invoiceType.UBLVersionID = new sunat.comprobante.invoice.UBLVersionIDType();
            invoiceType.UBLVersionID.Value = versionValue;

            // CUSTOMIZATIONID
            invoiceType.CustomizationID = new sunat.comprobante.invoice.CustomizationIDType();
            invoiceType.CustomizationID.Value = versionCustom;
            invoiceType.CustomizationID.schemeAgencyName = "PE:SUNAT";

            // FECHA DE EMISION
            invoiceType.IssueDate = new sunat.comprobante.invoice.IssueDateType();
            invoiceType.IssueDate.Value = item.FechaHoraEmision;

            // HORA DE EMISION
            invoiceType.IssueTime = new sunat.comprobante.invoice.IssueTimeType();
            invoiceType.IssueTime.Value = item.FechaHoraEmision;

            // TIPO COMPROBANTE
            invoiceType.InvoiceTypeCode = new sunat.comprobante.invoice.InvoiceTypeCodeType();
            invoiceType.InvoiceTypeCode.Value = TipoComprobante.Factura.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            invoiceType.InvoiceTypeCode.listAgencyName = "PE:SUNAT";
            invoiceType.InvoiceTypeCode.listName = "Tipo de Documento";
            invoiceType.InvoiceTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";

            // TIPO MONEDA
            invoiceType.DocumentCurrencyCode = new sunat.comprobante.invoice.DocumentCurrencyCodeType();
            invoiceType.DocumentCurrencyCode.Value = item.Moneda.Codigo;
            invoiceType.DocumentCurrencyCode.listID = "ISO 4217 Alpha";
            invoiceType.DocumentCurrencyCode.listName = "Currency";
            invoiceType.DocumentCurrencyCode.listAgencyName = "United Nations Economic Commission for Europe";

            // FECHA DE VENCIMIENTO
            if (item.FechaVencimiento.HasValue)
            {
                invoiceType.DueDate = new sunat.comprobante.invoice.DueDateType();
                invoiceType.DueDate.Value = item.FechaVencimiento.Value;
            }
            #endregion

            #region Datos del Emisor
            // NÚMERO DE RUC
            invoiceType.AccountingSupplierParty = new sunat.comprobante.invoice.SupplierPartyType();
            invoiceType.AccountingSupplierParty.Party = new sunat.comprobante.invoice.PartyType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification = new sunat.comprobante.invoice.PartyIdentificationType[1];
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0] = new sunat.comprobante.invoice.PartyIdentificationType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID = new sunat.comprobante.invoice.IDType();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.Value = empresa.Ruc;
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeID = TipoDocumentoIdentidad.RUC.GetAttributeOfType<DefaultValueAttribute>().Value.ToString();
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeName = "Documento de Identidad";
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeAgencyName = "PE:SUNAT";
            invoiceType.AccountingSupplierParty.Party.PartyIdentification[0].ID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";

            // NOMBRE COMERCIAL
            invoiceType.AccountingSupplierParty.Party.PartyName = new sunat.comprobante.invoice.PartyNameType[1];
            invoiceType.AccountingSupplierParty.Party.PartyName[0] = new sunat.comprobante.invoice.PartyNameType();
            invoiceType.AccountingSupplierParty.Party.PartyName[0].Name = new sunat.comprobante.invoice.NameType1();
            invoiceType.AccountingSupplierParty.Party.PartyName[0].Name.Value = empresa.NombreComercial;

            // RAZON SOCIAL O NOMBRES
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity = new sunat.comprobante.invoice.PartyLegalEntityType[1];
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0] = new sunat.comprobante.invoice.PartyLegalEntityType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationName = new sunat.comprobante.invoice.RegistrationNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationName.Value = empresa.RazonSocial;

            //DOMICILIO FISCAL
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress = new sunat.comprobante.invoice.AddressType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine = new sunat.comprobante.invoice.AddressLineType[1];
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0] = new sunat.comprobante.invoice.AddressLineType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0].Line = new sunat.comprobante.invoice.LineType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.AddressLine[0].Line.Value = empresa.Direccion;

            // URBANIZACIÓN
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CitySubdivisionName = new sunat.comprobante.invoice.CitySubdivisionNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CitySubdivisionName.Value = "";

            //PROVINCIA
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CityName = new sunat.comprobante.invoice.CityNameType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CityName.Value = empresa.Distrito.Provincia.Nombre;

            // UBIGEO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID = new sunat.comprobante.invoice.IDType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.Value = empresa.Distrito.CodigoUbigeo;
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.schemeAgencyName = "PE:INEI";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.ID.schemeName = "Ubigeos";

            // DEPARTAMENTO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CountrySubentity = new sunat.comprobante.invoice.CountrySubentityType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.CountrySubentity.Value = empresa.Distrito.Provincia.Departamento.Nombre;

            // DISTRITO
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.District = new sunat.comprobante.invoice.DistrictType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.District.Value = empresa.Distrito.Nombre;

            // PAIS
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country = new sunat.comprobante.invoice.CountryType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode = new sunat.comprobante.invoice.IdentificationCodeType();
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.Value = empresa.Distrito.Provincia.Departamento.Pais.CodigoSunat;
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listID = "ISO 3166-1";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listAgencyName = "United Nations Economic Commission for Europe";
            invoiceType.AccountingSupplierParty.Party.PartyLegalEntity[0].RegistrationAddress.Country.IdentificationCode.listName = "Country";

            #endregion

            return invoiceType;
        }


        public DebitNoteType ObtenerComprobante(NotaDebitoBe item)
        {
            DebitNoteType debitNoteType = new DebitNoteType();

            return debitNoteType;
        }

        public CreditNoteType ObtenerComprobante(NotaCreditoBe item)
        {
            CreditNoteType creditNoteType = new CreditNoteType();

            return creditNoteType;
        }
    }
}