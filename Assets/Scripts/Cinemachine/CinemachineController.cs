using Cinemachine;
using IndividualGames.UniPoly.Multiplayer;
using UnityEngine;

namespace IndividualGames.UniPoly.Cinemachine
{
    /// <summary>
    /// Blend controller for cinemachine cameras.
    /// </summary>
    public class CinemachineController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_vcamMenu;
        [SerializeField] private CinemachineVirtualCamera m_vcamOverworld;


        private void Awake()
        {
            PhotonController.JoinedRoom.Connect(SwitchPriority);
        }

        /// <summary> Switch to overworld camera. </summary>
        public void SwitchPriority()
        {
            m_vcamMenu.Priority = 0;
            m_vcamOverworld.Priority = 1;
        }
    }
}