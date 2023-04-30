using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] soundclips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playclip(int clipid,float volume)
    {
        AudioSource.PlayClipAtPoint(soundclips[clipid], gameObject.transform.position,volume);
       
    }
}
