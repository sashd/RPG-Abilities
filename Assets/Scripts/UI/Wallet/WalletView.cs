using TMPro;
using UnityEngine;

namespace UI.Wallet
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void UpdateCount(int count) => _text.text = string.Format("Points: {0}", count);
    }
}
