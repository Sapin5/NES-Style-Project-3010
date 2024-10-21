using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    private int track2 = 0;
    private int currheart = 1;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private int heartToAdd;
    private ArrayList hearts = new();
    private Transform heartDisp;
    [SerializeField]private Sprite[] heartStage;
    private Vector3 distanceBetween = new (0f, 0f, 0f);

    private void Awake(){
    
        heartDisp = GetComponent<Transform>();
    
        for(int i = 0; i<heartToAdd; i++){
            AddHearts();
        }

        CheckHeartAmount();
    }

    public void AddHearts(){
        GameObject obj = Instantiate(heartPrefab, transform.position+distanceBetween, transform.rotation) ;
        obj.transform.SetParent(heartDisp, true);
        distanceBetween+=new Vector3(0.2f, 0f, 0f);
    }

    public void CheckHeartAmount(){
        foreach (Transform child in heartDisp){
            hearts.Add(child);
        }
    }

    public void UpdateHP(){
        track2+=1;
        hearts[^currheart].ConvertTo<Transform>().GetComponent<SpriteRenderer>().sprite = heartStage[track2];
        if(track2 >= 2){
            currheart+=1;
            track2 = 0;
        }
    }

    public void HealOne(){
        if(track2 <= 0){
            currheart-=1;
            track2 += 1;
            hearts[^currheart].ConvertTo<Transform>().GetComponent<SpriteRenderer>().sprite = heartStage[track2];
        }else{
            track2-=1;
            hearts[^currheart].ConvertTo<Transform>().GetComponent<SpriteRenderer>().sprite = heartStage[track2];
        }
    }

    public void FullHeal(){
        currheart =1;
        track2 = 0;
        foreach (Transform heart in hearts){
            heart.ConvertTo<Transform>().GetComponent<SpriteRenderer>().sprite = heartStage[track2];
        }
    }
}
