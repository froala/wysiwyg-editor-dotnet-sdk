using System;
using System.Web.Mvc;
using ImageMagick;

namespace demo.Controllers
{
    public class FroalaApiController : Controller
    {
        public ActionResult UploadImage()
        {
            string uploadPath = "/Public/";

            object response;
            try
            {
                var image = new FroalaEditor.Image(System.Web.HttpContext.Current);
                response = image.Upload(uploadPath);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadFile()
        {
            string uploadPath = "/Public/";

            object response;
            try
            {
                var file = new FroalaEditor.File(System.Web.HttpContext.Current);
                response = file.Upload(uploadPath);
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult LoadImages()
        {
            string uploadPath = "/Public/";

            object response;
            try
            {
                var image = new FroalaEditor.Image(System.Web.HttpContext.Current);
                response = image.List(uploadPath);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult UploadImageResize()
        {
            string fileRoute = "/Public/";

            MagickGeometry resizeGeometry = new MagickGeometry(300, 300);
            resizeGeometry.IgnoreAspectRatio = true;

            FroalaEditor.ImageOptions options = new FroalaEditor.ImageOptions
            {
                ResizeGeometry = resizeGeometry
            };

            object response;
            try
            {
                var image = new FroalaEditor.Image(System.Web.HttpContext.Current);
                response = image.Upload(fileRoute, options);
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(response);
        }

        public ActionResult UploadImageValidation()
        {
            string fileRoute = "/Public/";

            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                MagickImageInfo info = new MagickImageInfo(filePath);

                //Console.WriteLine("====");
                Console.WriteLine(info.Width);
                Console.WriteLine(info.Height);


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

            object response;
            try
            {
                var image = new FroalaEditor.Image(System.Web.HttpContext.Current);
                response = image.Upload(fileRoute, options);
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(response);
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

            object response;
            try
            {
                var image = new FroalaEditor.Image(System.Web.HttpContext.Current);
                response = image.Upload(fileRoute, options);
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(response);
        }

        public ActionResult DeleteFile()
        {
            try
            {
                FroalaEditor.File.Delete(HttpContext.Request.Form["src"]);
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(true);
        }

        public ActionResult DeleteImage()
        {
            try
            {
                FroalaEditor.Image.Delete("/Public/" + HttpContext.Request.Form["src"]);
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(true);
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
                SecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY")
            };

            return Json(FroalaEditor.S3.GetHash(config), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
