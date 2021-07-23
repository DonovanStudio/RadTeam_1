using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOrb : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float loopTime;
    [SerializeField] float loopTimeJitter;
    [SerializeField] Vector3 startTransform { get; set; }
    float loopPoint;
    float time;
    private void OnValidate()
    {
    }
    private void Awake()
    {
        startTransform = transform.localPosition;
        time = Random.Range(0, loopTime);

        if (gameObject.name == "small spirit 1" && GameManager.instance.GetOrb1Status())
        {
            Destroy(gameObject);
        }
        if (gameObject.name == "small spirit 2" && GameManager.instance.GetOrb2Status())
        {
            Destroy(gameObject);
        }
        if (gameObject.name == "small spirit 3" && GameManager.instance.GetOrb3Status())
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .7f);
        Gizmos.DrawSphere(transform.position + Vector3.up * this.height * (1 - loopPoint), .5f);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > loopTime)
            time -= loopTime + Random.Range(-loopTimeJitter, loopTimeJitter);
        loopPoint = (-Mathf.Cos(2 * Mathf.PI * time / loopTime) + 1) * 0.5F;
        transform.localPosition = startTransform + Vector3.up * height * loopPoint;
    }
}
