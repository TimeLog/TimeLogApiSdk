namespace TimeLog.TransactionalApi.SDK.RawHelper
{
    using System;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.Xml;

    /// <summary>
    /// This is the binding element that, when plugged into a custom binding, will enable the Raw encoder
    /// </summary>
    public sealed class RawMessageEncodingBindingElement : MessageEncodingBindingElement, IPolicyExportExtension
    {
        // We will use an inner binding element to store information required for the inner encoder
        private MessageEncodingBindingElement innerBindingElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="RawMessageEncodingBindingElement"/> class. 
        /// By default, use the default text encoder as the inner encoder
        /// </summary>
        public RawMessageEncodingBindingElement()
            : this(new TextMessageEncodingBindingElement())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawMessageEncodingBindingElement"/> class. 
        /// </summary>
        /// <param name="messageEncoderBindingElement">The binding element</param>
        public RawMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
        {
            this.innerBindingElement = messageEncoderBindingElement;
        }

        /// <summary>
        /// Gets or sets the binding element
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get
            {
                return this.innerBindingElement;
            }

            set
            {
                this.innerBindingElement = value;
            }
        }

        public override MessageVersion MessageVersion
        {
            get
            {
                return this.innerBindingElement.MessageVersion;
            }

            set
            {
                this.innerBindingElement.MessageVersion = value;
            }
        }

        // Main entry point into the encoder binding element. Called by WCF to get the factory that will create the message encoder
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new RawMessageEncoderFactory(this.innerBindingElement.CreateMessageEncoderFactory());
        }

        public override BindingElement Clone()
        {
            return new RawMessageEncodingBindingElement(this.innerBindingElement);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return this.innerBindingElement.GetProperty<T>(context);
            }
            
            return base.GetProperty<T>(context);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
        {
            if (policyContext == null)
            {
                throw new ArgumentNullException("policyContext");
            }

            XmlDocument document = new XmlDocument();
            policyContext.GetBindingAssertions().Add(document.CreateElement(
                RawMessageEncodingPolicyConstants.RawEncodingPrefix,
                RawMessageEncodingPolicyConstants.RawEncodingName,
                RawMessageEncodingPolicyConstants.RawEncodingNamespace));
        }
    }
}
