using System;
using System.IO;
using System.ServiceModel.Channels;

namespace TimeLog.TransactionalAPI.SDK.RawHelper
{
    /// <summary>
    ///     The raw message encoder factory.
    /// </summary>
    public class RawMessageEncoderFactory : MessageEncoderFactory
    {
        /// <summary>
        ///     The encoder.
        /// </summary>
        private readonly MessageEncoder encoder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RawMessageEncoderFactory" /> class.
        /// </summary>
        /// <param name="messageEncoderFactory">The message encoder factory.</param>
        public RawMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory)
        {
            if (messageEncoderFactory == null)
            {
                throw new ArgumentNullException(nameof(messageEncoderFactory),
                    "A valid message encoder factory must be passed to the GZipEncoder");
            }

            encoder = new RawMessageEncoder(messageEncoderFactory.Encoder);
        }

        /// <summary>
        ///     Gets the encoder.
        /// </summary>
        public override MessageEncoder Encoder => encoder;

        /// <summary>
        ///     Gets the message version.
        /// </summary>
        public override MessageVersion MessageVersion => encoder.MessageVersion;

        /// <summary>
        ///     The raw message encoder.
        /// </summary>
        private class RawMessageEncoder : MessageEncoder
        {
            /// <summary>
            ///     The inner encoder.
            /// </summary>
            private readonly MessageEncoder innerEncoder;

            /// <summary>
            ///     Initializes a new instance of the <see cref="RawMessageEncoder" /> class.
            /// </summary>
            /// <param name="messageEncoder">The message encoder</param>
            public RawMessageEncoder(MessageEncoder messageEncoder)
            {
                if (messageEncoder == null)
                {
                    throw new ArgumentNullException("messageEncoder",
                        "A valid message encoder must be passed to the GZipEncoder");
                }

                innerEncoder = messageEncoder;
            }

            /// <summary>
            ///     Gets the content type.
            /// </summary>
            public override string ContentType => "text/xml";

            /// <summary>
            ///     Gets the media type.
            /// </summary>
            public override string MediaType => "text/xml";

            /// <summary>
            ///     Gets the message version.
            /// </summary>
            public override MessageVersion MessageVersion => innerEncoder.MessageVersion;

            /// <summary>
            ///     The read message.
            /// </summary>
            /// <param name="stream">The stream.</param>
            /// <param name="maxSizeOfHeaders">The max size of headers.</param>
            /// <param name="contentType">The content type.</param>
            /// <returns>The <see cref="Message" />.</returns>
            public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
            {
                var returnMessage = innerEncoder.ReadMessage(stream, maxSizeOfHeaders);

                RawMessageHelper.Instance.AddResponse(returnMessage);
                return returnMessage;
            }

            /// <summary>
            ///     The read message.
            /// </summary>
            /// <param name="buffer">The buffer</param>
            /// <param name="bufferManager">The buffer manager</param>
            /// <param name="contentType">The content type.</param>
            /// <returns>The <see cref="Message" />.</returns>
            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager,
                string contentType)
            {
                var returnMessage = innerEncoder.ReadMessage(buffer, bufferManager);
                returnMessage.Properties.Encoder = this;

                RawMessageHelper.Instance.AddResponse(returnMessage);
                return returnMessage;
            }

            /// <summary>
            ///     The write message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="stream">The stream.</param>
            public override void WriteMessage(Message message, Stream stream)
            {
                RawMessageHelper.Instance.AddRequest(message);
                innerEncoder.WriteMessage(message, stream);
                stream.Flush();
            }

            /// <summary>
            ///     The write message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="maxMessageSize">The max message size.</param>
            /// <param name="bufferManager">The buffer manager.</param>
            /// <param name="messageOffset">The message offset.</param>
            /// <returns>The ArraySegment</returns>
            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize,
                BufferManager bufferManager, int messageOffset)
            {
                RawMessageHelper.Instance.AddRequest(message);
                var buffer = innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, 0);
                return buffer;
            }
        }
    }
}