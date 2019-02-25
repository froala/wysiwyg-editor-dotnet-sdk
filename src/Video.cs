using System;
using System.IO;
using System.Collections.Generic;
#if netcore
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
#else
using System.Web;
#endif

namespace FroalaEditor
{
    /// <summary>
    /// Video functionality.
    /// </summary>
    public static class Video
    {
        /// <summary>
        /// Content type string used in http multipart.
        /// </summary>

        public static VideoOptions defaultOptions = new VideoOptions();

        /// <summary>
        /// Uploads an video to disk.
        /// </summary>
        /// <param name="httpContext">The HttpContext object containing information about the request.</param>
        /// <param name="fileRoute">Server route where the file will be uploaded. This route must be public to be accesed by the editor.</param>
        /// <param name="options">File options.</param>
        /// <returns>Object with link.</returns>
        public static object Upload (HttpContext httpContext, string fileRoute, FileOptions options = null)
        {
            if (options == null)
            {
                options = defaultOptions;
            }

            return File.Upload(httpContext, fileRoute, options);
        }

        /// <summary>
        /// Delete an video from disk.
        /// </summary>
        /// <param name="src">Server video path.</param>
        public static void Delete(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
