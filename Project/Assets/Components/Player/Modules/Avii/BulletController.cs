using System.Drawing;
using System.IO;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour
{
    float speed = 30f;
    int timer = 200;

    void Update()
    {
        timer--;
        if (timer <= 0) Destroy(gameObject);
    }

    public void SetObjective(Transform objective)
    {
        // Set angle
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(objective.position, transform.position)));

        // Set linear velocity
        GetComponent<Rigidbody2D>().linearVelocity = (objective.position - transform.position).normalized * speed;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) Destroy(gameObject);
    }
}
