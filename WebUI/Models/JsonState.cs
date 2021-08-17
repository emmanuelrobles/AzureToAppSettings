using System;

namespace WebUI.Models
{
    public class JsonState
    {
        private string _json;
        public string Json
        {
            get => _json;
            set
            {
                _json = value;
                NotifyStateChanged();
            }
        }
        
        private string _appSettingsJson;
        public string AppSettingsJson
        {
            get => _appSettingsJson;
            set
            {
                _appSettingsJson = value;
                NotifyStateChanged();
            }
        }

        private Error _error;

        public Error Error
        {
            get => _error;
            set
            {
                _error = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}