using EPi.Media.Svg.Thumbnails.Data;
using EPi.Media.Svg.Thumbnails.Extensions;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPi.Media.Svg.Thumbnails.Manager
{

    public class CustomThumbnailManager : ThumbnailManager
    {
        private static Injected<IContentRepository> ContentRepo { get; set; }
        private static Injected<BlobFactory> BlobFactory { get; set; }
        private static Injected<BlobResolver> BlobResolver { get; set; }
        private static SvgManager SvgManager { get; set; }
        public CustomThumbnailManager() : base(ContentRepo.Service, BlobFactory.Service, BlobResolver.Service)
        {
            SvgManager = new SvgManager(BlobFactory.Service);
        }
        public override Blob CreateImageBlob(Blob sourceBlob, string propertyName, ImageDescriptorAttribute descriptorAttribute)
        {
            if (sourceBlob.IsSvgBlob())
            {
                BitmapBlob blob = SvgManager.ConvertSvgBlobToPng(sourceBlob);

                return CreateBlob(blob.ThumbnailUri, blob.ConvertedBlob, descriptorAttribute.Width, descriptorAttribute.Height);
            }
            return base.CreateImageBlob(sourceBlob, propertyName, descriptorAttribute);
        }
    }

}
