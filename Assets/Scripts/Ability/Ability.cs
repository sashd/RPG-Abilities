using Ability.Model;
using UnityEngine;
using Util;

namespace Ability
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] private string _id;
        public string Id => _id;
        public bool Locked => _model.State == AbilityState.Locked;
        public bool IsBase => _model.IsBase;

        public AbilityModel Model => _model;
        private AbilityModel _model;

        public void Init(AbilityModel model)
        {
            _model = model;
            var components = GetComponentsInChildren<IInitializable<Ability>>();
            foreach (var component in components)
            {
                component.Init(this);
            }
        }
        public void SetState(AbilityState state)
        {
            _model.State = state;
        }
    }
}
