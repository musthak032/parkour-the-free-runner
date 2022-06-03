using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigdollonoff : MonoBehaviour
{
    public CharacterController cc;

    public GameObject thisguyrig;

    public Animator thisguyanim;

    Collider[] ragdollcol;
    Rigidbody[] limbsrb;
    TrailRenderer[] trail;
    public bool ragdollmode = false;
    void Start()
    {
        getragdollbits();
        ragdollmodeoff();
        offtraileffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void getragdollbits()
    {
        ragdollcol = thisguyrig.GetComponentsInChildren<Collider>();
        limbsrb = thisguyrig.GetComponentsInChildren<Rigidbody>();
        trail = thisguyrig.GetComponentsInChildren<TrailRenderer>();
    }
    public   void ragdollmodeon()
    {
      


            thisguyanim.enabled = false;


            foreach (Collider col in ragdollcol)
            {
                col.enabled = true;
            }
            foreach (Rigidbody rig in limbsrb)
            {
                rig.isKinematic = false;

            }
            cc.enabled = false;
            //ragdollmode = true;
        
    }  
     public  void ragdollmodeoff()
     {

        foreach(Collider col in ragdollcol)
        {
            col.enabled = false;
        }     
        foreach(Rigidbody rig in limbsrb)
        {
            rig.isKinematic = true;

        }

        thisguyanim.enabled = true;
        cc.enabled = true;
        //ragdollmode = false;
    }

    public void ontraileffect()
    {

        foreach(TrailRenderer traileffect in trail)
        {

            traileffect.enabled = true;
        }
    }    
    public void offtraileffect()
    {

        foreach(TrailRenderer traileffect in trail)
        {

            traileffect.enabled = false;
        }
    }
}
