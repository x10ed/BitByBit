using Assets.Scripts.Common;
using UnityEngine;
using AssemblyCSharp;

namespace Assets.Scripts {
    public class BaseGate : MonoBehaviour {
        public bool IsDisabled;		
		public ColorEnum Color;
		public bool hiddeLabel;
		public bool IsDisabledStartAnimation;
		
		void Update(){
			IsDisabledStartAnimation = MainMenu.animatingTileCount > 0;
		}

        public void Start() {
            ScoreBoard.UpdateLabel();
        }
    }
}