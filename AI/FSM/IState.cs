﻿namespace Disorder.Unity.Core
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}