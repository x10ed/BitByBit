using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts.Soomla {
    public class SoomalStoreWraper {

      
	}
	
	public class BuyFullVersion : MonoBehaviour {

        public GameObject errorScreen;
        GameObject test;
        // Use this for initialization
        void Start () {
            // MainMenu.redirectToShopScreen = false;
            test = GameObject.Find("parentalGate");
            test.SetActive(false);
        }

        void OnMouseUp(){


            test.SetActive(true);

        }
	}
}