using System;
using System.Collections.Generic;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class PaymentTermReadModel
    {
        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        public int PaymentMethodID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is paid by employee.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is paid by employee; otherwise, <c>false</c>.
        /// </value>
        public bool IsPaidByEmployee { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default for employee expense.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is default for employee expense; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultForEmployeeExpense { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default for project expense.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is default for project expense; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultForProjectExpense { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default for import.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is default for import; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultForImport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is personal credit card.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is personal credit card; otherwise, <c>false</c>.
        /// </value>
        public bool IsPersonalCreditCard { get; set; }

        /// <summary>
        /// Gets or sets the credit card employee user identifier.
        /// </summary>
        /// <value>
        /// The credit card employee user identifier.
        /// </value>
        public int? CreditCardEmployeeUserID { get; set; }
    }
}