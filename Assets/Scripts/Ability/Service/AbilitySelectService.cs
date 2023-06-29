using System;

namespace Ability.Service
{
    public class AbilitySelectService
    {
        public Ability SelectedAbility { get; private set; }
        public event Action<Ability> OnSelectedChanged;
        public void Select(Ability ability)
        {
            SelectedAbility = ability;
            OnSelectedChanged?.Invoke(SelectedAbility);
        }
    }
}
