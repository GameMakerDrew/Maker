using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    private List<GameObject> shootables = new List<GameObject>();
    private GameObject target;
    private bool isShooting;

    public GameObject projectile;
    public GameObject barrelEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateProjectile", 0f, .2f);
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestShootable();
        RotateTowardsShootable();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shootable"))
        {
            shootables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (shootables.Contains(other.gameObject))
        {
            shootables.Remove(other.gameObject);
        }
    }

    public void FindClosestShootable()
    {
        float closestDistance = Mathf.Infinity;
        int index = 0;
        int closest = 0;

        target = null;

        if (shootables.Count > 0 )
        {
            foreach (GameObject shootable in shootables)
            {
                float testDist = Vector3.Distance(shootable.transform.position, transform.position);
                if (testDist < closestDistance)
                {
                    closestDistance = testDist;
                    closest = index;
                }
                index++;
            }
            target = shootables[closest];
          
        }
    }

    public void RotateTowardsShootable()
    {
        if (target)
        {
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        }
    }

    public void InstantiateProjectile()
    {
        if (target)
        {
            Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
        }
    }
}

