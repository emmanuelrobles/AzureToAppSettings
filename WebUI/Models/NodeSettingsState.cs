using System;

namespace WebUI.Models
{
    public class NodeSettingsState
    {
        private string _separatorFlag = "__";

        public string SeparatorFlag
        {
            get => _separatorFlag;
            set
            {
                _separatorFlag = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
