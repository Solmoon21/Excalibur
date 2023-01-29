using UnityEngine;
using System.Collections;
public class Platform : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsters;
    Transform spawnpoint;
    float length;
    [SerializeField]
    Transform[] positions;

    void Awake(){
        spawnpoint = transform.GetChild(0);
        length = GetComponent<SpriteRenderer>().bounds.size.x/4; // 0.98 / 2
    }

    void Start(){
        var enemy = GameObject.Instantiate(
            monsters[Random.Range(0,3)],
            new Vector3(spawnpoint.position.x,spawnpoint.position.y+1f,0),
            Quaternion.identity
        );
        enemy.transform.SetParent(transform);
        enemy.GetComponent<Monster>().SetUp(positions);
    }

}
