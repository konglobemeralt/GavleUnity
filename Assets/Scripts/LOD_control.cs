﻿using UnityEngine;
using System.Collections;

public class LOD_control : MonoBehaviour
{

    //Cameras
    public Camera cameraFPS;
    public Camera cameraBird;

    public float[] ranges = new float[3] { 50, 75, 100 };
    public GameObject[] LodModels = new GameObject[3];

    public int currentLOD = 2;


    void Start()
    {

        cameraFPS = GameObject.Find("cameraFPS").GetComponent<Camera>();
        cameraBird = GameObject.Find("cameraBird").GetComponent<Camera>();

        for (int i = 0; i < LodModels.Length; i++)
        {
            LodModels[i].SetActive(false);

           
        }
    }


    void Update()
    {

        float dist = 0;

        if (cameraFPS.isActiveAndEnabled)
        {
            dist = Vector3.Distance(cameraFPS.transform.position, transform.position);
        }
        else
        {
            dist = Vector3.Distance(cameraBird.transform.position, transform.position);
        }


        if (dist < ranges[0])
        {
            currentLOD = 0;
            LodModels[0].SetActive(true);
            LodModels[1].SetActive(false);
            LodModels[2].SetActive(false);
        }
        else if (dist >= ranges[0] && dist < ranges[1] && currentLOD != 1)
        {
            currentLOD = 1;
            LodModels[0].SetActive(false);
            LodModels[1].SetActive(true);
            LodModels[2].SetActive(false);
        }
        else if (dist >= ranges[2] && dist != 2)
        {
            currentLOD = 2;
            LodModels[0].SetActive(false);
            LodModels[1].SetActive(false);
            LodModels[2].SetActive(true);

        }
    }
}
