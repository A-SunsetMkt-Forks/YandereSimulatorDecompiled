﻿using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000553 RID: 1363
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x001EF1E4 File Offset: 0x001ED3E4
		public override bool active
		{
			get
			{
				return base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Depth) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Normals) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.MotionVectors);
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x001EF210 File Offset: 0x001ED410
		public override DepthTextureMode GetCameraFlags()
		{
			BuiltinDebugViewsModel.Mode mode = base.model.settings.mode;
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			switch (mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				depthTextureMode |= DepthTextureMode.Depth;
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				depthTextureMode |= DepthTextureMode.DepthNormals;
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
				break;
			}
			return depthTextureMode;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x001EF257 File Offset: 0x001ED457
		public override CameraEvent GetCameraEvent()
		{
			if (base.model.settings.mode != BuiltinDebugViewsModel.Mode.MotionVectors)
			{
				return CameraEvent.BeforeImageEffectsOpaque;
			}
			return CameraEvent.BeforeImageEffects;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x001EF271 File Offset: 0x001ED471
		public override string GetName()
		{
			return "Builtin Debug Views";
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x001EF278 File Offset: 0x001ED478
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ref BuiltinDebugViewsModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			material.shaderKeywords = null;
			if (this.context.isGBufferAvailable)
			{
				material.EnableKeyword("SOURCE_GBUFFER");
			}
			switch (settings.mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				this.DepthPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				this.DepthNormalsPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				this.MotionVectorsPass(cb);
				break;
			}
			this.context.Interrupt();
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x001EF308 File Offset: 0x001ED508
		private void DepthPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.DepthSettings depth = base.model.settings.depth;
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._DepthScale, 1f / depth.scale);
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 0);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x001EF364 File Offset: 0x001ED564
		private void DepthNormalsPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 1);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x001EF398 File Offset: 0x001ED598
		private void MotionVectorsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.MotionVectorsSettings motionVectors = base.model.settings.motionVectors;
			int nameID = BuiltinDebugViewsComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(nameID, this.context.width, this.context.height, 0, FilterMode.Bilinear);
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.sourceOpacity);
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, nameID, material, 2);
			if (motionVectors.motionImageOpacity > 0f && motionVectors.motionImageAmplitude > 0f)
			{
				int tempRT = BuiltinDebugViewsComponent.Uniforms._TempRT2;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionImageOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionImageAmplitude);
				cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, tempRT, material, 3);
				cb.ReleaseTemporaryRT(nameID);
				nameID = tempRT;
			}
			if (motionVectors.motionVectorsOpacity > 0f && motionVectors.motionVectorsAmplitude > 0f)
			{
				this.PrepareArrows();
				float num = 1f / (float)motionVectors.motionVectorsResolution;
				float x = num * (float)this.context.height / (float)this.context.width;
				cb.SetGlobalVector(BuiltinDebugViewsComponent.Uniforms._Scale, new Vector2(x, num));
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionVectorsOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionVectorsAmplitude);
				cb.DrawMesh(this.m_Arrows.mesh, Matrix4x4.identity, material, 0, 4);
			}
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x001EF58C File Offset: 0x001ED78C
		private void PrepareArrows()
		{
			int motionVectorsResolution = base.model.settings.motionVectors.motionVectorsResolution;
			int num = motionVectorsResolution * Screen.width / Screen.height;
			if (this.m_Arrows == null)
			{
				this.m_Arrows = new BuiltinDebugViewsComponent.ArrowArray();
			}
			if (this.m_Arrows.columnCount != num || this.m_Arrows.rowCount != motionVectorsResolution)
			{
				this.m_Arrows.Release();
				this.m_Arrows.BuildMesh(num, motionVectorsResolution);
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x001EF604 File Offset: 0x001ED804
		public override void OnDisable()
		{
			if (this.m_Arrows != null)
			{
				this.m_Arrows.Release();
			}
			this.m_Arrows = null;
		}

		// Token: 0x04004AB1 RID: 19121
		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		// Token: 0x04004AB2 RID: 19122
		private BuiltinDebugViewsComponent.ArrowArray m_Arrows;

		// Token: 0x02000696 RID: 1686
		private static class Uniforms
		{
			// Token: 0x04005028 RID: 20520
			internal static readonly int _DepthScale = Shader.PropertyToID("_DepthScale");

			// Token: 0x04005029 RID: 20521
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x0400502A RID: 20522
			internal static readonly int _Opacity = Shader.PropertyToID("_Opacity");

			// Token: 0x0400502B RID: 20523
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x0400502C RID: 20524
			internal static readonly int _TempRT2 = Shader.PropertyToID("_TempRT2");

			// Token: 0x0400502D RID: 20525
			internal static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");

			// Token: 0x0400502E RID: 20526
			internal static readonly int _Scale = Shader.PropertyToID("_Scale");
		}

		// Token: 0x02000697 RID: 1687
		private enum Pass
		{
			// Token: 0x04005030 RID: 20528
			Depth,
			// Token: 0x04005031 RID: 20529
			Normals,
			// Token: 0x04005032 RID: 20530
			MovecOpacity,
			// Token: 0x04005033 RID: 20531
			MovecImaging,
			// Token: 0x04005034 RID: 20532
			MovecArrows
		}

		// Token: 0x02000698 RID: 1688
		private class ArrowArray
		{
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x06002708 RID: 9992 RVA: 0x001FFEBE File Offset: 0x001FE0BE
			// (set) Token: 0x06002709 RID: 9993 RVA: 0x001FFEC6 File Offset: 0x001FE0C6
			public Mesh mesh { get; private set; }

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x0600270A RID: 9994 RVA: 0x001FFECF File Offset: 0x001FE0CF
			// (set) Token: 0x0600270B RID: 9995 RVA: 0x001FFED7 File Offset: 0x001FE0D7
			public int columnCount { get; private set; }

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x0600270C RID: 9996 RVA: 0x001FFEE0 File Offset: 0x001FE0E0
			// (set) Token: 0x0600270D RID: 9997 RVA: 0x001FFEE8 File Offset: 0x001FE0E8
			public int rowCount { get; private set; }

			// Token: 0x0600270E RID: 9998 RVA: 0x001FFEF4 File Offset: 0x001FE0F4
			public void BuildMesh(int columns, int rows)
			{
				Vector3[] array = new Vector3[]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 0f)
				};
				int num = 6 * columns * rows;
				List<Vector3> list = new List<Vector3>(num);
				List<Vector2> list2 = new List<Vector2>(num);
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						Vector2 item = new Vector2((0.5f + (float)j) / (float)columns, (0.5f + (float)i) / (float)rows);
						for (int k = 0; k < 6; k++)
						{
							list.Add(array[k]);
							list2.Add(item);
						}
					}
				}
				int[] array2 = new int[num];
				for (int l = 0; l < num; l++)
				{
					array2[l] = l;
				}
				this.mesh = new Mesh
				{
					hideFlags = HideFlags.DontSave
				};
				this.mesh.SetVertices(list);
				this.mesh.SetUVs(0, list2);
				this.mesh.SetIndices(array2, MeshTopology.Lines, 0);
				this.mesh.UploadMeshData(true);
				this.columnCount = columns;
				this.rowCount = rows;
			}

			// Token: 0x0600270F RID: 9999 RVA: 0x00200097 File Offset: 0x001FE297
			public void Release()
			{
				GraphicsUtils.Destroy(this.mesh);
				this.mesh = null;
			}
		}
	}
}
