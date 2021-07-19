using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float hSpeed;
    [SerializeField] float lifetime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.left * hSpeed;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * speed);
    }
}
