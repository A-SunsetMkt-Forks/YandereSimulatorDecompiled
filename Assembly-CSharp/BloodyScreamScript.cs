﻿using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class BloodyScreamScript : MonoBehaviour
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0005B3AF File Offset: 0x000595AF
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Screams[UnityEngine.Random.Range(0, this.Screams.Length)];
		component.Play();
	}

	// Token: 0x04000BB3 RID: 2995
	public AudioClip[] Screams;
}
