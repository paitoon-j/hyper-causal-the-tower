using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Reflection;
using TMPro.Examples;

public class GameController : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private GameObject _piecePrefab;
    [SerializeField] private GameObject _toppingPrefab;

    private List<TowerModule> _towerList = new List<TowerModule>();
    private List<float> _sizeList = new List<float>();
    private Sequence _sequence;
    private int _createFloorDefault = 1;
    private int _bonusFloorTower = 2;
    private float _speed = 1f;
    private float _distanceConfig = 3f;
    private int _maxTowerConfig = 5;
    private float _randomChancePercent = 60;
    private float _baseMaxSpeed = 20f;
    private int _currentPerfectStack = 0;
    private int _maxPerfectStack = 5;

    #region get data
    private float GetBorderScreen(float posX, float speedMuliply)
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        float velocity = 1f * speedMuliply;
        float origin = towerCurrent.Pos.x;
        float target = towerCurrent.Pos.x + posX;
        float distance = Mathf.Abs(target - origin);
        float delay = distance / velocity;
        return delay;
    }

    private float GetSize(float value)
    {
        float result = 0;
        for (int index = 0; index < _sizeList.Count; index++)
        {
            if (_sizeList[index] < value)
            {
                result = _sizeList[index];
            }
        }
        return result;
    }

    private float GetWidth
    {
        get
        {
            float height = 2f * _camera.orthographicSize;
            float width = height * _camera.aspect;
            TowerModule towerCurrent = _towerList[_towerList.Count - 1];
            float value = (width / 2) - (towerCurrent.Sprite.size.x / 2);
            return value;
        }
    }

    private float GetHeight
    {
        get
        {
            float height = 2f * _camera.orthographicSize;
            TowerModule towerCurrent = _towerList[_towerList.Count - 1];
            float value = ((height / 2) - (towerCurrent.Sprite.size.y / 2)) + towerCurrent.Sprite.size.y;
            return value;
        }
    }

    private float GetLevelSpeedUp
    {
        get
        {
            int everyTowerCount = 10;
            int everyTowerRandom = 15;
            float min = 0;
            float max = 100;
            float randomRang = Helper.getRandomRang(min, max);
            bool isEveryTower = _towerList.Count % everyTowerRandom == 0;
            bool isRandomChance = _randomChancePercent <= randomRang;

            if (isEveryTower && isRandomChance)
            {
                return Helper.getRandomRang(min, _baseMaxSpeed);
            }
            else
            {
                return Mathf.Min(_baseMaxSpeed, _speed + ((_towerList.Count) / everyTowerCount));
            }
        }
    }

    private bool IsGameOver
    {
        get
        {
            TowerModule towerCurrent = _towerList[_towerList.Count - 1];
            TowerModule towerBefore = _towerList[_towerList.Count - 2];
            float result1 = towerCurrent.Pos.x + towerBefore.Sprite.size.x;
            float result2 = towerCurrent.Pos.x - towerBefore.Sprite.size.x;
            bool isGameOver = result1 < towerBefore.Pos.x || result2 > towerBefore.Pos.x;
            return isGameOver;
        }
    }
    #endregion

    ////////////////////////////////////////////////////

    public void Init()
    {
        SetSizeList();
    }

    public async void SetBaseTowerAsync(bool isStartHead, Action complete)
    {
        await StartBaseTowerTweenAsync();
        CreateAndMoveTower(complete);
    }

    public void SetPlaceTower(Action gameOver, Action complete)
    {
        _sequence.Kill();
        StopTower(gameOver, complete);
    }

    public void SetSlowTower(float value, float duration)
    {
        Time.timeScale = value;
        StartCoroutine(SlowDuration(duration));
    }

    public void SetHeadStartTower(int towerCount)
    {
        _createFloorDefault = towerCount;
    }

    public void ClearData()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _createFloorDefault = 1;
        _currentPerfectStack = 0;
        _towerList.Clear();
        _sequence.Kill();
    }

    ////////////////////////////////////////////////////

    #region private function

    private async UniTask StartBaseTowerTweenAsync()
    {
        float delay = 0.2f;

        for (int index = 0; index < _createFloorDefault; index++)
        {
            CreateFloorTower();
            SetTowerHeightPos();
            SetTowerMoveToBase(index, delay, () =>
            {
                MoveBackgroundTower();
            });

            await Helper.DelayAsync(delay + 0.1f);
        }
    }

    private void SetTowerMoveToBase(int index, float delay, Action complete)
    {
        TowerModule towerCurrent = _towerList[index];
        float target = (GetHeight - towerCurrent.Sprite.size.y) - (_towerList.Count - 1);
        towerCurrent.SetTweenY(-target, delay, complete);
    }

    private void SetTowerHeightPos()
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        float towerBasePosY = (towerCurrent.Pos.y + GetHeight) + (_towerList.Count * towerCurrent.Sprite.size.y);
        towerCurrent.Pos = new Vector2(towerCurrent.Pos.x, towerBasePosY);
    }

    ////////////////////////////////////////////////////

    private void CreateAndMoveTower(Action complete)
    {
        CreateFloorTower();
        SetScaleTower();
        MoveTowerHorizontal();
        MoveBackgroundTower(complete);
    }

    private void CreateFloorTower()
    {
        GameObject obj = Instantiate(_towerPrefab, transform.position, Quaternion.identity);
        obj.transform.position = _towerPrefab.transform.position;
        obj.transform.parent = transform;
        _towerList.Add(obj.GetComponent<TowerModule>());
    }

    private void SetScaleTower()
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];
        towerCurrent.Sprite.size = towerBefore.Sprite.size;
    }

    private void MoveTowerHorizontal()
    {
        bool isMoveLeft = (_towerList.Count % 2) == 0;
        if (isMoveLeft)
        {
            SetTowerStartPos(-GetWidth);
            MoveTweenLoop(GetWidth, GetLevelSpeedUp);
        }
        else
        {
            SetTowerStartPos(GetWidth);
            MoveTweenLoop(-GetWidth, GetLevelSpeedUp);
        }
    }

    private void SetTowerStartPos(float pos)
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];
        float posY = towerBefore.Pos.y + _distanceConfig;
        towerCurrent.Pos = new Vector2(pos, posY);
    }

    private void MoveTweenLoop(float posX, float speedMuliply)
    {
        _sequence = DOTween.Sequence();
        float delay = GetBorderScreen(posX, speedMuliply);
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        Tween tween1 = towerCurrent.transform.DOMoveX(posX, delay).SetEase(Ease.Linear);
        Tween tween2 = towerCurrent.transform.DOMoveX(-posX, delay).SetEase(Ease.Linear);
        _sequence.Append(tween1);
        _sequence.Append(tween2);
        _sequence.SetLoops(-1);
    }

    private void MoveBackgroundTower(Action complete = null)
    {
        if (_towerList.Count > _maxTowerConfig)
        {
            float delay = 0.1f;
            TowerModule tower = _towerList[0];
            _camera.MoveTween(tower.Sprite.size.y * (_towerList.Count - _maxTowerConfig), delay, complete);
        }
        else
        {
            complete?.Invoke();
        }
    }

    ////////////////////////////////////////////////////

    private void StopTower(Action gameOver, Action complete)
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];
        float target = towerBefore.Pos.y + towerBefore.Sprite.size.y;
        float delay = 0.2f;

        towerCurrent.SetTweenY(target, delay, async () =>
        {
            if (IsGameOver)
            {
                towerCurrent.SetCutPieceAnimation();
                SetStickMoveEnd(gameOver);
            }
            else
            {
                SetSnapTower();
                await SetBonusTowerAsync();
                CreateAndMoveTower(complete);
            }
        });
    }

    private async Task SetBonusTowerAsync()
    {
        bool isBonus = (_currentPerfectStack % _maxPerfectStack) == _maxPerfectStack - 1;
        if (isBonus)
        {
            for (int index = 0; index < _bonusFloorTower; index++)
            {
                CreateFloorTower();
                SetBonusTowerPos();
                SetImageSpriteTower();
                SetScaleTower();
                MoveBackgroundTower();
                await Helper.DelayAsync(0.1f);
            }
        }
    }

    private void SetBonusTowerPos()
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];
        float towerBasePosY = towerBefore.Pos.y + towerBefore.Sprite.size.y;
        towerCurrent.Pos = new Vector2(towerBefore.Pos.x, towerBasePosY);
    }

    private void SetImageSpriteTower()
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        towerCurrent.SetImageSprite(1);
    }

    ////////////////////////////////////////////////////

    private void SetSnapTower()
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];

        float towerSizeBefore = towerBefore.Sprite.size.x;
        float towerPosCurrent = towerCurrent.Pos.x;
        float towerPosBefore = towerBefore.Pos.x;

        if (towerPosCurrent >= towerPosBefore)
        {
            SetSnapTowerValue(towerSizeBefore, -towerPosCurrent, towerPosBefore);
        }
        else
        {
            SetSnapTowerValue(towerSizeBefore, towerPosCurrent, -towerPosBefore);
        }
    }

    private void SetSnapTowerValue(float towerSizeBefore, float towerPosCurrent, float towerPosBefore)
    {
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        TowerModule towerBefore = _towerList[_towerList.Count - 2];

        float sizeCurrent = (towerSizeBefore + towerPosCurrent) + towerPosBefore;
        float nearSize = ((towerCurrent.Sprite.size.x / towerCurrent.MaxPiece) / 2);
        float overlapSize = ((_towerList[0].Sprite.size.x / towerCurrent.MaxPiece) / 2f);
        float overlapPercent = Helper.getNumberWithPercent(overlapSize, towerCurrent.OverlapPercent);
        float cutTowerValue = MathF.Abs(sizeCurrent - towerSizeBefore);

        bool isTowerPosLeft = towerCurrent.Pos.x < towerBefore.Pos.x;
        float leftPos = -((GetSize(cutTowerValue) / 2) + towerPosBefore);
        float rightPos = (GetSize(cutTowerValue) / 2) + towerPosBefore;
        float towerPosX = isTowerPosLeft ? leftPos : rightPos;
        float overlapPosX = isTowerPosLeft ? leftPos - overlapPercent : rightPos + overlapPercent;
        float sizeX = towerCurrent.Sprite.size.x - GetSize(cutTowerValue);
        float overlapCut = cutTowerValue - GetSize(cutTowerValue);
        bool isCutValue = GetSize(cutTowerValue) > 0;

        if (overlapCut > overlapPercent)
        {
            towerCurrent.Sprite.size = new Vector2(sizeX, 1f);
            towerCurrent.Pos = new Vector2(overlapPosX, towerCurrent.Pos.y);
            _currentPerfectStack = 0;
            Debug.Log("Good");
        }
        else if (cutTowerValue > nearSize)
        {
            towerCurrent.Sprite.size = new Vector2(sizeX, 1f);
            if (isCutValue)
            {
                towerCurrent.Pos = new Vector2(towerPosX, towerCurrent.Pos.y);
                _currentPerfectStack = 0;
                Debug.Log("Great");
            }
            else
            {
                towerCurrent.Pos = new Vector2(towerBefore.Pos.x, towerCurrent.Pos.y);
                _currentPerfectStack++;
                Debug.Log("Perfect");
            }
        }
        else
        {
            towerCurrent.Sprite.size = new Vector2(towerBefore.Sprite.size.x, towerBefore.Sprite.size.y);
            towerCurrent.Pos = new Vector2(towerBefore.Pos.x, towerCurrent.Pos.y);
            _currentPerfectStack++;
            Debug.Log("Perfect");
        }

        SetSpawnCutPiece(towerCurrent, towerBefore, cutTowerValue, isCutValue);
    }

    private void SetSpawnCutPiece(TowerModule towerCurrent, TowerModule towerBefore, float cutTowerValue, bool isCutValue)
    {
        if (isCutValue)
        {
            GameObject obj = Instantiate(_piecePrefab, transform.position, Quaternion.identity);
            TowerModule piece = obj.GetComponent<TowerModule>();

            bool isTowerPosLeft = towerCurrent.Pos.x < towerBefore.Pos.x;
            float pieceLeftPos = (towerCurrent.Pos.x - (towerCurrent.Sprite.size.x / 2)) - (GetSize(cutTowerValue) / 2);
            float pieceRightPos = (towerCurrent.Pos.x + (towerCurrent.Sprite.size.x / 2)) + (GetSize(cutTowerValue) / 2);
            float piecePosX = isTowerPosLeft ? pieceLeftPos : pieceRightPos;

            piece.Sprite.size = new Vector2(GetSize(cutTowerValue), piece.Sprite.size.y);
            piece.Pos = new Vector2(piecePosX, towerCurrent.Pos.y);
            piece.transform.parent = transform.parent;
            piece.SetCutPieceAnimation();
        }
    }

    ////////////////////////////////////////////////////

    private void SetStickMoveEnd(Action complete)
    {
        TowerModule towerBefore = _towerList[_towerList.Count - 2];
        float delay = 1f;
        GameObject obj = Instantiate(_toppingPrefab, transform.position, Quaternion.identity);
        obj.transform.position = new Vector2(towerBefore.Pos.x, GetHeight + (_towerList.Count * towerBefore.Sprite.size.y));
        obj.transform.parent = transform;
        obj.transform.Rotate(0, 0, 45);
        obj.transform.DOMoveY(towerBefore.Pos.y + 1.5f, delay)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _camera.MoveTween(0, 0.2f * _towerList.Count, complete);
            });
    }

    ////////////////////////////////////////////////////

    private void SetSizeList()
    {
        float result = 0f;
        TowerModule towerCurrent = _towerList[_towerList.Count - 1];
        float maxPieceSize = towerCurrent.Sprite.size.x / towerCurrent.MaxPiece;

        for (int index = 0; index < towerCurrent.MaxPiece; index++)
        {
            result += maxPieceSize;
            _sizeList.Add(result);
        }
    }

    private IEnumerator SlowDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        float defaultTime = 1;
        Time.timeScale = defaultTime;
    }
    #endregion
}