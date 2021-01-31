using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public Sprite[] bloodSprite;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = bloodSprite[Random.Range(0, bloodSprite.Length - 1)];
    }
}
