using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillActivate : MonoBehaviour {

	// Use this for initialization
	public GameObject[] skillsParticles;
	public GameObject[] buttons;
	public static int selectedSkill = -1;
	GameObject selectedSkillParticle;

	public void PreActivateSkill(int skillId)
	{
		selectedSkill = skillId;
		selectedSkillParticle = Instantiate (skillsParticles [0], new Vector3(9999,9999,9999), new Quaternion()) as GameObject;//TODO: [i]
		for (int i = 0; i < buttons.Length; i++) {
			if (i != skillId) {
				Button b = buttons[i].GetComponent<Button>();
				b.interactable = false;
			}
		}
	}

	public void Update(){
		if (Input.GetButton ("Fire2")) {
			if (selectedSkill >= 0) {
				Destroy (selectedSkillParticle);
				selectedSkill = -1;
				for (int i = 0; i < buttons.Length; i++) {
					Button b = buttons [i].GetComponent<Button> ();
					if (b.GetComponent<UITag> ().Tag != null) {
						if ((float)b.GetComponent<UITag> ().Tag <= 0.01) {
							b.interactable = true;
						}
					} else {
						b.interactable = true;
					}
				}
			}
		}
		if (Input.GetButton ("Fire1")) {
			for (int i = 0; i < buttons.Length; i++) {
				if (i == selectedSkill) {
					Button b = buttons[i].GetComponent<Button>();
					b.GetComponent<UITag>().Tag = 50.0f;
					selectedSkill = -1;
					var skill = Instantiate (skillsParticles [0], selectedSkillParticle.transform.position, new Quaternion ());
					Destroy (selectedSkillParticle);
					Destroy (skill, 1);
				}
			}
		}

		if (selectedSkill >= 0) {
			//draw skill on mouse
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Vector3 position = hit.point;
				position = new Vector3 (position.x, 3, position.z);
				selectedSkillParticle.transform.position = position;
			}
		}

		UpdateSkillButtonsCaption ();
	}

	void UpdateSkillButtonsCaption()
	{
		for (int i = 0; i < buttons.Length; i++) {
			Button b = buttons [i].GetComponent<Button> ();
			float time = 0.0f;
			if (b.GetComponent<UITag> ().Tag != null) {
				time = (float)b.GetComponent<UITag> ().Tag;
			}
			if (time <= 0.01) {
				time = 0;
				if (selectedSkill == -1) {
					b.interactable = true;
					b.GetComponentInChildren<Text> ().text = "S" + i;
				}
			} else {
				time -= Time.deltaTime;
				b.GetComponentInChildren<Text>().text = ((int)time).ToString("00");
				b.interactable = false;
			}
			b.GetComponent<UITag>().Tag = time;

		}
	}
}
