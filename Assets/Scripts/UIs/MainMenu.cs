using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using MyUtils;

using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject background,title,start,play,setting,settingpanel,highScore,loader;

    public GameObject pI,sI,container,sfx,music,sT,T;

    Animator anim;
    bool settingOn = false;
    void Awake(){
        background = Utils.GOByIndex(transform,0);
        title = Utils.GOByIndex(transform,1);
        start = Utils.GOByIndex(transform,2);
        play = Utils.GOByIndex(transform,3);
        setting = Utils.GOByIndex(transform,4);
        settingpanel = Utils.GOByIndex(transform,5);
        highScore = Utils.GOByIndex(transform,6);
        loader = Utils.GOByIndex(transform,7);
        anim = loader.GetComponent<Animator>();

        pI = Utils.GOByIndex(transform,3,0);
        sI = Utils.GOByIndex(transform,4,0);
        container = Utils.GOByIndex(transform,5,0);
        sfx = Utils.GOByIndex(transform,5,0,0);
        music = Utils.GOByIndex(transform,5,0,1);
        sT = Utils.GOByIndex(transform,5,0,2);
        T = Utils.GOByIndex(transform,5,0,2,0);
    }

    void Start(){

        setting.GetComponent<Button>().onClick.AddListener(()=>{
            settingOn = true;
            Utils.ClickAnim(setting);
            Utils.MoveTo(settingpanel,new Vector3(0,0,0),true);
            settingpanel.SetActive(true);
        });

        sfx.GetComponent<Button>().onClick.AddListener(() => {
            Utils.ClickAnim(sfx);
        });

        music.GetComponent<Button>().onClick.AddListener(() => {
            Utils.ClickAnim(music);
        });

        if(PlayerPrefs.HasKey("HighScore")){
            highScore.GetComponent<TextMeshProUGUI>().text = "Highscore : " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void ToggleHover(){
        if(settingOn){
            Utils.MoveTo(settingpanel,new Vector3(0,800,0),true);
            settingOn = false;
        }
        else{
            anim.SetTrigger("Play");
            Utils.ToScene(gameObject,1);
        }
    }
    
}
