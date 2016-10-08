using UnityEngine;
using System.Collections;

public class mouseMain : MonoBehaviour {
    public Camera cameraFPS;
    public Camera cameraBird;

   public GameObject selectedObject;

   public Shader shaderStandard;
   public Shader shaderOutline;

    void start()
    {
        cameraFPS = GameObject.Find("cameraFPS").GetComponent<Camera>();
        cameraBird = GameObject.Find("cameraBird").GetComponent<Camera>();

        shaderStandard = Shader.Find("Diffuse");
        shaderOutline = Shader.Find("Outlined/Silhouetted Diffuse");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();

            bool hit;

            if (cameraFPS.isActiveAndEnabled)
            {
                hit = Physics.Raycast(cameraFPS.ScreenPointToRay(Input.mousePosition), out hitInfo);
            }
            else
            {
                hit = Physics.Raycast(cameraBird.ScreenPointToRay(Input.mousePosition), out hitInfo);
            }
            
            if (hit)
            {
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Houses")
                {
                    selectObject(hitInfo.transform.gameObject);
                   // Debug.Log("It's working!");
                }
                else {
                   // Debug.Log("nopz");
                    
                }
            }
            else {
                //Debug.Log("No hit");
                clearSelection();
            }
   
        }
    }


    void selectObject(GameObject obj)
    {
        
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;


            clearSelection();

        }
        selectedObject = obj;

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            foreach (Material mat in r.materials)
            {
                mat.shader = shaderOutline;

                //mat.color = Color.green;
            }
        }
    }

    void clearSelection()
    {

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            foreach (Material mat in r.materials)
            {
                mat.shader = shaderStandard;
                //mat.color = Color.white;
            }
        }
        selectedObject = null;
    }
}
