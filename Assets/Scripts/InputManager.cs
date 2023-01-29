using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InputManager : MonoBehaviour
{
    #region Singleton class: Input
    public static InputManager Instance;
    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    #endregion

    Camera cam;
    public Sword sword;
    public Projectile proj;
    [SerializeField] float pushForce = 4f;
    bool isDragging = false;
    [Range(1,25)]
    public float minJumpCost=10f;
    [SerializeField]
    float minDist = 1f, maxDist = 5f;

    Vector2 start,end,direction,force;
    float distance;
    GameManager gameManager;

    [SerializeField]
    GameObject scorePrefab;
    [SerializeField]
    Transform player;
    public float up,time;

    void Start(){
        cam = Camera.main;
        sword.DeactivateRb();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    } 

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
            gameManager.Pause();

        if(gameManager.isPaused)
            return;

        if(Input.GetMouseButtonDown(0)){
            isDragging = true;
            OnDragStart();
        }

        if(Input.GetMouseButtonUp(0)){
            isDragging = false;
            OnDragEnd();
        }

        if(isDragging){
            OnDrag();
        }
    }

    private void OnDrag()
    {
        //Debug.Log("Dragging");
        if(sword.isAiming){
            end = cam.ScreenToWorldPoint(Input.mousePosition);
            distance = Vector2.Distance(start,end);
            direction = (start-end).normalized;
            distance = Mathf.Clamp(distance,minDist,maxDist);
        }
        
        force = direction * distance * pushForce;
        Debug.DrawRay(start,end);
        proj.UpdateDots(sword.position,force);
        if(sword.currStamina<minJumpCost && sword.isAiming){
            OnDragEnd();
        }
    }
    
    private void OnDragEnd()
    {
        //Debug.Log("End "+force.magnitude);
        proj.Hide();
        sword.Push(force);  
    }

    void OnDragStart(){
        //Debug.Log("Start");
        proj.Show();
        //sword.DeactivateRb();
        sword.isAiming = true;
        start = cam.ScreenToWorldPoint(Input.mousePosition);
    }

}
