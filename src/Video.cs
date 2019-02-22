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

        /// <summary>
        /// List videos from disk.
        /// </summary>
        /// <param name="folderPath">Server folder path.</param>
        /// <param name="thumbPath">Optional. Server thumb path.</param>
        /// <returns></returns>
        public static List<object> List(string folderPath, string thumbPath = null)
        {
            // Use thumbPath as folderPath.
            if (thumbPath == null)
            {
                thumbPath = folderPath;
            }

            // Array of videos objects to return.
            List<object> response = new List<object>();

            string absolutePath = File.GetAbsoluteServerPath(folderPath);

            string[] videoMimetypes = VideoValidation.AllowedVideoMimetypesDefault;

            // Check if path exists.
            if (!System.IO.Directory.Exists(absolutePath))
            {
                throw new Exception("Videos folder does not exist!");
            }

            string[] fileEntries = System.IO.Directory.GetFiles(absolutePath);

            // Add videos.
            foreach (string filePath in fileEntries)
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                if (System.IO.File.Exists(filePath))
                {   
#if netcore
                    string mimeType;
                    new FileExtensionContentTypeProvider().TryGetContentType(filePath, out mimeType);
                    if (mimeType == null) {
                        mimeType = "application/octet-stream";
                    }
#else
                    string mimeType = System.Web.MimeMapping.GetMimeMapping(filePath);
#endif        

                    if (Array.IndexOf(videoMimetypes, mimeType) >= 0)
                    {
                        response.Add(new
                        {
                            url = folderPath.Replace("wwwroot/", "") + fileName,
                            thumb = thumbPath.Replace("wwwroot/", "") + fileName,
                            name = fileName
                        });
                    }
                }
            }
            return response;
        }
    }
}
