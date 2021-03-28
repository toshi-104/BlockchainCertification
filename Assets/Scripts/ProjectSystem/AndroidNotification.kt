package projectSystem

import android.app.AlertDialog
import android.app.ProgressDialog
import android.widget.Toast
import com.unity3d.player.UnityPlayer

class AndroidNotification {
    private val activity = UnityPlayer.currentActivity
    private val progressDialog = ProgressDialog(activity)

    fun showShortToast(message: String) {
        Toast.makeText(activity.applicationContext, message, Toast.LENGTH_SHORT).show()
    }

    fun showLongToast(message: String) {
        Toast.makeText(activity.applicationContext, message, Toast.LENGTH_LONG).show()
    }

    fun showDialog(title: String, message: String, buttonText : String) {
        AlertDialog.Builder(activity)
            .setTitle(title)
            .setMessage(message)
            .setPositiveButton(buttonText) { _, _ ->  }
            .show()
    }

    // 表示中はメインスレッドが止まり（？）Unityから非同期実行できないため消せない
    // またProgressDialog自体が非推奨
    fun showProgressDialog(message: String){
        progressDialog.setMessage(message)
        progressDialog.show()
    }

    fun hideProgressDialog(){
        if (progressDialog.isShowing){
            progressDialog.dismiss()
        }
    }
}
