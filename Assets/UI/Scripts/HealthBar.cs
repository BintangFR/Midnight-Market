using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject health;
    private PlayerController playerHealth;

    private int heart;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawHeart();
    }


    public void DrawHeart(){
        GameObject newHeart;
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            if (heart < playerHealth.maxHealth)
            {
                heart +=1;
                newHeart = Instantiate(health);
                newHeart.transform.SetParent(this.gameObject.transform);
                newHeart.name = "Heart" + (i);
                newHeart.transform.localScale = new Vector3(0.25f,1,1);
            }
            if (heart > playerHealth.maxHealth)
            {
                heart -= 1;
                Destroy(GameObject.Find("Heart" + playerHealth.maxHealth));
            }
        }
    }

}
