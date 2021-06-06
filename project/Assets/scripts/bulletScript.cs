using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField]
    private bool friendly;
    [SerializeField]
    private int[] damageRange = new int[2];
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifestap());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 50);
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * 500, ForceMode.Impulse);
    }

    IEnumerator lifestap()
	{
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("今");
        Destroy(this.gameObject);
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<playerController>())
        {
            if (friendly)
            { return; }
            col.GetComponent<playerController>().TakeDamage(Random.Range(damageRange[0], damageRange[1]));
        }
        else if (col.GetComponent<EnemyHP>())
		{
			if (!friendly)
			{ return; }
            col.GetComponent<EnemyHP>().TakeDamage(Random.Range(damageRange[0],damageRange[1]));
		}
        Destroy(this.gameObject);
	}
}
