using UnityEngine;
using System.Collections;

public class CollisionControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void OnTriggerEnter(Collider other)
    {

        other.GetComponent<Controller>().AddHealth(50);
        Destroy(gameObject);
    }
}
