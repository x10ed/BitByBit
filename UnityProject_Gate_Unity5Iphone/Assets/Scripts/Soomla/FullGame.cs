using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AssemblyCSharp
{
	public class FullGame 
	{



		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion() {
			return 0;
		}
		/*
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies() {
			return new VirtualCurrency[]{};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods() {
			return new VirtualGood[] { FULL_GAME};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{};
		}

		public const string FULL_GAME_PRODUCT_ID = "bitbybitfull"; */
		
		
		/** Virtual Currency Packs **/	
		
		
		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		/* public static VirtualGood FULL_GAME = new LifetimeVG(
			"bitbybitfull", 														// name
			"buy a full game",				 									// description
			"bitbybitfull",														// item id
			new PurchaseWithMarket(FULL_GAME_PRODUCT_ID, 3.99)); */	// the way this virtual good is purchased
	}
}

