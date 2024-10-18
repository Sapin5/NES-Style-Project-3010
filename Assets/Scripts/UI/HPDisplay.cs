using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    private int track2 = 0;
    private int currheart =1;
    private ArrayList hearts = new();
    private Transform heartdisp;
    [SerializeField]private Sprite[] heartStage;

    private void Awake(){
        heartdisp = GetComponent<Transform>();
        foreach (Transform child in heartdisp){
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
