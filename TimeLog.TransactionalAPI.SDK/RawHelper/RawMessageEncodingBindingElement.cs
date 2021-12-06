using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace TimeLog.TransactionalAPI.SDK.RawHelper
{
    /// <summary>
    ///     This is the binding element that, when plugged into a custom binding, will enable the Raw encoder
    /// </summary>
    public sealed class RawMessageEncodingBindingElement : MessageEncodingBindingElement
    {
        // We will use an inner binding element to store information required for the inner encoder

        /// <summary>
        ///     Initializes a new instance of the <see cref="RawMessageEncodingBindingElement" /> class.
        ///     By default, use the default text encoder as the inner encoder
        /// </summary>
        public RawMessageEncodingBindingElement()
            : this(new TextMessageEncodingBindingElement())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RawMessageEncodingBindingElement" /> class.
        /// </summary>
        /// <param name="messageEncoderBindingElement">The binding element</param>
        public RawMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
        {
            InnerMessageEncodingBindingElement = messageEncoderBindingElement;
        }

        /// <summary>
        ///     Gets or sets the binding element
        /// </summary>
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement { get; set; }

        public override MessageVersion MessageVersion
        {
            get => InnerMessageEncodingBindingElement.MessageVersion;

            set => InnerMessageEncodingBindingElement.MessageVersion = value;
        }

        // Main entry point into the encoder binding element. Called by WCF to get the factory that will create the message encoder
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new RawMessageEncoderFactory(InnerMessageEncodingBindingElement.CreateMessageEncoderFactory());
        }

        public override BindingElement Clone()
        {
            return new RawMessageEncodingBindingElement(InnerMessageEncodingBindingElement);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return InnerMessageEncodingBindingElement.GetProperty<T>(context);
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
    }
}