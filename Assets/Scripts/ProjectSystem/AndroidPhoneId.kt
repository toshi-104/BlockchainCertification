package projectSystem

import android.Manifest
import android.content.Context.TELEPHONY_SERVICE
import android.content.pm.PackageManager
import android.telephony.TelephonyManager
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.unity3d.player.UnityPlayer

class AndroidPhoneId {
    private val context = UnityPlayer.currentActivity.applicationContext
    private val permission = Manifest.permission.READ_PHONE_STATE

    init {
        val activity = UnityPlayer.currentActivity
        if (ContextCompat.checkSelfPermission(context, permission) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(activity, arrayOf(permission), 1)
        }
    }

    fun getImei(): String? {
        if (ContextCompat.checkSelfPermission(context, permission) != PackageManager.PERMISSION_GRANTED) {
            // TODO: 許可されなかったときの処理
            return null
        }
        val manager = context.getSystemService(TELEPHONY_SERVICE) as TelephonyManager
        return manager.imei
    }
}
