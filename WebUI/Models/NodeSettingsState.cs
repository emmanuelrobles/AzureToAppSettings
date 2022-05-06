using System;

namespace WebUI.Models
{
    public class NodeSettingsState
    {
        private string _separatorFlag = "__";
        private string _keyWrapperStart;
        private string _keyWrapperEnd;
        private string _valueWrapperStart;
        private string _valueWrapperEnd;
        private string _nodeWrapperStart;
        private string _nodeWrapperEnd;
        private string _concatString;

        public string SeparatorFlag
        {
            get => _separatorFlag;
            set
            {
                _separatorFlag = value;
                NotifyStateChanged();
            }
        }

        public string KeyWrapperStart
        {
            get => _keyWrapperStart;
            set
            {
                _keyWrapperStart = value;
                NotifyStateChanged();
            }
        }
        
        public string KeyWrapperEnd
        {
            get => _keyWrapperEnd;
            set
            {
                _keyWrapperEnd = value;
                NotifyStateChanged();
            }
        }
        
        public string ValueWrapperStart
        {
            get => _valueWrapperStart;
            set
            {
                _valueWrapperStart = value;
                NotifyStateChanged();
            }
        }
        
        public string ValueWrapperEnd
        {
            get => _valueWrapperEnd;
            set
            {
                _valueWrapperEnd = value;
                NotifyStateChanged();
            }
        }
        
        public string NodeWrapperStart
        {
            get => _nodeWrapperStart;
            set
            {
                _nodeWrapperStart = value;
                NotifyStateChanged();
            }
        }
        
        public string NodeWrapperEnd
        {
            get => _nodeWrapperEnd;
            set
            {
                _nodeWrapperEnd = value;
                NotifyStateChanged();
            }
        }
        
        public string ConcatString
        {
            get => _concatString;
            set
            {
                _concatString = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
