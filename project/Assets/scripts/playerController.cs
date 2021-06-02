using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private int hp = 100;
    [SerializeField]
    private int currenthp = 0;
    [SerializeField]
    private bool alive = true;
    [SerializeField]
    private weapon[] weapon = new weapon[2];
    [SerializeField]
    private int weaponInUse = 0;
    [SerializeField]
    private float weaponSwithTimer = 0;
    [SerializeField]
    private float reloadTimer = 0;
    [SerializeField]
    private float secondsPerRegeneratedHp = 5;
    private float regenTimer=0;
    private float regenBlock = 0;

    [Header("Control stuff")]
    [SerializeField]
    float fireTimer;
    [SerializeField]
    [Range(5, 30)]
    private float moveSpeed;

    [SerializeField]
    [Range(5, 30)]
    private float rotationSpeed;

    [SerializeField]
    private float jumpForce = 2.0f;

    private float rotationX;
    private float rotationY;

    [SerializeField]
    private bool isGrounded;
    private Vector3 jump;
    private Rigidbody rigidbody;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject playerHands;
    [SerializeField]
    private Transform[] bulletSpawnPoint = new Transform[2];

    [SerializeField]
    private Animator handsAnimator;

    [SerializeField]
    private GameObject ak;
    [SerializeField]
    private GameObject akMag;

    [SerializeField]
    private AudioSource shotSound;
    [SerializeField]
    private AudioSource reloadSound;


    void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f); 
        alive = true;
        currenthp = hp;
        weapon[0].CurrentBullets = weapon[0].BulletsPerMag;
        weapon[1].CurrentBullets = weapon[1].BulletsPerMag;
    }

    [SerializeField]
    private TextMeshProUGUI ammoLabel;
    [SerializeField]
    private TextMeshProUGUI weaponLabel;
    [SerializeField]
    private TextMeshProUGUI healthLabel;

    void Update()
    {
        ammoLabel.text = weapon[weaponInUse].CurrentBullets + "/" + weapon[weaponInUse].BulletsPerMag;
        if(weaponInUse == 0) { weaponLabel.text = "AK-47"; }
        if (weaponInUse == 1) { weaponLabel.text = "Finger Gun"; ammoLabel.text = "∞"; }
        healthLabel.text = currenthp + "/" + hp;
        //player.transform.Rotate(0, 0.2f, 0);
        if (alive)
        {
            if(currenthp<hp)
			{
                if (regenBlock > 0)
                {
                    regenBlock -= Time.deltaTime;
                }
                else
                {
                    if (regenTimer < secondsPerRegeneratedHp)
                    {
                        regenTimer += Time.deltaTime;
                    }
                    else
                    {
                        currenthp++;
                        regenTimer = 0;
                    }
                }
			}
            movePlayer();
        }
    }

    public void restart()
	{
        SceneManager.LoadScene(1);
	}
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    [SerializeField]
    ParticleSystem muzzleFlash;

    void movePlayer()
    {
        //handsAnimator.SetBool("isReloading", false);
        if (Input.GetKey(KeyCode.W)) { player.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.S)) { player.transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.A)) { player.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed); }
        if (Input.GetKey(KeyCode.D)) { player.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { rigidbody.velocity = new Vector3(0,0,0); };
        rotationX += rotationSpeed * Input.GetAxis("Mouse X");
        rotationY -= rotationSpeed * Input.GetAxis("Mouse Y");
        player.transform.eulerAngles = new Vector3(player.transform.rotation.x, rotationX, 0f);
        camera.transform.eulerAngles = new Vector3(Mathf.Clamp(rotationY, -20, 35), rotationX, 0f);
        playerHands.transform.eulerAngles = new Vector3(Mathf.Clamp(rotationY, -15, 18), rotationX, 0f);
        if (Input.GetKey(KeyCode.Space) && isGrounded) {/*Debug.Log("はい");*/ rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse); isGrounded = false; }
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && (weapon[weaponInUse].CurrentBullets > 0 || weapon[weaponInUse].BulletsPerMag == -1))
        {
            handsAnimator.SetTrigger("isShooting");
            shotSound.Play();
            muzzleFlash.Play();
            GameObject.Instantiate(weapon[weaponInUse].BulletsPrefab, bulletSpawnPoint[weaponInUse].transform.position, camera.transform.rotation);
            fireTimer = weapon[weaponInUse].FireRate;
            weapon[weaponInUse].CurrentBullets--;
        }
        else
        { handsAnimator.SetBool("isShooting", false); };
        if(reloadTimer > 0)
		{
            reloadTimer -= Time.deltaTime;
		}
        else if(Input.GetKeyDown(KeyCode.R)) { handsAnimator.SetBool("isReloading", true);
            reloadSound.Play(); weapon[weaponInUse].CurrentBullets = weapon[weaponInUse].BulletsPerMag; reloadTimer = 2; };

        //Debug.Log(Input.mouseScrollDelta);
        if(weaponSwithTimer >  0)
		{
            weaponSwithTimer -= Time.deltaTime;
		}
        else if (Input.mouseScrollDelta.y > 0.5f || Input.mouseScrollDelta.y > 0.5f)
		{
            if(weaponInUse == 0)
			{
                weaponInUse = 1;
                handsAnimator.SetInteger("weapon", 1);
                ak.gameObject.SetActive(false);
                akMag.gameObject.SetActive(false);
            }
            else
			{
                weaponInUse = 0;
                handsAnimator.SetInteger("weapon", 0);
                ak.gameObject.SetActive(true);
                akMag.gameObject.SetActive(true);
            }
            weaponSwithTimer = 1f;
		}
    }

    [SerializeField]
    private GameObject gameOverPanel;

    public void TakeDamage(int amount)
    {
        currenthp -= amount;
        regenBlock = 4;

        if (currenthp <= 0)
        {
            currenthp = 0;
            alive = false;
            //Destroy(this.gameObject);
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            alive = false;
        }
    }
}
