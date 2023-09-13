using IndividualGames.UniPoly.Multiplayer;
using TMPro;
using UnityEngine;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// UI element callback handler.
    /// </summary>
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_playerLabel;
        [SerializeField] private TextMeshProUGUI m_itemPickUpLabel;


        private void Awake()
        {
            PhotonController.JoinedRoom.Connect(PlayerJoined);
            GetComponent<CanvasEventHub>().ItemDetected.Connect(ItemPickUpUpdated);
        }


        public void ExitGame()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("App Exiting.");
                PhotonController.Disconnect();
                Application.Quit();
            }
        }


        public void PlayerJoined()
        {
            m_playerLabel.text = $"Player {PhotonController.PlayerNumber}";
        }


        public void ItemPickUpUpdated(bool a_detected)
        {
            m_itemPickUpLabel.gameObject.SetActive(a_detected);
        }
    }
}