using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    private int track2 = 0;
    private int currheart = 1;

    [Header("Icon for Hearts or Shields:")]
    [SerializeField] private GameObject heartPrefab;
    [Header("Sprites for different states:")]
    [SerializeField]private Sprite[] heartStage;
    private float heartToAdd;
    private readonly ArrayList hearts = new();
    private Transform heartDisp;
    
    private Vector3 distanceBetween = new (0f, 0f, 0f);
    [Header("Enable if this is the Shield:")]
    [SerializeField] private bool isShield;

    private void Awake(){
        if(!isShield){
            heartToAdd = FindAnyObjectByType<PlayerHealth>().GetHealth()/2;
        }else{
            heartToAdd = FindAnyObjectByType<Shield>().GetShield()/2;
        }
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
        if(track2 >= heartStage.Length-1){
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

    public void BigHit(){
        currheart =1;
        track2 = 0;
        foreach (Transform heart in hearts){
            heart.ConvertTo<Transform>().GetComponent<SpriteRenderer>().sprite = heartStage[track2];
        }
    }
}
