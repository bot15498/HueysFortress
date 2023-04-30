using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour
{
    public Character owner;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    public void StartFlashRed()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        sprite.color = new Color(1, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 1);
        yield return null;
    }
}
