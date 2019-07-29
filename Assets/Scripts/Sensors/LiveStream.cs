/**
	This plugin interfaces with position and pressure sensors for Unity 
	with reference of previous work done for Qt. This version is x64 for VS2012

	[ 1 ] A.Granados et al. Real-Time Visualisation and Analysis of Internal Examinations – Seeing the Unseen (MICCAI)
	[ 2 ] SensorsPlugin - AGranados2015
	[ 3 ] ICPRegistrationPlugin - AGranados2015
	[ 3 ] FingerTPS API (PPSDaq)
	[ 4 ] starTRACK API (3DG)
	[ 5 ] Boost Threading
	
	Written by:
		Alejandro Granados ( PhD MSc DIC BSc )
		ICCESS (Imperial College Centre for Engagement and Simulation Science)
		Imperial College London 2011-2015
		Contact:
			a.granados@imperial.ac.uk
			agranados.eu@gmail.com

	Immersive Visualisation for the Training and Learning of Digital Rectal Examinations.
	In collaboration with: 
		* Nanyang Technological University (Singapore)
		* Lee Kong Chian School of Medicine (Singapore)
*/

using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HoloToolkit.Unity;

public class LiveStream : Singleton<LiveStream>
{

    /**
	 * Plugin interface
	 */
    // http://hojjatjafary.blogspot.ca/2013/01/c-plugin-debug-log.html
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MyDelegate(string str);

    [DllImport("SensorsPlugin")]
    private static extern void SetTimeFromUnity(float t);

    [DllImport("SensorsPlugin")]
    public static extern void SetDebugFunction(IntPtr fp);

    [DllImport("SensorsPlugin")]
    public static extern bool Recording_Initialise(IntPtr sensorsPtr);

    [DllImport("SensorsPlugin")]
    public static extern void LiveStream_Start();

    [DllImport("SensorsPlugin")]
    public static extern void LiveStream_Stop();

    [DllImport("SensorsPlugin")]
    public static extern void LiveStream_Record();

    [DllImport("SensorsPlugin")]
    public static extern void LiveStream_StopRecord();

    [DllImport("SensorsPlugin")]
    public static extern void LiveStream_RecordSaveCalibration(IntPtr fingerPtr, float fh);

    [DllImport("SensorsPlugin")]
    public static extern void Recording_Deinitialise();

    [DllImport("SensorsPlugin")]
    public static extern void Recording_GetRecordNode(IntPtr tsPtr, IntPtr xPtr, IntPtr qPtr);


    /**
	 * Public members
	 */
    public Vector3 TranslationOffset = new Vector3(2.18f, 1.19f, 5.05f);
    public Vector3 RotationOffset;
    public bool sensorTS1 = false;
    public bool sensorTS2 = false;
    public bool sensorTS3 = false;
    public bool sensorTS4 = false;
    public int sensorPPS1 = -1;
    public int sensorPPS2 = -1;
    public int sensorPPS3 = -1;
    public int sensorPPS4 = -1;
    public float sensorOffset1 = 0.0f;
    public float sensorOffset2 = 0.0f;
    public float sensorOffset3 = 0.0f;
    public float sensorOffset4 = 0.0f;
    public GameObject sensorFinger1;
    public GameObject sensorFinger2;
    public GameObject sensorFinger3;
    public GameObject sensorFinger4;
    public GameObject landmarks;

    public float size = 1.0f;

    public float valueA = 0.7f;
    public float pressureViz;


    /**
	 * UI
	 */
    string timestampRecordStr;
    string forcePPS1Str;
    string forcePPS2Str;
    string forcePPS3Str;
    string forcePPS4Str;


    /**
	 * Private Members
	 */

    /// <summary>Memory structure for time shared between DLL and C#</summary>
    /// <value>(ts,,,force)</value>
    private Vector4[] recordNodeTsPtr;

    /// <summary>Memory structure for sensor position shared between DLL and C#</summary>
    /// <value>(x, y, z)</value>
    private Vector4[] recordNodeXPtr;

    /// <summary>Memory structure for sensor orientation shared between DLL and C#</summary>
    /// <value>(x, y, z, w)</value>
    private Vector4[] recordNodeQPtr;
    private Vector4[] sensorsPtr;
    private Vector4[] recordCalibrationQPtr;
    private GCHandle rnTsHndl;
    private GCHandle rnXHndl;
    private GCHandle rnQHndl;
    private GCHandle sensorsHndl;
    private GCHandle rcQHndl;

    private Vector3 fX;
    private Quaternion fQ;
    private Matrix4x4 wM;
    [HideInInspector]
    public Vector3 fXLocal;

    /// <summary>Initial orientation of a finger pointing to the transmitter</summary>
    private Quaternion bQ;

    /// <summary>Struture to store the calibration of each position sensor</summary>
    private Quaternion[] cQ;

    /// <summary>Structure to map a position sensor to a pressure sensor</summary>
    private Dictionary<int, int> sensors;

    /// <summary>Structure to store the 3D mesh associated to each position sensor</summary>
    private GameObject[] fingersGO;

    /// <summary>Flag to indicate whether the initialisation of sensors was successfull</summary>
    private bool initOK;
    private Vector3 sensorOffset;
    private bool sensorOffsetSet = false;


    /**
	 * Callbacks
	 */
    static void CallBackFunction(string str)
    {
        Debug.Log("SensorsRecording :: " + str);
    }


    void Awake()
    {
        this.sensorOffset = Vector3.zero;
        Debug.Log("Awake");

        // Debug callback
        MyDelegate callback_delegate = new MyDelegate(CallBackFunction);
        IntPtr intptr_delegate = Marshal.GetFunctionPointerForDelegate(callback_delegate);
        SetDebugFunction(intptr_delegate);

        // init playbackNode
        this.fX.Set(0.0f, 0.0f, 0.0f);
        this.fQ.Set(1.0f, 0.0f, 0.0f, 0.0f);
        this.recordNodeTsPtr = new Vector4[4];
        this.rnTsHndl = GCHandle.Alloc(this.recordNodeTsPtr, GCHandleType.Pinned);  // (ts,,,force)
        this.recordNodeXPtr = new Vector4[4];
        this.rnXHndl = GCHandle.Alloc(this.recordNodeXPtr, GCHandleType.Pinned);    // (x,y,x)
        this.recordNodeQPtr = new Vector4[4];
        this.rnQHndl = GCHandle.Alloc(this.recordNodeQPtr, GCHandleType.Pinned);    // (x,y,z,w)
        this.recordCalibrationQPtr = new Vector4[4];
        this.rcQHndl = GCHandle.Alloc(this.recordCalibrationQPtr, GCHandleType.Pinned);

        // initialiase finger calibration
        this.cQ = new Quaternion[4];
        for (int s = 0; s < 4; s++)
        {
            this.cQ[s] = Quaternion.identity;
        }

        // create structure for sensor maps and show fingers
        this.fingersGO = new GameObject[4];
        this.sensors = new Dictionary<int, int>();
        if (this.sensorTS1)
        {
            this.fingersGO[0] = this.sensorFinger1;
            this.sensorFinger1.SetActive(true);
            this.sensors.Add(0, this.sensorPPS1);
        }
        if (this.sensorTS2)
        {
            this.fingersGO[1] = this.sensorFinger2;
            this.sensorFinger2.SetActive(true);
            this.sensors.Add(1, this.sensorPPS2);
        }
        if (this.sensorTS3)
        {
            this.fingersGO[2] = this.sensorFinger3;
            this.sensorFinger3.SetActive(true);
            this.sensors.Add(2, this.sensorPPS3);
        }
        if (this.sensorTS4)
        {
            this.fingersGO[3] = this.sensorFinger4;
            this.sensorFinger4.SetActive(true);
            this.sensors.Add(3, this.sensorPPS4);
        }
        this.sensorsPtr = new Vector4[4];
        this.sensorsHndl = GCHandle.Alloc(this.sensorsPtr, GCHandleType.Pinned);
        for (int s = 0; s < 4; s++)
        {
            this.sensorsPtr[s].x = -1;
        }
        int sensorNum = 0;
        foreach (KeyValuePair<int, int> pair in sensors)
        {
            this.sensorsPtr[sensorNum].x = pair.Key;
            this.sensorsPtr[sensorNum].y = pair.Value;
            sensorNum++;
        }
        for (int i = 0; i < sensorNum; i++)
        {
            Debug.Log("Sensors: pid=" + sensorsPtr[i].x + " fid=" + sensorsPtr[i].y);
        }

        // initialise sensors
        this.initOK = Recording_Initialise(this.sensorsHndl.AddrOfPinnedObject());
        Debug.Log("Recording_Initialise ends");

        // start sensors
        if (this.initOK)
        {
            LiveStream_Start();
        }
    }


    // Use this for initialization
    void Start()
    {
        Debug.Log("Start");
    }


    // update finger height
    /* NTU: Replace within 3D interface */
    /*public void updateFingerHeight()
	{
		sensorOffset1 = fingerHeightSlider.value;
	}*/


    private void calibrate(int _s)
    {
        // calibrate quaternion
        Quaternion a = this.fingersGO[_s].transform.localRotation;              // sensor
                                                                                //Quaternion b = fingerCalibrationTargetGO.transform.localRotation;	// target
        this.cQ[_s] = Quaternion.Inverse(a) * this.bQ;

        // save quaternion 
        this.recordCalibrationQPtr[_s].x = this.cQ[_s].x;
        this.recordCalibrationQPtr[_s].y = this.cQ[_s].y;
        this.recordCalibrationQPtr[_s].z = this.cQ[_s].z;
        this.recordCalibrationQPtr[_s].w = this.cQ[_s].w;
    }


    /**
	 * Record Live Stream
	 */
    public void recordStart()
    {
        Debug.Log("record");
        LiveStream_Record();
    }

    public void recordStop()
    {
        Debug.Log("stopRecord");
        LiveStream_StopRecord();
    }


    /**
	 * Update is called once per frame
	 */
    void Update()
    {

        if (this.initOK)
        {

            // get last recorded node
            Recording_GetRecordNode(this.rnTsHndl.AddrOfPinnedObject(), this.rnXHndl.AddrOfPinnedObject(), this.rnQHndl.AddrOfPinnedObject());

            /**
			 * UI
			 */
            this.timestampRecordStr = this.recordNodeTsPtr[0].x.ToString("F1");
            pressureViz = this.recordNodeTsPtr[0].w * 4.448f;
            //this.forcePPS1Str = this.recordNodeTsPtr [0].w.ToString ("F1");
            //Debug.Log (this.forcePPS1Str);

            /**
			 * get data from sensors
			 */
            foreach (KeyValuePair<int, int> pair in sensors)
            {
                this.fX.x = this.recordNodeXPtr[pair.Key].x * 0.01f * transform.lossyScale.x;
                this.fX.y = this.recordNodeXPtr[pair.Key].y * 0.01f * transform.lossyScale.y;
                this.fX.z = this.recordNodeXPtr[pair.Key].z * 0.01f * transform.lossyScale.z;
                this.fQ.x = this.recordNodeQPtr[pair.Key].x;
                this.fQ.y = this.recordNodeQPtr[pair.Key].y;
                this.fQ.z = this.recordNodeQPtr[pair.Key].z;
                this.fQ.w = this.recordNodeQPtr[pair.Key].w;

                this.fXLocal = (Vector3)recordNodeXPtr[pair.Key] * 0.001f + TranslationOffset * 0.01f;

                // update position
                Vector3 newLocalPosition = new Vector3(-fXLocal.y, -fXLocal.z, -fXLocal.x);
                this.fingersGO[pair.Key].transform.localPosition = newLocalPosition;

                // update orientation
                Quaternion sQ = new Quaternion(-fQ.y, -fQ.z, -fQ.x, fQ.w);
                Quaternion newSq = Quaternion.Euler(sQ.eulerAngles + RotationOffset);
                this.fingersGO[pair.Key].transform.localRotation = newSq * this.cQ[pair.Key];  // with finger calibration

                // apply finger height
                float offset = 0.0f;
                switch (pair.Key)
                {
                    case 0: offset = this.sensorOffset1; break;
                    case 1: offset = this.sensorOffset2; break;
                    case 2: offset = this.sensorOffset3; break;
                    case 3: offset = this.sensorOffset4; break;
                };
                Vector3 qmy;
                Quaternion currQ;
                currQ = this.fingersGO[pair.Key].transform.localRotation;
                qmy.x = 2.0f * (currQ.x * currQ.y - currQ.w * currQ.z);
                qmy.y = 1.0f - 2.0f * (currQ.x * currQ.x + currQ.z * currQ.z);
                qmy.z = 2.0f * (currQ.y * currQ.z + currQ.w * currQ.x);
                this.fingersGO[pair.Key].transform.position = this.fingersGO[pair.Key].transform.position - offset * qmy;

                // UI
                //string force = this.recordNodeTsPtr[pair.Key].w.ToString("F1");
                //switch (pair.Key)
                //{
                //    case 0: this.forcePPS1Str = force; break;
                //    case 1: this.forcePPS2Str = force; break;
                //    case 2: this.forcePPS3Str = force; break;
                //    case 3: this.forcePPS4Str = force; break;
                //};
            }

            //if (pressureViz < 0.1)
            //    pressureViz = 0.1f;
            //if (pressureViz > 10.0f)
            //    pressureViz = 10.0f;
        }
    }


    // clean resources
    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");

        if (initOK)
        {
            LiveStream_Stop();

            // stop recording
            Debug.Log("Recording_Deinitialise starts");
            Recording_Deinitialise();
            Debug.Log("Recording_Deinitialise ends");
        }

        // free handlers
        rnTsHndl.Free();
        rnXHndl.Free();
        rnQHndl.Free();
        rcQHndl.Free();
    }
}
