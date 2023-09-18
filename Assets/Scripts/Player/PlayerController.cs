using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// PlayerController for players.
    /// </summary>
    public class PlayerController : MonoBehaviour, ILockable, IRegisterable
    {
        [SerializeField] private Transform m_leftHand;
        [SerializeField] private Transform m_rightHand;
        [SerializeField] private Transform m_dropPosition;

        [HideInInspector] public bool BlockPlayerControls = false;

        private KeyboardController m_keyboardController;
        private MouseController m_mouseController;
        private ItemController m_itemController;

        private bool m_initialized = false;

        public void Init()
        {
            m_itemController = new(m_leftHand,
                                   m_rightHand,
                                   m_dropPosition);

            m_keyboardController = new(transform,
                                       Camera.main,
                                       GetComponent<ItemDetector>(),
                                       m_itemController,
                                       GetComponent<ActivatableDetector>().ActivatableDetected);

            m_mouseController = new(transform);

            m_initialized = true;
        }

        private void Update()
        {
            if (!BlockPlayerControls && m_initialized)
            {
                ///CaseNote: Update our controllers.
                m_keyboardController.UpdateState();
                m_mouseController.UpdateState();
            }
        }

        public void Lock()
        {
            BlockPlayerControls = true;
        }

        public void Unlock()
        {
            BlockPlayerControls = false;
        }
    }
}