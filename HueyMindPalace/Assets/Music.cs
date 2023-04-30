using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music _instance;

    public static Music Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Music>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<Music>();
                    singleton.name = typeof(Music).ToString();
                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
