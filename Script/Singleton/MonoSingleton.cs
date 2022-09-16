using System.Collections;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public enum LifeCycle
    {
        None,
        Enable,
        Destroy,
        Disable,
    }

    public static LifeCycle lifeCycle = LifeCycle.None;


    public static bool isShuttingDown = false;
    private static T _instance = null;
	public static T instance
	{
		get
		{
            //if (lifeCycle == LifeCycle.Destroy || lifeCycle == LifeCycle.Disable)
            //{
            //    Debug.Log("Singleton called after Destroy!!!: " + typeof(T).ToString());
            //    return null;
            //}

            if (_instance == null)
			{
                _instance = FindObjectOfType(typeof(T)) as T;
				if (_instance == null)
				{
                    _instance = new GameObject("@" + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    //DontDestroyOnLoad(_instance);
                }

                DontDestroyOnLoad(_instance);

                lifeCycle = LifeCycle.Enable;
			}
			return _instance;
		}
	}

	// 미리 생성 해놓을 필요가 있으면 호출
	public virtual void Init()
	{
	}

    public static void Destroy()
    {
        if (_instance != null)
        {
            lifeCycle = LifeCycle.Disable;
            DestroyImmediate(instance.gameObject);
        }
    }

    private void OnDestroy()
    {
        _instance = null;
        lifeCycle = LifeCycle.Disable;
    }

    void OnApplicationQuit()
    {
        //isShuttingDown = true;
        lifeCycle = LifeCycle.Destroy;
    }
}
