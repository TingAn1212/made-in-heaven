using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteTenc : MonoBehaviour
{
    public int length;
    public LineRenderer Line;
    public Vector3[] Segments;
    Vector3[] SegmentV;

    public Transform Self;
    public float TargetDist;
    public float SmoothSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Line.useWorldSpace = false;
        Line.positionCount = length;
        Segments = new Vector3[length];
        SegmentV = new Vector3[length];
        for (int i = 0;i<length;i++){
            Segments[i] = Self.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Line.useWorldSpace = true;
        Segments[0] = Self.position;
        for (int i = 1; i<Segments.Length; i++){
            Segments[i] = Vector3.SmoothDamp(Segments[i], Segments[i-1] + Self.right * TargetDist, ref SegmentV[i], SmoothSpeed);
        }   
        Line.SetPositions(Segments);
    }
}
