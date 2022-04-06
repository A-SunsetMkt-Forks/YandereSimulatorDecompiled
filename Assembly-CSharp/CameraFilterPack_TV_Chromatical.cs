﻿using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Chromatical")]
public class CameraFilterPack_TV_Chromatical : MonoBehaviour
{
	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06001121 RID: 4385 RVA: 0x00087237 File Offset: 0x00085437
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

	// Token: 0x06001122 RID: 4386 RVA: 0x0008726B File Offset: 0x0008546B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Chromatical");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0008728C File Offset: 0x0008548C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime * 2f;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Intensity", this.Intensity);
			this.material.SetFloat("Speed", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0008736D File Offset: 0x0008556D
	private void Update()
	{
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0008736F File Offset: 0x0008556F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040015CA RID: 5578
	public Shader SCShader;

	// Token: 0x040015CB RID: 5579
	private float TimeX = 1f;

	// Token: 0x040015CC RID: 5580
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040015CD RID: 5581
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x040015CE RID: 5582
	[Range(0f, 3f)]
	public float Speed = 1f;

	// Token: 0x040015CF RID: 5583
	private Material SCMaterial;
}
