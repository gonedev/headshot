using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AdminMenu : MonoBehaviour {

	public int window;
	JsonManager jsonManager;
	Popup popup;
	bool toog;
	public string txt,qID,aID;
	GameObject go;
	string strstart = "0";
	int start = 0;
	int width = 150;
	int height = 25;
	string pIDstr = "0";

	void Start () { 
        window = 1; 
		go = GameObject.FindGameObjectWithTag ("MainCamera");
		jsonManager = go.AddComponent<JsonManager> ();
		popup = go.AddComponent<Popup>();
	}

	void back(int a, int pos) {
		if (GUI.Button (new Rect (Screen.width/2, height * pos, Screen.width/2, height), "Назад")) { 
			window = a; 
		}
	}

	void OnGUI (){

		GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height)); 
		
		if (window == 1) {
			if (GUI.Button (new Rect (0, 0, Screen.width, height), "Добавить вопросы")) { 
				window = 2; 
			} 
			if (GUI.Button (new Rect (0, height, Screen.width, height), "Привязать картинки")) { 
				window = 3; 
			} 
			
		}

		if (window == 2) { 
			GUI.Label (new Rect (0, 0, width, height), "Выбери крч 3-еун біреун");

			if (GUI.Button (new Rect (0, height, Screen.width, height), "Questions")) { 
				window = 4;
			} 
			if (GUI.Button (new Rect (0, height * 2, Screen.width, height), "Answers")) { 
				window = 5;
			} 
			if (GUI.Button (new Rect (0, height * 3, Screen.width, height), "Pictures")) { 
				window = 6;
			}
			if (GUI.Button (new Rect (0, height * 5, Screen.width, height), "Назад")) { 
				window = 1; 
			} 
		}

		// QUESTIONS
		if (window == 4) {
			GUI.Label (new Rect(0, 0, Screen.width, height),"QUESTION NAME");
			txt = GUI.TextField (new Rect(0, height, Screen.width, height),txt, 1024);
			if (GUI.Button (new Rect (0, height * 2, Screen.width, height), "add")) {
				jsonManager.AddQuestion (txt);
				txt = "";
			}
			if (GUI.Button (new Rect (0, height * 3, Screen.width, height), "clear all questions")) {
				jsonManager.Clear ("Q");
			}
			back(2,5);
		}

		// ANSWEEEEEEEEEEEEEEEEEEEEEEEEEEEEERRRSSS
		if(window == 5) {
			GUI.Label (new Rect(0,0,Screen.width/2,height),"QUESTION ID");
			GUI.Label (new Rect(Screen.width/2,0,Screen.width/2,height),"ANSWER NAME");
			string[] qlist = jsonManager.GetQuestions(start);
			GUIContent[] strgui = new GUIContent[qlist.Length];
			for(int i = 0; i < qlist.Length; i++){
				strgui[i] = new GUIContent(qlist[i]);
			}
			int picked = popup.List(new Rect (0, height, Screen.width/2, height), strgui, GUIStyle.none, GUIStyle.none);
			txt = GUI.TextField (new Rect(Screen.width/2, height, Screen.width/2, height), txt, 1024);
			toog = GUI.Toggle (new Rect(Screen.width/2,height * 2,Screen.width/2,height),toog,"Right answer?");
			
			if (GUI.Button (new Rect (Screen.width/2,height * 3, Screen.width/2, height), "add")) {
				jsonManager.AddAnswer (txt, picked, toog);
				txt = "";
				qID = "";
				toog = false;
			}
			if (GUI.Button (new Rect (Screen.width/2, height * 4, Screen.width/2, height), "clear all answers")) {
				jsonManager.Clear ("A");
			}

			GUI.Label (new Rect(Screen.width/2,height * 6,Screen.width/2,height),"START FROM");
			
			strstart = GUI.TextField (new Rect(Screen.width/2, height * 7, Screen.width/2, height), strstart, 50);
			start = Int32.Parse(strstart);

			back(2,9);
		}

		// PICTUREEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSS
		if(window == 6) {
			int picked;
			if(!toog){
				GUI.Label (new Rect(0,0,Screen.width/2,height),"QUESTION ID");
				string[] qlist = jsonManager.GetQuestions(start);
				GUIContent[] strgui = new GUIContent[qlist.Length];
				for(int i = 0; i < qlist.Length; i++){
					strgui[i] = new GUIContent(qlist[i]);
				}
				picked = popup.List(new Rect (0, height, Screen.width/2, height), strgui, GUIStyle.none, GUIStyle.none);	
			} else {
				GUI.Label (new Rect(0,0,Screen.width/2,height),"ANSWER ID");
				string[] alist = jsonManager.GetAnswers(start);
				GUIContent[] strgui = new GUIContent[alist.Length];
				for(int i = 0; i < alist.Length; i++){
					strgui[i] = new GUIContent(alist[i]);
				}
				picked = popup.List(new Rect (0, height, Screen.width/2, height), strgui, GUIStyle.none, GUIStyle.none);	
			}

			GUI.Label (new Rect(Screen.width/2,0,Screen.width/2,height),"PICTURE ID");
			
			pIDstr = GUI.TextField (new Rect(Screen.width/2, height, Screen.width/2, height), pIDstr, 50);

			Texture tex = Resources.Load("Pictures/" + Int32.Parse(pIDstr), typeof(Texture)) as Texture;

			toog = GUI.Toggle (new Rect(Screen.width/2,height * 2,Screen.width/2,height),toog, "ANSWER?");
			GUI.DrawTexture (new Rect(Screen.width/2,height * 3,height * 3,height * 3),tex);

			if (GUI.Button (new Rect (Screen.width/2, height * 6, Screen.width/2, height), "add")) {
				jsonManager.AddPicture(Int32.Parse(pIDstr),toog,picked, picked);
				txt = "";
				qID = "";
				aID = "";
				toog = false;
			}
			if (GUI.Button (new Rect (Screen.width/2, height * 7, Screen.width/2, height), "clear all pictures")) {
				jsonManager.Clear ("P");
			}

			GUI.Label (new Rect(Screen.width/2,height * 9,Screen.width/2,height),"START FROM");
			strstart = GUI.TextField (new Rect(Screen.width/2, height * 10, Screen.width/2, height), strstart, 50);
			start = Int32.Parse(strstart);

			back(2,12);
		}

		GUI.EndGroup (); 
	}
}
