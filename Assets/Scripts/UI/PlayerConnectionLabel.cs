using IndividualGames.UniPoly.Multiplayer;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// Controls the label denoting player connection status.
    /// </summary>
    public class PlayerConnectionLabel : MonoBehaviour
    {
        private enum Status
        {
            Connecting,
            Connected,
            Hidden
        }

        private TextMeshProUGUI m_label;
        private Status m_currentStatus;
        private readonly WaitForSeconds m_waitConnectingUpdate = new(.1f);

        private StringBuilder m_stringBuilder = new();
        private const string c_stringDefault = "Player ";
        private const string c_stringDot = ".";
        private const string c_stringConnected = "Connected";
        private const string c_stringConnecting = "Connecting";
        private const string c_stringNetworkConnecting = "Network Connecting...";

        private bool m_connectingLocked = false;

        private void Awake()
        {
            m_label = GetComponent<TextMeshProUGUI>();

            StatusHidden();

            PhotonController.JoinedLobby.Connect(StatusConnected);
            PhotonController.JoiningLobby.Connect(StatusConnecting);
            PhotonController.NetworkConnecting.Connect(NetworkConnecting);
        }

        /// <summary> Player is connected. </summary>
        private void StatusConnected()
        {
            StopAllCoroutines();
            m_stringBuilder.Clear();
            m_stringBuilder.Append(c_stringDefault);
            m_stringBuilder.Append(c_stringConnected);
            m_label.text = m_stringBuilder.ToString();

            m_currentStatus = Status.Connected;
        }

        /// <summary> Hide the text entirely. </summary>
        private void StatusHidden()
        {
            StopAllCoroutines();
            m_label.text = string.Empty;
            m_currentStatus = Status.Hidden;
        }

        /// <summary> Player is currently connecting. </summary>
        private void StatusConnecting()
        {
            m_currentStatus = Status.Connecting;
            if (!m_connectingLocked)
            {
                StartCoroutine(Connecting());
            }
        }

        /// <summary> Update label during connecting with text based animation. </summary>
        private IEnumerator Connecting()
        {
            m_connectingLocked = true;

            while (m_currentStatus == Status.Connecting)
            {
                m_stringBuilder.Clear();
                m_stringBuilder.Append(c_stringDefault);
                m_stringBuilder.Append(c_stringConnecting);
                m_label.text = m_stringBuilder.ToString();

                yield return m_waitConnectingUpdate;

                for (int i = 0; i < 3; i++)
                {
                    UpdateLabelStringForConnecting();
                    yield return m_waitConnectingUpdate;
                }
            }
        }

        /// <summary> Update label string for connecting iteration.</summary>
        private void UpdateLabelStringForConnecting()
        {
            m_stringBuilder.Append(c_stringDot);
            m_label.text = m_stringBuilder.ToString();
        }

        private void NetworkConnecting()
        {
            StopAllCoroutines();
            m_stringBuilder.Clear();
            m_stringBuilder.Append(c_stringNetworkConnecting);
            m_label.text = m_stringBuilder.ToString();
        }
    }
}
