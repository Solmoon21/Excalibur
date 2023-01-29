using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    
    [SerializeField]
    GameObject platform;
    [SerializeField]
    float point = 1.95f;
    float lastY, gap = 4f;
    int count = 10;
    [SerializeField]
    Transform holder;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){
        lastY = transform.position.y;
        SpawnPlatforms();
    }

    public void SpawnPlatforms(){
        Vector2 temp = transform.position;
        for(int i=0;i<count;i++){
            temp.y = lastY;
            temp.x = Random.Range(1,3) <= 1 ? -point : point;
            GameObject obj = Instantiate(platform,temp,Quaternion.identity);
            obj.tag = temp.x < 0 ? Tags.PLEFT : Tags.PRIGHT;
            obj.transform.SetParent(holder);
            lastY += gap;
        }
    }

}
