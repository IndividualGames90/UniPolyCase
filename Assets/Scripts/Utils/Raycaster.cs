using UnityEngine;

namespace IndividualGames.UniPoly.Utils
{
    /// <summary>
    /// Raycast encapsulation for specific use cases.
    /// </summary>
    public static class Raycaster
    {
        private static RaycastHit m_hit;

        /// <summary> Detect if we are hitting ground layer. </summary>
        public static (bool, RaycastHit) HitGround(Vector3 a_origin, float a_maxDistance)
        {
            return (Physics.Raycast(a_origin,
                                    Vector3.down,
                                    out m_hit,
                                    a_maxDistance,
                                    Layers.Ground),
                    m_hit);
        }

        /// <summary> Debug rays by drawing </summary>
        private static void RayDebugger(Ray a_ray, float a_maxDistance)
        {
            Debug.DrawRay(a_ray.origin, a_ray.direction * a_maxDistance, Color.red, 5f);
        }
    }
}