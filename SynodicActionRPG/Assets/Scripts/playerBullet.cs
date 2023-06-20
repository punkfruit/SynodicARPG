using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the bullet
    public float lifeTime = 6f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame
    void FixedUpdate()
    {
        // Move the bullet to the right relative to itself
        rb.velocity = transform.right * speed;
    }

  


}
