﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000246 RID: 582
public class ChoiceScript : MonoBehaviour
{
	// Token: 0x06001251 RID: 4689 RVA: 0x0008D4FC File Offset: 0x0008B6FC
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x0008D524 File Offset: 0x0008B724
	private void Update()
	{
		this.Highlight.transform.localPosition = Vector3.Lerp(this.Highlight.transform.localPosition, new Vector3((float)(-360 + 720 * this.Selected), this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z), Time.deltaTime * 10f);
		if (this.Phase == 0)
		{
			this.Darkness.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 0f)
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 1)
		{
			if (this.InputManager.TappedLeft)
			{
				this.Darkness.color = new Color(1f, 1f, 1f, 0f);
				this.Selected = 0;
			}
			else if (this.InputManager.TappedRight)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.Selected = 1;
			}
			if (Input.GetButtonDown("A"))
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 1f)
			{
				GameGlobals.LoveSick = (this.Selected == 1);
				SceneManager.LoadScene("NewTitleScene");
			}
		}
	}

	// Token: 0x04001731 RID: 5937
	public InputManagerScript InputManager;

	// Token: 0x04001732 RID: 5938
	public PromptBarScript PromptBar;

	// Token: 0x04001733 RID: 5939
	public Transform Highlight;

	// Token: 0x04001734 RID: 5940
	public UISprite Darkness;

	// Token: 0x04001735 RID: 5941
	public int Selected;

	// Token: 0x04001736 RID: 5942
	public int Phase;
}
