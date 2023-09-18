using IndividualGames.UniPoly.UI;
using IndividualGames.UniPoly.Utils;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IndividualGames.UniPoly.Multiplayer
{
    /// <summary>
    /// Core Photon functionality and encapsulation.
    /// </summary>
    public class PhotonController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private CanvasEventHub m_canvasEventHub;

        public static int PlayerNumber => PhotonNetwork.LocalPlayer.ActorNumber;

        public readonly static BasicSignal NetworkConnecting = new();
        public readonly static BasicSignal JoinedLobby = new();
        public readonly static BasicSignal JoiningLobby = new();
        public readonly static BasicSignal JoinedRoom = new();

        private const string c_roomName = "UniPoly.ServerRoom01";

        private bool m_networkConnectingInProgress = false;

        private WaitForSeconds m_waitNetworkConnecting = new(1f);

        void Awake()
        {
            m_canvasEventHub.JoinGame.Connect(StartNetworkConnecting);
        }

        /// <summary> Try to join a Photon lobby. </summary>
        private void StartNetworkConnecting()
        {
            if (!m_networkConnectingInProgress)
            {
                m_networkConnectingInProgress = true;
                PhotonNetwork.ConnectUsingSettings();
                NetworkConnecting.Emit();
            }
        }

        /// <summary> Room has reached it's full player count. </summary>
        public static bool RoomIsMaxxed()
        {
            return PhotonNetwork.CurrentRoom.PlayerCount > PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        public override void OnJoinedLobby()
        {
            JoinedLobby.Emit();
            PhotonNetwork.JoinOrCreateRoom(c_roomName,
                                           new RoomOptions
                                           {
                                               MaxPlayers = 4,
                                               IsOpen = true,
                                               IsVisible = true
                                           },
                                           TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            if (RoomIsMaxxed())
            {
                OnJoinRandomFailed(0, "Room is full.");
                OnLeftRoom();
            }
            else
            {
                JoinedRoom.Emit();
            }
        }

        public override void OnConnectedToMaster()
        {
            m_networkConnectingInProgress = false;
            JoinLobby();
        }

        private void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
            JoiningLobby.Emit();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError($"{returnCode} {message}");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"{returnCode} {message}");
        }

        /// <summary> Disconnect from Photon Network. </summary>
        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        /// <summary> Room is Left. </summary>
        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveLobby();
        }

        /// <summary> Transfer ownership to local player. </summary>
        public static void TransferOwnershipToLocal(PhotonView a_photonView)
        {
            if (!a_photonView.IsMine)
            {
                a_photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }
    }
}