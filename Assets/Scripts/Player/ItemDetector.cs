using IndividualGames.UniPoly.UI;
using IndividualGames.UniPoly.Utils;
using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Trigger when item is contacted for pickups.
    /// </summary>
    public class ItemDetector : MonoBehaviour
    {
        public readonly BasicSignal<bool> ItemDetected = new();

        /// <summary> Item that is currently detected to grab. </summary>
        public GameObject DetectedItem => m_detectedItem;
        private GameObject m_detectedItem = null;

        [SerializeField] private CanvasEventHub canvasEventHub;


        private void Awake()
        {
            ItemDetected.Connect(canvasEventHub.OnItemDetected);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Item))
            {
                m_detectedItem = other.gameObject;
                ItemDetected.Emit(true);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Item))
            {
                m_detectedItem = null;
                ItemDetected.Emit(false);
            }
        }

        /// <summary> Emit to turn off labels upon grab. </summary>
        public void CurrentItemGrabbed()
        {
            if (m_detectedItem != null)
            {
                ItemDetected.Emit(false);
            }
        }
    }
}