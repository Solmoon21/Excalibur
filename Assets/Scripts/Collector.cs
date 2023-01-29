using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField]
    GameObject background;
    Spawner spawner;

    float height,maxY;

    void Awake(){
        spawner = GameObject.FindObjectOfType<Spawner>();
    }

    void Start(){
        height = background.GetComponent<BoxCollider2D>().bounds.size.y;
        maxY = 5 * height;
    }

    public void Reposition(GameObject bg){
        Vector3 pos = bg.transform.position;
        pos.y = maxY-height;
        bg.transform.position = pos;
        maxY += height;
    }

    void OnTriggerEnter2D(Collider2D target){
        switch(target.tag){
            case Tags.BG:
                spawner.SpawnPlatforms();
                Reposition(target.gameObject);
                break;
            case Tags.MONSTER:
                break;
            default:
                break;
        }
    }
}
