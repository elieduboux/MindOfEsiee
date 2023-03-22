using JetBrains.Annotations;
using MyMathTools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshGenerator : MonoBehaviour
{
    
    delegate Vector3 ComputePositionDelegate(float kX, float kZ);

    //[SerializeField] Texture2D m_HeightMap;

    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();

        // DOME
        //mf.mesh = CreateNormalizePlaneXZ(200, 200,
        //(float kX, float kZ) =>
        //{
        //    Spherical sph = new Spherical(5, 2 * Mathf.PI * kX, kZ);
        //    return CoordConvert.SphericalToCartesian(sph);
        //}
        //);

        ////CAILLOU
        //mf.mesh = CreateNormalizePlaneXZ(5, 5,
        //(float kX, float kZ) =>
        //{
        //    Spherical sph = new Spherical(5, 2 * Mathf.PI * kX, Mathf.PI * kZ);
        //    return CoordConvert.SphericalToCartesian(sph);
        //}
        //);

        ////DONUT
        //mf.mesh = CreateNormalizePlaneXZ(200, 200,
        //(float kX, float kZ) =>
        //{
        //    //vector : 0.Omega = [rho*cos(theta),0, rho*sin(theta)]
        //    //theta = 2*pi*kX
        //    //vector : Omega.P = [rho*cos(alpha),rho*sin(apha),0]
        //    //vector : 0.P = [rho*cos(theta)*i + rho*sin(theta)*k + rho*cos(alpha)*i' + rho*sin(apha)*j']
        //    //rotation matrice : [cos(theta), -sin(theta)]
        //    //                   [sin(theta),  cos(theta)]
        //    //vector : i' = 0.Omega/
        //    float R = 3, r = 1;
        //    float theta = 2 * Mathf.PI * kX;
        //    float alpha = 2 * Mathf.PI * kZ;
        //    Vector3 Oomega = new Vector3(R * Mathf.Cos(theta), 0, R*Mathf.Sin(theta));


        //    return Oomega +
        //    Oomega.normalized * r * Mathf.Cos(alpha) +
        //    Vector3.up * r * Mathf.Sin(alpha);
        //}
        //);

        //// GROUND BASIC
        //mf.mesh = CreateNormalizePlaneXZ(20, 20,
        //    (float kX, float kZ) =>
        //    {
        //        return new Vector3(Mathf.Lerp(-100, 100, kX), 0, Mathf.Lerp(-100, 100, kZ));
        //    }
        //    );

        //// PLAYER
        //mf.mesh = CreateNormalizePlaneXZ(100, 100,
        //    (float kX, float kZ) =>
        //    {
        //        Spherical sph = new Spherical(0.5f, 2 * Mathf.PI * kX, Mathf.PI * kZ);
        //        return CoordConvert.SphericalToCartesian(sph);
        //    }
        //    );

        //// SPAWN CUBE WALLS
        //mf.mesh = CreateNormalizePlaneXZ(20, 20,
        //    (float kX, float kZ) =>
        //    {
        //        return new Vector3(Mathf.Lerp(-5, 5, kX), 0, Mathf.Lerp(-1, 3, kZ));
        //    }
        //    );

        //// CYLINDRE
        //mf.mesh = CreateNormalizePlaneXZ(100, 100,
        //    (float kX, float kZ) =>
        //    {
        //        Cylindrical sph = new Cylindrical(0.5f, 2 * Mathf.PI * kX, Mathf.PI * kZ);
        //        return CoordConvert.CylindricalToCartesian(sph);
        //    }
        //    );

        // Oreilles de chat
        //mf.mesh = CreateNormalizePlaneXZ(200, 200,
        //    (float kX, float kZ) =>
        //    {
        //        return new Vector3(Mathf.Lerp(-2, 2, kX),
        //                          (2+ Mathf.Sin(kX * Mathf.PI * 1)) * 1f * (1 + Mathf.Cos(kZ * 2 * Mathf.PI * 1)),
        //                          Mathf.Lerp(-5, 5, kZ));
        //    }
        //    );

        // Petal --> enlever phi et multiplier par qqch de plus petit
        //mf.mesh = CreateNormalizePlaneXZ(200, 200,
        //(float kX, float kZ) =>
        //{
        //    Spherical sph = new Spherical(5, 2*Mathf.PI * kZ, kX * 0.01f);
        //    return CoordConvert.SphericalToCartesian(sph);
        //}
        //);

        //// HALF CYLINDER
        //mf.mesh = CreateNormalizePlaneXZ(100, 100,
        //    (float kX, float kZ) =>
        //    {
        //        Cylindrical sph = new Cylindrical(0.5f,Mathf.PI * kX, Mathf.PI * kZ);
        //        return CoordConvert.CylindricalToCartesian(sph);
        //    }
        //    );

        ////pillier
        //mf.mesh = CreateNormalizePlaneXZ(60, 60,
        //    (float kX, float kZ) =>
        //    {
        //        float x = kX * Mathf.PI * 2;
        //        Cylindrical pillier = new Cylindrical(Mathf.Cos(x * 20) + 15, x, kZ * 50);
        //        return CoordConvert.CylindricalToCartesian(pillier);
        //    }
        //    );

    }

    private Mesh CreateNormalizePlaneXZ(int nSegmentX, int nSegmentZ, ComputePositionDelegate posCompute)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        mesh.name = "StripXZ";

        Vector3[] vertices = new Vector3[(nSegmentX + 1) * (nSegmentZ + 1)];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[nSegmentX * nSegmentZ * 3 * 2];

        int index = 0;
        //vertices (+ uv & normals)
        for (int i = 0; i < nSegmentZ + 1; i++)
        {
            float kZ = (float)i / nSegmentZ;
            for (int j = 0; j < nSegmentX + 1; j++)
            {
                float kX = (float)j / nSegmentX;

                vertices[index] = (posCompute(kX, kZ) != null) ? posCompute(kX, kZ) : new Vector3(kX,kZ);

                normals[index] = Vector3.up;
                uv[index++] = new Vector3(kX, kZ);
            }
        }

        //Triangles
        index = 0;
        int indexOffset = 0;
        for (int i = 0; i < nSegmentZ; i++)
        {
            for (int j = 0; j < nSegmentX; j++)
            {
                triangles[index++] = indexOffset + j;
                triangles[index++] = indexOffset + j + nSegmentX + 1;
                triangles[index++] = indexOffset + j + 1;

                triangles[index++] = indexOffset + j + 1;
                triangles[index++] = indexOffset + j + nSegmentX + 1;
                triangles[index++] = indexOffset + j + nSegmentX + 1 + 1;
            }
            indexOffset += nSegmentX + 1;
        }
        //for (int i = 0; i < nSegmentZ; i++)
        //{
        //    for (int j = 0; j < nSegmentX; j++)
        //    {
        //        triangles[index++] = i * (nSegmentX + 1) + j;
        //        triangles[index++] = i * (nSegmentX + 1) + j + nSegmentX + 1 + 1;
        //        triangles[index++] = i * (nSegmentX + 1) + j + nSegmentX + 1;

        //        triangles[index++] = i * (nSegmentX + 1) + j;
        //        triangles[index++] = i * (nSegmentX + 1) + j + 1;
        //        triangles[index++] = i * (nSegmentX + 1) + j + nSegmentX + 1 + 1;
        //    }
        //}

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

}
