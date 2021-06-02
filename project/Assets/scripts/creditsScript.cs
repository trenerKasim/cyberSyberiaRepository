using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour
{
    [SerializeField] [Range(0.1f,50)]
    private float speed;

	[SerializeField]
    private RectTransform position;

	public void init()
	{
		position = GetComponent<RectTransform>();
		position.localPosition = new Vector3(0,-540,0);
	}

	// Update is called once per frame
	void Update()
    {
		position.position = new Vector3(position.position.x, (position.position.y) + (Time.deltaTime * speed), 0);
		if(position.localPosition.y > 539.9f) { position.localPosition = new Vector3(0, 540, 0); };
	}
}
