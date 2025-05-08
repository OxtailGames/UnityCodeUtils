using System;

namespace Oxtail.Utils
{
    public class ReactiveProperty<T>
    {
        public Action<T> OnPropertyChanged;

        private T m_Value;

        public T Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                OnPropertyChanged?.Invoke(m_Value);
            }
        }
    }
}
