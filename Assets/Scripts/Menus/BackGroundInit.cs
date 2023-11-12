using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!MainMenuinit.Initialized)
        {
            DontDestroyOnLoad(gameObject);
            MainMenuinit.Initialized = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
