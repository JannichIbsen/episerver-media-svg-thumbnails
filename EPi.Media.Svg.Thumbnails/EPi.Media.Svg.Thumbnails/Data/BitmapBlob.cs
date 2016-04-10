using EPiServer.Framework.Blobs;
using System;

namespace EPi.Media.Svg.Thumbnails.Data
{
    public class BitmapBlob
    {

        public Blob ConvertedBlob { get; private set; }
        public Uri ThumbnailUri { get; private set; }

        public BitmapBlob(Blob convertedBlob, Uri thumbNailUri)
        {
            ConvertedBlob = convertedBlob;
            ThumbnailUri = thumbNailUri;
        }

    }
}
