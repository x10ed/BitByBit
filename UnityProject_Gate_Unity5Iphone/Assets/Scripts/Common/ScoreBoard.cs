using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts.Common {
    public class ScoreBoard : MonoBehaviour  {
        public int TwoStarsThreshold = 1;
        public int ThreeStarsThreshold = 1;

        public List<GateLimitWorkSet> List; // see on inspectori jaoks
        //private List<GateLimitWorkSet> initialSetup; // algväärtus - ei ole incpectoris
        private List<GateLimitWorkSet> workingListList; // töötav väärtused  - ei ole incpectoris
        private bool isDebuge = false;

        void Start() {
            //initialSetup = new List<GateLimitWorkSet>();
            //foreach (GateLimitSetting gateLimitSetting in List) {
            //    var set = new GateLimitWorkSet();
            //    if(gateLimitSetting != null){
            //        set.name = gateLimitSetting.name;
            //        set.number = gateLimitSetting.maxNumber;
            //        initialSetup.Add(set);
            //    }
            //}
            Clear();

            workingListList = new List<GateLimitWorkSet>();
            foreach (GateLimitWorkSet gateLimitSetting in List) {
                var set = new GateLimitWorkSet();
                set.name = gateLimitSetting.name;
                set.number = gateLimitSetting.number;
                workingListList.Add(set);
            }
        }
        
        public bool IsCloneAllowed(GameObject o) {
            var sameName = GetObjectSameName(o);

            foreach (GateLimitWorkSet gateLimit in sameName) {
                if (gateLimit.number < 1) {
                    return false;
                }
                gateLimit.number --;
            }
            return true;
        }

        private List<GateLimitWorkSet> GetObjectSameName(GameObject o) {

            List<GateLimitWorkSet> sameName = new List<GateLimitWorkSet>();
            if (workingListList == null) {
                return sameName;
            }
            foreach (GateLimitWorkSet x in workingListList) {
                if (x.name != null && o.name.Contains(x.name)) {
                    sameName.Add(x);
                }
            }
            return sameName;
        }

        public void CountDestroy(GameObject o) {
            var sameName = GetObjectSameName(o);
            foreach (GateLimitWorkSet gateLimit in sameName) {

                gateLimit.number++;
            }
        }

        public void Clear() {
            workingListList = new List<GateLimitWorkSet>(); // clear
            foreach (GateLimitWorkSet gateLimitSetting in List) {
                var set = new GateLimitWorkSet();
                set.name = gateLimitSetting.name;
                set.number = gateLimitSetting.number;
                workingListList.Add(set);
            }
        }

        public string GetGount(GameObject o) {
            List<GateLimitWorkSet> objectSameName = GetObjectSameName(o);
            if (objectSameName.Count > 0) {
                int maxNumber = objectSameName[0].number;
                return maxNumber.ToString();
            }
            return string.Empty;
        }

        public int GetScore() {
            int sum = 0;
            foreach (var gateLimitWorkSet in workingListList) {
                sum = sum  + gateLimitWorkSet.number;
            }
            return sum;
        }

        public static void UpdateLabel() {

            var templateGates = GameObject.FindGameObjectsWithTag("Template");

            foreach (var templateGate in templateGates) {
                var baseGate = templateGate.GetComponent<BaseGate>();
                if (baseGate != null) {
                    if (baseGate.hiddeLabel) { continue; }
                    
                    ScoreBoard scoreBoard = GameMaster.GetScoreBoard();
                    if (scoreBoard != null) {
                        string message = baseGate.tag == "Template" && !baseGate.hiddeLabel ? scoreBoard.GetGount(baseGate.gameObject) : "";
                        int number = 0;
                        if (int.TryParse(message, out number)) {
                            baseGate.SetLabelValue(number);
                        }
                    }
                }
            }

        }
    }
}