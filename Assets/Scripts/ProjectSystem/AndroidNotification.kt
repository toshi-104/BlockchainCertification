package ProjectSystem

import android.app.AlertDialog
import android.app.ProgressDialog
import android.widget.Toast
import com.unity3d.player.UnityPlayer

class AndroidNotification {
    private val activity = UnityPlayer.currentActivity
    private val progressDialog = ProgressDialog(activity)

    fun ShowShortToast(message: String) {
        Toast.makeText(activity.applicationContext, message, Toast.LENGTH_SHORT).show()
    }

    fun ShowLongToast(message: String) {
        Toast.makeText(activity.applicationContext, message, Toast.LENGTH_LONG).show()
    }

    fun ShowDialog(title: String, message: String, buttonText : String) {
        AlertDialog.Builder(activity)
            .setTitle(title)
            .setMessage(message)
            .setPositiveButton(buttonText) { _, _ ->  }
            .show()
    }

    // 表示中はメインスレッドが止まり（？）Unityから非同期実行できないため消せない
    // またProgressDialog自体が非推奨
    fun ShowProgressDialog(message: String){
        progressDialog.setMessage(message)
        progressDialog.show()
    }

    fun HideProgressDialog(){
        if (progressDialog.isShowing){
            progressDialog.dismiss()
        }
    }
}
