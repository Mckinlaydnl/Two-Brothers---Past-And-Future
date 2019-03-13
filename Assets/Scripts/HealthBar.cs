using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    // Use this for initialization
    public Image playerHealthBar;

    private float healthPoints = 100;
    private float maxHealthPoints = 100;
    void Start () {
        UpdateHealthBar();
		
	}
    private void UpdateHealthBar()
    {
        float ratio = healthPoints / maxHealthPoints;
        playerHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    
    private void RecieveDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <0)
        {
            healthPoints = 0;
        }
        UpdateHealthBar();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
