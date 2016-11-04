namespace TheGame
{
    partial class TheGame
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._fpsTimer = new System.Windows.Forms.Timer(this.components);
            this._LoadWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // _fpsTimer
            // 
            this._fpsTimer.Interval = 1000;
            this._fpsTimer.Tick += new System.EventHandler(this.OnFpsCount);
            // 
            // _LoadWorker
            // 
            this._LoadWorker.WorkerReportsProgress = true;
            this._LoadWorker.WorkerSupportsCancellation = true;
            this._LoadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadForm);
            this._LoadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadProgressChanged);
            this._LoadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadCompleted);
            // 
            // TheGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(960, 720);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "TheGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Shown += new System.EventHandler(this.OnIdle);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer _fpsTimer;
        private System.ComponentModel.BackgroundWorker _LoadWorker;
    }
}

