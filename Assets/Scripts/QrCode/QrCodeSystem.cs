using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace QrCode {
    public static class QrCodeSystem {
        public static Texture2D CreateQrCode(string contents) {
            const int width = 256;
            const int height = 256;

            var writer = new BarcodeWriter {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions {
                    CharacterSet = "UTF-8",
                    Width = width,
                    Height = height
                }
            };

            const TextureFormat format = TextureFormat.ARGB32;
            var texture = new Texture2D(width, height, format, false);
            var colors = writer.Write(contents);

            texture.SetPixels32(colors);
            texture.Apply();

            return texture;
        }

        public static string ReadQrCode(WebCamTexture texture) {
            var reader = new BarcodeReader();
            var rawRGB = texture.GetPixels32();
            var width = texture.width;
            var height = texture.height;
            var result = reader.Decode(rawRGB, width, height);

            return result != null ? result.Text : string.Empty;
        }
    }
}
