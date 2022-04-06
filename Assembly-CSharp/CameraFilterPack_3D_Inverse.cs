﻿using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Inverse")]
public class CameraFilterPack_3D_Inverse : MonoBehaviour
{
	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00069B63 File Offset: 0x00067D63
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

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00069B97 File Offset: 0x00067D97
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Inverse");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00069BB8 File Offset: 0x00067DB8
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
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = -1f;
				}
				if (this._Distance < -1f)
				{
					this._Distance = 1f;
				}
				this.material.SetFloat("_Near", this._Distance);
			}
			else
			{
				this.material.SetFloat("_Near", this._Distance);
			}
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_LightIntensity", this.LightIntensity);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00069D65 File Offset: 0x00067F65
	private void Update()
	{
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00069D67 File Offset: 0x00067F67
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000E99 RID: 3737
	public Shader SCShader;

	// Token: 0x04000E9A RID: 3738
	private float TimeX = 1f;

	// Token: 0x04000E9B RID: 3739
	public bool _Visualize;

	// Token: 0x04000E9C RID: 3740
	private Material SCMaterial;

	// Token: 0x04000E9D RID: 3741
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000E9E RID: 3742
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000E9F RID: 3743
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000EA0 RID: 3744
	[Range(0f, 1f)]
	public float LightIntensity = 1f;

	// Token: 0x04000EA1 RID: 3745
	public bool AutoAnimatedNear;

	// Token: 0x04000EA2 RID: 3746
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000EA3 RID: 3747
	public static Color ChangeColorRGB;
}
