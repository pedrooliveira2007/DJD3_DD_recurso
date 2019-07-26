
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    internal float speed = 10;
    [SerializeField]
    internal int baseDamage = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

   
}
