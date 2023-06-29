using Ability.Component;
using Ability.Config;
using Ability.Service;
using UnityEngine;
using UnityEngine.UI;
using Wallet.Service;
using Zenject;

namespace UI.Ability
{
    public class AbilitiesScreen : MonoBehaviour
    {
        [Inject] private AbilityService _abilityService;
        [Inject] private WalletService _wallet;
        [Inject] private AbilitySelectService _selectService;

        [SerializeField] private AbilityConfig _config;
        [SerializeField] private AbilityLinksVisualizer _linksVisualizer;
        [Header("Buttons")]
        [SerializeField] private Button _getPointsButton;
        [SerializeField] private Button _openAbilityButton;
        [SerializeField] private Button _restoreAbilityButton;
        [SerializeField] private Button _restoreAllAbilitiesButton;

        private void Awake()
        {
            InitButtons();
            OnSelectedChanged(null);
            var abilities = GetComponentsInChildren<global::Ability.Ability>();
            _abilityService.Init(abilities, _config.Data);
            _linksVisualizer.Visualize(_abilityService.AbilityGraph);
            _selectService.OnSelectedChanged += OnSelectedChanged;
            _wallet.OnPointsChanged += OnPointsChanged;
        }

        private void InitButtons()
        {
            _getPointsButton.onClick.AddListener(AddPoints);
            _openAbilityButton.onClick.AddListener(OpenAbility);
            _restoreAbilityButton.onClick.AddListener(RestoreAbility);
            _restoreAllAbilitiesButton.onClick.AddListener(RestoreAll);
        }

        private void OnSelectedChanged(global::Ability.Ability ability)
        {
            _openAbilityButton.interactable = _abilityService.CanBeOpened(ability);
            _restoreAbilityButton.interactable = _abilityService.CanBeRestored(ability);
        }

        private void OnPointsChanged(int _) => OnSelectedChanged(_selectService.SelectedAbility);
        private void AddPoints() => _wallet.AddPoints(1);
        private void OpenAbility() => _abilityService.OpenAbility(_selectService.SelectedAbility);
        private void RestoreAbility()
        {
            _abilityService.RestoreAbility(_selectService.SelectedAbility);
            _selectService.Select(null);
        }
        private void RestoreAll()
        {
            _abilityService.RestoreAll();
            _selectService.Select(null);
        }

        private void OnDestroy()
        {
            _wallet.OnPointsChanged -= OnPointsChanged;
            _selectService.OnSelectedChanged -= OnSelectedChanged;
            _getPointsButton.onClick.RemoveAllListeners();
            _openAbilityButton.onClick.RemoveAllListeners();
            _restoreAbilityButton.onClick.RemoveAllListeners();
            _restoreAllAbilitiesButton.onClick.RemoveAllListeners();
        }
    }
}

