using IndividualGames.UniPoly.GameElements;
using IndividualGames.UniPoly.Utils;
using System;
using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Handles keyboard inputs.
    /// </summary>
    public class KeyboardController : IUpdateable
    {
        public BasicSignal Jumped { get; private set; } = new();

        private Transform m_transform;

        private bool m_grounded = true;

        private float m_horizontalInput;
        private float m_verticalInput;
        private float m_landingDistance;

        private const float c_moveSpeed = 10f;
        private const float c_landingPadding = 1f;
        private const float c_gravityForce = 9.18f;
        private const float c_jumpTotalDuration = .2f;
        private const float c_jumpingForce = 1.1f;
        private const float c_jumpSpeedTotal = 10f;
        private const float c_jumpRiseInterval = .01f;

        private float m_jumpDurationCurrent;
        private float m_jumpSpeedCurrent;

        private Camera m_mainCamera;
        private CharacterController m_characterController;
        private Vector3 m_movementVector;

        private bool m_itemDetected = false;
        private ItemController m_itemController;
        private ItemDetector m_itemDetector;

        private IActivateable m_nearbyActivateable = null;
        private bool m_nearbyActivateableEntered = false;

        private BasicSignal<Func<bool>, WaitForSeconds> m_coroutineCaller;
        private WaitForSeconds m_jumpWait = new(c_jumpRiseInterval);
        private bool m_jumpInProgress = false;


        public KeyboardController(Transform a_transform,
                                  Camera a_mainCamera,
                                  ItemDetector a_itemDetector,
                                  ItemController a_itemController,
                                  BasicSignal<(bool, IActivateable)> a_activatableDetected,
                                  CharacterController a_characterController,
                                  BasicSignal<Func<bool>, WaitForSeconds> a_coroutineCaller)
        {
            m_transform = a_transform;
            m_landingDistance = a_transform.localScale.y / 2 + c_landingPadding;
            m_mainCamera = a_mainCamera;
            m_itemDetector = a_itemDetector;
            m_itemController = a_itemController;
            m_characterController = a_characterController;
            m_coroutineCaller = a_coroutineCaller;

            a_itemDetector.ItemDetected.Connect(OnItemDetected);
            a_activatableDetected.Connect((a_tuple) => OnActivatableDetected(a_tuple.Item1, a_tuple.Item2));
        }

        /// <summary> Activatable detected near player. </summary>
        private void OnActivatableDetected(bool a_entered, IActivateable a_activateable)
        {
            m_nearbyActivateableEntered = a_entered;
            m_nearbyActivateable = a_activateable;
        }

        public void UpdateState()
        {
            ApplyGravity();

            AcquireAxesInput();
            AcquireJumpInput();
            AcquireItemInput();
        }

        /// <summary> Apply constant gravity when not touching ground. </summary>
        private void ApplyGravity()
        {
            LandingCheck();
            if (!m_grounded && !m_jumpInProgress)
            {
                m_characterController.Move(-m_characterController.transform.up * c_gravityForce * Time.deltaTime);
            }
        }

        /// <summary> Acquire keyboard input for item grab or drop. </summary>
        private void AcquireItemInput()
        {
            if (Input.GetKeyDown(KeyCode.E) && m_itemDetected)
            {
                m_itemDetector.CurrentItemGrabbed();
                m_itemController.GrabItem(m_itemDetector.DetectedItem);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                m_itemController.DropItem();
            }

            if (Input.GetKeyDown(KeyCode.E) && m_nearbyActivateableEntered)
            {
                if (!m_nearbyActivateable.ActivationState)
                {
                    m_nearbyActivateable.Activate();
                }
                else
                {
                    m_nearbyActivateable.Deactivate();
                }
            }
        }

        /// <summary> Acquire keyboard input for jumping. </summary>
        private void AcquireJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LandingCheck();
                if (m_grounded)
                {
                    Jump();
                }
            }
        }

        /// <summary> Acquire keyboard input for axes. </summary>
        private void AcquireAxesInput()
        {
            m_horizontalInput = Input.GetAxis("Horizontal");
            m_verticalInput = Input.GetAxis("Vertical");

            if (!Mathematics.AlmostZero(m_horizontalInput) || !Mathematics.AlmostZero(m_verticalInput))
            {
                ApplyMovement();
            }
        }

        /// <summary> Apply movement to transform. </summary>
        private void ApplyMovement()
        {
            var playerHit = Raycaster.HitPlayer(m_mainCamera.ScreenPointToRay(Input.mousePosition),
                                                Mathf.Infinity);

            if (!playerHit.Item1)
            {
                Vector3 forward = m_characterController.transform.forward;

                ///CaseNote: We update the existing vector3 to avoid garbage.
                m_movementVector.x = -m_verticalInput;
                m_movementVector.y = 0f;
                m_movementVector.z = -m_horizontalInput;
                m_movementVector = forward * c_moveSpeed * Time.deltaTime;

                m_characterController.Move(m_movementVector);
            }
        }

        /// <summary> Check for landing. </summary>
        private void LandingCheck()
        {
            m_grounded = Raycaster.HitGround(m_transform.position, m_landingDistance).Item1;
        }

        /// <summary> Perform jumping. </summary>
        private void Jump()
        {
            if (m_grounded)
            {
                Jumped.Emit();
                m_jumpDurationCurrent = c_jumpTotalDuration;
                m_jumpSpeedCurrent = c_jumpSpeedTotal;
                m_coroutineCaller.Emit(CharacterControllerJump, m_jumpWait);
            }
        }

        /// <summary> Move CharacterControler up to simulate jump iteration. </summary>
        private bool CharacterControllerJump()
        {
            m_characterController.Move(m_characterController.transform.up * c_jumpingForce);
            m_jumpDurationCurrent -= Time.deltaTime * m_jumpSpeedCurrent;
            m_jumpInProgress = m_jumpDurationCurrent > 0;

            var c_slowDownTreshhold = c_jumpSpeedTotal / 10;
            if (m_jumpDurationCurrent < c_slowDownTreshhold)
            {
                m_jumpSpeedCurrent -= Time.deltaTime * 10;
            }

            return m_jumpInProgress;
        }

        /// <summary> Item detection state is being updated. </summary>
        private void OnItemDetected(bool a_detected)
        {
            m_itemDetected = a_detected;
        }
    }
}