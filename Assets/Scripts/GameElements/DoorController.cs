using DG.Tweening;
using IndividualGames.UniPoly.Multiplayer;
using Photon.Pun;
using UnityEngine;

namespace IndividualGames.UniPoly.GameElements
{
    /// <summary>
    /// Activateable door component.
    /// </summary>
    public class DoorController : MonoBehaviour, IActivateable
    {
        [SerializeField, Range(-180, 180)] private float m_doorOpenAngle = 45;
        [SerializeField] private GameObject m_door;
        [SerializeField] private GameObject m_useLabel;

        public bool ActivationState { get; set; } = false;

        private Quaternion m_originalRotation;
        private float m_doorOpenSpeed = 1f;

        private void Awake()
        {
            m_originalRotation = transform.rotation;

            var playerDetector = GetComponent<PlayerDetector>();
            playerDetector.TriggerEntered.Connect(() => m_useLabel.SetActive(true));
            playerDetector.TriggerExited.Connect(() => m_useLabel.SetActive(false));
        }

        /// <summary> Show or hide use label. </summary>
        public void UseLabelVisibilityUpdate(bool a_visible)
        {
            m_useLabel.SetActive(a_visible);
        }

        /// <summary> Activate door functionality. </summary>
        public void Activate()
        {
            PhotonController.TransferOwnershipToLocal(GetComponent<PhotonView>());
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(0, m_doorOpenAngle, 0);
            transform.DORotate(rotation.eulerAngles, m_doorOpenSpeed);
            ActivationState = true;
        }

        /// <summary> Deactivate door functionality. </summary>
        public void Deactivate()
        {
            PhotonController.TransferOwnershipToLocal(GetComponent<PhotonView>());
            transform.DORotate(m_originalRotation.eulerAngles, m_doorOpenSpeed);
            ActivationState = false;
        }
    }
}