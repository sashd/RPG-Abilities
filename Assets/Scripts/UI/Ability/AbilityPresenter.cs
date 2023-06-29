using Ability.Service;
using UnityEngine;
using Util;
using Zenject;

namespace UI.Ability
{
    public class AbilityPresenter : MonoBehaviour, IInitializable<global::Ability.Ability>
    {
        [Inject] private AbilitySelectService _selectService;

        [SerializeField] private AbilityView _view;
        private global::Ability.Ability _owner;

        public void Init(global::Ability.Ability owner)
        {
            _owner = owner;
            _view.Init(_owner.Model, OnClick);
            _selectService.OnSelectedChanged += OnSelect;
        }

        private void OnSelect(global::Ability.Ability selected) 
        {
            _view.SetSelected(selected == _owner);
        }

        private void OnClick() 
        {
            _selectService.Select(_owner);
        }
    }
}
