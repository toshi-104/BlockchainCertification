using NUnit.Framework;
using ProjectSystem;
using QrCode;
using ZXing;

namespace Tests.EditMode {
    public class QrCodeSystemTest {
        [Test]
        public void CreateTest() {
            const string context = "Hello World";
            var texture = QrCodeSystem.CreateQrCode(context);

            var reader = new BarcodeReader();
            Assert.That(reader.Decode(texture.GetPixels32(), texture.width, texture.height).Text, Is.EqualTo(context));
        }
    }
}
