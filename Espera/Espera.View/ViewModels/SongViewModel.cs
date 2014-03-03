using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Espera.Core;
using Rareform.Patterns.MVVM;

namespace Espera.View.ViewModels
{
    public class SongViewModel : SongViewModelBase<SongViewModel>
    {
        private BitmapImage thumbnail;

        public Song Model
        {
            get { return this.Wrapped; }
        }

        public ImageSource Thumbnail
        {
            get
            {
                return null;
            }
        }

        public string Description
        {
            get
            {
                return null;
            }
        }

        public string Path
        {
            get { return this.Model.OriginalPath; }
        }

        public ICommand OpenPathCommand
        {
            get
            {
                return new RelayCommand(param => Process.Start(this.Path));
            }
        }

        public SongViewModel(Song model)
            : base(model)
        { }
    }
}