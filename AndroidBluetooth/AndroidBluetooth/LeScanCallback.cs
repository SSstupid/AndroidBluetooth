using Android.Bluetooth.LE;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBluetooth;

public class LeScanCallback : ScanCallback
{
    public event EventHandler<ScanResult> DeviceResult;

    public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
    {
        base.OnScanResult(callbackType, result);
        DeviceResult?.Invoke(this, result);
    }
}
