namespace ComputersBuildingSystemCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Raid : StorageProvider
    {
        private readonly ICollection<StorageProvider> drives;

        public Raid(int capacity, ICollection<StorageProvider> drives)
            : this(capacity, drives, new Dictionary<int, string>())
        {
        }

        public Raid(int capacity, ICollection<StorageProvider> drives, IDictionary<int, string> storedData)
            : base(capacity, storedData)
        {
            this.drives = drives;
        }

        public override void AddStorageDrive(StorageProvider drive)
        {
            if (drive == null)
            {
                throw new ArgumentNullException("Drive to add cannot be null.");
            }

            this.drives.Add(drive);
        }

        public override void RemoveStorageDrive(StorageProvider drive)
        {
            if (drive == null)
            {
                throw new ArgumentNullException("Drive to remove cannot be null.");
            }

            this.drives.Remove(drive);
        }

        public override void StoreData(int address, string data)
        {
            foreach (var drive in this.drives)
            {
                drive.StoreData(address, data);
            }
        }

        public override string LoadStoredData(int address)
        {
            var firstDrive = this.drives.FirstOrDefault();

            if (firstDrive == null)
            {
                throw new InvalidOperationException("Could not load data.");
            }

            var storedData = firstDrive.LoadStoredData(address);

            return storedData;
        }
    }
}
