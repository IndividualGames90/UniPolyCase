using IndividualGames.UniPoly.Utils;
using UnityEngine;

namespace IndividualGames.UniPoly.GameElements
{
    /// <summary>
    /// Detects if player has entered this trigger.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private Tags asd;

        public readonly BasicSignal TriggerEntered = new();
        public readonly BasicSignal TriggerExited = new();

        private void OnTriggerEnter(Collider a_other)
        {
            if (a_other.CompareTag(Tags.Player))
            {
                TriggerEntered.Emit();
            }
        }

        private void OnTriggerExit(Collider a_other)
        {
            if (a_other.CompareTag(Tags.Player))
            {
                TriggerExited.Emit();
            }
        }
    }
}