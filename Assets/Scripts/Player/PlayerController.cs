using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// PlayerController for players.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform m_leftHand;
        [SerializeField] private Transform m_rightHand;
        [SerializeField] private Transform m_dropPosition;

        private KeyboardController m_keyboardController;
        private MouseController m_mouseController;
        private ItemController m_itemController;

        private void Awake()
        {
            m_itemController = new(m_leftHand, m_rightHand, m_dropPosition);
            m_keyboardController = new(transform, Camera.main, GetComponent<ItemDetector>(), m_itemController);
            m_mouseController = new(transform);
        }

        private void Update()
        {
            ///CaseNote: Update our controllers.
            m_keyboardController.UpdateState();
            m_mouseController.UpdateState();
        }
    }
}