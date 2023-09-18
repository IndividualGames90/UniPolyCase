using IndividualGames.UniPoly.Multiplayer;
using IndividualGames.UniPoly.Player;
using IndividualGames.UniPoly.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// UI element callback handler.
    /// </summary>
    public class CanvasController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private TextMeshProUGUI m_playerLabel;
        [SerializeField] private TextMeshProUGUI m_itemPickUpLabel;
        [SerializeField] private GameObject m_connectButton;

        public static readonly int Hash = nameof(CanvasController).GetHashCode();

        private void Awake()
        {
            SceneController.Instance.Register(Hash, this);

            PhotonController.JoinedRoom.Connect(PlayerJoined);
            GetComponent<CanvasEventHub>().ItemDetected.Connect(ItemPickUpUpdated);
        }

        /// <summary> Quit application. </summary>
        public void ExitGame()
        {
            Debug.Log("App Exiting.");
            PhotonController.Disconnect();
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }

        /// <summary> Player Joined game. </summary>
        public void PlayerJoined()
        {
            m_playerLabel.text = $"Player {PhotonController.PlayerNumber}";
            m_connectButton.SetActive(false);

        }

        /// <summary> Item pick up state changed. </summary>
        public void ItemPickUpUpdated(bool a_detected)
        {
            m_itemPickUpLabel.gameObject.SetActive(a_detected);
        }
    }
}