//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SmartStringResources;


namespace Models.Resources.Strings {
    
    
    /// <summary>
    ///   Provides access to string resources.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("SmartStringResources", "1.2.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class Messages {
        
        /// <summary>
        ///   Looks up a localized string similar to: Values of the Fields {1} and {2} must be equal!
        /// </summary>
        public static string Compare(object arg1, object arg2, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.Compare, System.Tuple.Create("1", arg1), System.Tuple.Create("2", arg2));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("Compare", resourceCulture), System.Tuple.Create("1", arg1), System.Tuple.Create("2", arg2));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} must be equal to {1}
        /// </summary>
        public static string Confirm(object arg0, object arg1, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.Confirm, System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("Confirm", resourceCulture), System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: The {0} field value is already exist! Please choose another one.
        /// </summary>
        public static string Error001(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.Error001, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("Error001", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: Length of {0} could not be more than {1}!
        /// </summary>
        public static string MaxLength(object arg0, object arg1, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.MaxLength, System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("MaxLength", resourceCulture), System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} must be between {1} and {2}!
        /// </summary>
        public static string Range(object arg0, object arg1, object arg2, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.Range, System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1), System.Tuple.Create("2", arg2));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("Range", resourceCulture), System.Tuple.Create("0", arg0), System.Tuple.Create("1", arg1), System.Tuple.Create("2", arg2));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: You did not specify a valid cell phone number!
        /// </summary>
        public static string RegularExpressionForCellPhoneNumber([System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return global::Models.Resources.Messages.RegularExpressionForCellPhoneNumber;
            }
            return global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForCellPhoneNumber", resourceCulture);
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: For the field {0} you are just allowed to use integer or double numbers!
        /// </summary>
        public static string RegularExpressionForDouble(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForDouble, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForDouble", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is not a valid email address!
        /// </summary>
        public static string RegularExpressionForEmail(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForEmail, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForEmail", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 1 - 100 characters.
        /// </summary>
        public static string RegularExpressionForFileName(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForFileName, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForFileName", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: For the field {0} you are just allowed to use integer numbers!
        /// </summary>
        public static string RegularExpressionForInteger(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForInteger, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForInteger", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is invalid! {0} must contains 10 digits.
        /// </summary>
        public static string RegularExpressionForNationalCode(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForNationalCode, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForNationalCode", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 8 - 40 characters.
        /// </summary>
        public static string RegularExpressionForPassword(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForPassword, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForPassword", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is not valid!
        /// </summary>
        public static string RegularExpressionForSalary(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForSalary, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForSalary", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: For the field {0} you are just allowed to use integer numbers >=0
        /// </summary>
        public static string RegularExpressionForUnsignedInteger(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForUnsignedInteger, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForUnsignedInteger", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is not a valid url address!
        /// </summary>
        public static string RegularExpressionForUrl(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForUrl, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForUrl", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 6 - 20 characters.
        /// </summary>
        public static string RegularExpressionForUsername(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.RegularExpressionForUsername, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("RegularExpressionForUsername", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: The {0} is required!
        /// </summary>
        public static string Required(object arg0, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.Required, System.Tuple.Create("0", arg0));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("Required", resourceCulture), System.Tuple.Create("0", arg0));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: {0} must be between {2} and {1} characters!
        /// </summary>
        public static string StringLength(object arg0, object arg2, object arg1, [System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.StringLength, System.Tuple.Create("0", arg0), System.Tuple.Create("2", arg2), System.Tuple.Create("1", arg1));
            }
            return SmartStringResources.StringFormatWithExtension.FormatWith(global::Models.Resources.Messages.ResourceManager.GetString("StringLength", resourceCulture), System.Tuple.Create("0", arg0), System.Tuple.Create("2", arg2), System.Tuple.Create("1", arg1));
        }
        
        /// <summary>
        ///   Looks up a localized string similar to: An unknown error has occurred! Please contact the system administrator.
        /// </summary>
        public static string UnexpectedError([System.Runtime.InteropServices.OptionalAttribute()] System.Globalization.CultureInfo resourceCulture) {
            if (object.ReferenceEquals(resourceCulture, null)) {
                return global::Models.Resources.Messages.UnexpectedError;
            }
            return global::Models.Resources.Messages.ResourceManager.GetString("UnexpectedError", resourceCulture);
        }
    }
}
namespace Models.Resources.Strings {
    
    
    /// <summary>
    ///   Provides access to string resource keys.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("SmartStringResources", "1.2.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class MessagesKeys {
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: Values of the Fields {1} and {2} must be equal!
        /// </summary>
        public const string Compare = "Compare";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} must be equal to {1}
        /// </summary>
        public const string Confirm = "Confirm";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: The {0} field value is already exist! Please choose another one.
        /// </summary>
        public const string Error001 = "Error001";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: Length of {0} could not be more than {1}!
        /// </summary>
        public const string MaxLength = "MaxLength";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} must be between {1} and {2}!
        /// </summary>
        public const string Range = "Range";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: You did not specify a valid cell phone number!
        /// </summary>
        public const string RegularExpressionForCellPhoneNumber = "RegularExpressionForCellPhoneNumber";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: For the field {0} you are just allowed to use integer or double numbers!
        /// </summary>
        public const string RegularExpressionForDouble = "RegularExpressionForDouble";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is not a valid email address!
        /// </summary>
        public const string RegularExpressionForEmail = "RegularExpressionForEmail";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 1 - 100 characters.
        /// </summary>
        public const string RegularExpressionForFileName = "RegularExpressionForFileName";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: For the field {0} you are just allowed to use integer numbers!
        /// </summary>
        public const string RegularExpressionForInteger = "RegularExpressionForInteger";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is invalid! {0} must contains 10 digits.
        /// </summary>
        public const string RegularExpressionForNationalCode = "RegularExpressionForNationalCode";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 8 - 40 characters.
        /// </summary>
        public const string RegularExpressionForPassword = "RegularExpressionForPassword";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is not valid!
        /// </summary>
        public const string RegularExpressionForSalary = "RegularExpressionForSalary";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: For the field {0} you are just allowed to use integer numbers >=0
        /// </summary>
        public const string RegularExpressionForUnsignedInteger = "RegularExpressionForUnsignedInteger";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is not a valid url address!
        /// </summary>
        public const string RegularExpressionForUrl = "RegularExpressionForUrl";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} is invalid! {0} must contains _ and/or upercase and lowercase English character and/or number between 0 - 9 and its length must be between 6 - 20 characters.
        /// </summary>
        public const string RegularExpressionForUsername = "RegularExpressionForUsername";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: The {0} is required!
        /// </summary>
        public const string Required = "Required";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: {0} must be between {2} and {1} characters!
        /// </summary>
        public const string StringLength = "StringLength";
        
        /// <summary>
        ///   Provides access to string resource key of value similar to: An unknown error has occurred! Please contact the system administrator.
        /// </summary>
        public const string UnexpectedError = "UnexpectedError";
    }
}
