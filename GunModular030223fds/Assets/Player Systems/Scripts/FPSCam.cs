using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FPSCam : MonoBehaviour
{
    [Header("Input")]
    private KeyCode Forward = KeyCode.W;
    private KeyCode Backward = KeyCode.S;
    private KeyCode Right = KeyCode.D;
    private KeyCode Left = KeyCode.A;
    private KeyCode Sprint = KeyCode.LeftShift;
    [SerializeField]public float Xmouse;
    [SerializeField] public float ymouse;
    [SerializeField] public float inputX;
    [SerializeField] public float inputY;

    [SerializeField] public float Sensitivity;


    [Header("Physics Variables")]
    public float moveSpeed;
    public float sprintSpeed;
    public float accelerationSpeed;
    public float decelerationSpeed;
    public float friction;
    public Vector3 movementVector;
    public Vector3 jumpVector;
    public Vector3 inputDir;

    [Header("Jump Settings")]
    public float startJumpvel;
    public float jumpVel;
    public float jumpCharge;
    public float jumpChargeMax;
    public float jumpChargeSpeed;
    Vector3 colliderCentre;
    float colliderStartHeight;
    public float JumpAccelertation;


    [Header("Outsider Variables")]
    [SerializeField] public Modifier mod;
    public float speed;
    public CapsuleCollider col;
    public Collider jumpCollider;
    public Rigidbody rb;
    public bool useOldDir;
    float tempMaxSpeed;
    public float maxVelocity;


    [Header("Camera")]
    public Camera camera;
    public Camera handCamera;
    public float walkFOV;
    public float sprintFOV;


    public float fovtime;

    public SpringJoint joint;

    [Header("Ground Checks and settings")]
    public bool inAir;
    public Transform groundCheck;
    public float groundCheckDistance;

    [Header("In Air Speeds")]
    public float currentInAirSpeed;
    public float inairSpeed;
    public float nonGrappleSpeed;

    public float gravValue;

    public PlayerStatsManager STM;

   

    [Header("Slide")]
    public float reducedSlideHeight;
    private float normalSlideHeight;
    public float slideSpeed;
    private float slideSpeedCurrent;
    public float slowSpeed;
    
    public Vector3 Datoprint;
    public Vector3 veltoprint;
    public Vector3 slideVector;

    public PlayerInput Pm;

    public bool contactMod;
    public bool running;
    public FootstepSound fss;

    void Start()
    {
        col.hasModifiableContacts = true;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;
        mod = new Modifier
        {
            mod = this,
            walkIstanceID = col.GetInstanceID()
        };
        Physics.ContactModifyEvent += mod.ModificationCallback;
        Cursor.lockState = CursorLockMode.Locked;
        colliderCentre = col.center;
        colliderStartHeight = col.height;
        startJumpvel = jumpVel;
        tempMaxSpeed = maxVelocity;
        rb.useGravity = false;
        normalSlideHeight = col.height;
    }

    // Update is called once per frame
    void Update()
    {
        handCamera.fieldOfView = camera.fieldOfView;
        jumpVector = new Vector3(0, 0, 0);
        InputStuff();

        if (!Pm.InventoryUIManager.Active && GameManager.instance.isPaused == false)
        {
            ymouse += Input.GetAxis("Mouse X") * Sensitivity;
            Xmouse += Input.GetAxis("Mouse Y") * Sensitivity;
            Xmouse = Mathf.Clamp(Xmouse, -60, 60);



            transform.localEulerAngles = new Vector3(0f, ymouse, 0);
            camera.transform.localEulerAngles = new Vector3(-Xmouse, 0f, 0f);
        }


        if (inputX == 0 && inputY == 0)
        {
            speed = Mathf.MoveTowards(speed, 0, Time.deltaTime * decelerationSpeed);
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, walkFOV, Time.deltaTime * fovtime);
        }
        else
        {
            inputDir = new Vector3(inputX, 0f, inputY);
            if (Input.GetKey(Sprint))
            {
                running = true;
                speed = Mathf.MoveTowards(speed, sprintSpeed, Time.deltaTime * accelerationSpeed);
                //camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, sprintFOV, Time.deltaTime * fovtime);

            }
            else
            {
                running = false;
                speed = Mathf.MoveTowards(speed, moveSpeed, Time.deltaTime * decelerationSpeed);
                //camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, walkFOV, Time.deltaTime * fovtime);
            }
            inputDir = camera.transform.TransformDirection(inputDir.normalized);
        }



        inputDir = new Vector3(inputDir.x, 0f, inputDir.z) * (speed * STM.PlayerStats.Speed);
        movementVector = inputDir.normalized * (speed * STM.PlayerStats.Speed);


        if (contactMod)
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!inAir)
                {
                    rb.AddForce(Vector3.up * (JumpAccelertation * STM.PlayerStats.JumpHeight), ForceMode.Impulse);
                    fss.Jump();
                }

            }

            //col.height = colliderStartHeight  - jumpCharge;
            //col.center = colliderCentre + new Vector3(0, jumpCharge * 0.5f);
            //groundCheck.localPosition = col.center + new Vector3(0, -(col.height * 0.5f) + 0.05f);


            //if (Input.GetKey(KeyCode.Space))
            //{
            //    jumpVel = 0f;
            //    jumpCharge = Mathf.MoveTowards(jumpCharge, jumpChargeMax, jumpChargeSpeed * Time.deltaTime);
            //    jumpVector = new Vector3(0, -jumpVel, 0);

            //}
            //else if(jumpCharge > 0)
            //{
            //    if (wallRunning)
            //        StopWallRun();

            //    jumpCharge = Mathf.MoveTowards(jumpCharge, 0, jumpVel * Time.deltaTime);
            //    jumpVel += Time.deltaTime * JumpAccelertation;
            //    jumpVector = new Vector3(0, jumpVel, 0);
            //}
            //else
            //{
            //    jumpVel = 0;
            //    jumpVector = Vector3.zero;
            //}

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Slide();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
                NoSlide();




            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }


            if (Input.GetKeyDown(KeyCode.K))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //if (doWallRunCheck)
            //{
            //    if (Physics.Raycast(wallRunLeftCheck.position, wallRunLeftCheck.forward, out hitLeft, wallRunCheckDistance))
            //    {
            //        if (hitLeft.collider.tag == "WallRun")
            //        {
            //            wallrunningleft = true;
            //            StartWallRun(wallRunRightCheck);
            //        }

            //    }

            //    else if (Physics.Raycast(wallRunRightCheck.position, wallRunRightCheck.forward, out hitRift, wallRunCheckDistance))
            //    {
            //        if (hitRift.collider.tag == "WallRun")
            //        {
            //            wallrunningright = true;
            //            StartWallRun(!wallRunRightCheck);
            //        }

            //    }
            //    else if (wallRunning)
            //        StopWallRun();
            //}



            if (Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, groundCheckDistance))
            {
                if (inAir == true)
                    fss.Landed();
                inAir = false;
            }
            else
                inAir = true;

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    public void Slide()
    {
        
        col.height = reducedSlideHeight;
        rb.AddForce(new Vector3(camera.transform.forward.x * slideSpeed,0, camera.transform.forward.z * slideSpeed), ForceMode.VelocityChange);
        rb.drag = slowSpeed;
    }


    public void NoSlide()
    {
        col.height = colliderStartHeight;
        rb.drag = 0f;
    }

   
    public void AirControl()
    {

            if (joint != null)
                currentInAirSpeed = inairSpeed;
            else
                currentInAirSpeed = nonGrappleSpeed;
            rb.AddForce(movementVector * currentInAirSpeed, ForceMode.Acceleration);
    }

    void InputStuff()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    public void FixedUpdate()
    {



        Gravity();


    }

    public void Gravity()
    {
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * gravValue);
    }

    public void OnDrawGizmos()
    {
        //    Gizmos.DrawLine(wallRunRightCheck.position,wallRunRightCheck.position + wallRunRightCheck.forward * wallRunCheckDistance);
        //    Gizmos.DrawLine(wallRunLeftCheck.position, wallRunLeftCheck.position + wallRunLeftCheck.forward * wallRunCheckDistance);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + -groundCheck.up * groundCheckDistance);

    }
}



[System.Serializable]
public struct Modifier
{
    public FPSCam mod;
    public Collider col;
    public int walkIstanceID;

    public void ModificationCallback(PhysicsScene scene, NativeArray<ModifiableContactPair> pair)
    {
        if (mod.contactMod)
        {
            for (int i = 0; i < pair.Length; i++)
            {
                for (int j = 0; j < pair[i].contactCount; j++)
                {
                    int colID = pair[i].colliderInstanceID;
                    int otherID = pair[i].otherColliderInstanceID;


                    if (colID == walkIstanceID)
                    {
                        if (!mod.inAir)
                        {
                            mod.veltoprint = pair[i].GetTargetVelocity(j);
                            mod.Datoprint = Vector3.ProjectOnPlane(mod.movementVector, pair[i].GetNormal(j));
                            mod.Datoprint += mod.jumpVector;
                            pair[i].SetStaticFriction(j, mod.friction);
                            pair[i].SetDynamicFriction(j, mod.friction);
                            pair[i].SetTargetVelocity(j, mod.veltoprint + mod.Datoprint);
                        }

                    }


                }
            }
        }
    }
}
