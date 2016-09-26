//using UnityEngine;
//using System.Collections;

//public class characterController : MonoBehaviour
//{

//    public float speed = 10.0f;
//    public Camera cameraFPS;
//    public Camera cameraBird;

//    private Camera activeCamera;
//    // Use this for initialization

//    void Start()
//    {
//        cameraFPS = GameObject.Find("cameraFPS").GetComponent<Camera>();
//        cameraBird = GameObject.Find("cameraFPS").GetComponent<Camera>();

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (cameraFPS.isActiveAndEnabled)
//        {
//            activeCamera = cameraFPS;
//        }
//        else
//        {
//        }
//            float translation = Input.GetAxis("Vertical") * speed;
//            float strafe = Input.GetAxis("Horizontal") * speed;
//            translation *= Time.deltaTime;
//            strafe *= Time.deltaTime;

//            activeCamera.transform.Translate(strafe, 0, translation);
//    }



//}
