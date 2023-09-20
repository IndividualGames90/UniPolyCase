using IndividualGames.UniPoly.Utils;
using System;
using System.Collections;
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

        /// CaseNote: IndividualGames.Codebase library have an external coroutine call system for it's Controller system,
        /// this is a primitive and dependent version of that. That system is too complicated for this case's scope.
        /// <summary> Inner classes should emit this and pass their method for a coroutine call. Methods should also return
        /// a boolean as their satisfying condition to stop the coroutine.</summary>
        private BasicSignal<Func<bool>, WaitForSeconds> m_coroutineCaller = new();

        public void Init()
        {
            m_itemController = new(m_leftHand,
                                   m_rightHand,
                                   m_dropPosition);

            m_keyboardController = new(transform,
                                       Camera.main,
                                       GetComponent<ItemDetector>(),
                                       m_itemController,
                                       GetComponent<ActivatableDetector>().ActivatableDetected,
                                       GetComponent<CharacterController>(),
                                       m_coroutineCaller);

            m_mouseController = new(transform);

            m_coroutineCaller.Connect(RunCoroutine);

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

        /// <summary> Coroutine call for caller. </summary>
        private void RunCoroutine(Func<bool> a_methodToRun, WaitForSeconds a_wait)
        {
            StartCoroutine(RunMethod(a_methodToRun, a_wait));
        }

        /// <summary> Run a method inside coroutine using it's wait. </summary>
        private IEnumerator RunMethod(Func<bool> a_methodToRun, WaitForSeconds a_wait)
        {
            var condition = true;
            while (condition)
            {
                condition = a_methodToRun();
                yield return a_wait;
            }
        }
    }
}