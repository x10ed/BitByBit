using UnityEngine;
using System.Collections;
using AssemblyCSharp;

namespace Assets.Scripts {
	public class Door : BaseGate {

		public void DestroyGate(){
			Destroy(this.gameObject);
		}

	}
}