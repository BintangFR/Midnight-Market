using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject health;
    public PlayerController playerHealth;

    public Vector3 offset = new Vector3(0,1,0);
    
    // Start is called before the first frame update
    void Start()
    {
CreateHeart();

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {

        }

    }

    public void CreateHeart(){
        GameObject newHeart = Instantiate(health);
        newHeart.transform.SetParent(this.gameObject.transform);
    }

}
