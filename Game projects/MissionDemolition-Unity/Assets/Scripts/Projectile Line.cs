/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : ProjectileLine.cs
* DESCRIPTION     : spawns a line that follows the projectile
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/7	    Jayden Younger   		spawns a line that follows the ball
* 
*
/******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileLine : MonoBehaviour{
    static List<ProjectileLine> PROJ_LINES = new List<ProjectileLine>();

    private const float DIM_MULT = 0.75f;

    private LineRenderer _line;
    private bool _drawing = true;
    private Projectile _projectile;

    // Start is called before the first frame update
    void Start(){
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;

        _line.SetPosition(0, transform.position);

        //_projectile = GetComponent<Projectile>();

        _projectile = GetComponentInParent<Projectile>();

        ADD_LINE(this);
    }

    private void ADD_LINE(ProjectileLine newLine){
        Color col;

        foreach (ProjectileLine pl in PROJ_LINES){
            col = pl._line.startColor;

            col = col * DIM_MULT;
            pl._line.startColor = pl._line.endColor = col;
        }
        PROJ_LINES.Add(newLine);
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (_drawing){
            _line.positionCount++;

            _line.SetPosition(_line.positionCount - 1, transform.position);

            if(_projectile != null){
                if (!_projectile.Awake){
                    _drawing = false;
                    _projectile = null;
                }
            }
        }
    }

    private void OnDestroy(){
        PROJ_LINES.Remove(this);
    }
}
