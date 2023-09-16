using IndividualGames.UniPoly.Multiplayer;
using IndividualGames.UniPoly.Player;
using IndividualGames.UniPoly.SceneManagement;
using TMPro;
using UnityEngine;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// UI element callback handler.
    /// </summary>
    public class CanvasController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private SceneController m_sceneController;

        [SerializeField] private TextMeshProUGUI m_playerLabel;
        [SerializeField] private TextMeshProUGUI m_itemPickUpLabel;

        public static readonly int Hash = nameof(CanvasController).GetHashCode();

        private void Awake()
        {
            m_sceneController.Register(Hash, this);

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