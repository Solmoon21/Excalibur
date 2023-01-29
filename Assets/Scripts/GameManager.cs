using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MyUtils;

using TMPro;
public class GameManager : MonoBehaviour
{
    public bool isPaused { get; private set; } = false;
    public bool isOver {get; private set; } = false;
    Transform player;
    [SerializeField]
    GameObject scorePrefab;
    int score = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    GameObject gameoOverUI;

    void Start(){
        player = FindObjectOfType<Sword>().transform;
    }

    public void Pause(){
        isPaused = !isPaused;
        Debug.Log("Paused "+isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void IncreaseScore(int point){
        Vector3 pos = new Vector3 (player.position.x,player.position.y+1f,-1);
        GameObject obj = Instantiate(scorePrefab,pos,Quaternion.identity);
        score += point;
        obj.transform.DOMoveY(pos.y+1f,.45f,false).OnComplete(()=> Destroy(obj,.5f));
        scoreText.text = score + "";
    }

    public void GameOver(){
        isOver = true;
        Utils.MoveTo(gameoOverUI,Vector3.zero,true);
        gameoOverUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score + "";
        if(UpdateHS(score)){
            // HIGHSCORE ANIM
        }

    }

    bool UpdateHS(int newscore){
        int curr = PlayerPrefs.GetInt("HighScore",-1);
        if(newscore > curr){
            PlayerPrefs.SetInt("HighScore",newscore);
            return true;
        }
        return false;
    }

    public void Restart(){
        Utils.ToScene(gameObject,1);
    }

    public void Home(){
        Utils.ToScene(gameObject,0);
    }
}
