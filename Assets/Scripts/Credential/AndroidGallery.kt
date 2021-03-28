package credential

import android.content.Intent
import com.unity3d.player.UnityPlayer

class AndroidGallery {
    fun openGallery() {
        val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
        intent.addCategory(Intent.CATEGORY_OPENABLE)
        intent.type = "image/*"
        val activity = UnityPlayer.currentActivity
        activity.startActivityForResult(intent, 42)
    }
}
