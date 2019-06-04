using UnityEngine;
using AssemblyCSharp;

namespace Assets.Scripts {
  public class Wall : MonoBehaviour {

//Seda classi kasutatakse TAG'ina
//Ball Class on kontroll mis tuvastab kas tegu on seinaga
// vaata üle ennem kui kustutad :)

	public ColorEnum IgnoreBounceColor;

    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
	
    }

    void OnTriggerEnter(Collider other) {
    }
	
    void OnTriggerExit(Collider other){
    }
  }
}
