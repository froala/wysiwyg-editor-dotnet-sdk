using System;

namespace FroalaEditor
{
    /// <summary>
    /// Video Validation.
    /// </summary>
    public class VideoValidation:FileValidation
    {
        /// <summary>
        /// Allowed video validation default extensions.
        /// </summary>
        public static string[] AllowedVideoExtsDefault = new string[] { "mp4", "webm", "ogg" };

        /// <summary>
        /// Allowed video validation default mimetypes.
        /// </summary>
        public static string[] AllowedVideoMimetypesDefault = new string[] { "video/mp4", "video/webm", "video/ogg" };

        /// <summary>
        /// Init default video validation settings.
        /// </summary>
        protected override void initDefault()
        {
            AllowedExts = AllowedVideoExtsDefault;
            AllowedMimeTypes = AllowedVideoMimetypesDefault;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public VideoValidation(): base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="allowedExts">Allowed validation video extensions.</param>
        /// <param name="allowedMimeTypes">Allowed validation video mimetypes.</param>
        public VideoValidation(string[] allowedExts, string[] allowedMimeTypes): base(allowedExts, allowedMimeTypes)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="function">Custom function used for validation.</param>
        public VideoValidation(Func<string, string, bool> function): base(function)
        {
        }
    }
}
