﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace YeTi
{
    public class YeTiContainer
    {
        readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        public void Register<TRegistration, TImplementation>()
        {
            _registrations.Add(typeof(TRegistration), typeof(TImplementation));
        }

        object Resolve(Type type)
        {
            var requested_type = type;

            Type actual_type = _registrations[requested_type];

            var ctors = actual_type.GetConstructors();

            if (ctors.Length > 1)
            {
                throw new ComponentHasMultipleConstructorsException(actual_type);
            }

            var ctor = ctors.First();

            IEnumerable<Type> dependency_types = ctor.GetParameters()
                .Select(x => x.ParameterType);

            var dependencies = dependency_types
                .Select(x => this.Resolve(x))
                .ToArray();

            var instance = Activator.CreateInstance(actual_type, dependencies);

            return instance;
        }

        public T Resolve<T>()
        {
            return (T)this.Resolve(typeof(T));
        }
    }

    public abstract class CompositionException : Exception
    {
        public readonly Type Type;

        public CompositionException(Type type)
        {
            Type = type;
        }
    }

    public class ComponentHasMultipleConstructorsException : CompositionException
    {
        public ComponentHasMultipleConstructorsException(Type type) : base(type)
        {
        }
    }
}