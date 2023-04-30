using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explainscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void explainopen()
    {
        anim.Play("explainOpen");
    }


    public void explainClose()
    {
        anim.Play("ExplainClose");
    }

}
