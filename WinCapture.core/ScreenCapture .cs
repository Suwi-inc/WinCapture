namespace WinCapture.core
{
    /// <summary>
    /// Defines the <see cref="ScreenCapture" />
    /// </summary>
    public class ScreenCapture
    {
        /// <summary>
        /// Defines the screenCaptureForm
        /// </summary>
        private ScreenCaptureForm screenCaptureForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCapture"/> class.
        /// </summary>
        /// <param name="selectionColor">The selectionColor<see cref="Color"/></param>
        public ScreenCapture(Color selectionColor = default)
        {
            screenCaptureForm = new ScreenCaptureForm(selectionColor);
        }

        /// <summary>
        /// The SetSelectionColor
        /// </summary>
        /// <param name="color">The color<see cref="Color"/></param>
        public void SetSelectionColor(Color color)
        {
            screenCaptureForm.SetSelectionColor(color);
        }

        /// <summary>
        /// The CaptureImage
        /// </summary>
        /// <returns>The <see cref="Bitmap"/></returns>
        public Bitmap CaptureImage()
        {
            Bitmap? capturedImage = screenCaptureForm.CaptureImage();
            if (capturedImage == null)
            {
                throw new InvalidOperationException("Failed to capture screenshot.");
            }
            return capturedImage;
        }
    }
}
