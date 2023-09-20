using System;

namespace IndividualGames.UniPoly.Utils
{
    /// <summary>
    /// Basic Signal for action.
    /// </summary>
    public class BasicSignal
    {
        private Action m_action;


        public void Connect(Action a_action)
        {
            m_action += a_action;
        }

        public void Disconnect(Action a_action)
        {
            m_action -= a_action;
        }

        public void Emit()
        {
            m_action?.Invoke();
        }
    }


    /// <summary>
    /// Basic Signal for action with single parameter.
    /// </summary>
    public class BasicSignal<T>
    {
        private Action<T> m_action;


        public void Connect(Action<T> a_action)
        {
            m_action += a_action;
        }

        public void Disconnect(Action<T> a_action)
        {
            m_action -= a_action;
        }

        public void Emit(T a_parameter)
        {
            m_action?.Invoke(a_parameter);
        }
    }

    /// <summary>
    /// Basic Signal for action with two parameters.
    /// </summary>
    public class BasicSignal<T1, T2>
    {
        private Action<T1, T2> m_action;


        public void Connect(Action<T1, T2> a_action)
        {
            m_action += a_action;
        }

        public void Disconnect(Action<T1, T2> a_action)
        {
            m_action -= a_action;
        }

        public void Emit(T1 a_first, T2 a_second)
        {
            m_action?.Invoke(a_first, a_second);
        }
    }
}
