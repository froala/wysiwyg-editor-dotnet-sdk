using ImageMagick;

namespace FroalaEditor
{
    /// <summary>
    /// Video Options used for uploading.
    /// </summary>
    public class VideoOptions:FileOptions
    {
        /// <summary>
        /// Parameter for resizing an video.
        /// </summary>
        public MagickGeometry ResizeGeometry { get; set; }

        /// <summary>
        /// Init default video upload settings.
        /// </summary>
        protected override void initDefault()
        {
            Validation = new VideoValidation();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public VideoOptions(): base() { }
    }
}
