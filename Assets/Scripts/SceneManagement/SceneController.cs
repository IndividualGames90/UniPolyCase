using IndividualGames.CodeBase.Generics;
using IndividualGames.UniPoly.Player;
using System.Collections.Generic;

namespace IndividualGames.UniPoly.SceneManagement
{
    /// <summary>
    /// Manages the scene states. Singleton.
    /// </summary>
    public class SceneController : PersistentSingletonBehavior<SceneController>
    {
        private Dictionary<int, IRegisterable> m_registry = new();

        /// <summary> Register an entry with key and value. </summary>
        public void Register(int a_key, IRegisterable a_entry)
        {
            m_registry.Add(a_key, a_entry);
        }

        /// <summary> Retrieve registerable via key. </summary>
        public IRegisterable Retrieve(int a_key)
        {
            return m_registry[a_key];
        }
    }
}