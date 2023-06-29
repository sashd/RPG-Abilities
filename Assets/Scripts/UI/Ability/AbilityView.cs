using Ability.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.Ability
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        public Action OnClick;

        private AbilityModel _model;

        public void Init(AbilityModel model, Action OnClick)
        {
            _model = model;
            _nameText.text = model.Id;
            SetStateColor(model.State);
            this.OnClick = OnClick;
            _button.onClick.AddListener(OnButtonClick);
            model.OnStateChanged += OnStateChanged;
        }

        public void SetSelected(bool selected)
        {
            if (!selected)
            {
                SetStateColor(_model.State);
                return;
            }
            _background.color = Color.gray;
        }

        private void OnStateChanged(AbilityState state) => SetStateColor(state);
        
        private void SetStateColor(AbilityState state)
        {
            switch (state)
            {
                case AbilityState.Locked:
                    _background.color = Color.black;
                    break;
                case AbilityState.Unlocked:
                    _background.color = Color.green;
                    break;
            }
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
