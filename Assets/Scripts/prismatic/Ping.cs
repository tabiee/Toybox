using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour
{
    [SerializeField] private Transform circleSprite;
    [SerializeField] private SphereCollider sphereTrigger;
    [SerializeField] private float range;
    [SerializeField] private float rangeMax = 20f;
    [SerializeField] private float rangeSpeed = 5f;
    private void Update()
    {
        //make circle bigger untill it goes above the max, then reset
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax)
        {
            range = 0f;
        }
        circleSprite.localScale = new Vector3(range, range, 1);

        if (circleSprite.localScale.x < 1)
        {
            sphereTrigger.radius = range * 4;
        }
        else
        {
            //Debug.Log("Scale is not 1 anymore");
            sphereTrigger.radius = 4.35f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "red")
        {
            other.GetComponent<FadeOutAlt>().FadeIn();
        }
    }
}
