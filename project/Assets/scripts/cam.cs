using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class cam : MonoBehaviour
{
    [SerializeField] [Range(5, 30)]
    private float moveSpeed;

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private GameObject player;
    // Update is called once per frame
    void Update()
    {
        //player.transform.Rotate(0, 0.2f, 0);
        movePlayer();
    }

    void movePlayer()
	{
		if (Input.GetKey(KeyCode.W)) { player.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed); };
        if (Input.GetKey(KeyCode.S)) { player.transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed); };
        if (Input.GetKey(KeyCode.A)) { player.transform.Rotate(0, -0.1f, 0); };
        if (Input.GetKey(KeyCode.D)) { player.transform.Rotate(0, 0.1f, 0); };
    }
}
