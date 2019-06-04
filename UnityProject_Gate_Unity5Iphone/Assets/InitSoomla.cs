using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;
using Assets.Scripts;
using UnityEngine;

public class InitSoomla : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public GameObject errorScreen;
	//static bool IsSoomlaLoaded;
	// private static List<VirtualGood> virtualGood = null;
	
	public void Init() {
		/* if (!IsSoomlaLoaded) {
			
			StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
			StoreEvents.OnMarketPurchase += onMarketPurchase;
            //StoreEvents.OnUnexpectedErrorInStore += OnUnexpectedErrorInStore;      	Error - seda enam uues versioonis pole
		    StoreEvents.OnMarketItemsRefreshFailed += OnMarketItemsRefreshFailed;

            
			SoomlaStore.Initialize(new FullGame());
		} */
	}

    private void OnMarketItemsRefreshFailed(string obj) {

        Debug.Log("OnMarketItemsRefreshFailed !!!!!!!!!!!!!! Soomla Error: " + obj);
    }

    private void OnUnexpectedErrorInStore(string obj) {
		var btn = GameObject.Find ("BuyBtn");
		Destroy (btn);
		Instantiate(errorScreen, errorScreen.transform.position, Quaternion.identity);
        Debug.Log("OnUnexpectedErrorInStore !!!!!!!!!!!!!! Soomla Error: " + obj);
    }

   /* public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra) {

		try {
			StoreInventory.RefreshLocalInventory();
			var balance2 = StoreInventory.GetItemBalance("bitbybitfull");
			MainMenu.IsFullVersionVar = balance2 > 0;
			if (MainMenu.IsFullVersionVar == true) {
				//Application.LoadLevel ("ThankYou");
			}
			
		}
		catch (Exception ex) {
			Debug.Log("Soomla Error: " + ex.Message);
			MainMenu.IsFullVersionVar = false;
		}
		
	} 
	
	
	public void onSoomlaStoreInitialized() {
		//IsSoomlaLoaded = true;
		
		virtualGood = StoreInfo.Goods;
		StoreInventory.RefreshLocalInventory();
		
		var balance2 = StoreInventory.GetItemBalance("bitbybitfull");
		MainMenu.IsFullVersionVar = balance2 > 0;
		
	} */

}
