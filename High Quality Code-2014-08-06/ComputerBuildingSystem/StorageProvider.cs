namespace ComputersBuildingSystemCore
{
    using System;
    using System.Collections.Generic;

    internal abstract class StorageProvider
    {
        private IDictionary<int, string> storedData;
        private int capacity;

        public StorageProvider(int capacity)
            : this(capacity, new Dictionary<int, string>())
        {
            this.Capacity = capacity;
        }

        public StorageProvider(int capacity, IDictionary<int, string> storedData)
        {
            this.StoredData = storedData;
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity cannot be 0 or less.");
                }

                this.capacity = value;
            }
        }

        protected IDictionary<int, string> StoredData
        {
            get
            {
                return this.storedData;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Stored data cannot be null.");
                }

                this.storedData = value;
            }
        }

        public abstract void AddStorageDrive(StorageProvider drive);

        public abstract void RemoveStorageDrive(StorageProvider drive);

        public abstract void StoreData(int address, string data);

        public abstract string LoadStoredData(int address);
    }
}
