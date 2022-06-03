using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    [Header("player component")]
    public CharacterController cc;
    public Animator anim;
    public mobilecon swipemanager;
    Vector3 velocity;

    [Header("player state")]
    public bool playerdeath=false;

    [Header("move")]
    public float movespeed;
    public float speedupmovespeed;
    public float speedmoveovertime;
    float speedmovecurrenttime;
    float currentspeed;
   public bool speedup;


    [Header("jump move")]

    public float jumpforce;
    public float gravity = -9.8f;
    public bool isground;

    [Header("player move speed turn")]

  
    public float minside;   
    public float maxside;

    [Header("jumpdetector")]

    public GameObject bigjumpdetect;
    public Vector3 bigjumpoffset;
    public GameObject smalljumpdetect;
    public Vector3 smalljumpoffset;
    public LayerMask jumpobt;

    Vector3 collisionoffset;
    float colheight;
    bool jumpaction;


    [Header("open walk")]

    bool walk;

    [Header("player offturn")]

    public float turntime;

    public bool jumpdetct;

    bool isturn;
    float ttime;

    [Header("player slide")]

    public float slidetime = 1.10f;
    float stime;
    bool slideaction;

    [Header("ragdoll")]
    public rigdollonoff ragdoll;


   public List<GameObject> powercolibj = new List<GameObject>();
    void Start()
    {
        //walking
        walk = true;

        anim.SetBool("iswalk", true);
        anim.applyRootMotion = true;
        //
        playerdeath = false;
        stime = slidetime;
        ttime = turntime;
        collisionoffset = cc.center;
        colheight = cc.height;
        //speedmovetime
        currentspeed = movespeed;
        speedmovecurrenttime = speedmoveovertime;

        Invoke("stopwalk", 5f);
    }

   void stopwalk()
    {
        walk = false;
        anim.SetBool("iswalk", false);
        anim.applyRootMotion = false;
    }
    void Update()
    {


        if (!walk)
        {


            if (!playerdeath)
            {

                slide();
                turnplayer();
                movejump();
            }

        }

    }
    void movejump()
    {
        Vector3 movevector = transform.TransformDirection(Vector3.forward);
        //y-=gravity*-2f*Time.deltaTime;

        anim.SetBool("isrunning", true);

        if (cc.isGrounded)
        {
            anim.SetBool("njump", false);
            velocity.y = -1f;
            jumpcontrol();

        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }
        speedmove();

        if (!jumpaction)
        {


            cc.Move(movevector * movespeed * Time.deltaTime);
            cc.Move(velocity * Time.deltaTime);
        }

    }

    void speedmove()
    {

        if (speedup)
        {
            movespeed = speedupmovespeed;
            speedmovecurrenttime -= Time.deltaTime;
            if (speedmovecurrenttime <= 0)
            {
                movespeed = currentspeed;
                ragdoll.offtraileffect();
                speedmovecurrenttime = speedmoveovertime;
                speedup = false;
            }
        }
    }

  public void ontrail()
    {

        ragdoll.ontraileffect();
    }

    private void jumpcontrol()
    {
        

        if (Input.GetKeyDown(KeyCode.W)||swipemanager.swipeup)
        {
            if (!slideaction)
            {

               


                if (!detectbigjump() && !detectsmalljump())
                {
                    velocity.y = jumpforce;
                    anim.SetBool("njump", true);

                }
                if (detectbigjump() && !detectsmalljump() && !jumpaction)
                {
                    anim.applyRootMotion = true;
                    jumpaction = true;

                    anim.SetBool("bigjump", true);
                    cc.center = new Vector3(0, 1.9f, 0);
                    cc.height = 1f;


                }
                if (detectsmalljump() && detectbigjump() && !jumpaction)
                {

                    anim.applyRootMotion = true;
                    jumpaction = true;
                    cc.center = new Vector3(0, 1.9f, 0);
                    cc.height = 1f;

                    int no = new int();
                    no = Random.Range(0, 4);

                    if (no <= 2)
                    {
                        anim.SetBool("jump2", true);
                    }
                    if (no > 2)
                    {
                        anim.SetBool("jump1", true);

                    }



                }
            }
            


        }


    }
    void slide()
    {
        if ((Input.GetKey(KeyCode.S) )||(swipemanager.swipedown) )
        {
            if (!jumpaction&&!slideaction)
            {
                if (cc.isGrounded)
                {

                    cc.center = new Vector3(0, .4f, 0);
                    cc.height = .2f;

                    anim.applyRootMotion = true;

                    slideaction = true;

                    anim.SetBool("isslide", true);

                }
            }
        }

        if (slideaction)
        {
            stime -= Time.deltaTime;
            if (stime <= 0)
            {
                
                stime = slidetime;
                offslide();
            }

        }
    }
    void turnplayer()
    {
        if (transform.position.x > minside)
        {
            
            if (Input.GetKeyDown(KeyCode.A)||swipemanager.swipeleft)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, -45, transform.eulerAngles.z));
                //Invoke("offturn", turntime);
                isturn = true;

            }
        }
        if (transform.position.x < maxside)
        {

            if (Input.GetKeyDown(KeyCode.D)||swipemanager.swiperight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 45, transform.eulerAngles.z));
                //Invoke("offturn", turntime);
                isturn = true;
            }
        }
        if (transform.position.x < minside)
        {
            transform.position = new Vector3(minside, transform.position.y, transform.position.z);
            //if (isturn)
            //{
            //    offturn();

            //}
        }
        if (transform.position.x > maxside)
        {
            transform.position = new Vector3(maxside, transform.position.y, transform.position.z);
            //if (isturn)
            //{
            //    offturn();

            //}
        }

        if (isturn)
        {
            ttime -= Time.deltaTime;
            if (ttime <= 0)
            {

                isturn = false;
                ttime = turntime;
                offturn();
            }
        }
    }
    void offturn()
    {
       
      
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z));
    }

   

    //check style jump
    bool detectbigjump()
    {

       
        if (Physics.CheckBox(bigjumpdetect.transform.position, bigjumpoffset, transform.rotation, jumpobt))
        {
            return true;
        }
        else
        {
            return false;
        }
     
    }
    bool detectsmalljump()
    {

        
        if (Physics.CheckBox(smalljumpdetect.transform.position, smalljumpoffset, transform.rotation, jumpobt))
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "obtacles")
        {
            if (!speedup)
            {


                playerdeath = true;

                ragdoll.ragdollmodeon();
            }
            else
            {
                //if (hit.collider.gameObject.layer == jumpobt)
                {
                    powercolibj.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                    Invoke("onpowercolliobj", 3f);
                }
            }

        }
  
    }
    void onpowercolliobj()
    {

        foreach (GameObject obj in powercolibj)
        {
            obj.SetActive(true);
        }
        powercolibj.Clear();
    }
   
    void reload()
    {
        SceneManager.LoadScene(0);
    }

    public void offslide()
    {


        velocity.y = -1f;
        cc.center = collisionoffset;
        cc.height = colheight;
       
        anim.applyRootMotion = false;
        anim.SetBool("isslide", false);
        slideaction = false;



    }
    public void offbigjump()
    {
        //col normal
        cc.center = collisionoffset;
        cc.height = colheight;
        //
        velocity.y = -1f;
        jumpaction = false;
        anim.applyRootMotion = false;
        anim.SetBool("bigjump", false);

    }
    public void offjump1()
    {
        
        jumpaction = false;
        velocity.y = -1f;

        anim.applyRootMotion = false;
        anim.SetBool("jump1", false);
        //col normal
        cc.center = collisionoffset;
        cc.height = colheight;
        //

    }
    public void offjump2()
    {
        //col normal
        cc.center = collisionoffset;
        cc.height = colheight;
        //
        jumpaction = false;

        velocity.y = -1f;
        anim.applyRootMotion = false;
        anim.SetBool("jump2", false);
        cc.center = collisionoffset;
        cc.height = colheight;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
      
        Gizmos.DrawWireCube(bigjumpdetect.transform.position, bigjumpoffset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(smalljumpdetect.transform.position, smalljumpoffset);

    }

}
