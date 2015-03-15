using System;

namespace NTH.Framework.Collections.Generic
{
    public struct LinkedStack<T>
    {
        private readonly LinkedStack<T>? _previous;
        private readonly T _value;
        private readonly int _itemCount;

        internal LinkedStack(int itemCount, T firstValue, LinkedStack<T>? previous)
        {
            _itemCount = itemCount;
            _value = firstValue;
            _previous = previous;
        }

        public T Pop()
        {
            if (_itemCount == 0)
                throw new InvalidOperationException("Stack is empty.");

            var res = _value;
            if (_previous.HasValue)
                this = _previous.Value;
            else if (_itemCount == 1 || _itemCount == 0)
                this = new LinkedStack<T>();
            return res;
        }

        public void Push(T value)
        {
            var newTop = new LinkedStack<T>(_itemCount + 1, value, this);
            this = newTop;
        }
    }
}
