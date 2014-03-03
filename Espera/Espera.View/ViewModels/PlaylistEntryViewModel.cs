﻿using System;
using Espera.Core;

namespace Espera.View.ViewModels
{
    public class PlaylistEntryViewModel : SongViewModelBase<PlaylistEntryViewModel>
    {
        private bool isPlaying;
        private bool isInactive;
        private int cacheProgress;
        private bool hasCachingFailed;

        public bool HasCachingFailed
        {
            get { return this.hasCachingFailed; }
            set
            {
                if (this.HasCachingFailed != value)
                {
                    this.hasCachingFailed = value;
                    this.OnPropertyChanged(vm => vm.HasCachingFailed);
                }
            }
        }

        public int CacheProgress
        {
            get { return this.cacheProgress; }
            set
            {
                if (this.CacheProgress != value)
                {
                    this.cacheProgress = value;
                    this.OnPropertyChanged(vm => vm.CacheProgress);
                }
            }
        }

        public int Index { get; private set; }

        public bool IsPlaying
        {
            get { return this.isPlaying; }
            set
            {
                if (this.IsPlaying != value)
                {
                    this.isPlaying = value;
                    this.OnPropertyChanged(vm => vm.IsPlaying);
                }
            }
        }

        public bool IsInactive
        {
            get { return this.isInactive; }
            set
            {
                if (this.IsInactive != value)
                {
                    this.isInactive = value;
                    this.OnPropertyChanged(vm => vm.IsInactive);
                }
            }
        }

        public string Source
        {
            get
            {
                if (this.Wrapped is LocalSong)
                {
                    return "Local";
                }

                

                throw new InvalidOperationException();
            }
        }

        public PlaylistEntryViewModel(Song model, int index)
            : base(model)
        {
            this.Index = index;

            if (this.Wrapped.IsCached)
            {
                this.CacheProgress = 100;
            }

            else
            {
                this.Wrapped.CachingProgressChanged +=
                    (sender, e) => this.CacheProgress = (int)((e.TransferredBytes * 1.0 / e.TotalBytes) * 100);

                this.Wrapped.CachingFailed += (sender, args) => this.HasCachingFailed = true;

                this.Wrapped.CachingCompleted += (sender, e) => this.CacheProgress = 100;
            }
        }
    }
}