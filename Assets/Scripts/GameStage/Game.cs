using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModelView
{
    Skin,
    Prostate,
    Anatomy
}

public class Game : Singleton<Game> {

    public event OnModelViewChangeDelegate OnModelViewChange;
    public delegate void OnModelViewChangeDelegate(ModelView newVal);

    [SerializeField]
    private ModelView _modelView = ModelView.Skin;
    public ModelView ModelView
    {
        get
        {
            return _modelView;
        }
        set
        {
            if (_modelView == value) return;
            _modelView = value;
            if (OnModelViewChange != null)
                OnModelViewChange(_modelView);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
