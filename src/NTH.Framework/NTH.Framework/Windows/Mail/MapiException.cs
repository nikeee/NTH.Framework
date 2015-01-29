using NTH.Framework.NativeTypes;
using System;
using System.Collections.Generic;

namespace NTH.Framework.Windows.Mail
{
    public class MapiException : Exception
    {
        internal SendMailReturnValue ErrorCode { get; private set; }

        internal MapiException(SendMailReturnValue errorCode)
            : base(GetMessageFromErrorCode(errorCode))
        {
            ErrorCode = errorCode;
        }

        private static Dictionary<SendMailReturnValue, string> _errorDict;
        private static string GetMessageFromErrorCode(SendMailReturnValue errorCode)
        {
            if (_errorDict == null)
                InitializeErrorDictionary();
            if (!_errorDict.ContainsKey(errorCode))
                return "Unknown error.";
            return _errorDict[errorCode];
        }
        private static void InitializeErrorDictionary()
        {
            _errorDict = new Dictionary<SendMailReturnValue, string>()
                         {
                             {SendMailReturnValue.MAPI_E_AMBIGUOUS_RECIPIENT, "A recipient matched more than one of the recipient descriptor structures and MAPI_DIALOG was not set. No message was sent."},
                             {SendMailReturnValue.MAPI_E_ATTACHMENT_NOT_FOUND, "The specified attachment was not found. No message was sent."},
                             {SendMailReturnValue.MAPI_E_ATTACHMENT_OPEN_FAILURE, "The specified attachment could not be opened. No message was sent."},
                             {SendMailReturnValue.MAPI_E_BAD_RECIPTYPE, "The type of a recipient was not MAPI_TO, MAPI_CC, or MAPI_BCC. No message was sent."},
                             {SendMailReturnValue.MAPI_E_INSUFFICIENT_MEMORY, "There was insufficient memory to proceed. No message was sent."},
                             {SendMailReturnValue.MAPI_E_INVALID_RECIPS, "One or more recipients were invalid or did not resolve to any address."},
                             {SendMailReturnValue.MAPI_E_TEXT_TOO_LARGE, "The text in the message was too large. No message was sent."},
                             {SendMailReturnValue.MAPI_E_TOO_MANY_FILES, "There were too many file attachments. No message was sent."},
                             {SendMailReturnValue.MAPI_E_TOO_MANY_RECIPIENTS, "There were too many recipients. No message was sent."},
                             {SendMailReturnValue.MAPI_E_UNICODE_NOT_SUPPORTED, "The MAPI_FORCE_UNICODE flag is specified and Unicode is not supported. Note: This value can be returned by MAPISendMailW"},
                             {SendMailReturnValue.MAPI_E_UNKNOWN_RECIPIENT, "A recipient did not appear in the address list. No message was sent."},
                             {SendMailReturnValue.MAPI_E_USER_ABORT, "The user canceled one of the dialog boxes. No message was sent."},
                             {SendMailReturnValue.MAPI_E_FAILURE, "One or more unspecified errors occurred. No message was sent."},
                             {SendMailReturnValue.MAPI_E_LOGIN_FAILURE, "There was no default logon, and the user failed to log on successfully when the logon dialog box was displayed. No message was sent."},
                             {SendMailReturnValue.SUCCESS, "No errors."},
                         };
        }
    }
}
