# WinCapture - A C# Library to make screen captures
## Usage 
#### 1. Create a `WinForms` Application and add `WinCapture.core.dll` to the project references.
#### 2. to initialize the ScreenCapture Class use:
```sh
public ScreenCapture(Color selectionColor = default)
```
#### To set the section Color use:
```sh
public void SetSelectionColor(Color color)
```
#### To Capture the screen use:
```sh
public Bitmap CaptureImage()
```
## Usage Example
```sh
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsClipSolver.Core;

namespace WindowsClipSolver.WinFormsApp
{
    public partial class MainForm : Form
    {
        private ScreenCapture screenCapture;

        public MainForm()
        {
            InitializeComponent();
            screenCapture = new ScreenCapture();
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap capturedImage = screenCapture.CaptureImage();
                // Display the image in a PictureBox or save it
                pictureBox.Image = capturedImage;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

```
