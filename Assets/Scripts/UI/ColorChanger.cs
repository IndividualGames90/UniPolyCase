using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// Changes color of UI elements in a HSV cycle.
    /// </summary>
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Graphic m_graphic = null;

        private float m_h;
        private float m_s;
        private float m_v;
        protected float m_a;

        private Color m_newColor;
        private float m_lerpValue = .3f;


        private void Awake()
        {
            m_a = m_graphic.color.a;
        }

        private void Update()
        {
            m_graphic.color = CycleColor(m_graphic.color);
        }


        /// <summary> Cycle color in HSV. </summary>
        protected virtual Color CycleColor(Color a_originalColor)
        {
            Color.RGBToHSV(a_originalColor, out m_h, out m_s, out m_v);

            m_h += m_lerpValue * Time.deltaTime;
            m_h = Mathf.Repeat(m_h, 1);

            m_newColor = Color.HSVToRGB(m_h, m_s, m_v);
            m_newColor.a = m_a;

            return m_newColor;
        }
    }
}