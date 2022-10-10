using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace AndroidBluetooth;

public partial class MainViewModel : INotifyPropertyChanged
{
    public AndroidBluetooth androidBluetooth = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public string DeviceName { get; set; }

    public MainViewModel()
    {
        androidBluetooth.OnScanResultHandler += OnScanBluetooth;
    }

    [RelayCommand]
    private async void SearchAsync()
    {
        androidBluetooth.ScanLeDevice();
    }

    private async void OnScanBluetooth(object sender, ScanResult scanResult)
    {
        DeviceName = scanResult.Device.Name;
    }
}
