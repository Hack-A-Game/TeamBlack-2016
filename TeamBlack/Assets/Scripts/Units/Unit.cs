﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    public int mana = 0;
    public float HP = 0.0f;
    public float MAXHP = 0.0f;
    public float Att = 0.0f;
    public float Def = 0.0f;
    public float AttSp = 0.0f;
    public float Speed = 2.0f;
    public bool isAttacking;
    public float countdown = 0.0f;
    public List<Unit> encounterList = new List<Unit>();
    protected Unit target;
    public float range;
    public HPBar bar;
    public bool canCapture = false;

    public void getAttacked(float damage)
    {
        
        HP -= damage / Def;
        
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }
    public double getHP()
    {
        return HP;
    }

    public double getAtt()
    {
        return Att;
    }

    public double getDef()
    {
        return Def;
    }

    public double getAttSp()
    {
        return AttSp;
    }

    public int getMana()
    {
        return mana;
    }

    public float getSpeed()
    {
        return Speed;
    }
    public virtual void Start() { }
    public virtual void Update()
    {

        if (bar != null) {
            bar.updateHP(HP, MAXHP, transform.position);
        }

        if (getHP() <= 0.0f)
        {
            Destroy(gameObject);
        }

        if (encounterList.Count > 0 || target)
        {
            Debug.Log(gameObject + " attacking " + target);
            isAttacking = true;
            countdown -= Time.deltaTime;

            if (target == null)
            {
                target = encounterList[0];
                Debug.Log(gameObject + " setTarget " + target);
            }

            if (isInRange(range) && target.getHP() > 0 && countdown <= 0.0f)
            {
                target.getAttacked(Att);
                countdown = AttSp;

                if (target.getHP() <= 0.0f)
                {
                    Debug.Log(target + " Dead");
                    encounterList.RemoveAt(0);
                    target = null;
                }
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    public void targetOutOfRange()
    {
        if (target != null)
        {
            encounterList.RemoveAt(0);
        }
        target = null;
    }

    public Unit getTarget()
    {
        return target;
    }

    public bool isInRange(float range)
    {
        if (target)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            return dist < range;
        }

        return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.tag == "AttackUnit" && collision.gameObject.tag == "Monster")
            || (gameObject.tag == "Monster" && collision.gameObject.tag == "AttackUnit"))
        {
            Debug.Log(gameObject + " found " + collision.gameObject);
            encounterList.Add(collision.gameObject.GetComponent<Unit>());
        }else if (canCapture == true && collision.gameObject.tag=="Flag")
        {
            encounterList.Add(collision.gameObject.GetComponent<Unit>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if ((gameObject.tag == "AttackUnit" && collision.gameObject.tag == "Monster")
            || (gameObject.tag == "Monster" && collision.gameObject.tag == "AttackUnit"))
        {
            Unit unit = collision.gameObject.GetComponent<Unit>();
            encounterList.Remove(unit);
            if (target == unit)
            {
                target = unit;
            }
        }
    }
}
