using EPiServer.Framework.Blobs;

namespace EPi.Media.Svg.Thumbnails.Extensions
{
    public static class BlobExtensions
    {
        public static bool IsSvgBlob(this Blob blob)
        {
            if(blob != null)
            {
               return blob.ID != null 
                    && !string.IsNullOrEmpty(blob.ID.AbsolutePath) 
                    && blob.ID.AbsolutePath.EndsWith("svg", System.StringComparison.OrdinalIgnoreCase);
               
            }
            return false;
        }
    }
}
