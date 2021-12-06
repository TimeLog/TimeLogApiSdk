using System;
using System.IO;
using System.ServiceModel.Channels;

namespace TimeLog.TransactionalAPI.SDK.RawHelper
{
    /// <summary>
    ///     The raw message helper.
    /// </summary>
    public class RawMessageHelper
    {
        private static RawMessageHelper _instance;

        private RawMessageHelper()
        {
            Request = string.Empty;
            Response = string.Empty;
        }

        /// <summary>
        ///     Gets the current instance of the helper
        /// </summary>
        public static RawMessageHelper Instance => _instance ?? (_instance = new RawMessageHelper());

        /// <summary>
        ///     Gets the latest raw request
        /// </summary>
        public string Request { get; private set; }

        /// <summary>
        ///     Gets the latest raw response
        /// </summary>
        public string Response { get; private set; }

        /// <summary>
        ///     Adds a request to memory
        /// </summary>
        /// <param name="message">The raw SOAP message</param>
        public void AddRequest(Message message)
        {
            Request = message.ToString();
        }

        /// <summary>
        ///     Adds a response to memory
        /// </summary>
        /// <param name="message">The raw SOAP message</param>
        public void AddResponse(Message message)
        {
            Response = message.ToString();
        }

        /// <summary>
        ///     Saves the request and response from the latest interaction to a text file
        /// </summary>
        /// <param name="path">File to place the file</param>
        public void SaveRecentRequestResponsePair(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.AppendAllText(path, "<!---- " + DateTime.Now.ToString("O") + " ---->\r\n");
            File.AppendAllText(path, "<!---- " + SettingsHandler.Instance.Url + " ---->\r\n");
            File.AppendAllText(path, "<!--------------- REQUEST ------------------->\r\n\r\n");
            File.AppendAllText(path, Request);
            File.AppendAllText(path, "\r\n\r\n<!--------------- RESPONSE ------------------->\r\n\r\n");
            File.AppendAllText(path, Response);
        }
    }
}