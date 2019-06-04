using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UrlParentalGate : MonoBehaviour {


	public string Url;
	Text calculationText;
	int firstNr;
	int secondNr;
	int rightAnswer;

	GameObject calculatorModal;
	GameObject calculatorScr;
	GameObject correctScr;
	GameObject wrongScr;

	GameObject closeBtn;

	List<GameObject> selectableNrs;
	List <int> usedNumbers = new List<int>();
	// Use this for initialization
	void Start () {

		calculatorModal = GameObject.Find ("parentalGate");
		calculatorScr = GameObject.Find ("calculatorScr");
		correctScr = GameObject.Find ("correctScr");
		wrongScr = GameObject.Find ("wrongScr");

		calculatorScr.SetActive(true);
		correctScr.SetActive (false);
		wrongScr.SetActive (false);

		selectableNrs = new List<GameObject>(GameObject.FindGameObjectsWithTag("selectableNr"));

		for(var i = 0; i < selectableNrs.Count; i++){
			GameObject selectableEl = selectableNrs[i];
			selectableNrs[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => checkIfCorrectNr(selectableEl)); 
		}

		calculationText = GameObject.Find("calculation").GetComponent<Text>();
		createCalc ();
		cycleList ();
	}


	void OnEnable() {


	}

	int getRandomNr(int rndFrom, int rndTo){
		return Random.Range (rndFrom, rndTo);
	}
	int getRandomNrAnswer(int rndFrom, int rndTo){
		int ran = Random.Range (rndFrom, rndTo);
		while(usedNumbers.Contains(ran)){
			ran = Random.Range(rndFrom, rndTo);
		}
		usedNumbers.Add (ran);
		return ran;
	}

	void createCalc(){
		firstNr = getRandomNr (10,30);
		secondNr = getRandomNr (10,30);
		rightAnswer = firstNr + secondNr;
		usedNumbers.Add (rightAnswer);

		calculationText.text = (firstNr + " + " + secondNr + " = ?").ToString();
	}

	void cycleList(){
		for(var i = 0; i < selectableNrs.Count; i++){
			Text btnText = selectableNrs[i].gameObject.GetComponentInChildren<Text>();
			int rand = getRandomNrAnswer (20, 60);
			btnText.text = rand.ToString();
		}
		selectableNrs[Random.Range(0,selectableNrs.Count)].gameObject.GetComponentInChildren<Text>().text = rightAnswer.ToString();
	}

	void checkIfCorrectNr(GameObject selectableEl){
		int nr = int.Parse(selectableEl.transform.GetComponentInChildren<Text>().text);
		if (nr == rightAnswer) {
			GoToUrl ();
		} else {
			cantGoToUrl ();
		}
	}

	void GoToUrl(){
		calculatorScr.SetActive (false);
		correctScr.SetActive (true);
		wrongScr.SetActive (false);
		Application.OpenURL (Url);
		closeModal();

	}

	void cantGoToUrl(){
		calculatorScr.SetActive (false);
		correctScr.SetActive (false);
		wrongScr.SetActive (true);
	}

	public void tryAgain(){
		createCalc ();
		cycleList ();
		calculatorScr.SetActive (true);
		correctScr.SetActive (false);
		wrongScr.SetActive (false);
	}

	public void closeModal(){
		calculatorModal.SetActive (false);
		calculatorScr.SetActive (true);
		correctScr.SetActive (false);
		wrongScr.SetActive (false);
		createCalc();
		cycleList();
	}
}
