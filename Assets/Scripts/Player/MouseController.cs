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
        private Transform m_transform;

        public MouseController(Transform a_transform)
        {
            m_transform = a_transform;
            m_mainCamera = Camera.main;
        }

        public void UpdateState()
        {
            m_ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);

            m_hit = Raycaster.HitGround(m_ray.origin, Mathf.Infinity);

            if (m_hit.Item1)
            {
                m_transform.LookAt(m_hit.Item2.point);
            }
        }
    }
}