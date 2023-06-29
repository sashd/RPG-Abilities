using Ability.Config;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Ability.Model
{
    public enum AbilityState
    {
        Locked,
        Unlocked
    }

    public class AbilityModel
    {
        public string Id { get; private set; }
        public int Price { get; private set; }
        public bool IsBase { get; private set; }

        public List<string> LinkedAbilities;

        public Action<AbilityState> OnStateChanged;
        private AbilityState _currentState;

        public AbilityState State
        {
            get => _currentState;
            set
            {
                if (IsBase)
                    return;

                _currentState = value;
                OnStateChanged?.Invoke(value);
            }
        }

        public AbilityModel(AbilityData data) 
        {
            Id = data.Id;
            Price = data.Price;
            LinkedAbilities = data.LinkedAbilities;
            IsBase = data.IsBase;
            _currentState = IsBase ? AbilityState.Unlocked : AbilityState.Locked;
        }
    }
}
