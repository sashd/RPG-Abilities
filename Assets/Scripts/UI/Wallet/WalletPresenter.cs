using UnityEngine;
using Zenject;
using Wallet.Service;

namespace UI.Wallet
{
    public class WalletPresenter : MonoBehaviour
    {
        [Inject] private WalletService _walletService;
        [SerializeField] private WalletView _view;

        private void Awake()
        {
            _walletService.OnPointsChanged += UpdateView;
        }

        private void UpdateView(int points) => _view.UpdateCount(points);

        private void OnDestroy()
        {
            _walletService.OnPointsChanged -= UpdateView;
        }
    }

}
