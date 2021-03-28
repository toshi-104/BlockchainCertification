package projectSystem

import android.app.Activity
import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import com.unity3d.player.UnityPlayer
import com.unity3d.player.UnityPlayerActivity
import java.io.BufferedInputStream
import java.io.ByteArrayOutputStream
import java.util.*

class MainActivity : UnityPlayerActivity() {
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode != 42 || resultCode != Activity.RESULT_OK) return

        val uri = data?.data
        val inputStream = this.contentResolver.openInputStream(uri!!)
        val bitmap = BitmapFactory.decodeStream(BufferedInputStream(inputStream!!))
        inputStream.close()

        val outputStream = ByteArrayOutputStream()
        bitmap.compress(Bitmap.CompressFormat.PNG, 100, outputStream)
        val bytes = outputStream.toByteArray()
        outputStream.close()
        val base64 = Base64.getEncoder().encodeToString(bytes)

        UnityPlayer.UnitySendMessage("Manager", "SetIdPhoto", base64)
    }
}
