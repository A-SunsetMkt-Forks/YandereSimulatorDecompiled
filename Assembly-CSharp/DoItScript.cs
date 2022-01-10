﻿using System;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class DoItScript : MonoBehaviour
{
	// Token: 0x06001395 RID: 5013 RVA: 0x000B7878 File Offset: 0x000B5A78
	private void Start()
	{
		this.MyLabel.fontSize = UnityEngine.Random.Range(50, 100);
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000B7890 File Offset: 0x000B5A90
	private void Update()
	{
		base.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
		if (!this.Fade)
		{
			this.MyLabel.alpha += Time.deltaTime;
			if (this.MyLabel.alpha >= 1f)
			{
				this.Fade = true;
				return;
			}
		}
		else
		{
			this.MyLabel.alpha -= Time.deltaTime;
			if (this.MyLabel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001CFB RID: 7419
	public UILabel MyLabel;

	// Token: 0x04001CFC RID: 7420
	public bool Fade;
}
