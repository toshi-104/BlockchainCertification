package bluetooth

import android.bluetooth.BluetoothAdapter
import android.bluetooth.BluetoothDevice
import android.bluetooth.BluetoothSocket
import android.content.pm.PackageManager
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.*
import java.io.IOException
import java.io.InputStream
import java.io.OutputStream
import java.nio.charset.StandardCharsets
import java.util.*

class AndroidBluetoothSystem {
    private val sppUuid = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB")
    private val context = UnityPlayer.currentActivity.applicationContext
    private var deviceTable = mutableMapOf<String, String>()
    private var adapter: BluetoothAdapter? = null
    private var socket: BluetoothSocket? = null
    private var inputStream: InputStream? = null
    private var outputStream: OutputStream? = null

    init {
        val permissionLocation = android.Manifest.permission.ACCESS_COARSE_LOCATION
        val activity = UnityPlayer.currentActivity
        if (ContextCompat.checkSelfPermission(context, permissionLocation) != PackageManager.PERMISSION_GRANTED
        ) {
            ActivityCompat.requestPermissions(activity, arrayOf(permissionLocation), 1)
        }
        adapter = BluetoothAdapter.getDefaultAdapter()
    }

    @Suppress("BlockingMethodInNonBlockingContext")
    fun server() {
        GlobalScope.launch {
            val serverSocket = adapter?.listenUsingInsecureRfcommWithServiceRecord("SPP", sppUuid)
            socket = serverSocket?.accept()
            serverSocket?.close()
        }
    }

    fun client(deviceName: String?) {
        val macAddress = deviceTable[deviceName]
        val device: BluetoothDevice? = adapter?.getRemoteDevice(macAddress)

        try {
            socket = device?.createInsecureRfcommSocketToServiceRecord(sppUuid)
            socket?.connect()
            Toast.makeText(context, "connected", Toast.LENGTH_SHORT).show()
        } catch (e: IOException) {
            socket = null
            Toast.makeText(context, "socket error", Toast.LENGTH_SHORT).show()
        }
    }

    fun getDevices(): Array<String> {
        val devices = adapter?.bondedDevices ?: return emptyArray()
        if (devices.size >= 1) {
            for (i in devices) {
                deviceTable[i.name] = i.address
            }
        }
        return deviceTable.keys.toTypedArray()
    }

    fun send(message: String) {
        try {
            outputStream = socket?.outputStream
        } catch (connectException: IOException) {
            Toast.makeText(context, "stream error", Toast.LENGTH_SHORT).show()
            try {
                socket?.close()
                socket = null
            } catch (closeException: IOException) {
                return
            }
        }

        if (outputStream != null) {
            Toast.makeText(context, "stream is not null", Toast.LENGTH_SHORT).show()
        }

        val byte = message.toByteArray()
        try {
            outputStream?.write(byte)
            Toast.makeText(context, "send!", Toast.LENGTH_SHORT).show()
        } catch (e: IOException) {
            Toast.makeText(context, "send error", Toast.LENGTH_SHORT).show()
            try {
                socket?.close()
            } catch (e1: IOException) {

            }
        }
    }

    @Suppress("BlockingMethodInNonBlockingContext")
    fun receive(gameObject: String, method: String) {
        GlobalScope.launch {
            while (socket == null){
                delay(1000)
            }
            try {
                inputStream = socket?.inputStream
            } catch (connectException: IOException){
                closeSocket()
                socket = null
                return@launch
            }

            while (true) {
                val incomingBuff = ByteArray(64)

                val bytes = inputStream?.read(incomingBuff)
                val buff = ByteArray(bytes!!)
                System.arraycopy(incomingBuff, 0, buff, 0, bytes)
                val message = String(buff, StandardCharsets.UTF_8)
                UnityPlayer.UnitySendMessage(gameObject, method, message)
            }
        }
    }

    fun closeSocket() {
        try {
            socket?.close()
        } catch (e: IOException) {

        }
    }
}
