using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCamera;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
