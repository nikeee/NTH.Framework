namespace NTH.Framework.NativeTypes
{
    internal enum SendMailReturnValue : uint
    {
        /// <summary>A recipient matched more than one of the recipient descriptor structures and MAPI_DIALOG was not set. No message was sent.</summary>
        MAPI_E_AMBIGUOUS_RECIPIENT = 21,

        /// <summary>The specified attachment was not found. No message was sent.</summary>
        MAPI_E_ATTACHMENT_NOT_FOUND = 11,

        /// <summary>The specified attachment could not be opened. No message was sent.</summary>
        MAPI_E_ATTACHMENT_OPEN_FAILURE = 12,

        /// <summary>The type of a recipient was not MAPI_TO, MAPI_CC, or MAPI_BCC. No message was sent.</summary>
        MAPI_E_BAD_RECIPTYPE = 15,

        /// <summary>One or more unspecified errors occurred. No message was sent.</summary>
        MAPI_E_FAILURE = 2,

        /// <summary>There was insufficient memory to proceed. No message was sent.</summary>
        MAPI_E_INSUFFICIENT_MEMORY = 5,

        /// <summary>One or more recipients were invalid or did not resolve to any address.</summary>
        MAPI_E_INVALID_RECIPS = 25,

        /// <summary>There was no default logon, and the user failed to log on successfully when the logon dialog box was displayed. No message was sent.</summary>
        MAPI_E_LOGIN_FAILURE = 3,

        /// <summary>The text in the message was too large. No message was sent.</summary>
        MAPI_E_TEXT_TOO_LARGE = 18,

        /// <summary>There were too many file attachments. No message was sent.</summary>
        MAPI_E_TOO_MANY_FILES = 9,

        /// <summary>There were too many recipients. No message was sent.</summary>
        MAPI_E_TOO_MANY_RECIPIENTS = 10,

        /// <summary>The MAPI_FORCE_UNICODE flag is specified and Unicode is not supported.</summary>
        /// <remarks>Note  This value can be returned by MAPISendMailW only.</remarks>
        MAPI_E_UNICODE_NOT_SUPPORTED = 27,

        /// <summary>A recipient did not appear in the address list. No message was sent.</summary>
        MAPI_E_UNKNOWN_RECIPIENT = 14,

        /// <summary>The user canceled one of the dialog boxes. No message was sent.</summary>
        MAPI_E_USER_ABORT = 1,

        /// <summary>No errors.</summary>
        SUCCESS = 0
    }
}
