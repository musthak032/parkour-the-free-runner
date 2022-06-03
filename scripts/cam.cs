using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player;
    public Transform playerdethdis;
    public float camdisz = 7.8f;
    public float camdeathdisz = 7.8f;
    public float camdizy;

    playercontrol playercon;
    void Start()
    {
        playercon=player.gameObject.GetComponent<playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        //transform.position = new Vector3(transform.position.x, player.position.y+camdizy, player.position.z - camdisz);
        if (!playercon.playerdeath)
        {
            transform.position = new Vector3(player.position.x, player.position.y + camdizy, player.position.z - camdisz);

        }
        else
        {
            transform.position = new Vector3(playerdethdis.position.x, playerdethdis.position.y + camdizy, playerdethdis.position.z - camdeathdisz);

        }

    }
}
