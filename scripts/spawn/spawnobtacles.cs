using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnobtacles : MonoBehaviour
{
    public GameObject[] obtacles;

    public Transform[] position;

    public bool obtacle;
    List<GameObject> spawnobj = new List<GameObject>();

    int num;

    playercontrol player;
    void Start()
    {
        player = FindObjectOfType<playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!player.playerdeath)
        {


            num = Random.Range(0, 20);

            if (other.gameObject.tag == "Player" && obtacle)
            {

                for (int i = 0; i < position.Length; i++)
                {

                    GameObject obj = Instantiate(obtacles[Random.Range(0, obtacles.Length)], position[i].position, Quaternion.identity);
                    spawnobj.Add(obj);
                }
            }
            //if (num > 15)
            {


                if (other.gameObject.tag == "Player" && !obtacle)
                {
                    //for (int i = 0; i < position.Length; i++)
                    //{

                    //    GameObject obj = Instantiate(obtacles[Random.Range(0, obtacles.Length)], position[i].position, Quaternion.identity);
                    //    spawnobj.Add(obj);
                    //}
                    GameObject obj = Instantiate(obtacles[Random.Range(0, obtacles.Length)], position[Random.Range(0, position.Length)].position, Quaternion.identity);


                }
            }
            //else
            //{
            //    //GameObject obj = Instantiate(obtacles[Random.Range(0, obtacles.Length)], position[Random.Range(0, position.Length)].position, Quaternion.identity);
            //}
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i =0; i<spawnobj.Count; i++)
            {
                Destroy(spawnobj[i].gameObject);
            }
            spawnobj.Clear();

        }
    }




}
