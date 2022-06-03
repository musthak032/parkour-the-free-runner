using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stagespwn : MonoBehaviour
{
    public Transform stage1;
    public Transform stage2;
    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            stage1.position = new Vector3(stage1.position.x, stage1.position.y, stage1.position.z + distance*2f);
        }
    }
}
