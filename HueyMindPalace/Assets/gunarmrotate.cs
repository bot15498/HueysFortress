using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunarmrotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(gameObject.transform.position)).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
