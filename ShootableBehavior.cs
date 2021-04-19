using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShootableBehavior : MonoBehaviour
{
    private bool isDead;
    public float health;
    private float maxHealth;
    public float projDamage;
    public GameObject follow;
    public Image red;
    public Image green;

    public AudioClip hit;
    public AudioClip pop;


    

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("PlayerGFX");
        maxHealth = health;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        green.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth);
        if (GetComponent<NavMeshAgent>().enabled)
        {
            GetComponent<NavMeshAgent>().SetDestination(new Vector3(0,0,8));
        }

        DeathCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Projectile"))
        {
            GetComponent<AudioSource>().PlayOneShot(hit);
            Debug.Log(other.transform.tag);
            Destroy(other.gameObject);
            takeDamage();
        }
    }

    public void takeDamage()
    {
        
        health -= projDamage;
    }

    public void DeathCheck()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine("Death");
        }
    }

    private IEnumerator Death()
    {
        GetComponent<AudioSource>().PlayOneShot(pop);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<Image>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        
        GetComponent<Collider>().isTrigger = false;
        yield return new WaitForSeconds(.5f);
        
        Destroy(gameObject);
    }

   
}
