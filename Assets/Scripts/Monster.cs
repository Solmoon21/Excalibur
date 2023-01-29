using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyUtils;

public class Monster : MonoBehaviour
{
    bool isAlive = false;
    public float speed;
    [SerializeField]
    Transform[] positions;
    int index;
    Rigidbody2D rb;
    GameManager iManager;
    [SerializeField]
    Tags.MonsterType type;

    public void SetUp(Transform[] pos){
        positions = pos;
        index = transform.parent.tag == Tags.PLEFT ? 1 : 0;
        isAlive = true;
    }

    void Start(){
        iManager = FindObjectOfType<GameManager>();
    }

    void Update(){
        if(!isAlive)
            return;

        transform.position = Vector2.MoveTowards(transform.position,positions[index].position,speed * Time.deltaTime);
        if(transform.position == positions[index].position){
            index = (index+1)%2;
        }
    }

    public void Die(){
        GetComponent<BoxCollider2D>().enabled = false;
        iManager.IncreaseScore(MyPoint());
        Utils.DamageEnemy(gameObject);
        Destroy(gameObject,1f);
    }

    int MyPoint(){
        switch (type)
        {
            case Tags.MonsterType.RED:
                return 7;
            case Tags.MonsterType.GREEN:
                return 5;
            case Tags.MonsterType.BAT:
                return 10;
            default:
                return 0;
        }
    }

}
