using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterControl : MonoBehaviour
{
    public float walkForce = 5f;
    public float maxWalkSpeed = 6f;
    public float jumpForce = 5f; 
    private float extendedJumpTime = .5f;


    public bool onGround = true;

    private Rigidbody rbody;
    private Collider collider;
    private Camera mainCamera;
    private Animator animComp;

    private Transform transform;

    private float proximityThreshold = 2.6f;
    public UI ui;

    private float blockHitDelay = .5f;

    public GameObject EmptyBlock;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animComp = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float castDistance = 0.6f;
        Vector3 bottom = transform.position;
        bottom.y += 0.5f;
        Ray standingRay = new Ray(bottom, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * castDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(standingRay, out hit, castDistance))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }


        //onGround = Physics.Raycast(transform.position, Vector3.down, castDistance);
        animComp.SetBool("On Ground", onGround);

        // move left or right
        float axis = Input.GetAxis("Horizontal");
        rbody.AddForce(Vector3.right * axis * walkForce, ForceMode.Force);

        //cap walk movement
        if (Mathf.Abs(rbody.velocity.x) > maxWalkSpeed)
        {
            float newX = maxWalkSpeed * Mathf.Sign(rbody.velocity.x);
            rbody.velocity = new Vector3(newX, rbody.velocity.y, rbody.velocity.z);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            extendedJumpTime = .5f;
        }

        //reduce extendedJumpTime everyframe
        extendedJumpTime = extendedJumpTime - Time.deltaTime;

        //extended jump
        if (Input.GetButton("Jump") && !onGround && extendedJumpTime > 0f)
        {
            rbody.AddForce(Vector3.up *2f, ForceMode.Acceleration);
        }

        //slow the character down
        if (axis < 0.1f)
        {
            float newX = rbody.velocity.x  * (1f - Time.deltaTime * 3f);
            rbody.velocity = new Vector3(newX, rbody.velocity.y, rbody.velocity.z);
        }

        //fix camera to character
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        //animation velocity
        animComp.SetFloat("Speed", rbody.velocity.magnitude);

        //Raytracing to destroy blocks jumped into
        Ray ray = new Ray(transform.position, Vector3.up);
        RaycastHit hitInfo;

        blockHitDelay = blockHitDelay - Time.deltaTime;
        //Debug.DrawRay(transform.position, Vector3.up * proximityThreshold, Color.red);
        if (Physics.Raycast(ray, out hitInfo, proximityThreshold))
        {
            if (blockHitDelay <= 0f)
            {
                if (hitInfo.collider.gameObject.name == ("Brick(Clone)"))
                {
                    Destroy(hitInfo.collider.gameObject);
                    ui.UpdateScore(100);
                    blockHitDelay = .5f;
                }
                else if (hitInfo.collider.gameObject.name == ("QuestionBlock(Clone)"))
                {
                    Transform location = hitInfo.collider.gameObject.transform;

                    Destroy(hitInfo.collider.gameObject);

                    var block = Instantiate(EmptyBlock);
                    block.transform.position = location.position;

                    ui.UpdateScore(100);
                    ui.UpdateCoins();
                    blockHitDelay = .5f;
                }
            }
        }

        //change direction of facing dependent upon movement
        if (rbody.velocity.x < 0f)
        {
            Transform rotate = GetComponent<Transform>();
            float x = 0f;
            float y = 270f;
            float z = 0f;

            rotate.rotation = Quaternion.Euler(x, y, z);
        }
        else if (rbody.velocity.x > 0f)
        {
            Transform rotate = GetComponent<Transform>();
            float x = 0f;
            float y = 90f;
            float z = 0f;

            rotate.rotation = Quaternion.Euler(x, y, z);
        }
    }

    public void GameOver()
    {
        Debug.Log("You lose");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
