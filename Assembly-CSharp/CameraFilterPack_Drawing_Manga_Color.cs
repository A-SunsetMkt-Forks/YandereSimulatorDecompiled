﻿using System;
using UnityEngine;

// Token: 0x02000195 RID: 405
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Color")]
public class CameraFilterPack_Drawing_Manga_Color : MonoBehaviour
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000E3F RID: 3647 RVA: 0x000799F0 File Offset: 0x00077BF0
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00079A24 File Offset: 0x00077C24
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00079A48 File Offset: 0x00077C48
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00079ACE File Offset: 0x00077CCE
	private void Update()
	{
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00079AD0 File Offset: 0x00077CD0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001288 RID: 4744
	public Shader SCShader;

	// Token: 0x04001289 RID: 4745
	private float TimeX = 1f;

	// Token: 0x0400128A RID: 4746
	private Material SCMaterial;

	// Token: 0x0400128B RID: 4747
	[Range(1f, 8f)]
	public float DotSize = 1.6f;

	// Token: 0x0400128C RID: 4748
	public static float ChangeDotSize;
}
