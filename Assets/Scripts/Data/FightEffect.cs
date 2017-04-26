using UnityEngine;
using System.Collections;

public class FightEffect {

    public int effectID { get; private set; }

    public string remark { get; private set; }

    public string sound { get; private set; }

    public string effect { get; private set; }
    public string effectParent { get; private set; }

    public int effectPosX { get; private set; }
    public int effectPosY { get; private set; }

    public int effectRatio { get; private set; }

    public int projectile { get; private set; }

    public int shake { get; private set; }

    public int shakeRange { get; private set; }

    public int shakeTime { get; private set; }
    public int shakeDelay { get; private set; }
    public int soundDelay { get; private set; }

    public FightEffect(int newEffectID, string newRemark, string newSound, string newEffectParent, string newEffect,
        int newEffectPosX, int newEffectPosY, int newEffectRatio, int newProjectile, int newShake,
        int newShakeRange, int newShakeTime, int newSoundDelay, int newShakeDelay)
    {
        this.effectID = newEffectID;
        this.remark = newRemark;
        this.sound = newSound;
        this.effectParent = newEffectParent;
        this.effect = newEffect;
        this.effectPosX = newEffectPosX;
        this.effectPosY = newEffectPosY;
        this.effectRatio = newEffectRatio;
        this.projectile = newProjectile;
        this.shake = newShake;
        this.shakeRange = newShakeRange;
        this.shakeTime = newShakeTime;
        this.soundDelay = newSoundDelay;
        this.shakeDelay = newShakeDelay;
    }
}