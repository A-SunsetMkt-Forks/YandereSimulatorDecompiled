﻿using System;
using UnityEngine;

// Token: 0x02000510 RID: 1296
public class RivalBarScript : MonoBehaviour
{
	// Token: 0x06002186 RID: 8582 RVA: 0x001EF04C File Offset: 0x001ED24C
	private void Start()
	{
		for (int i = 1; i < 11; i++)
		{
			this.Bars[i].transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x001EF08C File Offset: 0x001ED28C
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.UpdateBars();
		}
		this.Timer += Time.deltaTime;
		if (this.ID < 11)
		{
			if (this.Timer > 1f)
			{
				this.UpdateBars();
				this.Timer = 0f;
			}
		}
		else if (this.Timer > 2.5f)
		{
			this.UpdateBars();
			this.Timer = 0f;
		}
		for (int i = 1; i < this.ID; i++)
		{
			this.Bars[i].transform.localScale = Vector3.Lerp(this.Bars[i].transform.localScale, new Vector3(1f, this.TargetHeights[i], 1f), Time.deltaTime * this.Speed);
			this.Bars[i].color = new Color(this.TargetHeights[i] / 7f, 1f - this.TargetHeights[i] / 7f, 0f);
			if (i == 1)
			{
				Debug.Log("R is: " + (this.TargetHeights[i] / 7f).ToString() + " G is: " + (1f - this.TargetHeights[i] / 7f).ToString());
			}
		}
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x001EF1EC File Offset: 0x001ED3EC
	private void UpdateBars()
	{
		int i = 1;
		if (this.ID < 11)
		{
			this.ID++;
			return;
		}
		while (i < this.ID)
		{
			this.TargetHeights[i] = UnityEngine.Random.Range(0.7f, 7f);
			i++;
		}
	}

	// Token: 0x040049E1 RID: 18913
	public int ID;

	// Token: 0x040049E2 RID: 18914
	public float Speed;

	// Token: 0x040049E3 RID: 18915
	public float Timer;

	// Token: 0x040049E4 RID: 18916
	public UISprite[] Bars;

	// Token: 0x040049E5 RID: 18917
	public float[] TargetHeights;
}
