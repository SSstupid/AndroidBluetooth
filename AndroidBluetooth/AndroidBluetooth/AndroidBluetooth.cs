using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Java.Lang;
using static Android.Bluetooth.BluetoothAdapter;
using Application = Android.App.Application;

namespace AndroidBluetooth;

public class AndroidBluetooth
{
    //BluetoothManager
    private LeScanCallback _leScanCallback = new();
    private static long SCAN_PERIOD = 10000;

    public BluetoothManager bluetoothManager;
    public BluetoothAdapter bluetoothAdapter;
    public BluetoothLeScanner bluetoothLeScanner;
    private Handler handler = new();

    public event EventHandler<ScanResult> OnScanResultHandler;

    public bool scanning;

    public AndroidBluetooth()
    {
        bluetoothManager = (BluetoothManager)Application.Context.GetSystemService(Context.BluetoothService);
        bluetoothAdapter = bluetoothManager.Adapter;
        bluetoothLeScanner = bluetoothAdapter.BluetoothLeScanner;
        _leScanCallback.DeviceResult += OnScanResultHandler;

        if (bluetoothAdapter == null)
        {
            // Device doesn't support Bluetooth
        }

        if (bluetoothAdapter.Enable() == false)
        {
            // REQUEST_ENABLE_BT는 0보다 크면 되는 듯 합니다.
            // UI에 블루투스를 활성화 할 것인가를 사용자에 묻는 코드입니다.
            // 이러한 경우 Android에 종속적인 코드로 짜는 것보다 MAUI의 UI를 이용하는 것이 Cross-Platform에 좋습니다. 
            Int32 REQUEST_ENABLE_BT = 1;
            Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
            Platform.CurrentActivity.StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
        }
    }

    // Stops scanning after 10 seconds.
    public void ScanLeDevice()
    {
        Action StopScanAction = () =>
        {
            scanning = false;
            bluetoothLeScanner.StopScan(_leScanCallback);
        };

        if (!scanning)
        {
            // Stops scanning after a predefined scan period.
            handler.PostDelayed(StopScanAction, SCAN_PERIOD);

            scanning = true;
            bluetoothLeScanner.StartScan(_leScanCallback);
        }
        else
        {
            scanning = false;
            bluetoothLeScanner.StopScan(_leScanCallback);
        }
    }
}
