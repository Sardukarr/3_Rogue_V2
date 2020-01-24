using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float distanceZ = -40;
    [SerializeField] float distanceY = 18;
    //Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target= GameObject.Find("V2").transform;
      //  transform = GetComponent < Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.z += distanceZ;
        pos.y += distanceY;
        transform.position = pos;

    }
}
