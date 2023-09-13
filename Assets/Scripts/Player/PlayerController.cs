using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// PlayerController for players.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private KeyboardController m_keyboardController;
        private MouseController m_mouseController;

        private void Awake()
        {
            m_keyboardController = new(transform);
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