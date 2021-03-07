/*
Copyright (c) 2018, 2019 Jan Tietze

Permission is hereby granted, free of charge, 
to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*/
using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshCommence.Base
{
    /// <summary>
    /// Creates a Powershell dynamic parameter (a parameter with a ValidateSet)
    /// </summary>
    public class PowershellDynamicParameter
    {
        private string[] ValidateSetValues;
        private string Name { get; set; }
        public bool AllowMultiple { get; set; }
        public string HelpMessage { get; set; }
        public string ParameterSetName { get; set; }
        public bool Mandatory { get; set; }
        public int? Position { get; set; }

        // constructor
        public PowershellDynamicParameter(PSCmdlet parent, string name, params string[] validateSetValues)
        {
            ValidateSetValues = validateSetValues;
            AllowMultiple = false;
            Name = name;
            Position = null;
            Mandatory = false;
            Cmdlet = parent;
        }
        public string Value
        {
            get
            {
                if (!Cmdlet.MyInvocation.BoundParameters.ContainsKey(Name))
                {
                    return null;
                }
                return Cmdlet.MyInvocation.BoundParameters[Name] as string;
            }
        }
        public string[] Values
        {
            get
            {
                if (!Cmdlet.MyInvocation.BoundParameters.ContainsKey(Name))
                {
                    return null;
                }
                return Cmdlet.MyInvocation.BoundParameters[Name] as string[];
            }
        }

        public PSCmdlet Cmdlet { get; private set; }



// if we delete (or just not call) this method,
// we can create our own implementation that allows
// for adding multiple parameters to a RuntimeDefinedParameterDictionary
        public RuntimeDefinedParameterDictionary GetParamDictionary()
        {
            var paramDictionary = new RuntimeDefinedParameterDictionary();
            AddToParamDictionary(paramDictionary);
            return paramDictionary;
        }

        public void AddToParamDictionary(RuntimeDefinedParameterDictionary paramDictionary)
        {
            var attributes = new Collection<Attribute>();

            var parameterAttribute = new ParameterAttribute();
            if (null != Position) parameterAttribute.Position = 1;
            if (null != HelpMessage) parameterAttribute.HelpMessage = HelpMessage;
            if (null != ParameterSetName) parameterAttribute.ParameterSetName = ParameterSetName;
            parameterAttribute.Mandatory = Mandatory;
            attributes.Add(parameterAttribute);

            if (null != ValidateSetValues)
            {
                var validateSetAttribute = new ValidateSetAttribute(ValidateSetValues);
                attributes.Add(validateSetAttribute);
            }

            RuntimeDefinedParameter OptionsRuntimeDefinedParam;
            if (AllowMultiple)
                OptionsRuntimeDefinedParam = new RuntimeDefinedParameter(Name, typeof(string[]), attributes);
            else
                OptionsRuntimeDefinedParam = new RuntimeDefinedParameter(Name, typeof(string), attributes);

            paramDictionary.Add(Name, OptionsRuntimeDefinedParam);
        }
    }
}