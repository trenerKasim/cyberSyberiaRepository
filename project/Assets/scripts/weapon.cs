using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class weapon
{
    //private Animator anim;

    //public float range = 100;
    [SerializeField]
    private int bulletsPerMag = 30;
    public int BulletsPerMag
	{
        get { return bulletsPerMag; }
	}
    [SerializeField]
    private int currentBullets;
    public int CurrentBullets
	{
        get { return currentBullets; }
        set { currentBullets = value; }
    }
    [SerializeField]
    private GameObject bulletsPrefab;
    public GameObject BulletsPrefab
	{
        get { return bulletsPrefab; }
	}

    //public Transform shootPoint;

    [SerializeField]
    private float fireRate = 0.2f;
    public float FireRate 
    {
        get { return fireRate; }
    }



    void init()
    {
        //anim = GetComponent<Animator>();
        currentBullets = bulletsPerMag;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetButton("Firel"))
    //    {
    //        Fire();
    //    }

    //    if (fireTimer < fireRate)
    //        fireTimer += Time.deltaTime;
    //}

    //void FixedUpdate()
    //{
    //    AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("Fire")) anim.SetBool("Fire", false);
    //}


    //private void Fire()
    //{
    //    if (fireTimer < fireRate) return;

    //    RaycastHit hit;

    //    if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
    //    {
    //        Debug.Log(hit.transform.name + " found!");
    //    }
    //    anim.CrossFadeInFixedTime("Fire", 0.01f);
    //    currentBullets--;
    //    fireTimer = 0.0f;
    //}
}
