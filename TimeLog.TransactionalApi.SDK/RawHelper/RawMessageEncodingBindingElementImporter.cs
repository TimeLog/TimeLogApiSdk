using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace TimeLog.TransactionalAPI.SDK.RawHelper
{
    /// <summary>
    ///     The raw message encoding binding element importer.
    /// </summary>
    public class RawMessageEncodingBindingElementImporter : IPolicyImportExtension
    {
        /// <summary>
        ///     The import policy.
        /// </summary>
        /// <param name="importer">The importer.</param>
        /// <param name="context">The context.</param>
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            if (importer == null)
            {
                throw new ArgumentNullException(nameof(importer));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ICollection<XmlElement> assertions = context.GetBindingAssertions();
            foreach (var assertion in assertions)
            {
                if (assertion.NamespaceURI == RawMessageEncodingPolicyConstants.RawEncodingNamespace &&
                    assertion.LocalName == RawMessageEncodingPolicyConstants.RawEncodingName)
                {
                    assertions.Remove(assertion);
                    context.BindingElements.Add(new RawMessageEncodingBindingElement());
                    break;
                }
            }
        }
    }
}