﻿using System.Windows.Input;

namespace SimpleMusicPlayer.Core.Interfaces
{
    public interface IKeyHandler
    {
        bool HandleKeyDown(Key key);
    }
}