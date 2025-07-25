﻿using Fluxor;
using GeoDroid.Data.SQL;
using Microsoft.JSInterop;
using SQLite;
using System.Runtime.Serialization;




namespace GEO_DROID.Store.Bluetooth
{
    public class BluetoothEffects
    {
        private IJSRuntime _JS;
        private readonly GeoDroidDatabase _database;

        public BluetoothEffects(IJSRuntime JS, GeoDroidDatabase database)
        {
            _JS = JS;
            _database = database;
        }


        [EffectMethod]
        public async Task SaveBluetoothDevice(SaveBluetoothDevice action, Fluxor.IDispatcher dispatcher)
        {
            try
            {
                BluetoothDevice current = await _database._database.Table<BluetoothDevice>().Where(d => d.DeviceName == action.device.DeviceName).FirstOrDefaultAsync();

                if (current is null)
                {
                    BluetoothDevice device = new BluetoothDevice();

                    device.Address = action.device.DeviceAddress.ToUInt64();
                    device.DeviceName = action.device.DeviceName;
                    device.ConnectionTime = DateTime.Now;

                    await _database.InsertAsync(device);
                }
                else
                {
                    await _database.UpdateAsync(current);
                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        [EffectMethod]
        public async Task SaveBluetoothDeviceBase(SaveBluetoothDeviceBase action, Fluxor.IDispatcher dispatcher)
        {

            action.device.ConnectionTime = DateTime.Now;

            BluetoothDevice current = await _database._database.Table<BluetoothDevice>().Where(d => d.DeviceName == action.device.DeviceName).FirstOrDefaultAsync();

            if (current is not null)
            {
                if (current.ID != 0)
                {
                    await _database.InsertAsync(action.device);
                }
                else
                {
                    await _database.UpdateAsync(action.device);
                }
            }
            else
            {

                await _database.InsertAsync(action.device);
            }

        }

        [EffectMethod]
        public async Task LoadLastBluetoothDevice(LoadLastBluetoothDevice action, Fluxor.IDispatcher dispatcher)
        {


            BluetoothDevice last = await _database._database.Table<BluetoothDevice>().OrderBy(d => d.ConnectionTime).FirstOrDefaultAsync();

            List<BluetoothDevice> lel = await _database._database.Table<BluetoothDevice>().ToListAsync();

            dispatcher.Dispatch(new ChangeConectedDevice(last));

        }

        [EffectMethod]
        public async Task LimpiarHistorialBluetoothDevices(LimpiarHistorialBluetoothDevices action, Fluxor.IDispatcher dispatcher)
        {
            List<BluetoothDevice> last = await _database._database.Table<BluetoothDevice>().ToListAsync();

            foreach (BluetoothDevice item in last)
            {
                await _database.DeleteAsync(item);
            }
            dispatcher.Dispatch(new SetBluetoothDeviceHistorial(null));
        }

        [EffectMethod]
        public async Task LimpiarHistorialBluetoothDevice(LimpiarHistorialBluetoothDevice action, Fluxor.IDispatcher dispatcher)
        {


            BluetoothDevice last = await _database._database.Table<BluetoothDevice>().Where(db => db.ID == action.device.ID).FirstOrDefaultAsync();

            if (last is not null)
            {
                await _database._database.DeleteAsync(last);
            }

            dispatcher.Dispatch(new LoadBluetoothDeviceHistorial());

        }

        [EffectMethod]
        public async Task LoadBluetoothDeviceHistorial(LoadBluetoothDeviceHistorial action, Fluxor.IDispatcher dispatcher)
        {

            List<BluetoothDevice> last = await _database._database.Table<BluetoothDevice>().OrderBy(d => d.ConnectionTime).ToListAsync();

            dispatcher.Dispatch(new SetBluetoothDeviceHistorial(last));

        }

    }


    public class BluetoothDevice
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int ID { get; set; }
        public ulong Address { get; set; }

        public string DeviceName { get; set; }

        public DateTime ConnectionTime { get; set; }

        public BluetoothDevice() { }

    }


}
