using System;
using UnityEngine;

namespace Credential {
    public static class IdPhoto {
        public static Texture2D GetPhoto(string base16) {
            var bytes = Convert.FromBase64String(base16);
            var texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }
    }
}
