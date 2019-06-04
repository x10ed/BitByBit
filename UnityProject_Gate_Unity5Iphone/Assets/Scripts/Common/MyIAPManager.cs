using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Assets.Scripts
{
	/*public class MyIAPManager : MonoBehaviour, IStoreListener {
        //string levalName = "playScreen";
        string levalName = "worldSelect";
        private IStoreController controller;
		private IExtensionProvider extensions;
		public string productId = "bitbybitfull";

		public void  Start ()
		{
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			builder.AddProduct(productId, ProductType.NonConsumable);

			UnityPurchasing.Initialize (this, builder);
		}

		public void BuyProduct(){

            Debug.Log("MyIAPManager  BuyProduct start ");
            if (IsInitialized ()) {
				
				Product product = controller.products.WithID (productId);
				if (product != null && product.availableToPurchase) {
					controller.InitiatePurchase (productId);
				} else {
					//TODO: handle this in UI
					Debug.Log ("MyIAPManager.BuyProduct -Purchase faild !!!");
				}
			} else {
				//TODO: handle this in UI
				Debug.Log ("MyIAPManager.BuyProduct - Initsialize faild!!!");
			}
			//controller.InitiatePurchase ("AddsFree");
		}

		public bool HasFullVersion(){
			bool fullVersion = false;
			bool.TryParse (PlayerPrefs.GetString ("FullVersion"), out fullVersion);

			if (fullVersion)
				return true;
			
            Debug.Log("MyIAPManager  HasFullVersion start ");
            if (!IsInitialized()) {
                Debug.Log("MyIAPManager  HasFullVersion IsInitialized is False ");
                return false;
            }


			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			string receipt = builder.Configure<IAppleConfiguration>().appReceipt;
			if (string.IsNullOrEmpty (receipt)) {
				Debug.Log("MyIAPManager receipt is null");
				return false;
			}

			PlayerPrefs.SetString ("FullVersion", "true");
			Debug.Log ("This is the receipt: " + receipt);
			return true;
			//Product product = controller.products.WithID (productId);
//            if (product == null) {
//                Debug.Log("MyIAPManager product is NULL - productId:" + productId);
//                return false;
//            }
//			return product.hasReceipt;
		}



        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases() {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized()) {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer) {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = extensions.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
					MainMenu.IsAddFreeVar = HasFullVersion();
					MainMenu.IsFullVersionVar = HasFullVersion();
                    Application.LoadLevel(levalName);
                });
                
            }
            // Otherwise ...
            else {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }




        public bool IsInitialized(){
			return controller != null && extensions != null;
		}

		/// <summary>
		/// Called when Unity IAP is ready to make purchases.
		/// </summary>
		public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
		{
            Debug.Log("MyIAPManager OnInitialized ");
            this.controller = controller;
			this.extensions = extensions;
			MainMenu.IsAddFreeVar = HasFullVersion();
			MainMenu.IsFullVersionVar = HasFullVersion();
		}

		/// <summary>
		/// Called when Unity IAP encounters an unrecoverable initialization error.
		///
		/// Note that this will not be called if Internet is unavailable; Unity IAP
		/// will attempt initialization until it becomes available.
		/// </summary>
		public void OnInitializeFailed (InitializationFailureReason error)
		{
            Debug.Log("MyIAPManager OnInitializeFailed ");
        }

		/// <summary>
		/// Called when a purchase completes.
		///
		/// May be called at any time after OnInitialized().
		/// </summary>
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args){
            Debug.Log("MyIAPManager PurchaseProcessingResult ");
            if (string.Equals(args.purchasedProduct.definition.id,productId,StringComparison.Ordinal)){
				MainMenu.IsAddFreeVar = HasFullVersion();
				MainMenu.IsFullVersionVar = HasFullVersion();
				Application.LoadLevel(levalName);

            }
			return PurchaseProcessingResult.Complete;
		}

		/// <summary>
		/// Called when a purchase fails.
		/// </summary>
		public void OnPurchaseFailed (Product i, PurchaseFailureReason p)
		{
            Debug.Log("MyIAPManager OnPurchaseFailed ");
        }
	}*/
}

