// Compiled shader for Windows, Mac, Linux

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Nature/Tree Soft Occlusion Bark" {
Properties {
 _Color ("Main Color", Color) = (1.000000,1.000000,1.000000,0.000000)
 _MainTex ("Main Texture", 2D) = "white" { }
 _BaseLight ("Base Light", Range(0.000000,1.000000)) = 0.350000
 _AO ("Amb. Occlusion", Range(0.000000,10.000000)) = 2.400000
[HideInInspector]  _TreeInstanceColor ("TreeInstanceColor", Vector) = (1.000000,1.000000,1.000000,1.000000)
[HideInInspector]  _TreeInstanceScale ("TreeInstanceScale", Vector) = (1.000000,1.000000,1.000000,1.000000)
[HideInInspector]  _SquashAmount ("Squash", Float) = 1.000000
}
SubShader { 
 Tags { "IGNOREPROJECTOR"="true" "RenderType"="TreeOpaque" "DisableBatching"="true" }
 Pass {
  Tags { "IGNOREPROJECTOR"="true" "RenderType"="TreeOpaque" "DisableBatching"="true" }
  Lighting On
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords: <none>
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "Tangent"
Uses vertex data channel "Normal"
Uses vertex data channel "Color"
Uses vertex data channel "TexCoord0"

Constant Buffer "VGlobals" (32 bytes) on slot 0 {
  Float _AO at 0
  Float _BaseLight at 4
  Vector4 _Color at 16
}
Constant Buffer "UnityLighting" (768 bytes) on slot 1 {
  Vector4 unity_LightColor[8] at 112
  Vector4 unity_LightPosition[8] at 240
  Vector4 unity_LightAtten[8] at 368
}
Constant Buffer "UnityPerDraw" (176 bytes) on slot 2 {
  Matrix4x4 unity_ObjectToWorld at 0
}
Constant Buffer "UnityPerFrame" (368 bytes) on slot 3 {
  Matrix4x4 unity_MatrixV at 144
  Matrix4x4 unity_MatrixVP at 272
  Vector4 glstate_lightmodel_ambient at 0
}
Constant Buffer "UnityTerrain" (288 bytes) on slot 4 {
  Matrix4x4 _TerrainEngineBendTree at 112
  Vector4 _TreeInstanceColor at 80
  Vector4 _TreeInstanceScale at 96
  Vector4 _SquashPlaneNormal at 176
  Float _SquashAmount at 192
}
Constant Buffer "UnityPerCamera2" (64 bytes) on slot 5 {
  Matrix4x4 _CameraToWorld at 0
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct VGlobals_Type
{
    float _AO;
    float _BaseLight;
    float4 _Color;
};

struct UnityLighting_Type
{
    float4 _WorldSpaceLightPos0;
    float4 _LightPositionRange;
    float4 _LightProjectionParams;
    float4 unity_4LightPosX0;
    float4 unity_4LightPosY0;
    float4 unity_4LightPosZ0;
    float4 unity_4LightAtten0;
    float4 unity_LightColor[8];
    float4 unity_LightPosition[8];
    float4 unity_LightAtten[8];
    float4 unity_SpotDirection[8];
    float4 unity_SHAr;
    float4 unity_SHAg;
    float4 unity_SHAb;
    float4 unity_SHBr;
    float4 unity_SHBg;
    float4 unity_SHBb;
    float4 unity_SHC;
    float4 unity_OcclusionMaskSelector;
    float4 unity_ProbesOcclusion;
};

struct UnityPerDraw_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_WorldToObject[4];
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
    float4 unity_RenderingLayer;
};

struct UnityPerFrame_Type
{
    float4 glstate_lightmodel_ambient;
    float4 unity_AmbientSky;
    float4 unity_AmbientEquator;
    float4 unity_AmbientGround;
    float4 unity_IndirectSpecColor;
    float4 hlslcc_mtx4x4glstate_matrix_projection[4];
    float4 hlslcc_mtx4x4unity_MatrixV[4];
    float4 hlslcc_mtx4x4unity_MatrixInvV[4];
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
    int unity_StereoEyeIndex;
    float4 unity_ShadowColor;
};

struct UnityTerrain_Type
{
    float4 _WavingTint;
    float4 _WaveAndDistance;
    float4 _CameraPosition;
    float3 _CameraRight;
    float3 _CameraUp;
    float4 _TreeInstanceColor;
    float4 _TreeInstanceScale;
    float4 hlslcc_mtx4x4_TerrainEngineBendTree[4];
    float4 _SquashPlaneNormal;
    float _SquashAmount;
    float3 _TreeBillboardCameraRight;
    float4 _TreeBillboardCameraUp;
    float4 _TreeBillboardCameraFront;
    float4 _TreeBillboardCameraPos;
    float4 _TreeBillboardDistances;
};

struct UnityPerCamera2_Type
{
    float4 hlslcc_mtx4x4_CameraToWorld[4];
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float4 TANGENT0 [[ attribute(1) ]] ;
    float3 NORMAL0 [[ attribute(2) ]] ;
    float4 COLOR0 [[ attribute(3) ]] ;
    float4 TEXCOORD0 [[ attribute(4) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position, invariant ]];
    float4 TEXCOORD0 [[ user(TEXCOORD0) ]];
    float4 TEXCOORD1 [[ user(TEXCOORD1) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant VGlobals_Type& VGlobals [[ buffer(0) ]],
    constant UnityLighting_Type& UnityLighting [[ buffer(1) ]],
    constant UnityPerDraw_Type& UnityPerDraw [[ buffer(2) ]],
    constant UnityPerFrame_Type& UnityPerFrame [[ buffer(3) ]],
    constant UnityTerrain_Type& UnityTerrain [[ buffer(4) ]],
    constant UnityPerCamera2_Type& UnityPerCamera2 [[ buffer(5) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    float4 u_xlat2;
    float4 u_xlat3;
    bool u_xlatb3;
    float3 u_xlat4;
    float u_xlat13;
    float u_xlat15;
    int u_xlati16;
    u_xlat0.xyz = input.POSITION0.xyz * UnityTerrain._TreeInstanceScale.xyz;
    u_xlat1.xyz = u_xlat0.yyy * UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[1].xyz;
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[0].xyz, u_xlat0.xxx, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[2].xyz, u_xlat0.zzz, u_xlat1.xyz);
    u_xlat1.xyz = fma((-input.POSITION0.xyz), UnityTerrain._TreeInstanceScale.xyz, u_xlat1.xyz);
    u_xlat0.xyz = fma(input.COLOR0.www, u_xlat1.xyz, u_xlat0.xyz);
    u_xlat15 = dot(UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat15 = u_xlat15 + UnityTerrain._SquashPlaneNormal.w;
    u_xlat1.xyz = fma((-float3(u_xlat15)), UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xyz);
    u_xlat0.xyz = fma(float3(UnityTerrain._SquashAmount), u_xlat0.xyz, u_xlat1.xyz);
    u_xlat1 = u_xlat0.yyyy * UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[0], u_xlat0.xxxx, u_xlat1);
    u_xlat0 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[2], u_xlat0.zzzz, u_xlat1);
    u_xlat0 = u_xlat0 + UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1.xyz = u_xlat0.yyy * UnityPerFrame.hlslcc_mtx4x4unity_MatrixV[1].xyz;
    u_xlat1.xyz = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixV[0].xyz, u_xlat0.xxx, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixV[2].xyz, u_xlat0.zzz, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixV[3].xyz, u_xlat0.www, u_xlat1.xyz);
    u_xlat2 = u_xlat0.yyyy * UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat2);
    u_xlat2 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat2);
    output.mtl_Position = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[3], u_xlat0.wwww, u_xlat2);
    u_xlat0.xyz = UnityPerFrame.glstate_lightmodel_ambient.xyz + UnityPerFrame.glstate_lightmodel_ambient.xyz;
    u_xlat15 = fma(VGlobals._AO, input.TANGENT0.w, VGlobals._BaseLight);
    u_xlat2.xyz = u_xlat0.xyz;
    u_xlati16 = 0x0;
    while(true){
        u_xlatb3 = u_xlati16>=0x4;
        if(u_xlatb3){break;}
        u_xlat3.xyz = fma((-u_xlat1.xyz), UnityLighting.unity_LightPosition[u_xlati16].www, UnityLighting.unity_LightPosition[u_xlati16].xyz);
        u_xlat3.w = (-u_xlat3.z);
        u_xlat13 = dot(u_xlat3.xyw, u_xlat3.xyw);
        u_xlat4.x = rsqrt(u_xlat13);
        u_xlat3.xyw = u_xlat3.xyw * u_xlat4.xxx;
        u_xlat4.xyz = u_xlat3.yyy * UnityPerCamera2.hlslcc_mtx4x4_CameraToWorld[1].xyz;
        u_xlat4.xyz = fma(UnityPerCamera2.hlslcc_mtx4x4_CameraToWorld[0].xyz, u_xlat3.xxx, u_xlat4.xyz);
        u_xlat3.xyw = fma(UnityPerCamera2.hlslcc_mtx4x4_CameraToWorld[2].xyz, u_xlat3.www, u_xlat4.xyz);
        u_xlat13 = fma(u_xlat13, UnityLighting.unity_LightAtten[u_xlati16].z, 1.0);
        u_xlat13 = float(1.0) / u_xlat13;
        u_xlat3.x = dot(input.NORMAL0.xyz, u_xlat3.xyw);
        u_xlat3.x = max(u_xlat3.x, 0.0);
        u_xlat3.x = u_xlat15 * u_xlat3.x;
        u_xlat3.x = u_xlat13 * u_xlat3.x;
        u_xlat2.xyz = fma(UnityLighting.unity_LightColor[u_xlati16].xyz, u_xlat3.xxx, u_xlat2.xyz);
        u_xlati16 = u_xlati16 + 0x1;
    }
    u_xlat2.w = 1.0;
    u_xlat0 = u_xlat2 * VGlobals._Color;
    output.TEXCOORD1 = u_xlat0 * UnityTerrain._TreeInstanceColor;
    output.TEXCOORD0 = input.TEXCOORD0;
    return output;
}


-- Fragment shader for "metal":
Set 2D Texture "_MainTex" to slot 0

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
constant uint32_t rp_output_remap_mask [[ function_constant(1) ]];
constant const uint rp_output_remap_0 = (rp_output_remap_mask >> 0) & 0xF;
struct Mtl_FragmentIn
{
    float4 TEXCOORD0 [[ user(TEXCOORD0) ]] ;
    float4 TEXCOORD1 [[ user(TEXCOORD1) ]] ;
};

struct Mtl_FragmentOut
{
    float4 SV_Target0 [[ color(rp_output_remap_0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    sampler sampler_MainTex [[ sampler (0) ]],
    texture2d<float, access::sample > _MainTex [[ texture(0) ]] ,
    Mtl_FragmentIn input [[ stage_in ]])
{
    Mtl_FragmentOut output;
    float3 u_xlat0;
    u_xlat0.xyz = _MainTex.sample(sampler_MainTex, input.TEXCOORD0.xy).xyz;
    output.SV_Target0.xyz = u_xlat0.xyz * input.TEXCOORD1.xyz;
    output.SV_Target0.w = 1.0;
    return output;
}


 }
 Pass {
  Name "ShadowCaster"
  Tags { "LIGHTMODE"="SHADOWCASTER" "IGNOREPROJECTOR"="true" "SHADOWSUPPORT"="true" "RenderType"="TreeOpaque" "DisableBatching"="true" }
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords: SHADOWS_DEPTH
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "Normal"
Uses vertex data channel "Color"

Constant Buffer "UnityLighting" (768 bytes) on slot 0 {
  Vector4 _WorldSpaceLightPos0 at 0
}
Constant Buffer "UnityShadows" (416 bytes) on slot 1 {
  Vector4 unity_LightShadowBias at 80
}
Constant Buffer "UnityPerDraw" (176 bytes) on slot 2 {
  Matrix4x4 unity_ObjectToWorld at 0
  Matrix4x4 unity_WorldToObject at 64
}
Constant Buffer "UnityPerFrame" (368 bytes) on slot 3 {
  Matrix4x4 unity_MatrixVP at 272
}
Constant Buffer "UnityTerrain" (288 bytes) on slot 4 {
  Matrix4x4 _TerrainEngineBendTree at 112
  Vector4 _TreeInstanceScale at 96
  Vector4 _SquashPlaneNormal at 176
  Float _SquashAmount at 192
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct UnityLighting_Type
{
    float4 _WorldSpaceLightPos0;
    float4 _LightPositionRange;
    float4 _LightProjectionParams;
    float4 unity_4LightPosX0;
    float4 unity_4LightPosY0;
    float4 unity_4LightPosZ0;
    float4 unity_4LightAtten0;
    float4 unity_LightColor[8];
    float4 unity_LightPosition[8];
    float4 unity_LightAtten[8];
    float4 unity_SpotDirection[8];
    float4 unity_SHAr;
    float4 unity_SHAg;
    float4 unity_SHAb;
    float4 unity_SHBr;
    float4 unity_SHBg;
    float4 unity_SHBb;
    float4 unity_SHC;
    float4 unity_OcclusionMaskSelector;
    float4 unity_ProbesOcclusion;
};

struct UnityShadows_Type
{
    float4 unity_ShadowSplitSpheres[4];
    float4 unity_ShadowSplitSqRadii;
    float4 unity_LightShadowBias;
    float4 _LightSplitsNear;
    float4 _LightSplitsFar;
    float4 hlslcc_mtx4x4unity_WorldToShadow[16];
    float4 _LightShadowData;
    float4 unity_ShadowFadeCenterAndType;
};

struct UnityPerDraw_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_WorldToObject[4];
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
    float4 unity_RenderingLayer;
};

struct UnityPerFrame_Type
{
    float4 glstate_lightmodel_ambient;
    float4 unity_AmbientSky;
    float4 unity_AmbientEquator;
    float4 unity_AmbientGround;
    float4 unity_IndirectSpecColor;
    float4 hlslcc_mtx4x4glstate_matrix_projection[4];
    float4 hlslcc_mtx4x4unity_MatrixV[4];
    float4 hlslcc_mtx4x4unity_MatrixInvV[4];
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
    int unity_StereoEyeIndex;
    float4 unity_ShadowColor;
};

struct UnityTerrain_Type
{
    float4 _WavingTint;
    float4 _WaveAndDistance;
    float4 _CameraPosition;
    float3 _CameraRight;
    float3 _CameraUp;
    float4 _TreeInstanceColor;
    float4 _TreeInstanceScale;
    float4 hlslcc_mtx4x4_TerrainEngineBendTree[4];
    float4 _SquashPlaneNormal;
    float _SquashAmount;
    float3 _TreeBillboardCameraRight;
    float4 _TreeBillboardCameraUp;
    float4 _TreeBillboardCameraFront;
    float4 _TreeBillboardCameraPos;
    float4 _TreeBillboardDistances;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float3 NORMAL0 [[ attribute(1) ]] ;
    float4 COLOR0 [[ attribute(2) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position, invariant ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant UnityLighting_Type& UnityLighting [[ buffer(0) ]],
    constant UnityShadows_Type& UnityShadows [[ buffer(1) ]],
    constant UnityPerDraw_Type& UnityPerDraw [[ buffer(2) ]],
    constant UnityPerFrame_Type& UnityPerFrame [[ buffer(3) ]],
    constant UnityTerrain_Type& UnityTerrain [[ buffer(4) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    float3 u_xlat2;
    float u_xlat6;
    float u_xlat9;
    float u_xlat10;
    bool u_xlatb10;
    u_xlat0.xyz = input.POSITION0.xyz * UnityTerrain._TreeInstanceScale.xyz;
    u_xlat1.xyz = u_xlat0.yyy * UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[1].xyz;
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[0].xyz, u_xlat0.xxx, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[2].xyz, u_xlat0.zzz, u_xlat1.xyz);
    u_xlat1.xyz = fma((-input.POSITION0.xyz), UnityTerrain._TreeInstanceScale.xyz, u_xlat1.xyz);
    u_xlat0.xyz = fma(input.COLOR0.www, u_xlat1.xyz, u_xlat0.xyz);
    u_xlat9 = dot(UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat9 = u_xlat9 + UnityTerrain._SquashPlaneNormal.w;
    u_xlat1.xyz = fma((-float3(u_xlat9)), UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xyz);
    u_xlat0.xyz = fma(float3(UnityTerrain._SquashAmount), u_xlat0.xyz, u_xlat1.xyz);
    u_xlat1 = u_xlat0.yyyy * UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[0], u_xlat0.xxxx, u_xlat1);
    u_xlat0 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[2], u_xlat0.zzzz, u_xlat1);
    u_xlat0 = u_xlat0 + UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1.xyz = fma((-u_xlat0.xyz), UnityLighting._WorldSpaceLightPos0.www, UnityLighting._WorldSpaceLightPos0.xyz);
    u_xlat10 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat10 = rsqrt(u_xlat10);
    u_xlat1.xyz = float3(u_xlat10) * u_xlat1.xyz;
    u_xlat2.x = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[0].xyz);
    u_xlat2.y = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[1].xyz);
    u_xlat2.z = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[2].xyz);
    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat10 = rsqrt(u_xlat10);
    u_xlat2.xyz = float3(u_xlat10) * u_xlat2.xyz;
    u_xlat1.x = dot(u_xlat2.xyz, u_xlat1.xyz);
    u_xlat1.x = fma((-u_xlat1.x), u_xlat1.x, 1.0);
    u_xlat1.x = sqrt(u_xlat1.x);
    u_xlat1.x = u_xlat1.x * UnityShadows.unity_LightShadowBias.z;
    u_xlat1.xyz = fma((-u_xlat2.xyz), u_xlat1.xxx, u_xlat0.xyz);
    u_xlatb10 = UnityShadows.unity_LightShadowBias.z!=0.0;
    u_xlat0.xyz = (bool(u_xlatb10)) ? u_xlat1.xyz : u_xlat0.xyz;
    u_xlat1 = u_xlat0.yyyy * UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat1);
    u_xlat1 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat1);
    u_xlat0 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[3], u_xlat0.wwww, u_xlat1);
    u_xlat1.x = UnityShadows.unity_LightShadowBias.x / u_xlat0.w;
    u_xlat1.x = min(u_xlat1.x, 0.0);
    u_xlat1.x = max(u_xlat1.x, -1.0);
    u_xlat6 = u_xlat0.z + u_xlat1.x;
    u_xlat1.x = min(u_xlat0.w, u_xlat6);
    output.mtl_Position.xyw = u_xlat0.xyw;
    u_xlat0.x = (-u_xlat6) + u_xlat1.x;
    output.mtl_Position.z = fma(UnityShadows.unity_LightShadowBias.y, u_xlat0.x, u_xlat6);
    return output;
}


-- Fragment shader for "metal":
Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
constant uint32_t rp_output_remap_mask [[ function_constant(1) ]];
constant const uint rp_output_remap_0 = (rp_output_remap_mask >> 0) & 0xF;
struct Mtl_FragmentOut
{
    float4 SV_Target0 [[ color(rp_output_remap_0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
)
{
    Mtl_FragmentOut output;
    output.SV_Target0 = float4(0.0, 0.0, 0.0, 0.0);
    return output;
}


//////////////////////////////////////////////////////
Keywords: SHADOWS_CUBE
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "Normal"
Uses vertex data channel "Color"

Constant Buffer "UnityLighting" (768 bytes) on slot 0 {
  Vector4 _WorldSpaceLightPos0 at 0
}
Constant Buffer "UnityShadows" (416 bytes) on slot 1 {
  Vector4 unity_LightShadowBias at 80
}
Constant Buffer "UnityPerDraw" (176 bytes) on slot 2 {
  Matrix4x4 unity_ObjectToWorld at 0
  Matrix4x4 unity_WorldToObject at 64
}
Constant Buffer "UnityPerFrame" (368 bytes) on slot 3 {
  Matrix4x4 unity_MatrixVP at 272
}
Constant Buffer "UnityTerrain" (288 bytes) on slot 4 {
  Matrix4x4 _TerrainEngineBendTree at 112
  Vector4 _TreeInstanceScale at 96
  Vector4 _SquashPlaneNormal at 176
  Float _SquashAmount at 192
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct UnityLighting_Type
{
    float4 _WorldSpaceLightPos0;
    float4 _LightPositionRange;
    float4 _LightProjectionParams;
    float4 unity_4LightPosX0;
    float4 unity_4LightPosY0;
    float4 unity_4LightPosZ0;
    float4 unity_4LightAtten0;
    float4 unity_LightColor[8];
    float4 unity_LightPosition[8];
    float4 unity_LightAtten[8];
    float4 unity_SpotDirection[8];
    float4 unity_SHAr;
    float4 unity_SHAg;
    float4 unity_SHAb;
    float4 unity_SHBr;
    float4 unity_SHBg;
    float4 unity_SHBb;
    float4 unity_SHC;
    float4 unity_OcclusionMaskSelector;
    float4 unity_ProbesOcclusion;
};

struct UnityShadows_Type
{
    float4 unity_ShadowSplitSpheres[4];
    float4 unity_ShadowSplitSqRadii;
    float4 unity_LightShadowBias;
    float4 _LightSplitsNear;
    float4 _LightSplitsFar;
    float4 hlslcc_mtx4x4unity_WorldToShadow[16];
    float4 _LightShadowData;
    float4 unity_ShadowFadeCenterAndType;
};

struct UnityPerDraw_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_WorldToObject[4];
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
    float4 unity_RenderingLayer;
};

struct UnityPerFrame_Type
{
    float4 glstate_lightmodel_ambient;
    float4 unity_AmbientSky;
    float4 unity_AmbientEquator;
    float4 unity_AmbientGround;
    float4 unity_IndirectSpecColor;
    float4 hlslcc_mtx4x4glstate_matrix_projection[4];
    float4 hlslcc_mtx4x4unity_MatrixV[4];
    float4 hlslcc_mtx4x4unity_MatrixInvV[4];
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
    int unity_StereoEyeIndex;
    float4 unity_ShadowColor;
};

struct UnityTerrain_Type
{
    float4 _WavingTint;
    float4 _WaveAndDistance;
    float4 _CameraPosition;
    float3 _CameraRight;
    float3 _CameraUp;
    float4 _TreeInstanceColor;
    float4 _TreeInstanceScale;
    float4 hlslcc_mtx4x4_TerrainEngineBendTree[4];
    float4 _SquashPlaneNormal;
    float _SquashAmount;
    float3 _TreeBillboardCameraRight;
    float4 _TreeBillboardCameraUp;
    float4 _TreeBillboardCameraFront;
    float4 _TreeBillboardCameraPos;
    float4 _TreeBillboardDistances;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float3 NORMAL0 [[ attribute(1) ]] ;
    float4 COLOR0 [[ attribute(2) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position, invariant ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant UnityLighting_Type& UnityLighting [[ buffer(0) ]],
    constant UnityShadows_Type& UnityShadows [[ buffer(1) ]],
    constant UnityPerDraw_Type& UnityPerDraw [[ buffer(2) ]],
    constant UnityPerFrame_Type& UnityPerFrame [[ buffer(3) ]],
    constant UnityTerrain_Type& UnityTerrain [[ buffer(4) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    float3 u_xlat2;
    float u_xlat9;
    float u_xlat10;
    bool u_xlatb10;
    u_xlat0.xyz = input.POSITION0.xyz * UnityTerrain._TreeInstanceScale.xyz;
    u_xlat1.xyz = u_xlat0.yyy * UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[1].xyz;
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[0].xyz, u_xlat0.xxx, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityTerrain.hlslcc_mtx4x4_TerrainEngineBendTree[2].xyz, u_xlat0.zzz, u_xlat1.xyz);
    u_xlat1.xyz = fma((-input.POSITION0.xyz), UnityTerrain._TreeInstanceScale.xyz, u_xlat1.xyz);
    u_xlat0.xyz = fma(input.COLOR0.www, u_xlat1.xyz, u_xlat0.xyz);
    u_xlat9 = dot(UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat9 = u_xlat9 + UnityTerrain._SquashPlaneNormal.w;
    u_xlat1.xyz = fma((-float3(u_xlat9)), UnityTerrain._SquashPlaneNormal.xyz, u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xyz);
    u_xlat0.xyz = fma(float3(UnityTerrain._SquashAmount), u_xlat0.xyz, u_xlat1.xyz);
    u_xlat1 = u_xlat0.yyyy * UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[0], u_xlat0.xxxx, u_xlat1);
    u_xlat0 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[2], u_xlat0.zzzz, u_xlat1);
    u_xlat0 = u_xlat0 + UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1.xyz = fma((-u_xlat0.xyz), UnityLighting._WorldSpaceLightPos0.www, UnityLighting._WorldSpaceLightPos0.xyz);
    u_xlat10 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat10 = rsqrt(u_xlat10);
    u_xlat1.xyz = float3(u_xlat10) * u_xlat1.xyz;
    u_xlat2.x = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[0].xyz);
    u_xlat2.y = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[1].xyz);
    u_xlat2.z = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[2].xyz);
    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
    u_xlat10 = rsqrt(u_xlat10);
    u_xlat2.xyz = float3(u_xlat10) * u_xlat2.xyz;
    u_xlat1.x = dot(u_xlat2.xyz, u_xlat1.xyz);
    u_xlat1.x = fma((-u_xlat1.x), u_xlat1.x, 1.0);
    u_xlat1.x = sqrt(u_xlat1.x);
    u_xlat1.x = u_xlat1.x * UnityShadows.unity_LightShadowBias.z;
    u_xlat1.xyz = fma((-u_xlat2.xyz), u_xlat1.xxx, u_xlat0.xyz);
    u_xlatb10 = UnityShadows.unity_LightShadowBias.z!=0.0;
    u_xlat0.xyz = (bool(u_xlatb10)) ? u_xlat1.xyz : u_xlat0.xyz;
    u_xlat1 = u_xlat0.yyyy * UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat1);
    u_xlat1 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat1);
    u_xlat0 = fma(UnityPerFrame.hlslcc_mtx4x4unity_MatrixVP[3], u_xlat0.wwww, u_xlat1);
    u_xlat1.x = min(u_xlat0.w, u_xlat0.z);
    u_xlat1.x = (-u_xlat0.z) + u_xlat1.x;
    output.mtl_Position.z = fma(UnityShadows.unity_LightShadowBias.y, u_xlat1.x, u_xlat0.z);
    output.mtl_Position.xyw = u_xlat0.xyw;
    return output;
}


-- Fragment shader for "metal":
Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
constant uint32_t rp_output_remap_mask [[ function_constant(1) ]];
constant const uint rp_output_remap_0 = (rp_output_remap_mask >> 0) & 0xF;
struct Mtl_FragmentOut
{
    float4 SV_Target0 [[ color(rp_output_remap_0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
)
{
    Mtl_FragmentOut output;
    output.SV_Target0 = float4(0.0, 0.0, 0.0, 0.0);
    return output;
}


 }
}
Dependency "BillboardShader" = "Hidden/Nature/Tree Soft Occlusion Bark Rendertex"
Fallback Off
}