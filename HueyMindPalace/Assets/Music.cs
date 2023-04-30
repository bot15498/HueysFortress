using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public AudioClip soundclips;
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playclip(int clipid)
    {
        AudioSource.PlayClipAtPoint(soundclips, gameObject.transform.position);
    }
}
