using UnityEngine;
using Zenject;
using Ability.Service;
using Ability.Model;

namespace Ability.Component
{
    public class AbilityLinksVisualizer : MonoBehaviour
    {
        [Inject] private AbilityService _abilityService;

        [SerializeField] private LineRenderer _linePrefab;

        public void Visualize(AbilityGraph graph)
        {
            foreach (var node in graph.Nodes)
            {
                var fromPosition = _abilityService.GetAbilityById(node.Key).transform.position;
                foreach (var childNode in node.Value)
                {
                    var toPosition = _abilityService.GetAbilityById(childNode).transform.position;
                    CreateLine(fromPosition, toPosition);
                }
            }
        }

        private void CreateLine(Vector3 from, Vector3 to)
        {
            var line = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity, transform);
            line.SetPositions(new Vector3[] { from, to });
        }
    }
}

