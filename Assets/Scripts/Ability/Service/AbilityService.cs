using Ability.Config;
using Ability.Model;
using ModestTree;
using System.Collections.Generic;
using Wallet.Service;
using Zenject;

namespace Ability.Service
{
    public class AbilityService
    {
        [Inject] private WalletService _wallet;

        public AbilityGraph AbilityGraph { get; private set; }
        private Dictionary<string, Ability> _abilityDictionary = new();

        public void Init(IEnumerable<Ability> abilities, List<AbilityData> data)
        {
            foreach (var ability in abilities)
            {
                ability.Init(new AbilityModel(data.Find(it => it.Id == ability.Id)));
                _abilityDictionary.Add(ability.Id, ability);
            }
            AbilityGraph = new AbilityGraph(abilities);
        }

        public Ability GetAbilityById(string id)
        {
            if (!_abilityDictionary.ContainsKey(id)) 
            {
                Assert.CreateException("Ability not found");
            }
            return _abilityDictionary[id];
        }

        public void OpenAbility(Ability ability)
        {
            if (!CanBeOpened(ability)) return;

            if (_wallet.TryRemove(ability.Model.Price))
                ability.SetState(AbilityState.Unlocked);
        }

        public void RestoreAbility(Ability ability)
        {
            if (!CanBeRestored(ability)) return;

            _wallet.AddPoints(ability.Model.Price);
            ability.SetState(AbilityState.Locked);
        }

        public void RestoreAll()
        {
            foreach (var node in AbilityGraph.Nodes)
            {
                var ability = GetAbilityById(node.Key);
                if (!ability.Locked)
                {
                    ability.SetState(AbilityState.Locked);
                    _wallet.AddPoints(ability.Model.Price);
                }
            }
        }

        public bool CanBeRestored(Ability ability)
        {
            if (!ability) return false;

            return NeigboursConnectedToBase(ability) && !ability.IsBase && !ability.Locked;
        }

        public bool CanBeOpened(Ability ability)
        {
            if (!ability) return false;

            return HasOpenedNeighbour(ability) && ability.Locked && _wallet.HasEnoughPoints(ability.Model.Price);
        }

        private bool HasOpenedNeighbour(Ability ability)
        {
            foreach (var neighbour in AbilityGraph.Nodes[ability.Id])
            {
                if (!GetAbilityById(neighbour).Locked)
                    return true;
            }
            return false;
        }

        private bool NeigboursConnectedToBase(Ability ability)
        {
            var startId = ability.Id;
            var openedAbilities = new LinkedList<string>();
            foreach (var item in AbilityGraph.Nodes[startId])
            {
                if (!GetAbilityById(item).Locked)
                    openedAbilities.AddLast(item);
            }
            foreach (var item in openedAbilities)
            {
                var has = HasBaseConnection(item, startId);
                if (!has) return false;
            }
            return true;
        }

        private bool HasBaseConnection(string startId, string excludeId)
        {
            if (GetAbilityById(startId).IsBase) return true;

            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            Stack<string> nodesToVisit = new Stack<string>();
            foreach (var node in AbilityGraph.Nodes)
                visited[node.Key] = false;

            foreach (var neighbour in AbilityGraph.Nodes[startId])
            {
                if (!GetAbilityById(neighbour).Locked && neighbour != excludeId)
                    nodesToVisit.Push(neighbour);
            }

            visited[startId] = true;
            while (nodesToVisit.Count > 0)
            {
                var item = nodesToVisit.Pop();
                if (GetAbilityById(item).IsBase) return true;
                if (visited[item]) continue;
                visited[item] = true;
                foreach (var child in AbilityGraph.Nodes[item])
                {
                    if (visited[child] || nodesToVisit.Contains(child) || GetAbilityById(item).Locked || child == excludeId)
                        continue;
                    if (GetAbilityById(child).IsBase)
                        return true;

                    nodesToVisit.Push(child);
                }
            }
            return false;
        }
    }
}
