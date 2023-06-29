using TMPro;
using UnityEngine;

namespace UI.Ability
{
    public class AbilityPriceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void UpdateCount(int count) => _text.text = string.Format("Price: {0}", count);
    }
}
