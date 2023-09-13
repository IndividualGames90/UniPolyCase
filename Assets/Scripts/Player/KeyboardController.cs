using IndividualGames.UniPoly.Utils;
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

        private const float c_moveSpeed = 5f;
        private const float c_landingPadding = 1f;
        private const float c_jumpingForce = 25f;

        private Rigidbody m_rigidbody;
        private Vector3 m_movementVector;

        public KeyboardController(Transform a_transform)
        {
            m_transform = a_transform;
            m_rigidbody = a_transform.GetComponent<Rigidbody>();
            m_landingDistance = a_transform.localScale.y / 2 + c_landingPadding;
        }

        public void UpdateState()
        {
            AcquireAxesInput();
            AcquireJumpInput();
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
            ///CaseNote: We update the existing vector 3 to avoid garbage.
            m_movementVector.x = m_horizontalInput;
            m_movementVector.y = 0f;
            m_movementVector.z = m_verticalInput;
            m_movementVector *= c_moveSpeed * Time.deltaTime;

            m_transform.Translate(m_movementVector);
        }

        /// <summary> Check for landing. </summary>
        private void LandingCheck()
        {
            m_grounded = Raycaster.HitGround(m_transform.position, m_landingDistance).Item1;
        }

        /// <summary> Perform jumping with rigidbody. </summary>
        private void Jump()
        {
            if (m_grounded)
            {
                if (m_rigidbody != null)
                {
                    Jumped.Emit();
                    m_rigidbody.AddForce(Vector3.up * c_jumpingForce, ForceMode.Impulse);
                }
            }
        }
    }
}