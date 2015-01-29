using NTH.Framework.NativeTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NTH.Framework.Windows.Mail
{
    public class RecipientList
    {
        private readonly List<Tuple<string, string>> _list;
        private readonly RecipientKind _kind;

        public void Add(string emailAddress)
        {
            Add(emailAddress, null);
        }
        public void Add(string emailAddress, string name)
        {
            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");
            if (_list.All(i => i.Item1 != emailAddress))
            {
                _list.Add(Tuple.Create(emailAddress, name));
            }
        }

        public void Remove(string emailAddress)
        {
            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");
            if (_list.All(i => i.Item1 != emailAddress))
                throw new ArgumentException("Invalid emailAddress (not in collection?)");
            _list.Remove(_list.Single(i => i.Item1 == emailAddress));
        }

        internal RecipientList(RecipientKind kind)
        {
            _list = new List<Tuple<string, string>>();
            _kind = kind;
        }

        internal IEnumerable<MapiRecipientDescription> GetRecipientObjects()
        {
            foreach (var r in _list)
            {
                var s = new MapiRecipientDescription();
                s.recipClass = _kind;
                if (r.Item2 == null)
                {
                    s.name = r.Item1;
                }
                else
                {
                    s.address = r.Item1;
                    s.name = r.Item2;
                }
                yield return s;
            }
        }
    }
}
