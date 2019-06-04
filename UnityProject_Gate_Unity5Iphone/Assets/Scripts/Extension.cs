using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public static class Extension
    {
        static List<Vector2> _spriteNumberMapList = new List<Vector2>();

        static Extension() {
            _spriteNumberMapList.Add(new Vector2(0,0));
            _spriteNumberMapList.Add(new Vector2(0.1f,0));
            _spriteNumberMapList.Add(new Vector2(0.2f,0));
            _spriteNumberMapList.Add(new Vector2(0.3f,0));
            _spriteNumberMapList.Add(new Vector2(0.4f,0));
            _spriteNumberMapList.Add(new Vector2(0.5f,0));
            _spriteNumberMapList.Add(new Vector2(0.6f,0));
            _spriteNumberMapList.Add(new Vector2(0.7f,0));
            _spriteNumberMapList.Add(new Vector2(0.8f,0));
            _spriteNumberMapList.Add(new Vector2(0.9f,0));
           
        }

        public static void SetLabelValue(this MonoBehaviour monoBehaviour, int number) {
			SetLabelValue (monoBehaviour.transform, number);
        }


        public static void SetLabelValue(this Transform thisTransform, int number) {
			 // rename to 

            var redDot = thisTransform.Find("nr_BG");
			if (redDot == null) {
				return;
			}


            if (number < 0) {
                return;
            }
            if (number > 9) {
                return;
            }
            redDot.gameObject.GetComponent<Renderer>().material.mainTextureOffset = _spriteNumberMapList[number] ;
            //redDot.gameObject.renderer.material.mainTexture(new Vector2(0.1f, 0));


        }
    }
}