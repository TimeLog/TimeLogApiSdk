namespace TimeLog.TransactionalApi.SDK.RawHelper
{
    using System;
    using System.Configuration;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;

    /// <summary>
    /// This class is necessary to be able to plug in the Raw encoder binding element through a configuration file
    /// </summary>
    public class RawMessageEncodingElement : BindingElementExtensionElement
    {
        // Called by the WCF to discover the type of binding element this config section enables
        public override Type BindingElementType
        {
            get { return typeof(RawMessageEncodingBindingElement); }
        }

        /// <summary>
        /// Gets or sets the only property we need to configure for our binding element is the type of inner encoder to use. Here, we support text and binary.
        /// </summary>
        [ConfigurationProperty("innerMessageEncoding", DefaultValue = "textMessageEncoding")]
        public string InnerMessageEncoding
        {
            get { return (string)base["innerMessageEncoding"]; }
            set { base["innerMessageEncoding"] = value; }
        }

        // Called by the WCF to apply the configuration settings (the property above) to the binding element
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            RawMessageEncodingBindingElement binding = (RawMessageEncodingBindingElement)bindingElement;
            PropertyInformationCollection propertyInfo = this.ElementInformation.Properties;
            var propertyInformation = propertyInfo["innerMessageEncoding"];
            if (propertyInformation != null && propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
            {
                switch (this.InnerMessageEncoding)
                {
                    case "textMessageEncoding":
                        binding.InnerMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
                        break;
                    case "binaryMessageEncoding":
                        binding.InnerMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
                        break;
                }
            }
        }

        // Called by the WCF to create the binding element
        protected override BindingElement CreateBindingElement()
        {
            RawMessageEncodingBindingElement bindingElement = new RawMessageEncodingBindingElement();
            this.ApplyConfiguration(bindingElement);
            return bindingElement;
        }
    }
}