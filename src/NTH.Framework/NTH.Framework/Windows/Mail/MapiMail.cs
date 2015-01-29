using NTH.Framework.NativeTypes;
using System;
using System.Collections.Generic;

namespace NTH.Framework.Windows.Mail
{
    public class MapiMail
    {
        private string _subject;
        public string Subject { get { return _subject ?? (_subject = string.Empty); } set { _subject = value; } }

        private string _messageBody;
        public string MessageBody { get { return _messageBody ?? (_messageBody = string.Empty); } set { _messageBody = value; } }

        private RecipientList _recipients;
        public RecipientList Recipients { get { return _recipients ?? (_recipients = new RecipientList(RecipientKind.To)); } }

        private RecipientList _ccRecipients;
        public RecipientList CcRecipients { get { return _ccRecipients ?? (_ccRecipients = new RecipientList(RecipientKind.Cc)); } }

        private RecipientList _bccRecipients;
        public RecipientList BccRecipients { get { return _bccRecipients ?? (_bccRecipients = new RecipientList(RecipientKind.Bcc)); } }
        public object Attachments { get; private set; }

        private IList<MapiRecipientDescription> CollectRecipientData()
        {
            var list = new List<MapiRecipientDescription>();
            list.AddRange(Recipients.GetRecipientObjects());
            list.AddRange(CcRecipients.GetRecipientObjects());
            list.AddRange(BccRecipients.GetRecipientObjects());
            return list;
        }


        public void SendPopup()
        {
            SendPopup(IntPtr.Zero);
        }
        public void SendPopup(IntPtr pwarentWindowHandle)
        {
            SendMailInternal(pwarentWindowHandle, SendOptions.LogonUI | SendOptions.Dialog);
        }


        public void SendDirect()
        {
            SendDirect(IntPtr.Zero);
        }
        public void SendDirect(IntPtr pwarentWindowHandle)
        {
            SendMailInternal(pwarentWindowHandle, SendOptions.LogonUI);
        }

        private void SendMailInternal(IntPtr parentWindow, SendOptions options)
        {
            var msg = new MapiMessage();
            msg.subject = Subject;
            msg.noteText = MessageBody;

            var resData = CollectRecipientData(); // TODO: At least one
            msg.recips = resData.ToNativeArray();
            msg.recipCount = resData.Count;

            // TODO: Files
            SendMailReturnValue res;
            try
            {
                res = NativeMethods.MAPISendMail(IntPtr.Zero, parentWindow, msg, options, 0);
            }
            finally
            {
                if (resData.Count > 0)
                    msg.recips.FreeNativeArray<MapiRecipientDescription>(resData.Count);
            }

            if (res != SendMailReturnValue.SUCCESS)
            {
                throw new MapiException(res);
            }
        }
    }
}
