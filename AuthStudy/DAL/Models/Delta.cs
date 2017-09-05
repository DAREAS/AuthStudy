using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FastMember;
using ServiceStack;

namespace AuthStudy.DAL.Models
{
    public class Delta<T> : DynamicObject where T : class
    {
        private HashSet<string> _changedMembers;
        private T _entity;
        private Type _entityType;
        private TypeAccessor _accessor;

        public Delta() : this(typeof(T))
        {

        }

        public Delta(Type entityType)
        {
            Initialize(entityType);
        }

        public Type EntityType
        {
            get
            {
                return _entityType;
            }
        }

        public void Clear()
        {
            Initialize(_entityType);
        }

        public bool TrySetPropertyValue(string name, object value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name = name.ToTitleCase(); // This could be better...
            _accessor[_entity, name] = value;
            _changedMembers.Add(name);
            return true;
        }

        public bool TryGetPropertyValue(string name, out object value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name = name.ToTitleCase(); // This could be better...
            try
            {
                value = _accessor[_entity, name];
                return true;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }
            return TrySetPropertyValue(binder.Name, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }
            return TryGetPropertyValue(binder.Name, out result);
        }

        public IEnumerable<string> GetChangedMemberNames()
        {
            return _changedMembers;
        }

        public IEnumerable<string> GetUnchangedMemberNames()
        {
            return _accessor.GetMembers().Select(p => p.Name).Except(GetChangedMemberNames());
        }

        public void CopyChangedValues(T original)
        {
            var from = _entity;
            var to = original;
            foreach (var property in GetChangedMemberNames())
            {
                _accessor[to, property] = _accessor[from, property];
            }
        }

        public void CopyUnchangedValues(T original)
        {
            var from = _entity;
            var to = original;
            foreach (var property in GetUnchangedMemberNames())
            {
                _accessor[to, property] = _accessor[from, property];
            }
        }

        public void Patch(T original)
        {
            CopyChangedValues(original);
        }

        public void Put(T original)
        {
            CopyChangedValues(original);
            CopyUnchangedValues(original);
        }

        private void Initialize(Type entityType)
        {
            _accessor = TypeAccessor.Create(entityType);
            _entity = Activator.CreateInstance(entityType) as T;
            _changedMembers = new HashSet<string>();
            _entityType = entityType;
        }

        public T GetEntity()
        {
            return _entity;
        }

        public IDictionary<string, object> ToHash()
        {
            return GetChangedMemberNames().ToDictionary(member => member, member => _accessor[_entity, member]);
        }
    }
}
