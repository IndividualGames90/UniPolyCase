using IndividualGames.UniPoly.SceneManagement;
using IndividualGames.UniPoly.Utils;
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

        public static int PlayerHash = nameof(PlayerController).GetHashCode();

        [HideInInspector] public bool BlockPlayerControls = false;

        private KeyboardController m_keyboardController;
        private MouseController m_mouseController;
        private ItemController m_itemController;


        public void Init()
        {
            GameObject.FindGameObjectWithTag(Tags.SceneController).GetComponent<SceneController>().Register(PlayerHash, this);

            m_itemController = new(m_leftHand, m_rightHand, m_dropPosition);
            m_keyboardController = new(transform, Camera.main, GetComponent<ItemDetector>(), m_itemController);
            m_mouseController = new(transform);
        }

        private void Update()
        {
            if (!BlockPlayerControls)
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