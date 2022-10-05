using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPicker : MonoBehaviour
{
    public GameObject enertyShieldPrefab;
    public int NumEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;
    // Start is called before the first frame update
    void Start()
    {   
        for (int i = 1; i <= NumEnergyShield; i++) {
            GameObject tShieldGo = Instantiate<GameObject>(enertyShieldPrefab);
            tShieldGo.transform.position = new Vector3 (0, energyShieldBottomY,0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
