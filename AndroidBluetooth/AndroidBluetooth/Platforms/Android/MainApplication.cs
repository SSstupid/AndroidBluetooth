using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Runtime;
using Microsoft.Maui.Platform;

namespace AndroidBluetooth;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
    }
    
    public void test()
    {
        //BluetoothManager
        BluetoothManager bluetoothManager = (BluetoothManager)Context.GetSystemService(Context.BluetoothService);
        BluetoothAdapter bluetoothAdapter = bluetoothManager.Adapter;
        if (bluetoothAdapter == null)
        {
            // Device doesn't support Bluetooth
        }

        if (bluetoothAdapter.Enable() == false)
        {
            Int32 REQUEST_ENABLE_BT = 1;
            Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
            Platform.CurrentActivity.StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
        }
    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
