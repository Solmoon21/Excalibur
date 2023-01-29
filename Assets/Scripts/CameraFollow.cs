using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    bool follow = true;
    [SerializeField]
    float speed = 0.125f;
    public Vector3 offset;
    void FixedUpdate(){
        Follow();
    }

    void Follow(){
        follow = player.position.y - speed > transform.position.y;
        if(follow){
            Vector3 temp = transform.position + offset;
            temp.y = player.position.y;
            Vector3 pos = Vector3.Lerp(transform.position,temp,speed);
            transform.position = pos;
        }
    }
}