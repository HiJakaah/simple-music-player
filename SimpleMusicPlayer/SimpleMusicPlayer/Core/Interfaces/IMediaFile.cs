﻿using System.Windows.Media.Imaging;

namespace SimpleMusicPlayer.Core.Interfaces
{
    public interface IMediaFile
    {
        string FullFileName { get; }
        int PlayListIndex { get; set; }

        PlayerState State { get; set; }

        string Title { get; }
        BitmapImage Cover { get; }
        string Album { get; }
        string FirstPerformer { get; }
        string FirstGenre { get; }

        bool IsVBR { get; }
    }
}