using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    private static ParticleEffectManager instance;

    [SerializeField] Particleeffects[] particle_effects;



    public static ParticleEffectManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayParticleEffect(ParticleEffectTypes type, Transform transform)
    {
        ParticleSystem particle_system;
        Vector3 position = transform.position;
        particle_system = GetParticleSystem(type);
        if (particle_system != null)
        {
            if (type == ParticleEffectTypes.PlayerSpawn)
                position.y -= 1.6f;
            else if (type == ParticleEffectTypes.EnemyHit)
                position.y += 1.1f;
            else if(type == ParticleEffectTypes.LevelComplete)
                position.y += 1.5f;
            particle_system.gameObject.transform.position = position;
            particle_system.Play();
        }
        else
            Debug.Log(type + " effect not found.");
    }

    private ParticleSystem GetParticleSystem(ParticleEffectTypes particle_effect_type)
    {
        Particleeffects effect = Array.Find(particle_effects, i => i.type_of_particle_effect == particle_effect_type);
        if (effect != null)
            return effect.particle_effect;
        return null;
    }
}

[Serializable]
public class Particleeffects
{
    public ParticleEffectTypes type_of_particle_effect;
    public ParticleSystem particle_effect;
}

public enum ParticleEffectTypes
{
    PlayerSpawn,
    LevelComplete,
    EnemyHit,
    GamneComplete
}
