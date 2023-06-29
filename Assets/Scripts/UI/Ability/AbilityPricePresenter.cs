using Ability.Service;
using UnityEngine;
using Zenject;

namespace UI.Ability
{
    public class AbilityPricePresenter : MonoBehaviour
    {
        [Inject] private AbilitySelectService _selectService;
        [SerializeField] private AbilityPriceView _view;

        private void Start()
        {
            _selectService.OnSelectedChanged += OnSelectedChanged;
        }

        private void OnSelectedChanged(global::Ability.Ability ability) => _view.UpdateCount(ability ? ability.Model.Price : 0);
        
        private void OnDestroy()
        {
            _selectService.OnSelectedChanged -= OnSelectedChanged;
        }
    }
}
