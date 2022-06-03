using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobilecon : MonoBehaviour
{
    //swipe1
    Vector2 starttouchpos, endtouchpos;
    Vector2 timedelta;

    //swipe2

    Vector2 fposition;
    Vector2 lposition;
    float screenheight;

    public bool swipeleft, swiperight,swipeup,swipedown;
    void Start()
    {
        screenheight = Screen.height * 5 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        //swipe1();
        swipe2();
    }

    private void swipe2()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fposition = touch.position;
                lposition = touch.position;
            }
            else if (touch.phase==TouchPhase.Moved)
            {
                lposition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lposition = touch.position;

                //check conndition />20%

                if (Mathf.Abs(lposition.x - fposition.x) > screenheight || Mathf.Abs(fposition.y - lposition.y) > screenheight)
                {

                    //check vecrtical or horizontal

                    if (Mathf.Abs(lposition.x - fposition.x) > Mathf.Abs(lposition.y - fposition.y))
                    {
                        if (lposition.x > fposition.x)
                        {
                            swiperight = true;

                            swipeleft = false;
                           
                            swipeup = false;
                            swipedown = false;
                            Invoke("offswipe", .5f);

                        }
                        else
                        {
                            swipeleft = true;
                           
                            swiperight = false;
                            swipeup = false;
                            swipedown = false;
                            Invoke("offswipe", .5f);

                        }
                    }
                    else
                    {
                        if (lposition.y > fposition.y)
                        {
                            swipeup = true;
                            swipeleft = false;
                            swiperight = false;
                           
                            swipedown = false;
                            Invoke("offswipe", .5f);


                        }
                        else
                        {
                            swipedown = true;
                            swipeleft = false;
                            swiperight = false;
                            swipeup = false;
                            Invoke("offswipe", .5f);


                        }
                    }
                }


            }
        }
    }

    private void swipe1()
    {
        timedelta = Vector2.zero;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            starttouchpos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endtouchpos = Input.GetTouch(0).position;
            timedelta = endtouchpos - starttouchpos;



            if ((endtouchpos.x < starttouchpos.x) && (endtouchpos.y > 200f && endtouchpos.y < 700f))
            {
                swipeup = false;

                swipeleft = true;
                swiperight = false;
                swipedown = false;


                Invoke("offswipe", .1f);
                return;

            }
            if ((endtouchpos.x > starttouchpos.x) && (endtouchpos.y > 200f && endtouchpos.y < 700f))
            {
                swipeup = false;
                swipeleft = false;
                swiperight = true;
                swipedown = false;



                Invoke("offswipe", .1f);
                return;


            }
            if ((endtouchpos.y > starttouchpos.y))
            {
                swipeup = true;
                swipeleft = false;
                swiperight = false;
                swipedown = false;


            }
            if ((endtouchpos.y < starttouchpos.y))
            {
                swipedown = true;
                swipeup = false;
                swipeleft = false;
                swiperight = false;



            }

            Invoke("offswipe", .1f);


        }
    }

    void offswipe()
    {
        swipeleft = false;
        swiperight = false;
        swipeup = false;
        swipedown = false;
    }
}
