
namespace FroalaEditor
{
    /// <summary>
    /// Amazon S3 config string.
    /// </summary>
    public class AzureConfig
    {
        /// <summary>
        /// Bucket name.
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Region. Use region from here: http://docs.aws.amazon.com/general/latest/gr/rande.html#s3_region
        /// </summary>
        public string accessKey { get; set; }

        /// <summary>
        /// Starting foldername where the files will be saved under the bucket.
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// Amazon Canned Access Control List: http://docs.aws.amazon.com/AmazonS3/latest/dev/acl-overview.html
        /// </summary>
        public string SASToken { get; set; }

        /// <summary>
        /// Amazon Secret key. Can be found on your Account page, under Security Credentials > Access Keys.
        /// </summary>
        public string uploadURL { get; set; }
    }
}
