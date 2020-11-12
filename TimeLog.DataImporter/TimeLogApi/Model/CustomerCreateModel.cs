using System;
using System.Collections.Generic;
using System.Text;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class CustomerCreateModel
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        /// /// <value>
        /// The Name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Currency ID
        /// </summary>
        /// <value>
        /// The Currency ID
        /// </value>
        public int CurrencyID { get; set; }

        /// <summary>
        /// Gets or sets the Customer status ID
        /// </summary>
        /// <value>
        /// The Customer status ID
        /// </value>
        public int CustomerStatusID { get; set; }


        //Optional

        /// <summary>
        /// Gets or sets the Customer no.
        /// </summary>
        /// <value>
        /// The Customer no.
        /// </value>
        public string CustomerNo { get; set; }

        /// <summary>
        /// Gets or sets the Nick name 
        /// </summary>
        /// <value>
        /// The Nick name
        /// </value>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the user name of the Primary KAM
        /// </summary>
        /// <value>
        /// The Primary KAM user name
        /// </value>
        public int? PrimaryKAMID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Secondary KAM 
        /// </summary>
        /// <value>
        /// The Secondary KAM ID
        /// </value>
        public int? SecondaryKAMID { get; set; }

        /// <summary>
        /// Gets or sets the date the customer started
        /// </summary>
        /// <value>
        /// The start date of the customer
        /// </value>
        public DateTime CustomerSince { get; set; }

        /// <summary>
        /// Gets or sets the Industry ID
        /// </summary>
        /// <value>
        /// The Industry ID
        /// </value>
        public int? IndustryID { get; set; }

        /// <summary>
        /// Gets or sets the phone number 
        /// </summary>
        /// <value>
        /// The phone number
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        /// <value>
        /// The fax number
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        /// <value>
        /// The email
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the customers website
        /// </summary>
        /// <value>
        /// The website address
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the customer address
        /// </summary>
        /// <value>
        /// The address
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the customer address
        /// </summary>
        /// <value>
        /// The address
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the customer address
        /// </summary>
        /// <value>
        /// The address
        /// </value>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the customer address zip code
        /// </summary>
        /// <value>
        /// The address zip code
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the customer address city
        /// </summary>
        /// <value>
        /// The address city
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the customer address state
        /// </summary>
        /// <value>
        /// The address state
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the customer address country 
        /// </summary>
        /// <value>
        /// The customer address country ID
        /// </value>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets if customer have separate invoicing address
        /// </summary>
        /// <value>
        /// <c>true</c> if using invoicing address; otherwise, <c>false</c>.
        /// </value>
        public bool UseInvoicingAddress { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address
        /// </summary>
        /// <value>
        /// The invoicing address
        /// </value>
        public string InvoicingAddress { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address
        /// </summary>
        /// <value>
        /// The invoicing address
        /// </value>
        public string InvoicingAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address
        /// </summary>
        /// <value>
        /// The invoicing address
        /// </value>
        public string InvoicingAddress3 { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address
        /// </summary>
        /// <value>
        /// The  invoicing address 
        /// </value>
        public string InvoicingAddressZipCode { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address zip code
        /// </summary>
        /// <value>
        /// The invoicing address zip code
        /// </value>
        public string InvoicingAddressCity { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address state
        /// </summary>
        /// <value>
        /// The invoicing address state
        /// </value>
        public string InvoicingAddressState { get; set; }

        /// <summary>
        /// Gets or sets the customer invoicing address country 
        /// </summary>
        /// <value>
        /// The customer invoicing address country ID
        /// </value>
        public int InvoicingAddressCountryID { get; set; }

        /// <summary>
        /// Gets or sets the VAT no
        /// </summary>
        /// <value>
        /// The VAT no
        /// </value>
        public string VatNo { get; set; }

        /// <summary>
        /// Gets or sets the organization no
        /// </summary>
        /// <value>
        /// The organization no
        /// </value>
        public string OrganizationNo { get; set; }

        /// <summary>
        /// Gets or sets the default mileage distance
        /// </summary>
        /// <value>
        /// The default mileage distance
        /// </value>
        public int DefaultMileageDistance { get; set; }

        /// <summary>
        /// Gets or sets if expenses are billable
        /// </summary>
        /// <value>
        /// <c>true</c> if expense is billable; otherwise, <c>false</c>.
        /// </value>
        public bool ExpenseIsBillable { get; set; }

        /// <summary>
        /// Gets or sets if mileage is billable
        /// </summary>
        /// <value>
        /// <c>true</c> if mileage is billable; otherwise, <c>false</c>.
        /// </value>
        public bool MileageIsBillable { get; set; }

        /// <summary>
        /// Gets or sets the default mileage distance is the max billable distance
        /// </summary>
        /// <value>
        /// <c>true</c> if the default mileage distance is the max billable distance; otherwise, <c>false</c>.
        /// </value>
        public bool DefaultDistIsMaxBillable { get; set; }



        // Invoice settings 

        /// <summary>
        /// Gets or sets the contact person
        /// </summary>
        /// <value>
        /// The ID of the contact person
        /// /// Additional options
        /// [-1]: Project's contact
        /// [0]: Select when invoicing
        /// [>0]: ID of existing customer contact person
        /// </value>
        public int ContactID { get; set; }

        /// <summary>
        /// Gets or sets the customer address or the customer contact address should be used for invoicing
        /// </summary>
        /// <value>
        ///  CustomersAddress = 1,
        /// ContactsAddress = 2,
        /// CustomerInvoicingAddress = 4,
        /// NotSpecified = 0
        /// </value>
        public int InvoiceAddressToUse { get; set; }

        /// <summary>
        /// Gets or sets the internal reference
        /// </summary>
        /// <value>
        /// The ID of the internal reference.
        /// Additional options
        /// [-3]: Project's account manager
        /// [-2]: Project's project manager
        /// [-1]: Customer's owner    
        /// [0]: Select when invoicing
        /// [>0]: The ID of the employee
        /// </value>
        public int InternalReferenceID { get; set; }

        /// <summary>
        /// Gets or sets the customer reference
        /// </summary>
        /// <value>
        /// Additional options
        /// [-1]: Project's contact
        /// [0]: Select when invoicing
        /// [>0]: ID of the customer contact
        /// </value>
        public int CustomerReferenceID { get; set; }

        /// <summary>
        /// Gets or sets the payment term ID
        /// </summary>
        /// <value>
        /// The ID of the payment term
        /// </value>
        public int PaymentTermID { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage
        /// </summary>
        /// <value>
        /// The discount percentage
        /// </value>
        public double DiscountPercentage { get; set; }

        /// <summary>
        /// Gets or sets if VAT should be calculated on the customer
        /// </summary>
        /// <value>
        /// <c>true</c> if VAT should be calculated; otherwise, <c>false</c>.
        /// </value>
        public bool CalculateVat { get; set; }

        /// <summary>
        /// Gets or sets the VAT percentage
        /// </summary>
        /// <value>
        /// The VAT percentage
        /// </value>
        public double VatPercentage { get; set; }

        /// <summary>
        /// Gets or sets if customer uses EAN
        /// </summary>
        /// <value>
        /// <c>true</c> if the customer uses EAN; otherwise, <c>false</c>.
        /// </value>
        public bool UseEanNo { get; set; }

        /// <summary>
        /// Gets or sets the EAN no.
        /// </summary>
        /// <value>
        /// The EAN no.
        /// </value>
        public string EanNo { get; set; }
    }
}