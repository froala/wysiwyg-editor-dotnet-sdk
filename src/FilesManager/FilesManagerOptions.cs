using ImageMagick;

namespace FroalaEditor
{
    /// <summary>
    /// FilesManager Options used for uploading.
    /// </summary>
    public class FilesManagerOptions
    {
          /// <summary>
        /// Parameter for resizing an image.
        /// </summary>
        public MagickGeometry ResizeGeometry { get; set; }

        public static string FieldnameDefault = "file";

        /// <summary>
        /// Tag name that points to the file.
        /// </summary>
        public string Fieldname { get; set; }

        /// <summary>
        /// FilesManager validation.
        /// </summary>
        public FilesManagerValidation Validation { get; set; }

        /// <summary>
        /// Init default file upload settings.
        /// </summary>
        protected virtual void initDefault()
        {
            Validation = new FilesManagerValidation();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FilesManagerOptions()
        {
            // Set default fieldname.
            Fieldname = FieldnameDefault;

            // Init default settings.
            initDefault();
        }
    }
}
