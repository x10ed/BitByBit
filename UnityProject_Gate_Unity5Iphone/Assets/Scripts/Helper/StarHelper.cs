using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helper {
    class StarHelper {
        public static void DisableStar(GameObject gameobject){
			if (gameobject == null){
				return; 
			}
			gameobject.transform.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(0.5f,0);
        }
        public static void EnableStar(GameObject gameobject) {
			gameobject.transform.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(0, 0);
        }
    }
}
