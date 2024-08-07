using UnityEngine;

public class GameLoginState : GameState
{
    public GameLoginState(GameManager manager) : base(manager)
    {
        this.manager = manager;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter State : " + this);
        manager.ChangeState(EState.HOME);
    }

    public override void OnExit()
    {
        Debug.Log("Exit State : " + this);
    }

    public override void SubscribeEvent()
    {
        
    }

    public override void UnSubscribeEvent()
    {

    }
}
