using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace HDC1000
{
    public class HDC1000
    {
        private I2cDevice _device;

        /// <summary>
        /// Genetate Device Info
        /// BusSpeed: Fast
        /// sharetingMode: Shared
        /// </summary>
        /// <returns>I2CDeviceSetting</returns>
        private I2cConnectionSettings GenerateI2CSetting()
        {
            var setting = new I2cConnectionSettings(0x40)
            {
                BusSpeed = I2cBusSpeed.FastMode,
                SharingMode = I2cSharingMode.Shared
            };
            return setting;
        }

        /// <summary>
        /// Get Device Data
        /// </summary>
        /// <returns>I2CDevice</returns>
        private async Task<I2cDevice> GetDeviceAsync()
        {
            var selecter = I2cDevice.GetDeviceSelector();
            var deviceInfo = await DeviceInformation.FindAllAsync(selecter);
            if (deviceInfo.Count == 0)
            {
                return null;
            }
            return _device = await I2cDevice.FromIdAsync(deviceInfo[0].Id, GenerateI2CSetting());
        }

        /// <summary>
        /// Initialize HDC1000 Library
        /// </summary>
        public async void InitHdc1000()
        {
            _device = await GetDeviceAsync();
            // get Temp and Hum Mode
            _device.Write(new byte[] { 0x02, 0x10, 0x00 });
        }

        /// <summary>
        /// Measurement HDC1000 sense 
        /// </summary>
        /// <returns>HDC1000 sense Value</returns>
        public async Task<HDC1000Data> Measurement()
        {
            _device.Write(new byte[] { 0x00 });
            await Task.Delay(TimeSpan.FromSeconds(10));
            var buf = new byte[4];
            _device.Read(buf);
            return new HDC1000Data
            {
                Temp = (buf[0] * 256.0 + buf[1]) / 65536 * 165.0 - 40.0,
                Hum = (buf[0] * 256.0 + buf[1]) / 65536 * 100.0
            };
        }
    }
}
