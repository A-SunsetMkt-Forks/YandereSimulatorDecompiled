﻿using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class opencloseWindow1 : MonoBehaviour
{
	// Token: 0x06002100 RID: 8448 RVA: 0x001E3E0E File Offset: 0x001E200E
	private void Start()
	{
		this.open = false;
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x001E3E18 File Offset: 0x001E2018
	private void OnMouseOver()
	{
		if (this.Player && Vector3.Distance(this.Player.position, base.transform.position) < 15f)
		{
			if (!this.open)
			{
				if (Input.GetMouseButtonDown(0))
				{
					base.StartCoroutine(this.opening());
					return;
				}
			}
			else if (this.open && Input.GetMouseButtonDown(0))
			{
				base.StartCoroutine(this.closing());
			}
		}
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x001E3E8F File Offset: 0x001E208F
	private IEnumerator opening()
	{
		MonoBehaviour.print("you are opening the Window");
		this.openandclosewindow1.Play("Openingwindow 1");
		this.open = true;
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x001E3E9E File Offset: 0x001E209E
	private IEnumerator closing()
	{
		MonoBehaviour.print("you are closing the Window");
		this.openandclosewindow1.Play("Closingwindow 1");
		this.open = false;
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	// Token: 0x04004892 RID: 18578
	public Animator openandclosewindow1;

	// Token: 0x04004893 RID: 18579
	public bool open;

	// Token: 0x04004894 RID: 18580
	public Transform Player;
}
