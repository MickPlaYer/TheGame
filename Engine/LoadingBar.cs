using System.Windows.Forms;

namespace TheGame.Engine
{
    public partial class LoadingBar : Form
    {
        private bool _isLoadFail = false;

        public LoadingBar()
        {
            InitializeComponent();
        }

        public void SetValue(int value)
        {
            _progressBar.Value = value;
        }

        public bool IsLoadFail
        {
            get { return _isLoadFail; }
            set { _isLoadFail = value; }
        }
    }
}
