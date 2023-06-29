using System.Collections.Generic;

namespace Ability.Model
{
    public class AbilityGraph
    {
        private readonly Dictionary<string, LinkedList<string>> _nodes;
        public Dictionary<string, LinkedList<string>> Nodes => _nodes;

        public AbilityGraph(IEnumerable<Ability> abilities)
        {
            _nodes = new Dictionary<string, LinkedList<string>>();
            foreach (var ability in abilities)
            {
                foreach (var linkedAbility in ability.Model.LinkedAbilities)
                {
                    AddEdge(ability.Model.Id, linkedAbility);
                }
            }
        }

        private void AddEdge(string from, string to)
        {
            if (!_nodes.ContainsKey(from))
                _nodes[from] = new LinkedList<string>();
            if (!_nodes.ContainsKey(to))
                _nodes[to] = new LinkedList<string>();

            _nodes[from].AddLast(to);
            _nodes[to].AddLast(from);
        }
    }
}
