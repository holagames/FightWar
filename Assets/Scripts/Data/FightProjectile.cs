using UnityEngine;
using System.Collections;

public class FightProjectile {

    public int projectileID { get; private set; }

    public string remark { get; private set; }

    public string sound { get; private set; }

    public string projectileEffect { get; private set; }

    public int projectilePosX { get; private set; }
    public int projectilePosY { get; private set; }
    public int projectilePosZ { get; private set; }
    public int projectileSpeed { get; private set; }

    public int effectRatio { get; private set; }

    public int distroy { get; private set; }

    public int touchMode { get; private set; }
    public string touchEffect { get; private set; }
    public string touchEffect2 { get; private set; }

    public int touchEffectPosX { get; private set; }
    public int touchEffectPosY { get; private set; }
    public int effectDelay { get; private set; }

    public int summonFlag { get; private set; }
    public int allyFlag { get; private set; }


    public FightProjectile(int newProjectileID, string newRemark, string newSound, string newProjectileEffect,
        int newProjectilePosX, int newProjectilePosY, int newProjectilePosZ, int newEffectRatio, int newDistroy, int newTouchMode,
        string newTouchEffect, string newTouchEffect2, int newTouchEffectPosX, int newTouchEffectPosY, int newProjectileSpeed,
        int newEffectDelay, int newSummonFlag, int newAllyFlag)
    {
        this.projectileID = newProjectileID;
        this.remark = newRemark;
        this.sound = newSound;
        this.projectileEffect = newProjectileEffect;
        this.projectilePosX = newProjectilePosX;
        this.projectilePosY = newProjectilePosY;
        this.projectilePosZ = newProjectilePosZ;
        this.effectRatio = newEffectRatio;
        this.distroy = newDistroy;
        this.touchMode = newTouchMode;
        this.touchEffect = newTouchEffect;
        this.touchEffect2 = newTouchEffect2;
        this.touchEffectPosX = newTouchEffectPosX;
        this.touchEffectPosY = newTouchEffectPosY;
        this.projectileSpeed = newProjectileSpeed;
        this.effectDelay = newEffectDelay;
        this.summonFlag = newSummonFlag;
        this.allyFlag = newAllyFlag;
    }
}