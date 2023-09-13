using IndividualGames.UniPoly.Utils;
using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Mouse input controller
    /// </summary>
    public class MouseController : IUpdateable
    {
        private Ray m_ray;
        private Camera m_mainCamera;
        private (bool, RaycastHit) m_hit;

        private Vector3 m_lookVector;
        private Quaternion m_lookDirecton;

        private Transform m_transform;

        public MouseController(Transform a_transform)
        {
            m_transform = a_transform;
            m_mainCamera = Camera.main;
        }

        public void UpdateState()
        {
            DetectGround();

            var groundHit = m_hit.Item1;
            if (groundHit)
            {
                RotateToLookVector();
            }
        }

        /// <summary> Raycast to ground layer and update members. </summary>
        private void DetectGround()
        {
            m_ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            m_hit = Raycaster.HitGround(m_ray, Mathf.Infinity);
        }

        /// <summary> Rotate the transform to look vector retaining the Y. </summary>
        private void RotateToLookVector()
        {
            m_lookVector = m_hit.Item2.point;
            m_lookVector.y = m_transform.position.y;

            m_transform.LookAt(m_lookVector);
        }
    }
}