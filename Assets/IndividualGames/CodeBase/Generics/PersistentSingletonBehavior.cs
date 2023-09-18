using UnityEngine;
/*
 * Part of IndividualGame.CodeBase library.
 * Contact: IndividualGames@yandex.com
 * https://github.com/IndividualGames90
 */

namespace IndividualGames.CodeBase.Generics
{
    /// <summary>
    /// Persistent Singleton is DontDestroyOnLoad. Line 49.
    /// Inherits MonoBehavior to use FindObjectOfType and OnDestroy. Line 34, 67.
    /// Has a MonoBehavior type so can be attached to a persistent GameObject. Line 46.
    /// </summary>
    public class PersistentSingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;

        private static object m_lock = new();
        private static bool m_applicationIsQuiting = false;

        /// <summary> Created only when accessed, RAII. </summary>
        public static T Instance
        {
            get
            {
                if (m_applicationIsQuiting)
                {
                    print("Application is quiting: Singleton destroyed, won't recreate. Return null.");
                    return null;
                }

                lock (m_lock)///FindObjectOfType is thread safe, this might be redundant.
                {
                    if (m_instance == null)
                    {
                        ///Look for singleton
                        m_instance = (T)FindObjectOfType(typeof(T));

                        ///Check for duplicates
                        if (FindObjectsOfType(typeof(T)).Length > 1)///TODO: Why do this again? Check why, maybe change with m_instance
                        {
                            print("Singleton Duplicate Error!");
                            return m_instance;
                        }

                        ///Initialize Singleton
                        if (m_instance == null)
                        {
                            GameObject singleton = new();
                            m_instance = singleton.AddComponent<T>();

                            DontDestroyOnLoad(singleton);

                            m_instance.gameObject.name = string.Format("Persistent.Singleton.{0}", typeof(T).Name);
                        }
                    }

                    return m_instance;
                }
            }
        }


        /// <summary>
        /// Upon quit unity destroys objects randomly, 
        /// calling onDestroy eliminates residual objects in Editor scene.
        /// </summary>
        public void OnDestroy()
        {
            m_applicationIsQuiting = true;
        }
    }
}