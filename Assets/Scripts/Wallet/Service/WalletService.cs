using System;

namespace Wallet.Service
{
    public class WalletService
    {
        public int CurrentPointCount
        {
            get => _count;
            private set
            {
                _count = value;
                OnPointsChanged?.Invoke(_count);
            }
        }

        private int _count = 0;
        public event Action<int> OnPointsChanged;

        public void AddPoints(int amount)
        {
            TryChange(amount);
        }

        public bool HasEnoughPoints(int amount) 
        {
            return CurrentPointCount >= amount;
        }

        public bool TryRemove(int amount)
        {
            return TryChange(-amount);
        }

        private bool TryChange(int delta)
        {
            var amount = CurrentPointCount;
            if (amount + delta < 0) 
                return false;
            CurrentPointCount = amount + delta;
            return true;
        }
    }
}
