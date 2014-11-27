namespace ComputersBuildingSystemCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class HardDrive : StorageProvider
    {
        public HardDrive(int capacity)
            : base(capacity)
        {
            this.Capacity = capacity;
        }

        public HardDrive(int capacity, IDictionary<int, string> storedData)
            : base(capacity, storedData)
        {
            this.Capacity = capacity;
        }

        public override void AddStorageDrive(StorageProvider drive)
        {
            throw new InvalidOperationException("Cannot add to a hard drive.");
        }

        public override void RemoveStorageDrive(StorageProvider drive)
        {
            throw new InvalidOperationException("Cannot remove from a hard drive.");
        }

        public override void StoreData(int address, string data)
        {
             if (address < 0 ||
                string.IsNullOrWhiteSpace(data))
             {
                 throw new ArgumentException("Invalid data or address.");
             }

             this.StoredData.Add(address, data);
        }

        public override string LoadStoredData(int address)
        {
            if (this.StoredData.ContainsKey(address) == false)
            {
                throw new ArgumentException("Invalid address.");
            }

            return this.StoredData[address];
        }
    }
}