using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using System.IO;
using System.Diagnostics;

namespace OnlineExamApp
{
    public class SoapMessageInterceptor : SoapExtension
    {
        public override Stream ChainStream(Stream stream)
        {
            // ChainStream method is called twice in the lifecycle of the SOAP
            // message processing. BEFORE the actual web service operation is
            // invoked and AFTER it has completed. 
            // 
            // In case you just want to intercept the incoming SOAP message then
            // you can read the stream initialized by .Net framework and hence, 
            // you dont need to chain a new stream. Therefore, pass the same 
            // stream back.
            //------------------------------------------------------------
            return stream;
        }

        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, 
                               SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override void Initialize(object initializer)
        {
            // do nothing...
        }

        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeDeserialize:
                    // Incoming message
                    LogMessageFromStream(message.Stream);
                    break;

                case SoapMessageStage.AfterDeserialize:
                    break;

                case SoapMessageStage.BeforeSerialize:
                    break;

                case SoapMessageStage.AfterSerialize:
                    break;
            }
        }

        private void LogMessageFromStream(Stream stream)
        {
            string soapMessage = string.Empty;

            // Just making sure again that we have got a stream which we 
            // can read from AND after reading reset its position 
            //------------------------------------------------------------
            if (stream.CanRead && stream.CanSeek)
            {
                stream.Position = 0;

                StreamReader rdr = new StreamReader(stream);
                soapMessage = rdr.ReadToEnd();


                // IMPORTANT!! - Set the position back to zero on the original 
                // stream so that HTTP pipeline can now process it
                //------------------------------------------------------------
                stream.Position = 0;
            }

            // You have raw SOAP message, log it the way you want. I am using 
            // Trace class as I have configured a trace listener which will 
            // write it to a database table, read:
            // http://girishjjain.com/blog/post/Advanced-Tracing.aspx
            //------------------------------------------------------------
            Trace.WriteLine(soapMessage);
        }
    }
    }
