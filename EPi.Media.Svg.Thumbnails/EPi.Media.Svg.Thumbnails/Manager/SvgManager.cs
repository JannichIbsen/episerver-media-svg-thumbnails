using EPi.Media.Svg.Thumbnails.Data;
using EPiServer.Framework.Blobs;
using Svg;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EPi.Media.Svg.Thumbnails.Manager
{
    public class SvgManager
    {
        private BlobFactory _blobFactory { get; set; }

        const string _thumbnailExtension = ".png";
        public SvgManager(BlobFactory blobfactory)
        {
            if (blobfactory == null)
            {
                throw new ArgumentNullException("blobfactory");
            }
            _blobFactory = blobfactory;
        }

        private Uri GetThumbnailUriFromBlob(Blob sourceBlob)
        {
            if (sourceBlob == null)
            {
                throw new ArgumentNullException("sourceBlob");
            }

            var blobUriParts = new object[] {
                        Blob.GetContainerIdentifier(sourceBlob.ID).ToString(),
                        Path.GetFileNameWithoutExtension(sourceBlob.ID.LocalPath), "Thumbnail" , _thumbnailExtension };

            return new Uri(string.Format("{0}{1}_{2}{3}", blobUriParts));
        }

        private Bitmap GetBitmapFromSvgBlob(Blob sourceBlob)
        {
            if (sourceBlob == null)
            {
                throw new ArgumentNullException("sourceBlob");
            }
            using (Stream stream = sourceBlob.OpenRead())
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);

                return svgDocument.Draw();
            }
        }

        private Blob CreateBlobFromBitmap(Blob sourceBlob, Bitmap bitmap)
        {
            if (sourceBlob == null)
            {
                throw new ArgumentNullException("sourceBlob");
            }
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            var blob = _blobFactory.CreateBlob(Blob.GetContainerIdentifier(sourceBlob.ID), _thumbnailExtension);

            var thumbnailStream = new MemoryStream();

            bitmap.Save(thumbnailStream, ImageFormat.Png);
            thumbnailStream.Position = 0;
            blob.Write(thumbnailStream);

            return blob;
        }

        public BitmapBlob ConvertSvgBlobToPng(Blob sourceBlob)
        {
            if (sourceBlob == null)
            {
                throw new ArgumentNullException("sourceBlob");
            }

            Bitmap svgBitmap = GetBitmapFromSvgBlob(sourceBlob);

            Blob bitmapBlob = CreateBlobFromBitmap(sourceBlob, svgBitmap);

            Uri thumbnailUri = GetThumbnailUriFromBlob(sourceBlob);

            return new BitmapBlob(bitmapBlob, thumbnailUri);
        }
    }
}
