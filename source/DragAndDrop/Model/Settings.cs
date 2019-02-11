namespace DragAndDrop.Model
{
    /// <summary>
    /// Application settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Image determination server url
        /// </summary>
        public string ImageDeterminationUrl { get; set; }

        /// <summary>
        /// Determinate type
        /// </summary>
        public DeterminatorType Determinator { get; set; }

        /// <summary>
        /// When unmatch determinator settings is true
        /// </summary>
        public bool IsUpdateDeterminator => (int)this.Determinator != Properties.Settings.Default.Determinator;

        /// <summary>
        /// Constractor
        /// </summary>
        public Settings()
        {
            this.Load();
        }

        /// <summary>
        /// Load settings
        /// </summary>
        public void Load()
        {
            this.ImageDeterminationUrl = Properties.Settings.Default.ImageDeterminationUrl;
            this.Determinator = (DeterminatorType)Properties.Settings.Default.Determinator;
        }

        /// <summary>
        /// Save settings
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.ImageDeterminationUrl = this.ImageDeterminationUrl;
            Properties.Settings.Default.Determinator = (int)this.Determinator;
            Properties.Settings.Default.Save();
        }
    }
}
