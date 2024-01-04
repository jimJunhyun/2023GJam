using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : PoolAble
{
    private ParticleSystem _particle;
    private bool _init = false;

    public ParticleSystem Particle
    {
        get
        {
            if (_particle == null)
                _particle = GetComponent<ParticleSystem>();
            
            return _particle;
        }
    }

    public override void Reset()
    {
        Particle.Stop();
        _init = false;
    }

    public void Init(Vector3 pos )
    {
        transform.position = pos;
        Particle.Play();
        _init = true;
    }

    public void Init(Vector3 pos, Quaternion qut)
    {
        transform.position = pos;
        transform.rotation = qut;
        
        Particle.Play();
        _init = true;
    }
    
    public void Init(Transform parent, Vector3 _vec)
    {
        transform.parent = parent;
        transform.position = parent.position + _vec;    
        Particle.Play();
        _init = true;
    }
    private void Update()
    {
        if (_init && Particle.isPlaying == false)
        {
            Particle.Stop();
            PoolManager.Instance.Push(this);
        }
    }
}
