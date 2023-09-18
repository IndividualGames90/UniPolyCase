using IndividualGames.UniPoly.SceneManagement;
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

        private void Awake()
        {
            var canvasController = (CanvasController)SceneController.Instance.Retrieve(CanvasController.Hash);

            ItemDetected.Connect(canvasController.GetComponent<CanvasEventHub>().OnItemDetected);
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