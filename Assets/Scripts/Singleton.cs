using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();

    private static bool appIsQuiting = false;

    public static T Instance
    {
        get
        {
            //if (appIsQuiting)
            //{
            //    Debug.LogWarningFormat($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
            //    return null;
            //}

            lock (_lock)
            {
                if (instance != null)
                {
                    return instance;
                }

                instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogErrorFormat($"[Singleton] More than one instance of '{typeof(T)}' exists.");
                    return instance;
                }

                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    instance = singleton.AddComponent<T>();
                    singleton.name = "(Singleton) " + typeof(T).ToString();

                    DontDestroyOnLoad(singleton);
                }

                return instance;
            }
        }
    }

    private void OnDestroy()
    {
        if (this == instance) appIsQuiting = true;
    }
}
