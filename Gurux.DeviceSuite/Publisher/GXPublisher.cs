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

using System.ComponentModel;
using System.Runtime.Serialization;
//using Gurux.Device.PresetDevices;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.ServiceHost;
using System;
using ServiceStack.ServiceInterface;
using Gurux.Device.PresetDevices;

namespace Gurux.Device.Publisher
{
    public class GXDownloadResponse
    {
        /// <summary>
        /// Template data.
        /// </summary>
        [DataMember(IsRequired = true)]
        public byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        /// Downloaded template.
        /// </summary>
        public GXTemplateVersion Template
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Download selected template from Gurux server.
    /// </summary>
    [Route("/download")]
    public class GXDownloadRequest : IReturn<GXDownloadResponse>
    {
        /// <summary>
        /// Template to download.
        /// </summary>
        public GXTemplateVersion Template
        {
            get;
            set;
        }
    }

    public class GXGXUpdaterResponse
    {
        /// <summary>
        /// Collection of manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        public GXDeviceManufacturerCollection Manufacturers
        {
            get;
            set;
        }

        public DateTime LastUpdated
        {
            get;
            set;
        }
    }

    [Route("/updater")]
    public class GXUpdater : IReturn<GXGXUpdaterResponse>
    {
        public DateTime LastUpdated
        {
            get;
            set;
        }
    }

    public class GXPublisherResponse
    {
        /// <summary>
        /// Collection of manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        public GXDeviceManufacturerCollection Manufacturers
        {
            get;
            set;
        }
    }

    [Route("/publisher")]
    public class GXPublisher : IReturn<GXPublisherResponse>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GXPublisher()
        {
            Manufacturers = new GXDeviceManufacturerCollection();
        }

        /// <summary>
        /// Is device publised as anynomous.
        /// </summary>
        [DefaultValue(false)]
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public bool Anynomous
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of manufacturers.
        /// </summary>
        [DataMember(IsRequired = true)]
        public GXDeviceManufacturerCollection Manufacturers
        {
            get;
            set;
        }

        /// <summary>
        /// Template data.
        /// </summary>
        [DataMember(IsRequired = true)]
        public byte[] Data
        {
            get;
            set;
        }
    }

    [DataContract()]
    public class GXAdminResponse
    {
        /// <summary>
        /// Action.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of published manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public GXDeviceManufacturerCollection Published
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of unpublished manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public GXDeviceManufacturerCollection Unpublished
        {
            get;
            set;
        }

        /// <summary>
        /// When items are last updated.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime LastUpdated
        {
            get;
            set;
        }
    }

    public enum Action
    {
        Get,
        Add,
        Modify,
        Delete
    }

    [DataContract()]
    [Route("/admin")]
    public class GXAdminRequest : IReturn<GXAdminResponse>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GXAdminRequest()
        {
            Published = new GXDeviceManufacturerCollection();
            Unpublished = new GXDeviceManufacturerCollection();
        }

        /// <summary>
        /// Action.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of published manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public GXDeviceManufacturerCollection Published
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of unpublished manufacturers.
        /// </summary>
        /// <remarks>
        /// On Add collection is updated with new version numbers.
        /// On Delete collection is empty.
        /// </remarks>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public GXDeviceManufacturerCollection Unpublished
        {
            get;
            set;
        }

        /// <summary>
        /// Wait time in seconds.
        /// </summary>
        [DefaultValue(0)]
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int WaitTime
        {
            get;
            set;
        }

        /// <summary>
        /// When items are last updated.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime LastUpdated
        {
            get;
            set;
        }
    }
}
