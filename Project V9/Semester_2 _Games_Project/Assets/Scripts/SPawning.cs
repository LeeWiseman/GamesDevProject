using UnityEngine;
using System.Collections;

public class SPawning : MonoBehaviour {

    // Use this for initialization
    public Transform prefabForSpawning;
    public Transform enemyPrefab;

	void Start () {

    //    Instantiate(prefabForSpawning);
        Instantiate(enemyPrefab);
    }
	
	// Update is called once per frame
	void Update () {

       

       
    

}
}
