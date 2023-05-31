using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{  
    Transform player;
    Transform origin;

    public Transform target;
    public float maxBulletDistance=10;
    public int minDamage;
    public int maxDamage;
    public float knockBack;
    public float shootDelay;
    float timeOfLastShot;

    public TrailRenderer bulletTrail;
    public static Weapons instance;

    IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hit)
    {
        float time = 0;

        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {

            trail.transform.position = Vector3.Lerp(startPos, hit, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit;

        Destroy(trail.gameObject, trail.time);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        origin = transform.GetChild(0);
        timeOfLastShot = Time.time;

    }

    public void shoot()
    {
        if(timeOfLastShot + shootDelay < Time.time)
        {
            timeOfLastShot = Time.time;
            Vector3 dir = findDirection();

            Ray ray = new Ray(origin.position, dir);
            Debug.DrawRay(origin.position, dir * maxBulletDistance, Color.green, 5f);

            if (Physics.Raycast(ray, out RaycastHit hitData, maxBulletDistance))
            {//if we hit
                TrailRenderer trail = Instantiate(bulletTrail, origin.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hitData.point));
                
                if (hitData.transform.CompareTag("Zombie"))
                {
                    int damage = Random.Range(minDamage, maxDamage+1);
                    Debug.Log(damage);
                    hitData.transform.GetComponent<ZombieController>().takeDamage(damage, knockBack, player);
                }
            }
            else
            {//if we don't hit
                TrailRenderer trail = Instantiate(bulletTrail, origin.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, origin.position+(findDirection()*maxBulletDistance)));
            }
            Debug.Log("shotFired");
        }
        
    }

    Vector3 findDirection()
    {
        Vector3 direction = target.position - origin.position;
        direction.y = 0;
        direction.Normalize();
        return direction;
    }
}
