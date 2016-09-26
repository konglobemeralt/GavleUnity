using UnityEngine;
using System.Collections;


public class cameraMain : MonoBehaviour
{
    //Cameras
    public Camera cameraFPS;
    public Camera cameraBird;

    public GameObject characterFPS;
    public GameObject characterBird;

    public float speed = 10.0f;
    public float scrollSpeed = 20.0f;

    //rotation attributes
    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    private float translation;
    private float strafe;
    private float height;

    private float guiMoveX;
    private float guiMoveZ;
    private float guiRotY;
    private float guiRotX;

    public float yMax = 75;
    public float yMin = 5;

    void LateUpdate()
    {
       

         
        moveCamera();
        rotateCamera();
        getInput();  
        

    }


    void Start()
    {

        cameraFPS = GameObject.Find("cameraFPS").GetComponent<Camera>();
        cameraBird = GameObject.Find("cameraBird").GetComponent<Camera>();

        characterFPS = cameraFPS.transform.parent.gameObject;
        characterBird = cameraBird.transform.parent.gameObject;

        cameraFPS.enabled = true;
        cameraBird.enabled = false;

    }


    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 500, 100), "Camera Controlls");

        //Switch To bird
        if (GUI.Button(new Rect(20, 40, 80, 20), "BirdCam"))
        {
            cameraBird.enabled = true;
            cameraFPS.enabled = false;

        }

        //Switch To Fps
        if (GUI.Button(new Rect(20, 70, 80, 20), "FPS cam"))
        {
            cameraBird.enabled = false;
            cameraFPS.enabled = true;

        }

        
        if (GUI.Button(new Rect(120, 40, 80, 20), "+x"))
        {
            guiMoveX = 100;
            
        }

        if (GUI.Button(new Rect(120, 70, 80, 20), "-x"))
        {
            guiMoveX = -100;
        }

        if (GUI.Button(new Rect(220, 40, 80, 20), "+z"))
        {
            guiMoveZ = 100;

        }

        if (GUI.Button(new Rect(220, 70, 80, 20), "-z"))
        {
            guiMoveZ = -100;
        }

        if (GUI.Button(new Rect(320, 40, 80, 20), "RotUp"))
        {
            guiRotX = 100;
        }

        if (GUI.Button(new Rect(320, 70, 80, 20), "RotDown"))
        {
            guiRotX = -100;
            
        }

        if (GUI.Button(new Rect(420, 40, 80, 20), "RotRight"))
        {
            guiRotY = 100;
        }

        if (GUI.Button(new Rect(420, 70, 80, 20), "RotLeft"))
        {
            guiRotY = -100;

        }


    }

    void getInput()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            switchCamera();
           
        }

        translation = Input.GetAxis("Vertical") * speed;
        strafe = Input.GetAxis("Horizontal") * speed;
        height = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
       

    }


    void switchCamera()
    {
        //toggle enabled camera
        cameraFPS.enabled = !cameraFPS.enabled;
        cameraBird.enabled = !cameraBird.enabled;
    }


    void moveCamera()
    {
        

        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;
        guiMoveX *= Time.deltaTime;
        guiMoveZ *= Time.deltaTime;
        guiRotX *= Time.deltaTime;
        guiRotY *= Time.deltaTime;

        if (cameraFPS.isActiveAndEnabled)
        {
            characterFPS.transform.Translate(strafe + guiMoveZ, 0, translation + guiMoveX);
        }
        else if (cameraBird.isActiveAndEnabled)
        {
            characterBird.transform.Translate(strafe + guiMoveZ, height, translation + guiMoveX);
        }


    }

    void rotateCamera()
    {
        var md = new Vector2(0, 0);

        if (Input.GetMouseButton(1))
        {

            md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        else
        {
            md = new Vector2(guiRotY, guiRotX);
        }
                md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
                smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                mouseLook += smoothV;

                mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            if (cameraFPS.isActiveAndEnabled)
            {
                cameraFPS.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                characterFPS.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, characterFPS.transform.up);
            }
            else if (cameraBird.isActiveAndEnabled)
            {
                cameraBird.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                characterBird.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, characterBird.transform.up);
            }

        }
    }

