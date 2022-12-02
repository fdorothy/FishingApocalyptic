using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool useStatic = true;
    public bool yonly = true;
    new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (useStatic)
        {
            transform.rotation = camera.transform.rotation;
        }
        else
        {
            Vector3 p = camera.transform.position;
            transform.LookAt(p);
            if (yonly)
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
}
