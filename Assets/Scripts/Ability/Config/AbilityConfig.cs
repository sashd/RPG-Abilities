using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ability.Config
{
    [CreateAssetMenu(fileName = "AbilityConfigs", menuName = "ScriptableObjects/AbilityConfigs")]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityData> _data;

        public List<AbilityData> Data => _data;
    }

    [Serializable]
    public class AbilityData
    {
        public string Id;
        public int Price;
        public bool IsBase;
        public List<string> LinkedAbilities;
    }
}

