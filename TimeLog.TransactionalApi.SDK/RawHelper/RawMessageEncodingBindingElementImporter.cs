namespace TimeLog.TransactionalApi.SDK.RawHelper
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Description;
    using System.Xml;

    /// <summary>
    /// The raw message encoding binding element importer.
    /// </summary>
    public class RawMessageEncodingBindingElementImporter : IPolicyImportExtension
    {
        /// <summary>
        /// The import policy.
        /// </summary>
        /// <param name="importer">The importer.</param>
        /// <param name="context">The context.</param>
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ICollection<XmlElement> assertions = context.GetBindingAssertions();
            foreach (XmlElement assertion in assertions)
            {
                if ((assertion.NamespaceURI == RawMessageEncodingPolicyConstants.RawEncodingNamespace) && (assertion.LocalName == RawMessageEncodingPolicyConstants.RawEncodingName))
                {
                    assertions.Remove(assertion);
                    context.BindingElements.Add(new RawMessageEncodingBindingElement());
                    break;
                }
            }
        }
    }
}
