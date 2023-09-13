using IndividualGames.UniPoly.Multiplayer;
using TMPro;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// UI element callback handler.
    /// </summary>
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_playerLabel;
        [SerializeField] private TextMeshProUGUI m_resoureLabel;
        //[SerializeField] private UnitSignalHub m_unitSignalHub;
        [SerializeField] private GameObject m_menuFrame;


        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            m_menuFrame.SetActive(true);
            PhotonController.JoinedRoom.Connect(PlayerJoined);
            //m_unitSignalHub.ConnectToHub(ResourceCollected);
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
            m_menuFrame.SetActive(false);
            m_playerLabel.text = $"Player {PhotonController.PlayerNumber}";
            //OptimizationController.Instance.EnableGameElements();
        }


        public void ResourceCollected(int a_resourceCount)
        {
            m_resoureLabel.text = $"Wood: {a_resourceCount}";
        }
    }
}