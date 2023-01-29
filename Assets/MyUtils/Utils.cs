using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


namespace MyUtils{
    public class Utils
    {
        public static GameObject GOByIndex(Transform parent,params int[] nums){
            Transform holder = parent;
            foreach (int i in nums)
            {
                holder = holder.GetChild(i);
            }

            return holder.gameObject;
        }

        public static void ClickAnim(GameObject g){
            Transform tran = g.transform;
            tran.DORewind();
            tran.DOPunchScale(Vector3.one * 0.8f,.25f);
        }

        public static void DamagePlayer(GameObject g){
            Transform tran = g.transform;
            tran.DORewind();
            tran.DOPunchScale(Vector3.one * 0.8f,.25f).OnComplete(()=> g.GetComponent<Image>().DOFade(0f,.5f)) ;
        }

        public static void DamageEnemy(GameObject g){
            Transform tran = g.transform;
            tran.DORewind();
            tran.DOPunchScale(Vector3.one * 0.8f,.25f).OnComplete(()=> g.GetComponent<SpriteRenderer>().DOFade(0f,.5f)) ;
        }

        public static void MoveTo(GameObject g,Vector3 end,bool local){
            Transform tran = g.transform;
            if(local)
                tran.DOLocalMove(end,1.25f,true);
            else
                tran.DOMove(end,1.25f,true);
        }

        public static void ToScene(GameObject g,int i){
            Transform tran = g.transform;
            tran.DOLocalMoveZ(0f,1.5f).OnComplete( ()=> SceneManager.LoadScene(i) ); 
        }   
    }
}
