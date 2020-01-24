﻿using System;
using System.Collections.Generic;
using Jurassic.Library;

namespace Jurassic.Extensions.Fetch
{
    /// <summary>
    /// </summary>
    public partial class HeadersConstructor : ClrStubFunction
    {

        //     INITIALIZATION
        //_________________________________________________________________________________________

        /// <summary>
        /// Creates a new map constructor.
        /// </summary>
        /// <param name="prototype"> The next object in the prototype chain. </param>
        internal HeadersConstructor(ObjectInstance prototype)
            : base(prototype, __STUB__Construct, __STUB__Call)
        {
            // Initialize the constructor properties.
            var properties = GetDeclarativeProperties(Engine);
            InitializeConstructorProperties(properties, "Headers", 0, HeadersInstance.CreatePrototype(Engine, this));
            FastSetProperties(properties);
        }



        //     JAVASCRIPT PROPERTIES
        //_________________________________________________________________________________________

        /// <summary>
        /// A reference to the constructor function that is used to create derived objects.
        /// </summary>
        [JSProperty(Name = "@@species")]
        public FunctionInstance Species
        {
            get { return this; }
        }



        //     JAVASCRIPT INTERNAL FUNCTIONS
        //_________________________________________________________________________________________

        /// <summary>
        /// Called when the Map object is invoked like a function, e.g. var x = Map().
        /// Throws an error.
        /// </summary>
        [JSCallFunction]
        public object Call()
        {
            throw new JavaScriptException(Engine, ErrorType.TypeError, "Constructor Headers requires 'new'");
        }

        /// <summary>
        /// Creates a new Map.
        /// </summary>
        /// <param name="iterable"> iterable is an Array or other iterable object whose elements
        /// are key-value pairs (2-element Arrays). Each key-value pair is added to the new Map.
        /// <c>null</c> is treated as undefined. </param>
        /// <returns> A new Map object, either empty or initialised with the given values. </returns>
        [JSConstructorFunction]
        public HeadersInstance Construct(object iterable)
        {
            // Create a new set.
            var result = new HeadersInstance(this.InstancePrototype, null);

            // If iterable is not null or undefined, then iterate through the values and add them to the set.
            if (iterable != Undefined.Value && iterable != Null.Value)
            {
                var iterator = TypeUtilities.GetIterator(Engine, TypeConverter.ToObject(Engine, iterable));
                if (iterator != null)
                {
                    // Get a reference to the set function.
                    var setFunc = result["set"] as FunctionInstance;
                    if (setFunc == null)
                        throw new JavaScriptException(Engine, ErrorType.TypeError, "Missing 'set' function");

                    // Call the set function for each value.
                    foreach (var value in TypeUtilities.Iterate(Engine, iterator))
                    {
                        var obj = value as ObjectInstance;
                        if (obj == null)
                            throw new JavaScriptException(Engine, ErrorType.TypeError, $"Expected iterator return value to be an object, but was {value}");
                        setFunc.Call(result, obj[0], obj[1]);
                    }
                }
            }

            return result;
        }

    }
}
