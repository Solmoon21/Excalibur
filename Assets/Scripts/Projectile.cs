using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject container;
    [SerializeField] GameObject prefab;
    [SerializeField] float dotSpacing;

    Transform[] dots;
    Vector3 position;
    float timeStamp,g;

    void Start(){
        Hide();
        Init();
    }

    public void Show(){
        container.gameObject.SetActive(true);
    }

    public void Hide(){
        container.gameObject.SetActive(false);
    }

    void Init(){
        g = Physics2D.gravity.magnitude;
        dots = new Transform[dotsNumber];
        for(int i=0;i<dotsNumber;i++){
            dots[i] = Instantiate(prefab,Vector3.zero,Quaternion.identity).transform;
            dots[i].SetParent(container.transform);
        }
    }

    public void UpdateDots(Vector3 ball,Vector2 force){
        timeStamp = dotSpacing;
        for(int i=0;i<dotsNumber;i++){
            position.x = (ball.x + force.x*timeStamp);
            position.y = (ball.y + force.y*timeStamp) - (g * timeStamp * timeStamp) / 2f;
            position.z = 0f;
            dots[i].position = position;
            timeStamp += dotSpacing;
        }
    }
}
