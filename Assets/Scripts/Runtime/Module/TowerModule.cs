using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;

    private int _maxPartPiece = 7;
    private int _overlapPercent = 90;
 
    public SpriteRenderer Sprite
    {
        get { return this._sprite; }
        set { this._sprite.size = value.size; }
    }

    public SpriteRenderer Color
    {
        get { return this._sprite; }
        set { this._sprite.color = value.color; }
    }

    public Vector2 Pos
    {
        get { return this.gameObject.transform.position; }
        set { this.gameObject.transform.position = new Vector2(value.x, value.y); }
    }

    public int MaxPiece
    {
        get { return this._maxPartPiece; }
        set { this._maxPartPiece = value; }
    }

    public int OverlapPercent
    {
        get { return this._overlapPercent; }
        set { this._overlapPercent = value; }
    }

    public void SetTweenY(float target, float delay, Action complete)
    {
        this.gameObject.transform.DOMoveY(target, delay)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                complete();
            });
    }

    public void SetCutPieceAnimation()
    {
        float delay = 1f;
        this.gameObject.transform.DORotate(new Vector3(0, 0, 360), delay).SetRelative(true).SetEase(Ease.Linear);
        this.Color.DOFade(0, delay / 2);
        this.SetTweenY(-10, delay, () => Destroy(this.gameObject));
    }

    public void SetImageSprite(int sprite)
    {
        switch (sprite)
        {
            case 0: this._sprite.sprite = this._sprite1; break;
            case 1: this._sprite.sprite = this._sprite2; break;
            case 2: this._sprite.sprite = this._sprite3; break;
            default: Debug.Log("Not found sprite image"); break;
        }
    }
}