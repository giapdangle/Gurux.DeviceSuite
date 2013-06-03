//
// --------------------------------------------------------------------------
//  Gurux Ltd
// 
//
//
// Filename:        $HeadURL$
//
// Version:         $Revision$,
//                  $Date$
//                  $Author$
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
//  DESCRIPTION
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation; version 2 of the License.
// Gurux Device Framework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
//
// This code is licensed under the GNU General Public License v2. 
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GuruxAMI.Common;

namespace Gurux.DeviceSuite.Ami
{
    /// <summary>
    /// A helper class for showing a media type in the property grid.
    /// </summary>
    /// <remarks>
    /// The MediaTypeCollectionPropertyDescriptor is used to set default settings for the media type.
    /// <br />
    /// Example: The port number in TCP/IP protocol.
    /// </remarks>
    internal class GXAmiParameterDescriptor : Gurux.Device.Editor.GXCollectionPropertyDescriptor
    {
        public GXAmiParameterDescriptor(GXParameters coll, int idx)
            : base(idx, coll)
        {
        }

        GXParameters List
        {
            get
            {
                return (GXParameters)m_List;
            }
        }

        /// <summary>
        /// Returns value of property of the component in at position Index.
        /// </summary>
        public override object GetValue(object component)
        {
            if (m_List.Count <= Index)
            {
                return string.Empty;
            }
            return List[Index].Value;
        }

        /// <summary>
        /// Set new value.
        /// </summary>
        public override void SetValue(object component, object value)
        {
            List[Index].Value = value;
        }

        /// <summary>
        /// Type of property of the component at position Index.
        /// </summary>
        public override Type PropertyType
        {
            get
            {
                Type type = List[Index].Type;
                if (type == null)
                {
                    type = Type.GetType(List[Index].TypeAsString);
                }
                if (type == null)
                {
                    return typeof(string);
                }
                List[Index].Type = type;
                return type;
            }
        }

        public override string DisplayName
        {
            get
            {
                if (Index >= List.Count)
                {
                    return string.Empty;
                }
                return List[Index].Name;
            }
        }

        public override string Description
        {
            get
            {
                if (Index >= List.Count)
                {
                    return string.Empty;
                }
                return List[Index].Name + "Media properties";
            }
        }

        public override string Name
        {
            get
            {
                if (Index >= List.Count)
                {
                    return string.Empty;
                }
                return List[Index].Name.ToString();
            }
        }

        public override void ResetValue(object component)
        {
            //List[Index].DefaultMediaSettings = string.Empty;
        }
    }

    /// <summary>
    /// This is a special type converter which will be associated with the MediaTypeCollection class.
    /// It converts an MediaType object to a string representation for use in a property grid.
    /// </summary>    
    internal class GXAmiParameterConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Allows displaying the + symbol in the property grid.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <returns>True to find properties of this object.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Loops through all device types and adds them to the property list.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="value">An Object that specifies the type of array for which to get properties.</param>
        /// <param name="attributes">An array of type Attribute that is used as a filter.</param>
        /// <returns>Collection of properties exposed to this data type.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            GXParameters list = (GXParameters)value;
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);
            for (int pos = 0; pos < list.Count; ++pos)
            {
                PropertyDescriptor pd = new GXAmiParameterDescriptor(list, pos);
                pds.Add(pd);
            }
            return pds;
        }
    }

    [TypeConverter(typeof(GXAmiParameterConverter))]
    class GXParameters : List<GXAmiParameter>
    {

    }
}
