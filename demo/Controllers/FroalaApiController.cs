using System;
using System.Web.Helpers;
using System.Web.Mvc;
using ImageMagick;
using System.Linq;

namespace demo.Controllers
{
    public class FroalaApiController : Controller
    {
        public ActionResult UploadImage()
        {
            string uploadPath = "/Public/";

            try
            {
                return Json(FroalaEditor.Image.Upload(System.Web.HttpContext.Current, uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadVideo()
        {
            string uploadPath = "/Public/";

            try
            {
                return Json(FroalaEditor.Video.Upload(System.Web.HttpContext.Current, uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadFile()
        {
            string uploadPath = "/Public/";

            try
            {
                return Json(FroalaEditor.File.Upload(System.Web.HttpContext.Current, uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadFilesManager()
        {
            string uploadPath = "/Public/";

            try
            {
                return Json(FroalaEditor.FilesManager.Upload(System.Web.HttpContext.Current, uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }


        public ActionResult LoadImages()
        {
            string uploadPath = "/Public/";

            try
            {
                return Json(FroalaEditor.Image.List(uploadPath), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadImageResize()
        {
            string fileRoute = "/Public/";
            var fileMimeType = System.Web.HttpContext.Current.Request.Files.Get("file").ContentType;
            string[] ImageMimeTypes = new string[] { "image/gif", "image/jpeg", "audio/mpeg", "image/png", "image/webp" };
            if (!ImageMimeTypes.Contains(fileMimeType.ToString()))
            {
                return Json(new Exception("Invalid contentType. It must be Image"));
            }

            MagickGeometry resizeGeometry = new MagickGeometry(300, 300);
            resizeGeometry.IgnoreAspectRatio = true;

            FroalaEditor.ImageOptions options = new FroalaEditor.ImageOptions
            {
                ResizeGeometry = resizeGeometry
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(System.Web.HttpContext.Current, fileRoute, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadImageValidation()
        {
            string fileRoute = "/Public/";

            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                MagickImageInfo info = new MagickImageInfo(filePath);

                if (info.Width != info.Height)
                {
                    return false;
                }

                return true;
            };

            FroalaEditor.ImageOptions options = new FroalaEditor.ImageOptions
            {
                Fieldname = "myImage",
                Validation = new FroalaEditor.ImageValidation(validationFunction)
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(System.Web.HttpContext.Current, fileRoute, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadFilesManagerValidation()
        {
            string fileRoute = "/Public/";
            var fileMimeType = System.Web.HttpContext.Current.Request.Files.Get("myImage").ContentType;
            string[] ImageMimeTypes = new string[] { "image/gif", "image/jpeg", "audio/mpeg", "image/png", "image/webp" };
            if(!ImageMimeTypes.Contains(fileMimeType.ToString()))
            {
                return Json(new Exception("Invalid contentType. It must be Image"));
            }
            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                MagickImageInfo info = new MagickImageInfo(filePath);

                if (info.Width != info.Height)
                {
                    return false;
                }
                long size = new System.IO.FileInfo(filePath).Length;
                if (size > 10 * 1024 * 1024)
                {
                    return false;
                }
                return true;
            };

            FroalaEditor.FilesManagerOptions options = new FroalaEditor.FilesManagerOptions
            {
                Fieldname = "myImage",
                Validation = new FroalaEditor.FilesManagerValidation(validationFunction)
            };

            try
            {
                return Json(FroalaEditor.FilesManager.Upload(System.Web.HttpContext.Current, fileRoute, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadFileValidation()
        {
            string fileRoute = "/Public/";

            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                long size = new System.IO.FileInfo(filePath).Length;
                if (size > 10 * 1024 * 1024)
                {
                    return false;
                }

                return true;
            };

            FroalaEditor.FileOptions options = new FroalaEditor.FileOptions
            {
                Fieldname = "myFile",
                Validation = new FroalaEditor.FileValidation(validationFunction)
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(System.Web.HttpContext.Current, fileRoute, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult DeleteFile()
        {
            try
            {
                FroalaEditor.File.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult DeleteImage()
        {
            try
            {
                FroalaEditor.Image.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

	    public ActionResult DeleteVideo()
        {
            try
            {
                FroalaEditor.Video.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public object S3Signature()
        {
            FroalaEditor.S3Config config = new FroalaEditor.S3Config
            {
                Bucket = Environment.GetEnvironmentVariable("AWS_BUCKET"),
                Region = Environment.GetEnvironmentVariable("AWS_REGION"),
                KeyStart = Environment.GetEnvironmentVariable("AWS_KEY_START"),
                Acl = Environment.GetEnvironmentVariable("AWS_ACL"),
                AccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"),
                SecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY"),
                Expiration = Environment.GetEnvironmentVariable("AWS_EXPIRATION")  // Expiration s3 image signature #11
            };

            return Json(FroalaEditor.S3.GetHash(config), JsonRequestBehavior.AllowGet);
        }

        public object Azure()
        {
            FroalaEditor.AzureConfig config = new FroalaEditor.AzureConfig
            {
                account = Environment.GetEnvironmentVariable("AZURE_ACCOUNT"),
                accessKey = Environment.GetEnvironmentVariable("AZURE_ACCESS_KEY"),
                container = Environment.GetEnvironmentVariable("AZURE_CONTAINER"),
                SASToken = Environment.GetEnvironmentVariable("AZURE_SAS_TOKEN"),
                uploadURL = Environment.GetEnvironmentVariable("AZURE_UPLOAD_URL")
            };

            return Json(config, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
