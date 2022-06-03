using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class characterik : MonoBehaviour
{
    public Animator anim;
    public LayerMask layermask;
    [Range(0,1f)]
    public float distancetoground;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));

            //anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);    
            //anim.SetIKPositionWeight(AvatarIKGoal.RightFoot,1f);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot,1f);

            //leftfoot
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, distancetoground + 1f,layermask))
            {

                //if (hit.transform.tag == "walkable")
                {
                    Vector3 footposition = hit.point;
                    footposition.y += distancetoground;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footposition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
            //rightfoot
            
             ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distancetoground + 1f, layermask))
            {

                //if (hit.transform.tag == "walkable")
                {
                    Vector3 footposition = hit.point;
                    footposition.y += distancetoground;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footposition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

        }
   
    }
   
}
