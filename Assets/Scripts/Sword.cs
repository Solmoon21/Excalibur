using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MyUtils;
using System.Linq;

public class Sword : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public PolygonCollider2D pol;
    [HideInInspector]public Vector3 position {get {return transform.position;} }
    [SerializeField]float rotationSpeed;

    [Header("Jump Stamina Variables")]
    float maxStamina = 100f;
    public float currStamina {get; private set; } = 100f;
    [Range(1,10)]
    public float drainMultiplier = 2f;
    [Range(1,20)]
    public float jumpCost = 5f;
    public bool isAiming, isFalling; // Player's dragging action   
    int health = 3;
    [SerializeField]
    Image energy;
    [SerializeField]
    Transform hearts;
    public Vector2 wallPos;

    #region Rotation
    bool isAirborne = false;
    float maxTime = 1f,elapsed;
    #endregion

    public float decelerationMultiplier = 0.5f;
    public float stickTime = 0.5f;
    bool isHitHandle = false;
    GameManager gameManager;
    Transform cameraPos;

    bool invisible = false;
    string[] stickSurfaces;

    void Awake(){
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        pol = GetComponent<PolygonCollider2D>();
        cameraPos = Camera.main.transform;
        stickSurfaces = new string[] { Tags.PLEFT, Tags.PRIGHT};
    }

    public void Push(Vector2 force){
        if(gameManager.isPaused)
            return;
        isAiming = false;
        ActivateRb();
        rb.AddForce(force,ForceMode2D.Impulse);
        rotationSpeed = force.magnitude;
        isAirborne = true;
        isHitHandle = false;
        currStamina -= jumpCost * (rotationSpeed/10f);
        currStamina = Mathf.Clamp(currStamina,0,maxStamina);
    }

    bool CanStick () {
        if(wallPos == Vector2.zero)
            return true;
        return Mathf.Abs(wallPos.x-transform.position.x)>=1.5f;
    }

    void Update(){
        if(gameManager.isPaused)
            return;

        if(gameManager.isOver)
            return;

        FallDeath();

        if(!isHitHandle && isAirborne && rb.velocity.magnitude>10f){
            transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            if(elapsed >= maxTime){
                transform.Rotate(Vector3.zero);
            }
        }

        UpdateStamina();

        
    }

    public void ActivateRb(){
        rb.isKinematic = false;
        pol.enabled = true;
    }

    public void DeactivateRb(){
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        pol.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.otherCollider.gameObject.name + " hit " +col.gameObject.name);
        if(gameManager.isOver)
            return;
        
        if(col.otherCollider is BoxCollider2D){
            isHitHandle = true;
            rb.AddForce(-rb.velocity * decelerationMultiplier, ForceMode2D.Impulse);
            if(!invisible && col.collider.tag == Tags.MONSTER){
               Damage();
            }
            return;
        }
        

        switch (col.transform.tag)
        {
            case Tags.PLEFT:
            case Tags.PRIGHT:
            // && rb.velocity.magnitude > 10f    
                if(!isHitHandle && isAirborne && CanStick()){
                    wallPos = col.transform.position;
                    DeactivateRb();
                    isAirborne = false;
                }
                break;
            case Tags.MONSTER:
                col.transform.GetComponent<Monster>().Die();
                break;
            default:
                break;
        }


    }

    void UpdateStamina(){
        if(isAiming){
            currStamina -= Time.deltaTime * drainMultiplier;
            if(currStamina <= 0){
                isFalling = true;
                isAiming = false;
            }
        }
        else{
            currStamina += Time.deltaTime * drainMultiplier;
        }
        energy.fillAmount = currStamina / maxStamina;
    }

    void Damage(){
        StartCoroutine(Invisible());
        health--;
        if(health == 0) 
            gameManager.GameOver();

        if(health < 0)
            return;    
        GameObject heart = hearts.GetChild(health).gameObject;
        Utils.DamagePlayer(heart);
    }

    bool FallDeath(){
        bool death = false;
        if(transform.position.y < cameraPos.position.y - 7.5f){
            Debug.Log("Died");
            gameManager.GameOver(); 
            death = true;
        }
        return death;
    }

    IEnumerator Invisible(){
        invisible = true;
        yield return new WaitForSeconds(2f);
        invisible = false;
    }
    
}
