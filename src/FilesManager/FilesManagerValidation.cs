using System;
using System.Linq;

namespace FroalaEditor
{
    /// <summary>
    /// FilesManager Validation.
    /// </summary>
    public class FilesManagerValidation
    {
        /// <summary>
        /// Allowed files validation default extensions.
        /// </summary>
        public static string[] AllowedFilesManagerExtsDefault = new string[] { "doc", "docx", "gif", "jpeg", "jpg","txt", "log", "mov", "mp3", "mp4", "ogg", "ogv", "pdf", "png", "webm", "webp", "wmv", "xls","xlsx", "zip" };

        /// <summary>
        /// Allowed files validation default mimetypes.
        /// </summary>
        public static string[] AllowedFilesManagerMimetypesDefault = new string[] { "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/gif", "image/jpeg", "text/plain", "type/text", "video/quicktime", "audio/mpeg", "video/mp4", "audio/ogg", "video/ogg", "application/pdf", "image/png", "video/webm", "image/webp", "video/x-ms-wmv", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/x-zip-compressed" };

        /// <summary>
        /// Allowed validation extensions.
        /// </summary>
        protected string[] AllowedExts { get; set; }

        /// <summary>
        /// Allowed validation mimetypes.
        /// </summary>
        protected string[] AllowedMimeTypes { get; set; }

        /// <summary>
        /// Custom function used or validation.
        /// </summary>
        protected Func<string, string, bool> Function { get; set; }

        /// <summary>
        /// Init default files validation settings.
        /// </summary>
        protected virtual void initDefault()
        {
            AllowedExts = AllowedFilesManagerExtsDefault;
            AllowedMimeTypes = AllowedFilesManagerMimetypesDefault;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FilesManagerValidation()
        {
            initDefault();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="allowedExts">Allowed validation files extensions.</param>
        /// <param name="allowedMimeTypes">Allowed validation files mimetypes.</param>
        public FilesManagerValidation(string[] allowedExts, string[] allowedMimeTypes)
        {
            initDefault();

            if (allowedExts != null)
            {
                AllowedExts = allowedExts;
            }
            
            if (allowedMimeTypes != null)
            {
                AllowedMimeTypes = allowedMimeTypes;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="function">Custom function used for validation.</param>
        public FilesManagerValidation(Func<string, string, bool> function)
        {
            initDefault();

            this.Function = function;
        }

        /// <summary>
        /// Check if file is valid.
        /// Use only the custom function if provided. Else check if the file has an allowed extension and mimetype.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="mimeType">File mimetype.</param>
        /// <returns></returns>
        public bool Check(string filePath, string mimeType)
        {
            if (Function != null)
            {
                return Function(filePath, mimeType);
            }

            return AllowedExts.Contains(Utils.GetFileExtension(filePath)) && AllowedMimeTypes.Contains(mimeType.ToLower());
        }
    }
}
