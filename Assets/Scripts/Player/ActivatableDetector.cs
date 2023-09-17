using IndividualGames.UniPoly.GameElements;
using IndividualGames.UniPoly.Utils;
using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Detects if a trigger of Activatable in entered.
    /// </summary>
    public class ActivatableDetector : MonoBehaviour
    {
        public readonly BasicSignal<(bool, IActivateable)> ActivatableDetected = new();

        private void OnTriggerEnter(Collider other)
        {
            DetectionCheck(true, other);
        }

        private void OnTriggerExit(Collider other)
        {
            DetectionCheck(false, other);
        }

        /// <summary> Check for activatable detection and respond. </summary>
        private void DetectionCheck(bool a_enter, Collider a_other)
        {
            var activatable = a_other.GetComponent<IActivateable>();
            if (activatable != null)
            {
                ActivatableDetected.Emit((a_enter, activatable));
            }
        }
    }
}